using Backend.Additional;
using Backend.DTOs;
using Backend.Entities;
using Backend.interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers;

[ApiController]
[Route("api/[Controller]")]
public class BlockchainController : ControllerBase
{
    private readonly IBlockchainService _blockchainService;

    public BlockchainController(IBlockchainService blockchainService)
    {
        _blockchainService = blockchainService;
    }


    [HttpPost]
    [Route("CommitVoteToBlockchain")]
    public async Task<IActionResult> CommitVoteToBlockchain(BlockchainVoteInputDto blockchainVoteInputDto)
    {
        ApiResponse apiResponse = new ApiResponse();
        try
        {
            apiResponse.Data = await _blockchainService.CommitVoteToBlockchain(blockchainVoteInputDto);
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
    [Route("GetVotesForParty")]
    public async Task<IActionResult> GetVotesForParty(string input)
    {
        ApiResponse apiResponse = new ApiResponse();
        try
        {
            apiResponse.Message = await _blockchainService.GetVotesForParty(input);
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


