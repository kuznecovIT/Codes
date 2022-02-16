using System.Linq;
using Codes.Domain.Entities;
using Codes.Domain.Enumerations;
using EnumsNET;
using FluentValidation;

namespace Codes.Api.Validators
{
    public class CodeValidator : AbstractValidator<Code>
    {
        public CodeValidator()
        {
            RuleFor(x => x.Value).NotNull();

            RuleFor(x => x.CodeType)
                .Must((_, codeType, context) => 
                {
                var validCodes = Enums.GetMembers<CodeType>()
                    .Select(x => $"Id: {(int)x.Value} - {x.Name}");
                context.MessageFormatter.AppendArgument("CodeId", codeType);
                context.MessageFormatter.AppendArgument("ValidCodeIds", string.Join(" , ", validCodes));
                return codeType.IsDefined(); 
                })
                .WithMessage("CodeType with ID: {CodeId} Not defined. Valid codeIds: {ValidCodeIds}");
        }
    }
}