

using CleanArchitecture.Blazor.Application.Features.Categories.DTOs;
using CleanArchitecture.Blazor.Application.Features.SubCategories.Caching;
using CleanArchitecture.Blazor.Application.Features.SubCategories.Mappers;

namespace CleanArchitecture.Blazor.Application.Features.SubCategories.Commands.Create;

public class CreateSubCategoryCommand: ICacheInvalidatorRequest<Result<int>>
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
    
    public class CreateSubCategoryCommandHandler : IRequestHandler<CreateSubCategoryCommand, Result<int>>
    {
        private readonly IApplicationDbContext _context;
        public CreateSubCategoryCommandHandler(
            IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Result<int>> Handle(CreateSubCategoryCommand request, CancellationToken cancellationToken)
        {
           var item = SubCategoryMapper.FromCreateCommand(request);

           _context.SubCategories.Add(item);

           await _context.SaveChangesAsync(cancellationToken);
           return  await Result<int>.SuccessAsync(item.Id);
        }
    }

