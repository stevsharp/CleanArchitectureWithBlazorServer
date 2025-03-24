
using CleanArchitecture.Blazor.Application.Features.Suppliers.Caching;
using CleanArchitecture.Blazor.Application.Features.Suppliers.Mappers;

namespace CleanArchitecture.Blazor.Application.Features.Suppliers.Commands.Update;

public class UpdateSupplierCommand: ICacheInvalidatorRequest<Result<int>>
{
      [Description("Id")]
      public int Id { get; set; }
            [Description("Name")]
    public string Name {get;set;} 
    [Description("Address")]
    public string? Address {get;set;} 
    [Description("Phone")]
    public string? Phone {get;set;} 
    [Description("Email")]
    public string? Email {get;set;} 
    [Description("Vat")]
    public string? VAT {get;set;} 

    [Description("CompanyType")]
    public string? CompanyType { get; set; } // New property for CompanyType

    [Description("IBAN")]
    public string? IBAN { get; set; }

    [Description("SWIFT")]
    public string? SWIFT { get; set; }

    [Description("TaxIdentificationNumber")]
    public string? TaxIdentificationNumber { get; set; }

    [Description("PublicFinancialService")]
    public string? PublicFinancialService { get; set; }

    [Description("ContactPerson")]
    public string? ContactPerson { get; set; }

    [Description("Notes")]
    public string? Notes { get; set; }

    [Description("IsActive")]
    public bool IsActive { get; set; }

    public string? Country {get;set;} 
    [Description("Created at")]
    public DateTime CreatedAt {get;set;} 

      public string CacheKey => SupplierCacheKey.GetAllCacheKey;
      public IEnumerable<string>? Tags => SupplierCacheKey.Tags;

}

public class UpdateSupplierCommandHandler : IRequestHandler<UpdateSupplierCommand, Result<int>>
{
    private readonly IApplicationDbContext _context;
    public UpdateSupplierCommandHandler(
        IApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<Result<int>> Handle(UpdateSupplierCommand request, CancellationToken cancellationToken)
    {

       var item = await _context.Suppliers.FindAsync(request.Id, cancellationToken);
       if (item == null)
       {
           return await Result<int>.FailureAsync($"Supplier with id: [{request.Id}] not found.");
       }
       SupplierMapper.ApplyChangesFrom(request, item);
	    // raise a update domain event
	   item.AddDomainEvent(new SupplierUpdatedEvent(item));
       await _context.SaveChangesAsync(cancellationToken);
       return await Result<int>.SuccessAsync(item.Id);
    }
}

