

namespace CleanArchitecture.Blazor.Application.Features.Suppliers.Specifications;
#nullable disable warnings
/// <summary>
/// Specification class for filtering Suppliers by their ID.
/// </summary>
public class SupplierByIdSpecification : Specification<Supplier>
{
    public SupplierByIdSpecification(int id) => Query.Where(q => q.Id == id);
}