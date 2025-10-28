using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace Outbox.Common;

public class RabbitPublisher1
{
    public async Task Publish<T>(EmployeeEvent employeeEvent, CancellationToken cancellationToken = default)
    {
        var factory = new ConnectionFactory() { HostName = "localhost" };
        using var connection = await factory.CreateConnectionAsync();
        using var channel = await connection.CreateChannelAsync();

        await channel.QueueDeclareAsync(queue: "outbox",
                              durable: false,
                              exclusive: false,
                              autoDelete: false,
                              arguments: null);

        // Serialize the EmployeeEvent to JSON
        var message = JsonSerializer.Serialize(employeeEvent);
        var body = Encoding.UTF8.GetBytes(message);

        

        //_ = await channel.BasicPublishAsync("",
        //                                    "outbox",
        //                                    mandatory: false,
        //                                    basicProperties: null,
        //                                    body: body,
        //                                    cancellationToken: cancellationToken);

        //logger.LogInformation("Published EmployeeEvent to RabbitMQ at: {time}", DateTimeOffset.Now);

        await Task.Delay(1000, cancellationToken);

    }

}
