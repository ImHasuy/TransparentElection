using Backend.Additional;
using Backend.DTOs;
using Backend.interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers;

    [ApiController]
    [Route("api/[Controller]")]
    public class VotingDistrictController : ControllerBase
    {
        private readonly IVotingDistrictService _votingDistrictService;

        public VotingDistrictController(IVotingDistrictService votingDistrictService)
        {
            _votingDistrictService = votingDistrictService;
        }


        /// <summary>
        /// Loads and processes distinct voting districts from a specified file.
        /// </summary>
        /// <param name="input">The file path or identifier containing the voting districts to be loaded.</param>
        /// <returns>An IActionResult indicating the status of the operation, including a message if successful or an error if it fails.</returns>
        [HttpPost]
        [Route("LoadDistinctsFromFile")]
        public async Task<IActionResult> LoadDistinctsFromFile(string input)
        {
            ApiResponse apiResponse = new ApiResponse();
            try
            {
                apiResponse.Message = await _votingDistrictService.LoadDistinctsFromFile(input);
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
        [Route("WhereAmI")]
        public async Task<IActionResult> WhereAmI(VoterInputDto address)
        {
            ApiResponse apiResponse = new ApiResponse();
            try
            {
                apiResponse.Message = await _votingDistrictService.WhereAmI(address);
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