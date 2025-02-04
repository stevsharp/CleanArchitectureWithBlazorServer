
namespace CleanArchitecture.Blazor.Application.Features.Steps.Commands.AddEdit;

public class AddEditStepCommandValidator : AbstractValidator<AddEditStepCommand>
{
    public AddEditStepCommandValidator()
    {
                RuleFor(v => v.Name).MaximumLength(50).NotEmpty(); 
    RuleFor(v => v.StepOrder).NotNull(); 

     }

}

