using MediatR;

namespace CqrsMediatrExample0.Notification
{
    public record ProductAddedNotification(Product Product) : INotification;
}
