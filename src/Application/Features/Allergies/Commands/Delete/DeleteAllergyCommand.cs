// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CleanArchitecture.Blazor.Application.Features.Allergies.Caching;


namespace CleanArchitecture.Blazor.Application.Features.Allergies.Commands.Delete;

    public class DeleteAllergyCommand:  ICacheInvalidatorRequest<Result<int>>
    {
      public int[] Id {  get; }
      public string CacheKey => AllergyCacheKey.GetAllCacheKey;
      public CancellationTokenSource? SharedExpiryTokenSource => AllergyCacheKey.GetOrCreateTokenSource();
      public DeleteAllergyCommand(int[] id)
      {
        Id = id;
      }
    }

    public class DeleteAllergyCommandHandler : 
                 IRequestHandler<DeleteAllergyCommand, Result<int>>

    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<DeleteAllergyCommandHandler> _localizer;
        public DeleteAllergyCommandHandler(
            IApplicationDbContext context,
            IStringLocalizer<DeleteAllergyCommandHandler> localizer,
             IMapper mapper
            )
        {
            _context = context;
            _localizer = localizer;
            _mapper = mapper;
        }
        public async Task<Result<int>> Handle(DeleteAllergyCommand request, CancellationToken cancellationToken)
        {
            var items = await _context.Allergies.Where(x=>request.Id.Contains(x.Id)).ToListAsync(cancellationToken);
            foreach (var item in items)
            {
			    // raise a delete domain event
				item.AddDomainEvent(new AllergyDeletedEvent(item));
                _context.Allergies.Remove(item);
            }
            var result = await _context.SaveChangesAsync(cancellationToken);
            return await Result<int>.SuccessAsync(result);
        }

    }

