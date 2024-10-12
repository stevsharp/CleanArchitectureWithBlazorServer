// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
using System.ComponentModel;
using CleanArchitecture.Blazor.Application.Features.Allergies.DTOs;
using CleanArchitecture.Blazor.Application.Features.Allergies.Caching;

namespace CleanArchitecture.Blazor.Application.Features.Allergies.Commands.Create;

public class CreateAllergyCommand: ICacheInvalidatorRequest<Result<int>>
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
             CreateMap<AllergyDto,CreateAllergyCommand>(MemberList.None);
             CreateMap<CreateAllergyCommand,Allergy>(MemberList.None);
        }
    }
}
    
    public class CreateAllergyCommandHandler : IRequestHandler<CreateAllergyCommand, Result<int>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<CreateAllergyCommand> _localizer;
        public CreateAllergyCommandHandler(
            IApplicationDbContext context,
            IStringLocalizer<CreateAllergyCommand> localizer,
            IMapper mapper
            )
        {
            _context = context;
            _localizer = localizer;
            _mapper = mapper;
        }
        public async Task<Result<int>> Handle(CreateAllergyCommand request, CancellationToken cancellationToken)
        {
           var item = _mapper.Map<Allergy>(request);
           // raise a create domain event
	       item.AddDomainEvent(new AllergyCreatedEvent(item));
           _context.Allergies.Add(item);
           await _context.SaveChangesAsync(cancellationToken);
           return  await Result<int>.SuccessAsync(item.Id);
        }
    }

