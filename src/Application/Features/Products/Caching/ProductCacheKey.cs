// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace CleanArchitecture.Blazor.Application.Features.Products.Caching;

public static class ProductCacheKey
{
    public const string GetAllCacheKey = "all-Products";
    public static string GetProductByIdCacheKey(int id)
    {
        return $"GetProductById,{id}";
    }

    public static string GetProductByCodeCacheKey(string code)
    {
        return $"GetProductByCode,{code}";
    }

    public static string GetProductByColorCacheKey(string color, string unit, string code)
    {
        return $"GetProductByColorAndUnit,{color}{unit}{code}";
    }

    public static string GetPaginationCacheKey(string parameters)
    {
        return $"ProductsWithPaginationQuery,{parameters}";
    }
    public static IEnumerable<string>? Tags => ["product"];
    public static void Refresh()
    {
        FusionCacheFactory.RemoveByTags(Tags);
    }

}
