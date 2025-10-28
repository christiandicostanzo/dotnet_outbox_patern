using MassTransit;
using Outbox.Common;
using Outbox.MessageRelay;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddMassTransit(x =>
{
    // 👇 Primero registrás el consumidor
    x.AddConsumer<EmployeeEventConsumer>();

    // 👇 Luego configurás RabbitMQ y los endpoints
    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host("localhost", 5672, "/", h =>
        {
            h.Username("guest");
            h.Password("guest");
        });

        // 👇 Configurás el endpoint que usará ese consumidor
        cfg.ReceiveEndpoint("myqueue", e =>
        {
            e.ConfigureConsumer<EmployeeEventConsumer>(context);
        });
    });

});



try
{


    var bus = builder.Services.BuildServiceProvider().GetService<IBus>();


    for (int i = 0; i < 100; i++)
    {
        var employeeEvent = new EmployeeEvent
        {
            Id = Guid.NewGuid(),
            FirstName = "John",
            Company = "Contoso"
        };


        var endpoint = await bus.GetSendEndpoint(new Uri("queue:myqueue"));
        await endpoint.Send(employeeEvent);
    }
}
catch (Exception ex)
{
    throw;
}


//builder.Services.AddScoped<RabbitPublisher>();


builder.Services.AddHostedService<Worker>();

var host = builder.Build();
host.Run();
