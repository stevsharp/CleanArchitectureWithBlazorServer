// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace CleanArchitecture.Blazor.Application.Features.Allergies.EventHandlers;

    public class AllergyUpdatedEventHandler : INotificationHandler<AllergyUpdatedEvent>
    {
        private readonly ILogger<AllergyUpdatedEventHandler> _logger;

        public AllergyUpdatedEventHandler(
            ILogger<AllergyUpdatedEventHandler> logger
            )
        {
            _logger = logger;
        }
        public Task Handle(AllergyUpdatedEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handled domain event '{EventType}' with notification: {@Notification} ", notification.GetType().Name, notification);
            return Task.CompletedTask;
        }
    }
