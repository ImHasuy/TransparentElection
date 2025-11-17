using Backend.Additional;
using Backend.DTOs;
using Backend.interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers;

[ApiController]

[Route("api/[Controller]")]
public class SingleMemberCandidateController : ControllerBase
{
    private readonly ISingleMemberCandidateService _partyListService;

    public SingleMemberCandidateController(ISingleMemberCandidateService partyListService)
    {
        _partyListService = partyListService;
    }

    
    
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
    
    [HttpPost]
    [Route("GetCandidatesForVotingDistrict")]
    public async Task<IActionResult>  GetCandidatesForVotingDistrict(SingleMemberCandidatesInputGetDto singleMemberCandidatesInputGetDto)
    {
        ApiResponse apiResponse = new ApiResponse();
        try
        {
            apiResponse.Data = await _partyListService.GetCandidatesForVotingDistrict(singleMemberCandidatesInputGetDto);
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