using MassTransit;
using Outbox.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Outbox.MessageRelay;

public class EmployeeEventConsumer : IConsumer<EmployeeEvent>
{
    public Task Consume(ConsumeContext<EmployeeEvent> context)
    {
        Console.WriteLine($"Recibido: {context.Message.FirstName}");
        return Task.CompletedTask;
    }
}