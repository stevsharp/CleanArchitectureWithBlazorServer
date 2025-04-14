using CleanArchitecture.Blazor.Application.Features.PicklistSets.DTOs;
using CleanArchitecture.Blazor.Application.Features.Products.Caching;
using CleanArchitecture.Blazor.Application.Features.Products.Mappers;
using CleanArchitecture.Blazor.Application.Features.SubProducts.DTOs;
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

    [Description("SubProducts")]
    public IEnumerable<SubProductDto>? SubProducts { get; set; } = [];
    public IReadOnlyList<IBrowserFile>? UploadPictures { get; set; }
    public string CacheKey => ProductCacheKey.GetAllCacheKey;
    public IEnumerable<string>? Tags => ProductCacheKey.Tags;
}

public class AddEditProductCommandHandler(IApplicationDbContext context ) : IRequestHandler<AddEditProductCommand, Result<int>>
{
    private readonly IApplicationDbContext _context = context;

    public async Task<Result<int>> Handle(AddEditProductCommand request, CancellationToken cancellationToken)
    {
        try
        {
           

            if (request is null)
            {
                return await Result<int>.FailureAsync("Product is null.");
            }

            if (request.Id > 0)
            {
                var item = await _context.Products
                    .Include(x => x.ColorOptions)
                    .Include(x => x.UnitOptions)
                    .Include(x => x.SubProducts)
                    .SingleOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

                if (item is null)
                {
                    return await Result<int>.FailureAsync($"Product with id: [{request.Id}] not found.");
                }

                ProductMapper.ApplyChangesFrom(request, item);

                // Clear existing color and unit options
                item.ColorOptions.Clear();
                item.UnitOptions.Clear();
                item.SubProducts.Clear();

                var incoming = request.SubProducts ?? [];
                //var existingIds = item.SubProducts.Select(x => x.Id).ToHashSet();
                var incomingIds = incoming.Where(x => x.Id > 0).Select(x => x.Id).ToHashSet();

                var json = JsonSerializer.Serialize(incoming);

                var priceByColor = incoming
                    .GroupBy(x => x.Color)
                    .ToDictionary(g => g.Key, g => g.First().RetailPrice); // or use your own price selection logic

                foreach (var dto in incoming)
                {
                    var color = dto.Color;
                    var price = priceByColor.TryGetValue(color, out var p) ? p : 0m;

                    item.SubProducts.Add(new SubProduct
                    {
                        ProductId = item.Id,
                        Color = dto.Color,
                        Unit = dto.Unit,
                        Stock = dto.Stock,
                        Price = dto.Stock,
                        RetailPrice = price
                    });
                }


                // Rebuild color options
                if (request.ColorData?.Any() == true)
                {
                    foreach (var color in request.ColorData.DistinctBy(x => x.Value))
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

                // Rebuild unit options
                if (request.UnitData?.Any() == true)
                {
                    foreach (var unit in request.UnitData.DistinctBy(x => x.Value))
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

                // Trigger domain event & save
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

                item.ColorOptions.Clear();
                item.UnitOptions.Clear();
                //item.SubProducts?.Clear();

                foreach (var req in request.SubProducts ?? [])
                {
                    item.SubProducts ??= [];

                    var match = item?.SubProducts?.SingleOrDefault(x => x.Id == req.Id && req.Id > 0);

                    if (match != null)
                    {
                        match.Color = req.Color;
                        match.Unit = req.Unit;
                        match.Stock = req.Stock;
                        match.Price = req.Price;
                        match.RetailPrice = req.RetailPrice;
                    }
                    else
                    {
                        item.SubProducts.Add(new SubProduct
                        {
                            ProductId = item.Id,
                            Color = req.Color,
                            Unit = req.Unit,
                            Stock = req.Stock,
                            Price = req.Price,
                            RetailPrice = req.RetailPrice
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
            //throw;
            return await Result<int>.SuccessAsync(request.Id);
        }


    }
}


//// Build a set of request IDs
//var requestIds = request.SubProducts?.Where(x => x.Id > 0).Select(x => x.Id).ToHashSet() ?? new();

//// Remove deleted sub-products (present in DB, missing from request)
//var toRemove = item.SubProducts
//    .Where(existing => existing.Id > 0 && !requestIds.Contains(existing.Id))
//    .ToList();

//foreach (var deleted in toRemove)
//{
//    item.SubProducts.Remove(deleted);
//}

//// Now handle insert or update
//foreach (var req in request.SubProducts ?? [])
//{
//    var existing = item.SubProducts.FirstOrDefault(x => x.Id == req.Id && req.Id > 0);

//    if (existing is not null)
//    {
//        // Update
//        existing.Color = req.Color;
//        existing.Unit = req.Unit;
//        existing.Stock = req.Stock;
//        existing.Price = req.Price;
//        existing.RetailPrice = req.RetailPrice;
//    }
//    else
//    {
//        // Insert
//        item.SubProducts.Add(new SubProduct
//        {
//            ProductId = item.Id,
//            Color = req.Color,
//            Unit = req.Unit,
//            Stock = req.Stock,
//            Price = req.Price,
//            RetailPrice = req.RetailPrice
//        });
//    }
//}


// Build a set of request IDs
//var requestIds = request.SubProducts?.Where(x => x.Id > 0).Select(x => x.Id).ToHashSet() ?? new();

//// Remove deleted sub-products (present in DB, missing from request)
//var toRemove = _context.SubProducts
//    .Where(existing => existing.Id > 0 && !requestIds.Contains(existing.Id))
//    .ToList();

//foreach (var deleted in toRemove)
//{
//    _context.SubProducts.Remove(deleted);
//}


//if (request is null)
//{
//    return await Result<int>.FailureAsync("Product is null.");
//}

//if (request.Id > 0)
//{
//    var item = await _context.Products
//        .Include(x => x.ColorOptions)
//        .Include(x => x.UnitOptions)
//        //.Include(x => x.SubProducts)
//        .AsSplitQuery()
//        .SingleOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

//    if (item is null)
//    {
//        return await Result<int>.FailureAsync($"Prduct with id: [{request.Id}] not found.");
//    }

//    ProductMapper.ApplyChangesFrom(request, item);

//    item.ColorOptions.Clear();
//    item.UnitOptions.Clear();


//    // STEP 1: Load existing SubProducts tracked by EF
//    var existing = await _context.SubProducts
//        .Where(x => x.ProductId == item.Id)
//        .ToListAsync(cancellationToken);

//    // STEP 2: Create lists to track changes
//    var incoming = request.SubProducts ?? [];
//    var existingIds = existing.Select(x => x.Id).ToHashSet();
//    var incomingIds = incoming.Where(x => x.Id > 0).Select(x => x.Id).ToHashSet();

//    var toDelete = existing.Where(e => !incomingIds.Contains(e.Id)).ToList();
//    var toUpdate = new List<SubProduct>();
//    var toInsert = new List<SubProduct>();

//    // STEP 3: Map updates or inserts
//    foreach (var dto in incoming)
//    {
//        if (dto.Id > 0)
//        {
//            var tracked = existing.FirstOrDefault(x => x.Id == dto.Id);
//            if (tracked != null)
//            {
//                // Update the tracked instance — no new object!
//                tracked.Color = dto.Color;
//                tracked.Unit = dto.Unit;
//                tracked.Stock = dto.Stock;
//                tracked.Price = dto.Price;
//                tracked.RetailPrice = dto.RetailPrice;

//                toUpdate.Add(tracked); // optional — for clarity
//            }
//        }
//        else
//        {
//            // Only new instances are created
//            toInsert.Add(new SubProduct
//            {
//                ProductId = item.Id,
//                Color = dto.Color,
//                Unit = dto.Unit,
//                Stock = dto.Stock,
//                Price = dto.Price,
//                RetailPrice = dto.RetailPrice
//            });
//        }
//    }

//    // STEP 4: Apply batch operations safely
//    _context.SubProducts.RemoveRange(toDelete);     // ✅ only deletes
//    _context.SubProducts.AddRange(toInsert);        // ✅ only new instances
//    _context.SubProducts.UpdateRange(toUpdate);

//    if (request?.ColorData?.Any() == true)
//    {
//        foreach (var color in request.ColorData.DistinctBy(x=>x.Value).ToArray())
//        {
//            item.ColorOptions.Add(new ProductColorOption
//            {
//                ProductId = item.Id,
//                Name = color.Name.ToString(),
//                Description = color.Description,
//                UnitId = color.Id,
//                Value = color?.Value?.ToString() ?? string.Empty,
//                Text = color?.Text?.ToString() ?? string.Empty
//            });
//        }
//    }

//    if (request?.UnitData?.Any() == true)
//    {
//        foreach (var unit in request.UnitData.DistinctBy(x => x.Value).ToArray())
//        {
//            item.UnitOptions.Add(new ProductUnitOption
//            {
//                ProductId = item.Id,
//                Name = unit.Name.ToString(),
//                Description = unit.Description,
//                UnitId = unit.Id,
//                Value = unit?.Value?.ToString() ?? string.Empty,
//                Text = unit?.Text?.ToString() ?? string.Empty
//            });
//        }
//    }

//    item.AddDomainEvent(new UpdatedEvent<Product>(item));
//    await _context.SaveChangesAsync(cancellationToken);
//    return await Result<int>.SuccessAsync(item.Id);
//}

// Classify for insert/update
//foreach (var dto in incoming)
//{
//    if (dto.Id > 0)
//    {
//        var tracked = item.SubProducts.FirstOrDefault(x => x.Id == dto.Id);
//        if (tracked != null)
//        {
//            tracked.Color = dto.Color;
//            tracked.Unit = dto.Unit;
//            tracked.Stock = dto.Stock;
//            tracked.Price = dto.Price;
//            tracked.RetailPrice = dto.RetailPrice;

//            var entry = _context.Entry(tracked); // 👈 this is just for checking/debugging

//            // ✅ If you want to force EF to track the changes
//            if (entry.State == EntityState.Detached)
//                _context.Attach(tracked); // attach only the entity

//            _context.Entry(tracked).State = EntityState.Modified;

//            Console.WriteLine($"SubProduct ID {tracked.Id} state: {_context.Entry(tracked).State}");
//        }
//    }
//    else
//    {
//        toInsert.Add(new SubProduct
//        {
//            ProductId = item.Id,
//            Color = dto.Color,
//            Unit = dto.Unit,
//            Stock = dto.Stock,
//            Price = dto.Price,
//            RetailPrice = dto.RetailPrice
//        });
//    }
//}

// Apply deletes
//_context.SubProducts.RemoveRange(toDelete);

// Apply inserts
//if (toInsert.Any())
//{
//    item.SubProducts.AddRange(toInsert);
//}

////Apply batch updates using ExecuteUpdateAsync
//foreach (var sub in toUpdate)
//{
//    await _context.SubProducts
//        .Where(x => x.Id == sub.Id)
//        .ExecuteUpdateAsync(setters => setters
//            .SetProperty(p => p.Color, sub.Color)
//            .SetProperty(p => p.Unit, sub.Unit)
//            .SetProperty(p => p.Stock, sub.Stock)
//            .SetProperty(p => p.Price, sub.Price)
//            .SetProperty(p => p.RetailPrice, sub.RetailPrice)
//            .SetProperty(p => p.LastModified, DateTime.UtcNow),
//            cancellationToken);
//}



//foreach (var dto in incoming)
//{
//    if (dto.Id > 0)
//    {
//        var tracked = item.SubProducts.FirstOrDefault(x => x.Id == dto.Id)
//                    ?? _context.ChangeTracker.Entries<SubProduct>().Select(e => e.Entity).FirstOrDefault(x => x.Id == dto.Id)
//                    ?? new SubProduct { Id = dto.Id };

//        if (_context.Entry(tracked).State == EntityState.Detached)
//            _context.Attach(tracked);

//        tracked.Color = dto.Color;
//        tracked.Unit = dto.Unit;
//        tracked.Stock = dto.Stock;
//        tracked.Price = dto.Price == 0 ? null : dto.Price;
//        tracked.RetailPrice = dto.RetailPrice == 0 ? null : dto.RetailPrice;

//        _context.Entry(tracked).State = EntityState.Modified;
//    }
//    else
//    {
//        // Prevent accidental duplicates (optional)
//        var exists = item.SubProducts.Any(x =>
//            x.Color == dto.Color &&
//            x.Unit == dto.Unit);

//        if (!exists)
//        {
//            item.SubProducts.Add(new SubProduct
//            {
//                ProductId = item.Id,
//                Color = dto.Color,
//                Unit = dto.Unit,
//                Stock = dto.Stock,
//                Price = dto.Price,
//                RetailPrice = dto.RetailPrice
//            });
//        }

//    }
//}

//        foreach (var dto in incoming)
//{
//    if (dto.Id > 0)
//    {
//        // First, check the local tracked collection
//        var tracked = item.SubProducts.FirstOrDefault(x => x.Id == dto.Id);

//        if (tracked == null)
//        {
//            // Not in the navigation property — try ChangeTracker
//            tracked = _context.ChangeTracker
//                .Entries<SubProduct>()
//                .Where(e => e.Entity.Id == dto.Id)
//                .Select(e => e.Entity)
//                .FirstOrDefault();
//        }

//        if (tracked == null)
//        {
//            // Final fallback — load from DB and attach safely
//            tracked = new SubProduct { Id = dto.Id };
//            _context.Attach(tracked); // safe: not yet tracked
//        }

//        // Update safely
//        tracked.Color = dto.Color;
//        tracked.Unit = dto.Unit;
//        tracked.Stock = dto.Stock;
//        tracked.Price = dto.Price;
//        tracked.RetailPrice = dto.RetailPrice;

//        // Optional: Mark manually if needed
//        _context.Entry(tracked).State = EntityState.Modified;
//    }
//    else
//    {
//        // New insert
//        item.SubProducts.Add(new SubProduct
//        {
//            ProductId = item.Id,
//            Color = dto.Color,
//            Unit = dto.Unit,
//            Stock = dto.Stock,
//            Price = dto.Price,
//            RetailPrice = dto.RetailPrice
//        });
//    }
//}