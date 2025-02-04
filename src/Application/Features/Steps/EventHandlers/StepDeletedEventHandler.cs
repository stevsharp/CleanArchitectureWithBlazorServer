
//namespace CleanArchitecture.Blazor.Application.Features.Steps.EventHandlers;

//    public class StepDeletedEventHandler : INotificationHandler<StepDeletedEvent>
//    {
//        private readonly ILogger<StepDeletedEventHandler> _logger;

//        public StepDeletedEventHandler(
//            ILogger<StepDeletedEventHandler> logger
//            )
//        {
//            _logger = logger;
//        }
//        public Task Handle(StepDeletedEvent notification, CancellationToken cancellationToken)
//        {
//            _logger.LogInformation("Handled domain event '{EventType}' with notification: {@Notification} ", notification.GetType().Name, notification);
//            return Task.CompletedTask;
//        }
//    }
