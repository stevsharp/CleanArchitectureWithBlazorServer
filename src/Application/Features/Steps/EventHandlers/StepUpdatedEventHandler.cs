
//namespace CleanArchitecture.Blazor.Application.Features.Steps.EventHandlers;

//    public class StepUpdatedEventHandler : INotificationHandler<StepUpdatedEvent>
//    {
//        private readonly ILogger<StepUpdatedEventHandler> _logger;

//        public StepUpdatedEventHandler(
//            ILogger<StepUpdatedEventHandler> logger
//            )
//        {
//            _logger = logger;
//        }
//        public Task Handle(StepUpdatedEvent notification, CancellationToken cancellationToken)
//        {
//            _logger.LogInformation("Handled domain event '{EventType}' with notification: {@Notification} ", notification.GetType().Name, notification);
//            return Task.CompletedTask;
//        }
//    }
