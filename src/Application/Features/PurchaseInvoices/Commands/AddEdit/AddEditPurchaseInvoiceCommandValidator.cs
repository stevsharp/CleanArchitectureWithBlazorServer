
namespace CleanArchitecture.Blazor.Application.Features.PurchaseInvoices.Commands.AddEdit;

public class AddEditPurchaseInvoiceCommandValidator : AbstractValidator<AddEditPurchaseInvoiceCommand>
{
    public AddEditPurchaseInvoiceCommandValidator()
    {
        RuleFor(v => v.SupplierId).NotNull();
        RuleFor(v => v.InvoiceNumber).NotNull();
        RuleFor(v => v.InvoiceNumber).MaximumLength(255); 
        RuleFor(v => v.InvoiceDate).NotNull(); 
        RuleFor(v => v.InvoiceType).MaximumLength(255); 
        RuleFor(v => v.TotalAmount).NotNull(); 
        RuleFor(v => v.VATAmount).NotNull(); 
        RuleFor(v => v.PaymentStatus).MaximumLength(255); 
        RuleFor(v => v.PaymentMethod).MaximumLength(255); 
        RuleFor(v => v.IBAN).MaximumLength(255); 
        RuleFor(v => v.SWIFT).MaximumLength(255); 
        RuleFor(v => v.Notes).MaximumLength(255); 
     }

}

