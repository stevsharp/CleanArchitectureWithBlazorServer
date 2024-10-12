namespace CleanArchitecture.Blazor.Application.Features.Allergies.Specifications;
#nullable disable warnings
/// <summary>
/// Specification class for filtering Allergies by their ID.
/// </summary>
public class AllergyByIdSpecification : Specification<Allergy>
{
    public AllergyByIdSpecification(int id)
    {
       Query.Where(q => q.Id == id);
    }
}