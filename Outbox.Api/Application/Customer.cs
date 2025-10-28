using MassTransit;

namespace Outbox.Api.Application;

public class Customer
{
    public Guid CustomerId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
}

public class CustomerCreatedEvent
{
    public Guid CustomerId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
}

public class  CustomerCreatedReceiveEvent : CustomerCreatedEvent
{

}

public class CustomerEventConsumer : IConsumer<CustomerCreatedEvent>
{
    public Task Consume(ConsumeContext<CustomerCreatedEvent> context)
    {
        return Task.Run(() => 
            {
                Console.WriteLine($"Received Customer Event: {context.Message.Name}, Email: {context.Message.Email}");
            }
        );
    }
}
