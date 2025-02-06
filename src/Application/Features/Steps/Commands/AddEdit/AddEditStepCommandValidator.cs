
using CleanArchitecture.Blazor.Application.Features.Steps.DTOs;

namespace CleanArchitecture.Blazor.Application.Features.Steps.Commands.AddEdit;

public class AddEditStepCommandValidator : AbstractValidator<AddEditStepCommand>
{
    public AddEditStepCommandValidator()
    {
            RuleFor(v => v.Name).MaximumLength(50).NotEmpty(); 
            RuleFor(v => v.StepOrder).NotNull(); 
     }
}

public class CommentDtoValidator : AbstractValidator<CommentDto>
{
    public CommentDtoValidator()
    {
        RuleFor(v => v.Content).MaximumLength(50).NotEmpty();
    }

}

