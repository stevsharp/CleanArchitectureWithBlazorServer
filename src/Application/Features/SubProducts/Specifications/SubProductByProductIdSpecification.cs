

namespace CleanArchitecture.Blazor.Application.Features.SubProducts.Specifications;

public class SubProductByProductIdSpecification : Specification<SubProduct>
{
    public SubProductByProductIdSpecification(int productId) => Query.Where(q => q.ProductId == productId);
}