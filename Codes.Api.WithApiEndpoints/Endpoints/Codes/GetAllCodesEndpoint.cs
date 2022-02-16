using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.ApiEndpoints;
using Codes.Api.WithApiEndpoints.Services.Contracts;
using Codes.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Codes.Api.WithApiEndpoints.Endpoints.Codes
{
    public class GetAllCodesRequest
    {
        // Some pagination and filtering there
    }
    
    public class GetAllCodesEndpoint : EndpointBaseAsync.
        WithoutRequest.
        WithActionResult<List<Code>>
    {
        private readonly ICodeService _codeService;

        public GetAllCodesEndpoint(ICodeService codeService)
        {
            _codeService = codeService;
        }

        [HttpGet("codes")]
        [SwaggerOperation(Summary = "Get all codes", 
            Description = "Get all codes",
            OperationId = "GetAllCodes", 
            Tags = new []{"CodesEndpoint"})]
        public override async Task<ActionResult<List<Code>>> HandleAsync(CancellationToken cancellationToken = default)
        {
            var codes = await _codeService.GetAllCodes();
            
            if (!codes.Any())
                return NotFound("Codes not found.");

            return Ok(codes);
        }
    }
}