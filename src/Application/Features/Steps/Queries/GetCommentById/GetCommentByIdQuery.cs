
using CleanArchitecture.Blazor.Application.Features.Steps.DTOs;
using CleanArchitecture.Blazor.Application.Features.Steps.Caching;
using CleanArchitecture.Blazor.Application.Features.Steps.Specifications;

namespace CleanArchitecture.Blazor.Application.Features.Steps.Queries.GetById;

public class GetCommentByIdQuery : ICacheableRequest<Result<PaginatedData< CommentDto>>>
{
    public  int StepId { get; set; }
    public string CacheKey => StepCacheKey.GetCommentByStepIdCacheKey($"{StepId}");
    public IEnumerable<string>? Tags => StepCacheKey.Tags;

}

public class GetCommentByIdQueryHandler(IApplicationDbContext context) : IRequestHandler<GetCommentByIdQuery, Result<PaginatedData<CommentDto>>>
{
    private readonly IApplicationDbContext _context = context;

    public async Task<Result<PaginatedData<CommentDto>>> Handle(GetCommentByIdQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var data = await _context.Steps
               .Include(x => x.Comments)
               .ApplySpecification(new StepByIdSpecification(request.StepId))
               .SelectMany(x => x.Comments)
               .AsNoTracking()
               .ToListAsync(cancellationToken);

            var commentDtos = data.Select(comment => new CommentDto
            {
                Id = comment.Id,
                StepId = comment.StepId,
                Content = comment.Content
            }).AsEnumerable();

            var paginatedData = new PaginatedData<CommentDto>(commentDtos,
                commentDtos.Count(), 0, 100);

            return await Result<PaginatedData<CommentDto>>.SuccessAsync(paginatedData);
        }
        catch (Exception ex)
        {
            var m = ex.Message;
            throw;
        }

       
    }
}