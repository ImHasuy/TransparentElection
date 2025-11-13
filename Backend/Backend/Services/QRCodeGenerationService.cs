using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using AutoMapper;
using Backend.Context;
using Backend.DTOs;
using Microsoft.EntityFrameworkCore;

namespace Backend.Services;

public class QRCodeGenerationService
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
    
    public async Task<string> GenerateQRCodesForDistrinct(string DistrictId)
    {
        var votingDistricts = await _context.VotingTokens.Where(c=> c.VotingDistrictId.ToString() == DistrictId).ToListAsync() ?? throw new Exception("No districts found");
        foreach (var district in votingDistricts)
        {
            
        }

        return ("fsd");
    }

    public string EncryptPayload(DataPayload dataPayload)
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
    
    
    
}