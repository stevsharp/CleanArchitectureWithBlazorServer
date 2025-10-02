
#nullable enable
#nullable disable warnings

using CleanArchitecture.Blazor.Application.Features.ServiceVariants.Caching;

namespace CleanArchitecture.Blazor.Application.Features.ServiceVariants.Commands.Delete;

public class DeleteServiceVariantCommand:  ICacheInvalidatorRequest<Result>
{
  public int[] Id {  get; }
  public string CacheKey => ServiceVariantCacheKey.GetAllCacheKey;
  public IEnumerable<string>? Tags => ServiceVariantCacheKey.Tags;
  public DeleteServiceVariantCommand(int[] id)
  {
    Id = id;
  }
}

public class DeleteServiceVariantCommandHandler : 
             IRequestHandler<DeleteServiceVariantCommand, Result>

{
    private readonly IApplicationDbContextFactory _dbContextFactory;
    public DeleteServiceVariantCommandHandler(IApplicationDbContextFactory dbContextFactory)
    {
        _dbContextFactory = dbContextFactory;
    }
    public async Task<Result> Handle(DeleteServiceVariantCommand request, CancellationToken cancellationToken)
    {
        await using var _context = await _dbContextFactory.CreateAsync(cancellationToken);
        var items = await _context.ServiceVariants.Where(x=>request.Id.Contains(x.Id)).ToListAsync(cancellationToken);
        foreach (var item in items)
        {
		    // raise a delete domain event
			item.AddDomainEvent(new ServiceVariantDeletedEvent(item));
            _context.ServiceVariants.Remove(item);
        }
        await _context.SaveChangesAsync(cancellationToken);
        return await Result.SuccessAsync();
    }

}

