using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Codes.Domain.Entities;
using CodesApi.Services.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CodesApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CodeController : ControllerBase
    {
        private readonly ICodeService _codeService;

        public CodeController(ICodeService codeService)
        {
            _codeService = codeService;
        }
        
        
        
        /// <summary>
        /// Get all codes from database
        /// </summary>
        /// <response code="200">Codes received</response>
        /// <response code="404">Codes not found</response>
        [ProducesResponseType(typeof(IEnumerable<Code>), 200)]
        [ProducesResponseType(typeof(NotFoundResult), 404)]
        [HttpGet]
        public async Task<IActionResult> GetCodes()
        {
            var codes = await _codeService.GetAllCodes();
            
            if (!codes.Any())
                return NotFound("Codes not found.");

            return Ok(codes);
        }
        
        /// <summary>
        /// Get code database by ID
        /// </summary>
        /// <response code="200">Code received</response>
        /// <response code="404">Code not found</response>
        [ProducesResponseType(typeof(Code), 200)]
        [ProducesResponseType(typeof(NotFoundResult), 404)]
        [HttpGet("{codeId:guid}")]
        public async Task<IActionResult> GetCodeById(Guid codeId)
        {
            var code = await _codeService.GetCodeById(codeId);
            
            if (code == null)
                return NotFound($"Code with id: {codeId} not found.");

            return Ok(code);
        }
        
        /// <summary>
        /// Create list of codes from json body
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     POST api/Code
        ///     {        
        ///       "codeType": 1,
        ///       "value": "first code value"
        ///     },
        ///     {        
        ///       "codeType": 2,
        ///       "value": "second code value"
        ///     }
        /// </remarks>
        /// <response code="200">Codes created</response>
        /// <response code="400">Codes validation not succesed</response>
        [ProducesResponseType(typeof(string), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 400)]
        [HttpPost]
        public async Task<IActionResult> CreateCodes([FromBody] List<Code> codes)
        {
            var createdCount = await _codeService.CreateCodes(codes);
            return Ok($"{createdCount} codes created.");
        }
    }
}