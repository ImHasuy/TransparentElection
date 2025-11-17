using Backend.Additional;
using Backend.DTOs;
using Backend.interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers;


[ApiController]
[Route("api/[Controller]")]
public class PartyListController : ControllerBase
{
    private readonly IPartyListService _partyListService;

    public PartyListController(IPartyListService partyListService)
    {
        _partyListService = partyListService;
    }



    [HttpPost]
    [Route("AddPartyToPartyList")]
    public async Task<IActionResult> AddPartyToPartyList(PartyListAddDto partyListAddDto)
    {
        ApiResponse apiResponse = new ApiResponse();
        try
        {
            apiResponse.Message = await _partyListService.AddPartyToPartyList(partyListAddDto);
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
    [Route("AddMemberToPartyList")]
    public async Task<IActionResult> AddMemberToPartyList(PartyListCandidateAddDto partyListCandidateAddDto)
    {
        ApiResponse apiResponse = new ApiResponse();
        try
        {
            apiResponse.Message = await _partyListService.AddMemberToPartyList(partyListCandidateAddDto);
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
    [Route("GetPartyList")]
    public async Task<IActionResult> GetPartyList()
    {
        ApiResponse apiResponse = new ApiResponse();
        try
        {
            apiResponse.Data = await _partyListService.GetPartyList();
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
    
