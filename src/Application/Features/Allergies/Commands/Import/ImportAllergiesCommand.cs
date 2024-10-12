// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CleanArchitecture.Blazor.Application.Features.Allergies.DTOs;
using CleanArchitecture.Blazor.Application.Features.Allergies.Caching;

namespace CleanArchitecture.Blazor.Application.Features.Allergies.Commands.Import;

    public class ImportAllergiesCommand: ICacheInvalidatorRequest<Result<int>>
    {
        public string FileName { get; set; }
        public byte[] Data { get; set; }
        public string CacheKey => AllergyCacheKey.GetAllCacheKey;
        public CancellationTokenSource? SharedExpiryTokenSource => AllergyCacheKey.GetOrCreateTokenSource();
        public ImportAllergiesCommand(string fileName,byte[] data)
        {
           FileName = fileName;
           Data = data;
        }
    }
    public record class CreateAllergiesTemplateCommand : IRequest<Result<byte[]>>
    {
 
    }

    public class ImportAllergiesCommandHandler : 
                 IRequestHandler<CreateAllergiesTemplateCommand, Result<byte[]>>,
                 IRequestHandler<ImportAllergiesCommand, Result<int>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<ImportAllergiesCommandHandler> _localizer;
        private readonly IExcelService _excelService;
        private readonly AllergyDto _dto = new();

        public ImportAllergiesCommandHandler(
            IApplicationDbContext context,
            IExcelService excelService,
            IStringLocalizer<ImportAllergiesCommandHandler> localizer,
            IMapper mapper
            )
        {
            _context = context;
            _localizer = localizer;
            _excelService = excelService;
            _mapper = mapper;
        }
        #nullable disable warnings
        public async Task<Result<int>> Handle(ImportAllergiesCommand request, CancellationToken cancellationToken)
        {

           var result = await _excelService.ImportAsync(request.Data, mappers: new Dictionary<string, Func<DataRow, AllergyDto, object?>>
            {
                { _localizer[_dto.GetMemberDescription(x=>x.AllergyType)], (row, item) => item.AllergyType = row[_localizer[_dto.GetMemberDescription(x=>x.AllergyType)]].ToString() }, 
{ _localizer[_dto.GetMemberDescription(x=>x.Comments)], (row, item) => item.Comments = row[_localizer[_dto.GetMemberDescription(x=>x.Comments)]].ToString() }, 
{ _localizer[_dto.GetMemberDescription(x=>x.PatientId)], (row, item) => item.PatientId =Convert.ToInt32(row[_localizer[_dto.GetMemberDescription(x=>x.PatientId)]]) }, 

            }, _localizer[_dto.GetClassDescription()]);
            if (result.Succeeded && result.Data is not null)
            {
                foreach (var dto in result.Data)
                {
            //        var exists = await _context.Allergies.AnyAsync(x => x.Name == dto.Name, cancellationToken);
            //        if (!exists)
            //        {
            //            var item = _mapper.Map<Allergy>(dto);
            //            // add create domain events if this entity implement the IHasDomainEvent interface
				        //// item.AddDomainEvent(new AllergyCreatedEvent(item));
            //            await _context.Allergies.AddAsync(item, cancellationToken);
            //        }
                 }
                 await _context.SaveChangesAsync(cancellationToken);
                 return await Result<int>.SuccessAsync(result.Data.Count());
           }
           else
           {
               return await Result<int>.FailureAsync(result.Errors);
           }
        }
        public async Task<Result<byte[]>> Handle(CreateAllergiesTemplateCommand request, CancellationToken cancellationToken)
        {
            // TODO: Implement ImportAllergiesCommandHandler method 
            var fields = new string[] {
                   // TODO: Define the fields that should be generate in the template, for example:
                   _localizer[_dto.GetMemberDescription(x=>x.AllergyType)], 
_localizer[_dto.GetMemberDescription(x=>x.Comments)], 
_localizer[_dto.GetMemberDescription(x=>x.PatientId)], 

                };
            var result = await _excelService.CreateTemplateAsync(fields, _localizer[_dto.GetClassDescription()]);
            return await Result<byte[]>.SuccessAsync(result);
        }
    }

