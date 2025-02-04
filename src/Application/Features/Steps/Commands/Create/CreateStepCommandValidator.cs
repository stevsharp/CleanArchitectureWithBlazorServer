
namespace CleanArchitecture.Blazor.Application.Features.Steps.Commands.Create;

public class CreateStepCommandValidator : AbstractValidator<CreateStepCommand>
{
        public CreateStepCommandValidator()
        {
                RuleFor(v => v.Name).MaximumLength(50).NotEmpty(); 
    RuleFor(v => v.StepOrder).NotNull(); 

        }
       
}

