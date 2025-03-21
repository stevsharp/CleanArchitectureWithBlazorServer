


using CleanArchitecture.Blazor.Application.Features.Categories.DTOs;
using CleanArchitecture.Blazor.Application.Features.SubCategories.Caching;
using CleanArchitecture.Blazor.Application.Features.SubCategories.Mappers;

namespace CleanArchitecture.Blazor.Application.Features.SubCategories.Commands.AddEdit;

public class AddEditSubCategoryCommand: ICacheInvalidatorRequest<Result<int>>
{
      [Description("Id")]
      public int Id { get; set; }
          [Description("Name")]
    public string Name {get;set;} 
    [Description("Categories")]
    public List<CategoryDto>? Categories {get;set;} 


      public string CacheKey => SubCategoryCacheKey.GetAllCacheKey;
      public IEnumerable<string>? Tags => SubCategoryCacheKey.Tags;
}

public class AddEditSubCategoryCommandHandler : IRequestHandler<AddEditSubCategoryCommand, Result<int>>
{
    private readonly IApplicationDbContext _context;
    public AddEditSubCategoryCommandHandler(
        IApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<Result<int>> Handle(AddEditSubCategoryCommand request, CancellationToken cancellationToken)
    {
        if (request.Id > 0)
        {
            var item = await _context.SubCategories.FindAsync(request.Id, cancellationToken);
            if (item == null)
            {
                return await Result<int>.FailureAsync($"SubCategory with id: [{request.Id}] not found.");
            }
            SubCategoryMapper.ApplyChangesFrom(request,item);

            await _context.SaveChangesAsync(cancellationToken);
            return await Result<int>.SuccessAsync(item.Id);
        }
        else
        {
            var item = SubCategoryMapper.FromEditCommand(request);

            _context.SubCategories.Add(item);

            await _context.SaveChangesAsync(cancellationToken);
            return await Result<int>.SuccessAsync(item.Id);
        }
       
    }
}

