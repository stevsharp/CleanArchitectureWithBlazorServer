// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.


using CleanArchitecture.Blazor.Application.Features.PicklistSets.DTOs;
using CleanArchitecture.Blazor.Application.Features.Products.Caching;
using CleanArchitecture.Blazor.Application.Features.Products.Mappers;
using Microsoft.AspNetCore.Components.Forms;

namespace CleanArchitecture.Blazor.Application.Features.Products.Commands.AddEdit;

public class AddEditProductCommand : ICacheInvalidatorRequest<Result<int>>
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public string? Unit { get; set; } = "default";
    public decimal RetailPrice { get; set; } = 0;
    public string? Brand { get; set; } = "default";
    public decimal Price { get; set; } = 0;
    public int Stock { get; set; } = 0;
    public string? Code { get; set; } = string.Empty;
    public string? AdditionalInfo { get; set; }
    public string? ProductUrl { get; set; }
    public string? ImageUrl { get; set; }
    public string? Color { get; set; } = "default";
    public int CategoryId { get; set; }

    public void SetDefaultValues()
    {
        Unit = "NO";
        Brand  = "default";
        Color = "default";  
    }

    public List<ProductImage>? Pictures { get; set; }

    public List<PicklistSetDto> ColorData = [];  

    public List<PicklistSetDto> UnitData = [];

    public IReadOnlyList<IBrowserFile>? UploadPictures { get; set; }
    public string CacheKey => ProductCacheKey.GetAllCacheKey;
    public IEnumerable<string>? Tags => ProductCacheKey.Tags;
}

public class AddEditProductCommandHandler : IRequestHandler<AddEditProductCommand, Result<int>>
{
    private readonly IApplicationDbContext _context;

    public AddEditProductCommandHandler(
        IApplicationDbContext context
    )
    {
        _context = context;
    }

    public async Task<Result<int>> Handle(AddEditProductCommand request, CancellationToken cancellationToken)
    {
        try
        {
            if (request.Id > 0)
            {
                var item = await _context.Products
                    .Include(x => x.ColorOptions)
                    .Include(x => x.UnitOptions)
                    .Include(x => x.SubProducts)
                    .SingleOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

                if (item == null)
                {
                    return await Result<int>.FailureAsync($"Prduct with id: [{request.Id}] not found.");
                }
                ProductMapper.ApplyChangesFrom(request, item);

                item.ColorOptions.Clear();
                item.UnitOptions.Clear();
                item.SubProducts?.Clear();

                if (request?.ColorData?.Any() == true)
                {
                    foreach (var color in request.ColorData.DistinctBy(x=>x.Value).ToArray())
                    {
                        item.ColorOptions.Add(new ProductColorOption
                        {
                            ProductId = item.Id,
                            Name = color.Name.ToString(),
                            Description = color.Description,
                            UnitId = color.Id,
                            Value = color?.Value?.ToString() ?? string.Empty,
                            Text = color?.Text?.ToString() ?? string.Empty
                        });
                    }
                }

                if (request?.UnitData?.Any() == true)
                {
                    foreach (var unit in request.UnitData.DistinctBy(x => x.Value).ToArray())
                    {
                        item.UnitOptions.Add(new ProductUnitOption
                        {
                            ProductId = item.Id,
                            Name = unit.Name.ToString(),
                            Description = unit.Description,
                            UnitId = unit.Id,
                            Value = unit?.Value?.ToString() ?? string.Empty,
                            Text = unit?.Text?.ToString() ?? string.Empty
                        });
                    }
                }

                item.AddDomainEvent(new UpdatedEvent<Product>(item));
                await _context.SaveChangesAsync(cancellationToken);
                return await Result<int>.SuccessAsync(item.Id);
            }
            else
            {
                var item = ProductMapper.FromEditCommand(request);

                item.AddDomainEvent(new CreatedEvent<Product>(item));

                _context.Products.Add(item);

                if (request?.ColorData?.Any() == true)
                {
                    foreach (var color in request.ColorData)
                    {
                        item.ColorOptions.Add(new ProductColorOption
                        {
                            ProductId = item.Id,
                            Name = color.Name.ToString(),
                            Description = color.Description,
                            UnitId = color.Id,
                            Value = color?.Value?.ToString() ?? string.Empty,
                            Text = color?.Text?.ToString() ?? string.Empty
                        });
                    }
                }

                if (request?.UnitData?.Any() == true)
                {
                    foreach (var unit in request.UnitData)
                    {
                        item.UnitOptions.Add(new ProductUnitOption
                        {
                            ProductId = item.Id,
                            Name = unit.Name.ToString(),
                            Description = unit.Description,
                            UnitId = unit.Id,
                            Value = unit?.Value?.ToString() ?? string.Empty,
                            Text = unit?.Text?.ToString() ?? string.Empty
                        });
                    }
                }

                await _context.SaveChangesAsync(cancellationToken);
                return await Result<int>.SuccessAsync(item.Id);
            }
        }
        catch (Exception ex)
        {
            var message = ex.ToString();
            throw;
        }


    }
}