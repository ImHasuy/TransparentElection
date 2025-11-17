using Backend.DTOs;

namespace Backend.interfaces;

public interface IQRCodeGenerationService
{
    Task<string> GenerateQRCodesForDistrict(string DistrictId);
    Task<string> QRCodeScan(QRCodeDecodeDto qrCodeDecodeDto);
}