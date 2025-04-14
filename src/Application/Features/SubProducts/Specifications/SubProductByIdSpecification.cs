

namespace CleanArchitecture.Blazor.Application.Features.SubProducts.Specifications;
#nullable disable warnings
/// <summary>
/// Specification class for filtering SubProducts by their ID.
/// </summary>
public class SubProductByIdSpecification : Specification<SubProduct>
{
    public SubProductByIdSpecification(int id) => Query.Where(q => q.Id == id);
}
