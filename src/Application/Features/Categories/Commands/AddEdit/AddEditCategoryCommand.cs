
using CleanArchitecture.Blazor.Application.Features.Categories.Caching;
using CleanArchitecture.Blazor.Application.Features.Categories.Mappers;
using CleanArchitecture.Blazor.Application.Features.SubCategories.DTOs;

namespace CleanArchitecture.Blazor.Application.Features.Categories.Commands.AddEdit;

public class AddEditCategoryCommand: ICacheInvalidatorRequest<Result<int>>
{
    [Description("Id")]
     public int Id { get; set; }
    [Description("Name")]
     public string? Name {get;set;}
     [Description("Comments")]
     public string? Comments { get;set;}

    [Description("SubCategories")]
    public List<SubCategoryDto> SubCategories { get; set; } = [];
    public string CacheKey => CategoryCacheKey.GetAllCacheKey;
      public IEnumerable<string>? Tags => CategoryCacheKey.Tags;
}

public class AddEditCategoryCommandHandler(
    IApplicationDbContext context) : IRequestHandler<AddEditCategoryCommand, Result<int>>
{
    private readonly IApplicationDbContext _context = context;


    public async Task<Result<int>> Handle(AddEditCategoryCommand request, CancellationToken cancellationToken)
    {
        try
        {
            if (request.Id > 0)
            {
                var item = await _context.Categories
                    .Include(c => c.SubCategories)
                    .FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken);

                if (item is null)
                {
                    return await Result<int>.FailureAsync($"Category with id: [{request.Id}] not found.");
                }

                CategoryMapper.ApplyChangesFrom(request, item);

                item.SubCategories.Clear();

                item.AddDomainEvent(new CategoryUpdatedEvent(item));

                await _context.SaveChangesAsync(cancellationToken);  // Save Category

                var requestedIds = request.SubCategories?.Select(dto => dto.Id).Distinct().ToList() ?? new List<int>();

                await _context.UpdateCategorySubCategoriesAsync(item.Id, requestedIds, cancellationToken);

                return await Result<int>.SuccessAsync(item.Id);
            }
            else
            {
                var item = CategoryMapper.FromEditCommand(request);

                item.SubCategories.Clear();

                item.AddDomainEvent(new CategoryCreatedEvent(item));
                
                _context.Categories.Add(item);

                await _context.SaveChangesAsync(cancellationToken);

                var requestedIds = request.SubCategories?.Select(dto => dto.Id).Distinct().ToList() ?? [];

                await _context.UpdateCategorySubCategoriesAsync(item.Id, requestedIds, cancellationToken);

                return await Result<int>.SuccessAsync(item.Id);
            }

        }
        catch (Exception ex)
        {
            var message = ex.Message;
            throw;
        }

       
    }
}

