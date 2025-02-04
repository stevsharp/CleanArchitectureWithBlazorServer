
//namespace CleanArchitecture.Blazor.Application.Features.Steps.EventHandlers;

//public class StepCreatedEventHandler : INotificationHandler<StepCreatedEvent>
//{
//        private readonly ILogger<StepCreatedEventHandler> _logger;

//        public StepCreatedEventHandler(
//            ILogger<StepCreatedEventHandler> logger
//            )
//        {
//            _logger = logger;
//        }
//        public Task Handle(StepCreatedEvent notification, CancellationToken cancellationToken)
//        {
//            _logger.LogInformation("Handled domain event '{EventType}' with notification: {@Notification} ", notification.GetType().Name, notification);
//            return Task.CompletedTask;
//        }
//}
