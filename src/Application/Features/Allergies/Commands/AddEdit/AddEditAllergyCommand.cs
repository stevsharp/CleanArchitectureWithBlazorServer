// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CleanArchitecture.Blazor.Application.Features.Allergies.DTOs;
using CleanArchitecture.Blazor.Application.Features.Allergies.Caching;
namespace CleanArchitecture.Blazor.Application.Features.Allergies.Commands.AddEdit;

public class AddEditAllergyCommand: ICacheInvalidatorRequest<Result<int>>
{
      [Description("Id")]
      public int Id { get; set; }
          [Description("Allergy type")]
    public string? AllergyType {get;set;} 
    [Description("Comments")]
    public string? Comments {get;set;} 
    [Description("Patient id")]
    public int PatientId {get;set;} 


      public string CacheKey => AllergyCacheKey.GetAllCacheKey;
      public CancellationTokenSource? SharedExpiryTokenSource => AllergyCacheKey.GetOrCreateTokenSource();

    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<AllergyDto,AddEditAllergyCommand>(MemberList.None);
            CreateMap<AddEditAllergyCommand,Allergy>(MemberList.None);
         
        }
    }
}

    public class AddEditAllergyCommandHandler : IRequestHandler<AddEditAllergyCommand, Result<int>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<AddEditAllergyCommandHandler> _localizer;
        public AddEditAllergyCommandHandler(
            IApplicationDbContext context,
            IStringLocalizer<AddEditAllergyCommandHandler> localizer,
            IMapper mapper
            )
        {
            _context = context;
            _localizer = localizer;
            _mapper = mapper;
        }
        public async Task<Result<int>> Handle(AddEditAllergyCommand request, CancellationToken cancellationToken)
        {
            if (request.Id > 0)
            {
                var item = await _context.Allergies.FindAsync(request.Id, cancellationToken);
                if (item == null)
                {
                    return await Result<int>.FailureAsync($"Allergy with id: [{request.Id}] not found.");
                }
                item = _mapper.Map(request, item);
				// raise a update domain event
				item.AddDomainEvent(new AllergyUpdatedEvent(item));
                await _context.SaveChangesAsync(cancellationToken);
                return await Result<int>.SuccessAsync(item.Id);
            }
            else
            {
                var item = _mapper.Map<Allergy>(request);
                // raise a create domain event
				item.AddDomainEvent(new AllergyCreatedEvent(item));
                _context.Allergies.Add(item);
                await _context.SaveChangesAsync(cancellationToken);
                return await Result<int>.SuccessAsync(item.Id);
            }
           
        }
    }

