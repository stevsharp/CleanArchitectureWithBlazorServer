
using CleanArchitecture.Blazor.Application.Features.Invoices.DTOs;

namespace CleanArchitecture.Blazor.Application.Features.Steps.DTOs;

[Description("Steps")]
public class StepDto
{
    [Description("Id")]
    public int Id { get; set; }
     [Description("Name")]
    public string Name {get;set;} 
    [Description("Invoice id")]
    public int? InvoiceId {get;set;} 
    [Description("Is completed")]
    public bool IsCompleted {get;set;} 
    [Description("Step order")]
    public int StepOrder {get;set;}

    [Description("Comments")]
    public List<CommentDto>? Comments { get; set; }

    //[Description("Invoice")]
    //public InvoiceDto Invoice {get;set;} 

}

public record CommentDto
{
    [Description("Id")]
    public int Id { get; set; }
    [Description("Step id")]
    public int StepId { get; set; }
    public string? Content { get; set; }
}

