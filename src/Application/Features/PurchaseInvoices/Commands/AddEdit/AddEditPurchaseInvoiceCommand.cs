
using CleanArchitecture.Blazor.Application.Features.PurchaseInvoices.Caching;
using CleanArchitecture.Blazor.Application.Features.PurchaseInvoices.Mappers;
using CleanArchitecture.Blazor.Application.Features.PurchaseItems.DTOs;
using CleanArchitecture.Blazor.Application.Features.Suppliers.DTOs;

namespace CleanArchitecture.Blazor.Application.Features.PurchaseInvoices.Commands.AddEdit;

public class AddEditPurchaseInvoiceCommand : ICacheInvalidatorRequest<Result<int>>
{
    [Description("Id")]
    public int Id { get; set; }
    [Description("Supplier id")]
    public int SupplierId { get; set; }
    [Description("Invoice number")]
    public string? InvoiceNumber { get; set; }
    [Description("Invoice date")]
    public DateTime? InvoiceDate { get; set; } = DateTime.Now;
    [Description("Invoice type")]
    public string? InvoiceType { get; set; } = "Τιμολόγιο – Δελτίο Αποστολής;";

    [Description("Total amount")]
    public decimal TotalAmount { get; set; }
    [Description("Vat amount")]
    public decimal VATAmount { get; set; } = 0m;

    [Description("Payment status")]
    public string? PaymentStatus { get; set; } = "Payed";

    [Description("Payment method")]
    public string? PaymentMethod { get; set; }= "Cash";

    [Description("Iban")]
    public string? IBAN { get; set; } = "GR1234567890123456789012345";
    [Description("Swift")]
    public string? SWIFT { get; set; } = "BIC123456";
    [Description("Notes")]
    public string? Notes { get; set; }

    [Description("Supplier")]
    public SupplierDto Supplier { get; set; } = new SupplierDto();

    [Description("Items")]
    public List<PurchaseItemDto>? Items { get; set; } = [];

    public string CacheKey => PurchaseInvoiceCacheKey.GetAllCacheKey;
    public IEnumerable<string>? Tags => PurchaseInvoiceCacheKey.Tags;

}

public class AddEditPurchaseInvoiceCommandHandler : IRequestHandler<AddEditPurchaseInvoiceCommand, Result<int>>
{
    private readonly IApplicationDbContext _context;
    public AddEditPurchaseInvoiceCommandHandler(
        IApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<Result<int>> Handle(AddEditPurchaseInvoiceCommand request, CancellationToken cancellationToken)
    {
        if (request.Id > 0)
        {
            var item = await _context.PurchaseInvoices.FindAsync(request.Id, cancellationToken);
            if (item == null)
            {
                return await Result<int>.FailureAsync($"PurchaseInvoice with id: [{request.Id}] not found.");
            }
            PurchaseInvoiceMapper.ApplyChangesFrom(request, item);
            //// raise a update domain event
            //item.AddDomainEvent(new PurchaseInvoiceUpdatedEvent(item));
            await _context.SaveChangesAsync(cancellationToken);
            return await Result<int>.SuccessAsync(item.Id);
        }
        else
        {
            var item = PurchaseInvoiceMapper.FromEditCommand(request);
            //         // raise a create domain event
            //item.AddDomainEvent(new PurchaseInvoiceCreatedEvent(item));
            _context.PurchaseInvoices.Add(item);
            await _context.SaveChangesAsync(cancellationToken);
            return await Result<int>.SuccessAsync(item.Id);
        }

    }
}

