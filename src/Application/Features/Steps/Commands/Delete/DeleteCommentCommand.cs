
namespace CleanArchitecture.Blazor.Application.Features.Steps.Commands.Delete;

public class DeleteCommentCommand : ICacheInvalidatorRequest<Result<int>>
{
    public int Id { get; set; }
    public int StepId { get; set; }
    public IEnumerable<string>? Tags { get; }
}


public class DeleteCommentCommandHandler(IApplicationDbContext context) :
             IRequestHandler<DeleteCommentCommand, Result<int>>
{
    private readonly IApplicationDbContext _context = context;
    public async Task<Result<int>> Handle(DeleteCommentCommand request, CancellationToken cancellationToken)
    {
        try
        {
            if (request.StepId > 0)
            {
                var item = await _context.Steps.FindAsync(request.StepId, cancellationToken);

                if (item == null)
                {
                    return await Result<int>.FailureAsync($"Step with id: [{request.Id}] not found.");
                }

                var comment = item.Comments.FirstOrDefault(x => x.Id == request.Id);

                if (comment is not null)
                {
                    item.Comments.Remove(comment);
                }

                await _context.SaveChangesAsync(cancellationToken);

                return await Result<int>.SuccessAsync(item.Id);
            }

            return await Result<int>.FailureAsync($"Step with id: [{request.Id}] not found.");
        }
        catch (Exception ex)
        {
            var errors = new List<string> { "There was an error adding/editing the comment" };
            throw;
        }
    }
}