// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace CleanArchitecture.Blazor.Application.Features.Allergies.EventHandlers;

    public class AllergyDeletedEventHandler : INotificationHandler<AllergyDeletedEvent>
    {
        private readonly ILogger<AllergyDeletedEventHandler> _logger;

        public AllergyDeletedEventHandler(
            ILogger<AllergyDeletedEventHandler> logger
            )
        {
            _logger = logger;
        }
        public Task Handle(AllergyDeletedEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handled domain event '{EventType}' with notification: {@Notification} ", notification.GetType().Name, notification);
            return Task.CompletedTask;
        }
    }
