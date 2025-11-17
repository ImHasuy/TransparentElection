using Backend.Additional;
using Backend.DTOs;
using Backend.interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers;

[ApiController]
[Route("api/[Controller]")]
public class QRCodeGenerationController : ControllerBase
{
    private readonly IQRCodeGenerationService _iqrCodeGenerationService;

    public QRCodeGenerationController(IQRCodeGenerationService iqrCodeGenerationService)
    {
        _iqrCodeGenerationService = iqrCodeGenerationService;
    }
    
    [HttpPost]
    [Route("LoadDistrictsFromFile")]
    public async Task<IActionResult> GenerateQrCodesForDistrict(string DistrictId)
    {
        ApiResponse apiResponse = new ApiResponse();
        try
        {
            apiResponse.Message = await _iqrCodeGenerationService.GenerateQRCodesForDistrict(DistrictId);
            return Ok(apiResponse);
        }
        catch (Exception e)
        {
            apiResponse.StatusCode = 400;
            apiResponse.Message = e.Message;
        }

        return BadRequest(apiResponse);
    }
    
    
    [HttpPost]
    [Route("QRCodeScan")]
    public async Task<IActionResult> QRCodeScan(QRCodeDecodeDto qrCodeDecodeDto)
    {
        ApiResponse apiResponse = new ApiResponse();
        try
        {
            apiResponse.Message = await _iqrCodeGenerationService.QRCodeScan(qrCodeDecodeDto);
            return Ok(apiResponse);
        }
        catch (Exception e)
        {
            apiResponse.StatusCode = 400;
            apiResponse.Message = e.Message;
        }

        return BadRequest(apiResponse);
    }
    
    
}