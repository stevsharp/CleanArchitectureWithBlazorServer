// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CleanArchitecture.Blazor.Application.Features.Products.Caching;
using CleanArchitecture.Blazor.Application.Features.Products.DTOs;
using CleanArchitecture.Blazor.Application.Features.Products.Mappers;
using CleanArchitecture.Blazor.Domain.Entities;

namespace CleanArchitecture.Blazor.Application.Features.Products.Queries.GetAll;

public class GetAllProductsQuery : ICacheableRequest<IEnumerable<ProductDto>>
{
    public string CacheKey => ProductCacheKey.GetAllCacheKey;
    public IEnumerable<string>? Tags => ProductCacheKey.Tags;
}

public class GetProductQuery : ICacheableRequest<ProductDto?>
{
    public required int Id { get; set; }

    public string CacheKey => ProductCacheKey.GetProductByIdCacheKey(Id);
    public IEnumerable<string>? Tags => ProductCacheKey.Tags;
}


public class GetProductByCodeQuery : ICacheableRequest<ProductDto?>
{
    public required string Code { get; set; }
    public string CacheKey => ProductCacheKey.GetProductByCodeCacheKey(Code);
    public IEnumerable<string>? Tags => ProductCacheKey.Tags;
}

public class GetProductByColorQuery : ICacheableRequest<ProductDto?>
{
    public required string Color { get; set; }
    public required string Code { get; set; }
    public required string Unit { get; set; }
    public string CacheKey => ProductCacheKey.GetProductByColorCacheKey(Color, Unit, Code);
    public IEnumerable<string>? Tags => ProductCacheKey.Tags;
}


public class GetAllProductsQueryHandler :
    IRequestHandler<GetAllProductsQuery, IEnumerable<ProductDto>>,
    IRequestHandler<GetProductQuery, ProductDto?>,
    IRequestHandler<GetProductByCodeQuery, ProductDto?>,
    IRequestHandler<GetProductByColorQuery, ProductDto?>

{
    private readonly IApplicationDbContext _context;

    public GetAllProductsQueryHandler(
        IApplicationDbContext context
    )
    {
        _context = context;
    }

    public async Task<IEnumerable<ProductDto>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
    {
        var data = await _context.Products
            .Include(x => x.SubProducts)
            .ProjectTo()
            .ToListAsync(cancellationToken);
        return data;
    }

    public async Task<ProductDto?> Handle(GetProductQuery request, CancellationToken cancellationToken)
    {
        var data = await _context.Products
                        .Where(x => x.Id == request.Id)
                        .AsNoTracking()
                       .ProjectTo()
                       .FirstOrDefaultAsync(cancellationToken);

        return data;
    }

    public async Task<ProductDto?> Handle(GetProductByCodeQuery request, CancellationToken cancellationToken)
    {
        var data = await _context.Products
                               .Include(x => x.SubProducts)
                               .Where(x => x.Code == request.Code)
                               .AsNoTracking()
                              .ProjectTo()
                              .FirstOrDefaultAsync(cancellationToken);

        return data;
    }
    public async Task<ProductDto?> Handle(GetProductByColorQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var data = await _context.Products
                .Include(x => x.SubProducts)
                .Where(x => x.Code == request.Code)
                .AsNoTracking()
                .ProjectTo()
                .FirstOrDefaultAsync(cancellationToken);

            if (data is null)
            {
                return null;
            }

            var matchingSubProduct = data?.SubProducts?
                    .FirstOrDefault(sp => sp.Color == request.Color && sp.Unit == request.Unit);

            data = data with
            {
                SubProducts = [matchingSubProduct ?? new SubProduct()]
            };

            return data;
        }
        catch (Exception ex)
        {
            var error = ex.Message;
            throw;
        }

    }
}