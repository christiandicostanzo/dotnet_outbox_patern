using MassTransit;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Outbox.Common;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace Outbox.MessageRelay;

public class Worker(
ILogger<Worker> logger
) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
           
        }
    }
}
