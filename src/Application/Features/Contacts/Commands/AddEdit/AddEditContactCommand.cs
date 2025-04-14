

using CleanArchitecture.Blazor.Application.Features.Contacts.Caching;
using CleanArchitecture.Blazor.Application.Features.Contacts.Mappers;

namespace CleanArchitecture.Blazor.Application.Features.Contacts.Commands.AddEdit;

public class AddEditContactCommand: ICacheInvalidatorRequest<Result<int>>
{
      [Description("Id")]
      public int Id { get; set; }
          [Description("Name")]
    public string? Name {get;set;} 
    [Description("Description")]
    public string? Description {get;set;} 
    [Description("Email")]
    public string? Email {get;set;} 
    [Description("Phone number")]
    public string? PhoneNumber {get;set;} 
    [Description("Country")]
    public string? Country {get;set;}

    [Description("Tax Identification Number")]
    public string? TaxIdentificationNumber { get; set; }

    [Description("Public Financial Service")]
    public string? PublicFinancialService { get; set; }

    [Description("Customer Type")]
    public int CustomerType { get; set; } = 0;
    public string CacheKey => ContactCacheKey.GetAllCacheKey;
      public IEnumerable<string>? Tags => ContactCacheKey.Tags;
}

public class AddEditContactCommandHandler : IRequestHandler<AddEditContactCommand, Result<int>>
{
    private readonly IApplicationDbContext _context;
    public AddEditContactCommandHandler(
        IApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<Result<int>> Handle(AddEditContactCommand request, CancellationToken cancellationToken)
    {
        if (request.Id > 0)
        {
            var item = await _context.Contacts.FindAsync(request.Id, cancellationToken);
            if (item == null)
            {
                return await Result<int>.FailureAsync($"Contact with id: [{request.Id}] not found.");
            }
            ContactMapper.ApplyChangesFrom(request,item);
			// raise a update domain event
			item.AddDomainEvent(new ContactUpdatedEvent(item));
            await _context.SaveChangesAsync(cancellationToken);
            return await Result<int>.SuccessAsync(item.Id);
        }
        else
        {
            var item = ContactMapper.FromEditCommand(request);
            // raise a create domain event
			item.AddDomainEvent(new ContactCreatedEvent(item));
            _context.Contacts.Add(item);
            await _context.SaveChangesAsync(cancellationToken);
            return await Result<int>.SuccessAsync(item.Id);
        }
       
    }
}

