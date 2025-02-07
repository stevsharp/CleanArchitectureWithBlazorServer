
using CleanArchitecture.Blazor.Application.Features.Steps.Caching;
using DocumentFormat.OpenXml.InkML;

namespace CleanArchitecture.Blazor.Application.Features.Steps.Commands.Delete;


public class DeleteStepCommand:  ICacheInvalidatorRequest<Result<int>>
{
  public int[] Id {  get; }
  public string CacheKey => StepCacheKey.GetAllCacheKey;
  public IEnumerable<string>? Tags => StepCacheKey.Tags;
  public DeleteStepCommand(int[] id)
  {
    Id = id;
  }
}

public class DeleteStepCommandHandler(
    IApplicationDbContext context) : 
             IRequestHandler<DeleteStepCommand, Result<int>>

{
    private readonly IApplicationDbContext _context = context;

    public async Task<Result<int>> Handle(DeleteStepCommand request, CancellationToken cancellationToken)
    {
        var items = await _context.Steps.Where(x=>request.Id.Contains(x.Id)).ToListAsync(cancellationToken);
        foreach (var item in items)
        {
		    // raise a delete domain event
			//item.AddDomainEvent(new StepDeletedEvent(item));
            _context.Steps.Remove(item);
        }
        var result = await _context.SaveChangesAsync(cancellationToken);
        return await Result<int>.SuccessAsync(result);
    }

}

