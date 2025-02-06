
namespace CleanArchitecture.Blazor.Application.Features.Steps.Commands.AddEdit;

public class AddEditCommentCommand : ICacheInvalidatorRequest<Result<int>>
{
    [Description("Id")]
    public int Id { get; set; }
    [Description("Step id")]
    public int StepId { get; set; }
    [Description("Content")]
    public string? Content { get; set; }
    public IEnumerable<string>? Tags { get; }
}

