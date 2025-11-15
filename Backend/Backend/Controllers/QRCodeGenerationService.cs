using Backend.Additional;
using Backend.interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers;

[ApiController]
[Route("api/[Controller]")]
public class QRCodeGenerationService : ControllerBase
{
    private readonly IQRCodeGenerationService _iqrCodeGenerationService;

    public QRCodeGenerationService(IQRCodeGenerationService iqrCodeGenerationService)
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

    
    
}