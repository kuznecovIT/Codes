using System;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.ApiEndpoints;
using Codes.Api.WithApiEndpoints.Services.Contracts;
using Codes.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Codes.Api.WithApiEndpoints.Endpoints.Codes
{
    public class GetCodeByIdRequest
    {
        public const string Route = "codes/{CodeId:guid}";
        public Guid CodeId { get; set; }
    }
    
    public class GetCodeByIdEndpoint : EndpointBaseAsync.WithRequest<GetCodeByIdRequest>.WithActionResult<Code>
    {
        private readonly ICodeService _codeService;

        public GetCodeByIdEndpoint(ICodeService codeService)
        {
            _codeService = codeService;
        }

        [HttpGet(GetCodeByIdRequest.Route)]
        [SwaggerOperation(Summary = "Get code by ID",
            Description = "Get code by ID",
            OperationId = "GetCodeById",
            Tags = new[] {"CodesEndpoint"})]
        public override async Task<ActionResult<Code>> HandleAsync([FromRoute] GetCodeByIdRequest request,
            CancellationToken cancellationToken = new CancellationToken())
        {
            var code = await _codeService.GetCodeByIdAsync(request.CodeId);
            
            if (code == null)
                return NotFound($"Code with id: {request.CodeId} not found.");

            return Ok(code);
        }
    }
}