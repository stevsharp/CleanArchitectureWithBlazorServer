// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CleanArchitecture.Blazor.Domain.Common.Entities;
using System;

namespace CleanArchitecture.Blazor.Domain.Entities;

public class Product : BaseAuditableEntity
{
    public string? Name { get; set; }
    public string? Code { get; set; }
    public string? Description { get; set; }
    public string? Brand { get; set; }
    public string? Unit { get; set; }
    public decimal Price { get; set; }
    public decimal? RetailPrice { get; set; }
    public int? Stock { get; set; }
    public string? AdditionalInfo { get; set; }
    public string? ProductUrl { get; set; }
    public string? ImageUrl { get; set; }

    public string? Properties { get; set; }
    public string? Color { get; set; }
    public int CategoryId { get; set; } // Foreign Key  

    public virtual List<ProductImage>? Pictures { get; set; } = [];
    public virtual List<SupplyItem>? SupplyItems { get; set; } = [];
    public ICollection<SubProduct> SubProducts { get; set; } = [];
    public ICollection<ProductColorOption> ColorOptions { get; set; } = [];
    public ICollection<ProductUnitOption> UnitOptions { get; set; } = [];

}

public class ProductUnitOption 
{
    public int Id { get; set; } 
    public int ProductId { get; set; }
    public int UnitId { get; set; } // Foreign Key
    public string Name { get; set; } = null!;
    public string Value { get; set; } = null!;
    public string Text { get; set; } = null!;
    public string? Description { get; set; }

}

public class ProductColorOption 
{
    public int Id { get; set; }
    public int ProductId { get; set; }
    public int UnitId { get; set; } // Foreign Key
    public string Name { get; set; } = null!;
    public string Value { get; set; } = null!;
    public string Text { get; set; } = null!;
    public string? Description { get; set; }

}
