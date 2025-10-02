
#nullable enable
#nullable disable warnings

using CleanArchitecture.Blazor.Application.Features.ServiceVariants.Caching;

namespace CleanArchitecture.Blazor.Application.Features.ServiceVariants.Commands.Create;

public class CreateServiceVariantCommand : ICacheInvalidatorRequest<Result<int>>
{
    [Description("Id")]
    public int Id { get; set; }
    [Description("Service id")]
    public int ServiceId { get; set; }
    [Description("Service")]
    public ServiceDto? Service { get; set; }
    [Description("Name")]
    public string Name { get; set; }
    [Description("Unit")]
    public string? Unit { get; set; }
    [Description("Complexity level")]
    public int? ComplexityLevel { get; set; }
    [Description("Base price")]
    public decimal? BasePrice { get; set; }

    public string CacheKey => ServiceVariantCacheKey.GetAllCacheKey;
    public IEnumerable<string>? Tags => ServiceVariantCacheKey.Tags;
    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<CreateServiceVariantCommand, ServiceVariant>(MemberList.None);
        }
    }
}

public class CreateServiceVariantCommandHandler : IRequestHandler<CreateServiceVariantCommand, Result<int>>
{
    private readonly IMapper _mapper;
    private readonly IApplicationDbContextFactory _dbContextFactory;
    public CreateServiceVariantCommandHandler(
            IMapper mapper,
            IApplicationDbContextFactory dbContextFactory)
    {
        _mapper = mapper;
        _dbContextFactory = dbContextFactory;
    }
    public async Task<Result<int>> Handle(CreateServiceVariantCommand request, CancellationToken cancellationToken)
    {
        await using var _context = await _dbContextFactory.CreateAsync(cancellationToken);
        var item = _mapper.Map<ServiceVariant>(request);
        // raise a create domain event
        item.AddDomainEvent(new ServiceVariantCreatedEvent(item));
        _context.ServiceVariants.Add(item);
        await _context.SaveChangesAsync(cancellationToken);
        return await Result<int>.SuccessAsync(item.Id);
    }
}

