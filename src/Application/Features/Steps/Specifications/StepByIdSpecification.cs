

namespace CleanArchitecture.Blazor.Application.Features.Steps.Specifications;
#nullable disable warnings
/// <summary>
/// Specification class for filtering Steps by their ID.
/// </summary>
public class StepByIdSpecification : Specification<Step>
{
    public StepByIdSpecification(int id)
    {
       Query.Where(q => q.Id == id);
    }
}