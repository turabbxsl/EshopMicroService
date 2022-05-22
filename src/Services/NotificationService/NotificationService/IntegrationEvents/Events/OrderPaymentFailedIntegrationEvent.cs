using EventBus.Base.Events;

namespace NotificationService.IntegrationEvents.Events
{
    public class OrderPaymentFailedIntegrationEvent : IntegrationEvent
    {
        public int OrderId { get; set; }

        public string ErrorMessage { get; set; }


        public OrderPaymentFailedIntegrationEvent(int orderid, string errorMessage)
        {
            OrderId = orderid;
            ErrorMessage = errorMessage;
        }
    }
}
