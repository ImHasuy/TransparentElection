using Backend.Additional;
using Backend.DTOs;
using Backend.interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers;

[ApiController]
[Route("api/[Controller]")]
public class EligibleVoterController : ControllerBase
{
    private readonly IEligibleVoterService _eligibleVoterService;

    public EligibleVoterController(IEligibleVoterService eligibleVoterService)
    {
        _eligibleVoterService = eligibleVoterService;
    }
   
    
    [HttpPost]
    [Route("GetAddressOptions")]
    public async Task<IActionResult> GetAddressOptions(VoterAddressesPostInputDto voterAddressesPostInputDto)
    {
        ApiResponse apiResponse = new ApiResponse();
        try
        {
            apiResponse.Data = await _eligibleVoterService.GetAddressOptions(voterAddressesPostInputDto);
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
    [Route("AddEligibleVoter")]
    public async Task<IActionResult> AddEligibleVoter(EligibleVoterAddDto eligibleVoterAddDto)
    {
        ApiResponse apiResponse = new ApiResponse();
        try
        {
            apiResponse.Message = await _eligibleVoterService.AddEligibleVoter(eligibleVoterAddDto);
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