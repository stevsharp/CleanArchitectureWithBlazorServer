// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
using System.ComponentModel;
using CleanArchitecture.Blazor.Application.Features.Allergies.DTOs;
using CleanArchitecture.Blazor.Application.Features.Allergies.Caching;

namespace CleanArchitecture.Blazor.Application.Features.Allergies.Commands.Update;

public class UpdateAllergyCommand: ICacheInvalidatorRequest<Result<int>>
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
            CreateMap<AllergyDto,UpdateAllergyCommand>(MemberList.None);
            CreateMap<UpdateAllergyCommand,Allergy>(MemberList.None);
        }
    }
}

    public class UpdateAllergyCommandHandler : IRequestHandler<UpdateAllergyCommand, Result<int>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<UpdateAllergyCommandHandler> _localizer;
        public UpdateAllergyCommandHandler(
            IApplicationDbContext context,
            IStringLocalizer<UpdateAllergyCommandHandler> localizer,
             IMapper mapper
            )
        {
            _context = context;
            _localizer = localizer;
            _mapper = mapper;
        }
        public async Task<Result<int>> Handle(UpdateAllergyCommand request, CancellationToken cancellationToken)
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
    }

