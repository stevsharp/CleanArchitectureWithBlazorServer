

namespace CleanArchitecture.Blazor.Application.Features.Steps.Specifications;
#nullable disable warnings
/// <summary>
/// Specification class for filtering Steps by their ID.
/// </summary>
public class StepByInvoiceIdSpecification : Specification<Step>
{
    public StepByInvoiceIdSpecification(int id) => Query.Where(q => q.InvoiceId == id);
}

public class StepByIdSpecification : Specification<Step>
{
    public StepByIdSpecification(int id) => Query.Where(q => q.Id == id);
}

public class AllStepByIdSpecification : Specification<Step>
{
    public AllStepByIdSpecification() => Query.Where(q => q.IsCompleted == false && q.Name == "Create Product");
}

public class AllStepByIdSpecificationWithStep : Specification<Step>
{
    public AllStepByIdSpecificationWithStep(int stepOrder, int invoiceId)
    {
        Query.Where(BuildCriteria(stepOrder, invoiceId));
    }

    private static Expression<Func<Step, bool>> BuildCriteria(int stepOrder, int invoiceId)
    {
        return q => q.StepOrder == stepOrder
                    && q.IsCompleted == false
                    && q.Invoice.Steps.Any(s => s.StepOrder == stepOrder - 1 && s.IsCompleted == true);
    }
}