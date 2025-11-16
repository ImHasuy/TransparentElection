using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using AutoMapper;
using Backend.Context;
using Backend.DTOs;
using Backend.interfaces;
using Microsoft.EntityFrameworkCore;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using ZXing;
using ZXing.QrCode;
using ZXing.Rendering;

namespace Backend.Services;

public class QRCodeGenerationService : IQRCodeGenerationService
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;
    private readonly IConfiguration _config;
    public QRCodeGenerationService(AppDbContext context, IMapper mapper, IConfiguration config)
    {
        _context = context;
        _mapper = mapper;
        _config = config;
    }
    
    public async Task<string> GenerateQRCodesForDistrict(string DistrictId)
    {
        var subectDistrict = await _context.VotingDistricts.FirstOrDefaultAsync(c=>c.Id.ToString() == DistrictId) ?? throw new Exception("District not found");
        var DistrictName =
            $"{subectDistrict.CountyName}_{subectDistrict.OEVK}_{subectDistrict.CityName}_{subectDistrict.PollingStationAddress}_StationNuber_{subectDistrict.PollingStationNumber}";
        
        var votingDistricts = await _context.VotingTokens.Where(c=> c.VotingDistrictId.ToString() == DistrictId).ToListAsync() ?? throw new Exception("No districts found");
        
        var payloadList = votingDistricts.Where(k => k.VotingDistrictId.ToString().Equals(DistrictId)).Select(c=> new DataPayload
        {
            Token = c.VotingToken.ToString(),
            VotingDist = c.VotingDistrictId.ToString()
        }).ToList();

        var encryptedPayloads = payloadList.AsParallel().Select(EncryptPayload).ToList();

        Console.WriteLine($"{encryptedPayloads.Count} payloads generated successfully");
        
        QuestPDF.Settings.License = LicenseType.Community;

        var document = Document.Create(container =>
            container.Page(page =>
            {
                page.Size(PageSizes.A4);
                page.Margin(0);

                page.Content().Table(table =>
                {

                    table.ColumnsDefinition(columns =>
                    {
                        columns.RelativeColumn();
                        columns.RelativeColumn();
                        columns.RelativeColumn();
                    });
                    
                    foreach (var encryptedString in encryptedPayloads)
                    {
                        table.Cell().Padding(3).Element(tablecell =>
                        {
                            tablecell
                                .Border(1)
                                .BorderColor(Colors.Grey.Medium)
                                .Padding(0)
                                .Column(column =>
                                {
                                    column.Item()
                                        .AspectRatio(1)
                                        .AlignCenter()
                                        .AlignMiddle()
                                        .Svg(size =>
                                        {
                                            var writer = new QRCodeWriter();
                                            var qrCode = writer.encode(encryptedString, BarcodeFormat.QR_CODE, (int)size.Width, (int)size.Height);
                                            var renderer = new SvgRenderer { FontName = "Lato" };
                                            return renderer.Render(qrCode, BarcodeFormat.QR_CODE, null).Content;
                                            //A Barcode itt roszsul volt ha hibás lenne
                                            
                                            
                                        });
                                    //Lehet jobb lenne a .QRCode(encryptedString); Ebben az esetben kellene hozzá padding is. De nem tudom
                                });
                        });
                    }
                });
            }));

        document.GeneratePdf($"ExportPdfs/{DistrictName}.pdf");
        return ("Successfully generated QR codes");
    }

    
    
    private string EncryptPayload(DataPayload dataPayload)
    {
        var datapayload = JsonSerializer.Serialize(dataPayload);
        var plaintext = Encoding.UTF8.GetBytes(datapayload);

        const int keySize = 32;
        const int saltSize = 16;
        const int nonceSize = 12;
        const int tagSize = 16;
        const int iterations = 100000;
        
        var salt = RandomNumberGenerator.GetBytes(saltSize);
        var nonce = RandomNumberGenerator.GetBytes(nonceSize);
        
        var password = _config.GetSection("SecretKey").Value!;
        
        var kdf = new Rfc2898DeriveBytes(password, salt, iterations, HashAlgorithmName.SHA256);
        var key = kdf.GetBytes(keySize);

        var ciphertext = new byte[plaintext.Length];
        var tag = new byte[tagSize];

        using (var aesGcm = new AesGcm(key))
        {
            aesGcm.Encrypt(nonce, plaintext, ciphertext, tag);
        }
        var encryptedResoult = new byte[saltSize + nonceSize + tagSize + ciphertext.Length];
        Buffer.BlockCopy(salt,0,encryptedResoult,0,saltSize);
        Buffer.BlockCopy(nonce, 0 , encryptedResoult, saltSize, nonceSize);
        Buffer.BlockCopy(tag,0,encryptedResoult,saltSize+nonceSize,tagSize);
        Buffer.BlockCopy(ciphertext,0,encryptedResoult, saltSize + nonceSize + tagSize, ciphertext.Length);

        return Convert.ToBase64String(encryptedResoult);
    }

    private DataPayload DecryptData(string encryptedData)
    {
        var encryptedString = Convert.FromBase64String(encryptedData);


        var secretKey = _config.GetSection("SecretKey").Value!;
        
        const int keySize = 32;
        const int saltSize = 16;
        const int nonceSize = 12;
        const int tagSize = 16;
        const int iterations = 100000;


        var salt = encryptedString[..saltSize];
        var nonce = encryptedString[saltSize.. (saltSize + nonceSize)];
        var tag = encryptedString[(saltSize + nonceSize).. (saltSize + nonceSize + tagSize)];
        var ciphertext = encryptedString[(saltSize + nonceSize + tagSize)..];
        
        //Key regenerate

        
        var kdf = new Rfc2898DeriveBytes(secretKey, salt, iterations, HashAlgorithmName.SHA256);
        var key = kdf.GetBytes(keySize);
        
        var plaintext = new byte[ciphertext.Length];

        using var aesGcm = new AesGcm(key);
        aesGcm.Decrypt(nonce,ciphertext,tag,plaintext);
        
        var payload = JsonSerializer.Deserialize<DataPayload>(plaintext) ?? throw new Exception("Failed to deserialize payload");

        return payload;
    }
    
}