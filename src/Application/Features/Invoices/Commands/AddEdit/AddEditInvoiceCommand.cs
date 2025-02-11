using CleanArchitecture.Blazor.Application.Features.InvoiceLines.DTOs;
using CleanArchitecture.Blazor.Application.Features.Invoices.Caching;
using CleanArchitecture.Blazor.Application.Invoices.Mappers;

namespace CleanArchitecture.Blazor.Application.Features.Invoices.Commands.AddEdit;

public class AddEditInvoiceCommand : ICacheInvalidatorRequest<Result<int>>
{
    [Description("Id")]
    public int Id { get; set; }
    [Description("Offer id")]
    public int? OfferId { get; set; }
    [Description("Invoice date")]
    public DateTime? InvoiceDate { get; set; }
    [Description("Total amount")]
    public decimal TotalAmount { get; set; }

    [Description("ShippingCosts")]
    public decimal ShippingCosts { get; set; }

    [Description("Status")]
    public string? Status { get; set; }

    [Description("PaymentType")]
    public string? PaymentType { get; set; } = "Cash";

    [Description("Draft")]
    public int? Draft { get; set; }

    [Description("StatDesignus")]
    public string? Design { get; set; }

    [Description("ShippingMethod")]
    public string? ShippingMethod { get; set; }


    [Description("Packaging")]
    public int? Packaging { get; set; } = 1;

    [Description("Invoice lines")]
    public List<InvoiceLineDto>? InvoiceLines { get; set; } = [];

    [Description("Customer Id")]
    public int CustomerId { get; set; }

    public string CacheKey => InvoiceCacheKey.GetAllCacheKey;
    public IEnumerable<string>? Tags => InvoiceCacheKey.Tags;
}

public class AddEditInvoiceCommandHandler(
    IApplicationDbContext context) : IRequestHandler<AddEditInvoiceCommand, Result<int>>
{
    private readonly IApplicationDbContext _context = context;

    public async Task<Result<int>> Handle(AddEditInvoiceCommand request, CancellationToken cancellationToken)
    {
        try
        {
            if (request.Id > 0)
            {
                var item = await _context.Invoices.FindAsync(request.Id, cancellationToken);
                if (item == null)
                {
                    return await Result<int>.FailureAsync($"Invoice with id: [{request.Id}] not found.");
                }
                InvoiceMapper.ApplyChangesFrom(request, item);

                // raise a update domain event
                //item.AddDomainEvent(new InvoiceUpdatedEvent(item));
                await _context.SaveChangesAsync(cancellationToken);
                return await Result<int>.SuccessAsync(item.Id);
            }
            else
            {
                var item = InvoiceMapper.FromEditCommand(request);
                // raise a create domain event
                //item.AddDomainEvent(new InvoiceCreatedEvent(item));

                item.InvoiceLines.ForEach(x => x.Product = null);

                _context.Invoices.Add(item);

                var steps = new List<Step>
                {
                    new() {
                        Name = "Make Deposit",
                        StepOrder = 1,
                        Invoice = item,
                        IsCompleted = false,
                        Comments =
                        [
                            new Comment { Content = "Deposit required to start the process." }
                        ]
                    },
                    new() {
                        Name = "Create Product",
                        StepOrder = 2,
                        Invoice = item,
                        IsCompleted = false,
                        Comments =
                        [
                            new Comment { Content = "Deposit required to start the process." }
                        ]
                    },
                    new() {
                        Name = "Print",
                        StepOrder = 3, // Changed to avoid duplicate StepOrder
                        Invoice = item,
                        IsCompleted = false,
                        Comments =
                        [
                            new Comment { Content = "Deposit required to start the process." }
                        ]
                    },
                    new() {
                        Name = "Approve Order",
                        StepOrder = 4, // Incremented to maintain sequence
                        Invoice = item,
                        IsCompleted = false,
                        Comments =
                        [
                            new Comment { Content = "Final approval is required from the manager." }
                        ]
                    }
                };

                // Add steps in a single operation
                _context.Steps.AddRange(steps);

                await _context.SaveChangesAsync(cancellationToken);

                return await Result<int>.SuccessAsync(item.Id);
            }
        }
        catch (Exception e)
        {
            var ex = e.Message;
            throw;
        }


    }
}

