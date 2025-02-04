
namespace CleanArchitecture.Blazor.Application.Features.Steps.Commands.Delete;

public class DeleteStepCommandValidator : AbstractValidator<DeleteStepCommand>
{
        public DeleteStepCommandValidator()
        {
          
            RuleFor(v => v.Id).NotNull().ForEach(v=>v.GreaterThan(0));
          
        }
}
    

