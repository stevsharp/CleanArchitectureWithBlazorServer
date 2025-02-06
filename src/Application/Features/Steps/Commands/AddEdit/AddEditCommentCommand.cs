
using CleanArchitecture.Blazor.Application.Features.Steps.Mappers;

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

public class AddEditCommentCommandHandler(
    IApplicationDbContext context) : IRequestHandler<AddEditCommentCommand, Result<int>>
{
    private readonly IApplicationDbContext _context = context;

    public async Task<Result<int>> Handle(AddEditCommentCommand request, CancellationToken cancellationToken)
    {
        if (request.StepId > 0)
        {
            var item = await _context.Steps.FindAsync(request.StepId, cancellationToken);

            if (item == null)
            {
                return await Result<int>.FailureAsync($"Step with id: [{request.Id}] not found.");
            }

            var comment = item.Comments.FirstOrDefault(x => x.Id == request.Id);

            if (comment is null)
                item.Comments.Add(new Comment { Content = request.Content });
            else
                comment.Content = request.Content;
            
            await _context.SaveChangesAsync(cancellationToken);
            return await Result<int>.SuccessAsync(item.Id);
        }

        return await Result<int>.FailureAsync($"Step with id: [{request.Id}] not found.");
    }
}