
using CleanArchitecture.Blazor.Application.Features.Invoices.DTOs;
using CleanArchitecture.Blazor.Application.Features.Steps.Caching;
using CleanArchitecture.Blazor.Application.Features.Steps.Mappers;

namespace CleanArchitecture.Blazor.Application.Features.Steps.Commands.AddEdit;

public class AddEditStepCommand: ICacheInvalidatorRequest<Result<int>>
{
      [Description("Id")]
      public int Id { get; set; }
          [Description("Name")]
    public string Name {get;set;} 
    [Description("Invoice id")]
    public int? InvoiceId {get;set;} 
    [Description("Is completed")]
    public bool IsCompleted {get;set;} 
    [Description("Step order")]
    public int StepOrder {get;set;} 
    //[Description("Comments")]
    //public List<CommentDtoDto>? Comments {get;set;} 
    [Description("Invoice")]
    public InvoiceDto Invoice {get;set;} 


      public string CacheKey => StepCacheKey.GetAllCacheKey;
      public IEnumerable<string>? Tags => StepCacheKey.Tags;
}

public class AddEditStepCommandHandler : IRequestHandler<AddEditStepCommand, Result<int>>
{
    private readonly IApplicationDbContext _context;
    public AddEditStepCommandHandler(
        IApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<Result<int>> Handle(AddEditStepCommand request, CancellationToken cancellationToken)
    {
        if (request.Id > 0)
        {
            var item = await _context.Steps.FindAsync(request.Id, cancellationToken);
            if (item == null)
            {
                return await Result<int>.FailureAsync($"Step with id: [{request.Id}] not found.");
            }
            StepMapper.ApplyChangesFrom(request,item);
			// raise a update domain event
			//item.AddDomainEvent(new StepUpdatedEvent(item));
            await _context.SaveChangesAsync(cancellationToken);
            return await Result<int>.SuccessAsync(item.Id);
        }
        else
        {
            var item = StepMapper.FromEditCommand(request);
            // raise a create domain event
			//item.AddDomainEvent(new StepCreatedEvent(item));
            _context.Steps.Add(item);
            await _context.SaveChangesAsync(cancellationToken);
            return await Result<int>.SuccessAsync(item.Id);
        }
       
    }
}

