namespace Backend.interfaces;

public interface IQRCodeGenerationService
{
    Task<string> GenerateQRCodesForDistrict(string DistrictId);
}