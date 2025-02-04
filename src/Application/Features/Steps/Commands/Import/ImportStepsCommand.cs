

using CleanArchitecture.Blazor.Application.Features.Steps.DTOs;
using CleanArchitecture.Blazor.Application.Features.Steps.Caching;
using CleanArchitecture.Blazor.Application.Features.Steps.Mappers;

namespace CleanArchitecture.Blazor.Application.Features.Steps.Commands.Import;

    public class ImportStepsCommand: ICacheInvalidatorRequest<Result<int>>
    {
        public string FileName { get; set; }
        public byte[] Data { get; set; }
        public string CacheKey => StepCacheKey.GetAllCacheKey;
        public IEnumerable<string>? Tags => StepCacheKey.Tags;
        public ImportStepsCommand(string fileName,byte[] data)
        {
           FileName = fileName;
           Data = data;
        }
    }
    public record class CreateStepsTemplateCommand : IRequest<Result<byte[]>>
    {
 
    }

    public class ImportStepsCommandHandler : 
                 IRequestHandler<CreateStepsTemplateCommand, Result<byte[]>>,
                 IRequestHandler<ImportStepsCommand, Result<int>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IStringLocalizer<ImportStepsCommandHandler> _localizer;
        private readonly IExcelService _excelService;
        private readonly StepDto _dto = new();

        public ImportStepsCommandHandler(
            IApplicationDbContext context,
            IExcelService excelService,
            IStringLocalizer<ImportStepsCommandHandler> localizer)
        {
            _context = context;
            _localizer = localizer;
            _excelService = excelService;
        }
        #nullable disable warnings
        public async Task<Result<int>> Handle(ImportStepsCommand request, CancellationToken cancellationToken)
        {

           var result = await _excelService.ImportAsync(request.Data, mappers: new Dictionary<string, Func<DataRow, StepDto, object?>>
            {
                { _localizer[_dto.GetMemberDescription(x=>x.Name)], (row, item) => item.Name = row[_localizer[_dto.GetMemberDescription(x=>x.Name)]].ToString() }, 
{ _localizer[_dto.GetMemberDescription(x=>x.InvoiceId)], (row, item) => item.InvoiceId =Convert.ToInt32(row[_localizer[_dto.GetMemberDescription(x=>x.InvoiceId)]]) }, 
{ _localizer[_dto.GetMemberDescription(x=>x.IsCompleted)], (row, item) => item.IsCompleted =Convert.ToBoolean(row[_localizer[_dto.GetMemberDescription(x=>x.IsCompleted)]]) }, 
{ _localizer[_dto.GetMemberDescription(x=>x.StepOrder)], (row, item) => item.StepOrder =Convert.ToInt32(row[_localizer[_dto.GetMemberDescription(x=>x.StepOrder)]]) }, 

            }, _localizer[_dto.GetClassDescription()]);
            if (result.Succeeded && result.Data is not null)
            {
                foreach (var dto in result.Data)
                {
                    var exists = await _context.Steps.AnyAsync(x => x.Name == dto.Name, cancellationToken);
                    if (!exists)
                    {
                        var item = StepMapper.FromDto(dto);
                        // add create domain events if this entity implement the IHasDomainEvent interface
				        // item.AddDomainEvent(new StepCreatedEvent(item));
                        await _context.Steps.AddAsync(item, cancellationToken);
                    }
                 }
                 await _context.SaveChangesAsync(cancellationToken);
                 return await Result<int>.SuccessAsync(result.Data.Count());
           }
           else
           {
               return await Result<int>.FailureAsync(result.Errors);
           }
        }
        public async Task<Result<byte[]>> Handle(CreateStepsTemplateCommand request, CancellationToken cancellationToken)
        {
            // TODO: Implement ImportStepsCommandHandler method 
            var fields = new string[] {
                   // TODO: Define the fields that should be generate in the template, for example:
                   _localizer[_dto.GetMemberDescription(x=>x.Name)], 
_localizer[_dto.GetMemberDescription(x=>x.InvoiceId)], 
_localizer[_dto.GetMemberDescription(x=>x.IsCompleted)], 
_localizer[_dto.GetMemberDescription(x=>x.StepOrder)], 

                };
            var result = await _excelService.CreateTemplateAsync(fields, _localizer[_dto.GetClassDescription()]);
            return await Result<byte[]>.SuccessAsync(result);
        }
    }

