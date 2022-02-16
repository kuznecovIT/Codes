using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.ApiEndpoints;
using Codes.Api.WithApiEndpoints.Services.Contracts;
using Codes.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Codes.Api.WithApiEndpoints.Endpoints.Codes
{
    public class CreateCodesEndpoint : EndpointBaseAsync
        .WithRequest<List<Code>>
        .WithActionResult<string>
    {
        private readonly ICodeService _codeService;

        public CreateCodesEndpoint(ICodeService codeService)
        {
            _codeService = codeService;
        }

        [HttpPost("codes")]
        [SwaggerOperation(Summary = "Create codes", 
            Description = "Create codes from input list",
            OperationId = "CreateCodes", 
            Tags = new []{"CodesEndpoint"})]
        public override async Task<ActionResult<string>> HandleAsync(List<Code> codes, CancellationToken cancellationToken = default)
        {
            var createdCount = await _codeService.CreateCodes(codes);
            return Ok($"{createdCount} codes created.");
        }
    }
}