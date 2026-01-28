using Backend.Additional;
using Backend.DTOs;
using Backend.interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers;

    [ApiController]
    [Route("api/[Controller]")]
    public class ImportAndGenerationController : ControllerBase
    {
        private readonly IImportAndGenerationService _importAndGenerationService;

        public ImportAndGenerationController(IImportAndGenerationService importAndGenerationService)
        {
            _importAndGenerationService = importAndGenerationService;
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
                apiResponse.Message = await _importAndGenerationService.LoadDistinctsFromFile(input);
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
                apiResponse.Message = await _importAndGenerationService.WhereAmI(address);
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
        [Route("LoadVoterCount")]
        public async Task<IActionResult> LoadVoterCount(string path)
        {
            ApiResponse apiResponse = new ApiResponse();
            try
            {
                apiResponse.Message = await _importAndGenerationService.LoadVoterCount(path);
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
        [Route("GenerateTokensForDistricts")]
        public async Task<IActionResult> GenerateTokensForDistricts()
        {
            ApiResponse apiResponse = new ApiResponse();
            try
            {
                apiResponse.Message = await _importAndGenerationService.GenerateTokensForDistricts();
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