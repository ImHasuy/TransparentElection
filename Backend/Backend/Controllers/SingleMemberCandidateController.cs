using Backend.Additional;
using Backend.DTOs;
using Backend.interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers;

[ApiController]
[Authorize]
[Route("api/[Controller]")]
public class SingleMemberCandidateController : ControllerBase
{
    private readonly ISingleMemberCandidateService _partyListService;

    public SingleMemberCandidateController(ISingleMemberCandidateService partyListService)
    {
        _partyListService = partyListService;
    }

    
    [AllowAnonymous]
    [HttpPost]
    [Route("AddSingleMemberCandidate")]
    public async Task<IActionResult> AddSingleMemberCandidate(SingleMemberCandidateAddDto singleMemberCandidateAddDto)
    {
        ApiResponse apiResponse = new ApiResponse();
        try
        {
            apiResponse.Message = await _partyListService.AddSingleMemberCandidate(singleMemberCandidateAddDto);
            return Ok(apiResponse);
        }
        catch (Exception e)
        {
            apiResponse.StatusCode = 400;
            apiResponse.Message = e.Message;
        }

        return BadRequest(apiResponse);
    }
    
    [HttpGet]
    [Route("GetCandidatesForVotingDistrict")]
    public async Task<IActionResult>  GetCandidatesForVotingDistrict()
    {
        ApiResponse apiResponse = new ApiResponse();
        try
        {
            apiResponse.Data = await _partyListService.GetCandidatesForVotingDistrict();
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