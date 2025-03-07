﻿
using CleanArchitecture.Blazor.Domain.Common.Entities;
namespace CleanArchitecture.Blazor.Domain.Entities;

public class OfferLine : BaseAuditableEntity
{
    public int OfferId { get; set; }
    public int ItemId { get; set; }
    public int Quantity { get; set; }
    public decimal Discount { get; set; }
    public decimal LineTotal { get; set; }
    public decimal? LinePrice { get; set; } = 0m;
    public int? SubItemId { get;set; }
    public string? Unit { get; set; }
    public string? Color { get; set; }
    public Product? Product { get; set; }
}