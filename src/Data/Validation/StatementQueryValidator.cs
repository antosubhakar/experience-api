using Doctrina.ExperienceApi.Data;
using Doctrina.ExperienceApi.Data.Validation;
using FluentValidation;

namespace Doctrina.Application.Statements.Queries
{
    public class StatementsQueryValidator : AbstractValidator<StatementsQuery>
    {
        public StatementsQueryValidator()
        {
            RuleFor(x => x)
                .Must(g => !(g.StatementId.HasValue && g.VoidedStatementId.HasValue))
                .WithMessage("VoidedStatementId and StatementId parameters cannot be used together.");

            RuleFor(x => x)
                .Must(ValidateParameters)
                .When(x => x.StatementId.HasValue)
                .WithMessage("Only attachments and format parameters are allowed with using statementId");

            RuleFor(x => x)
                .Must(ValidateParameters)
                .When(x => x.VoidedStatementId.HasValue)
                .WithMessage("Only attachments and format parameters are allowed with using voidedStatementId");

            RuleFor(x => x.Agent).SetValidator(new AgentValidator())
                .When(x => x.Agent != null);
        }

        private static bool ValidateParameters(StatementsQuery parameters)
        {
            var otherParameters = parameters.ToParameterMap(ApiVersion.GetLatest());
            otherParameters.Remove("attachments");
            otherParameters.Remove("format");
            return otherParameters.Count == 0;
        }
    }
}
