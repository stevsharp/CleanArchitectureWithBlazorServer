// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace CleanArchitecture.Blazor.Application.Features.Allergies.EventHandlers;

public class AllergyCreatedEventHandler : INotificationHandler<AllergyCreatedEvent>
{
        private readonly ILogger<AllergyCreatedEventHandler> _logger;

        public AllergyCreatedEventHandler(
            ILogger<AllergyCreatedEventHandler> logger
            )
        {
            _logger = logger;
        }
        public Task Handle(AllergyCreatedEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handled domain event '{EventType}' with notification: {@Notification} ", notification.GetType().Name, notification);
            return Task.CompletedTask;
        }
}
