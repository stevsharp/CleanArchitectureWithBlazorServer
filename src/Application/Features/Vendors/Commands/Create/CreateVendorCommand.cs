#nullable enable
#nullable disable warnings

namespace CleanArchitecture.Blazor.Application.Features.Vendors.Commands.Create;

public class CreateVendorCommand : ICacheInvalidatorRequest<Result<int>>
{
    [Description("Id")]
    public int Id { get; set; }
    [Description("Company id")]
    public int CompanyId { get; set; }
    [Description("Company")]
    public CompanyDto? Company { get; set; }
    [Description("Service category id")]
    public int? ServiceCategoryId { get; set; }
    [Description("Service category")]
    public ServiceCategoryDto? ServiceCategory { get; set; }
    [Description("Terms")]
    public string? Terms { get; set; }

    public string CacheKey => VendorCacheKey.GetAllCacheKey;
    public IEnumerable<string>? Tags => VendorCacheKey.Tags;
    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<CreateVendorCommand, Vendor>(MemberList.None);
        }
    }
}

public class CreateVendorCommandHandler : IRequestHandler<CreateVendorCommand, Result<int>>
{
    private readonly IMapper _mapper;
    private readonly IApplicationDbContextFactory _dbContextFactory;
    public CreateVendorCommandHandler(
            IMapper mapper,
            IApplicationDbContextFactory dbContextFactory)
    {
        _mapper = mapper;
        _dbContextFactory = dbContextFactory;
    }
    public async Task<Result<int>> Handle(CreateVendorCommand request, CancellationToken cancellationToken)
    {
        await using var _context = await _dbContextFactory.CreateAsync(cancellationToken);
        var item = _mapper.Map<Vendor>(request);
        // raise a create domain event
        item.AddDomainEvent(new VendorCreatedEvent(item));
        _context.Vendors.Add(item);
        await _context.SaveChangesAsync(cancellationToken);
        return await Result<int>.SuccessAsync(item.Id);
    }
}

