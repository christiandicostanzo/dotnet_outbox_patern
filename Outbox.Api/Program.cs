using MassTransit; 
using Outbox.Api.Application;
using Outbox.Api.Endpoints;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddLogging();
builder.Services.AddOpenApi();

builder.Services.AddMassTransit(x =>
{

    //x.AddConsumer<CustomerEventConsumer>();

    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host("localhost", "/", h =>
        {
            h.Username("guest");
            h.Password("guest");
        });

        cfg.ConfigureEndpoints(context);

        cfg.Publish<CustomerCreatedEvent>(x =>
        {
            x.Durable = false; // default: true
            x.AutoDelete = false; // default: false
            x.ExchangeType = "fanout"; // default, allows any valid exchange type
        });

        //EndpointConvention.Map<CustomerCreatedEvent>(new Uri("rabbitmq://localhost/CustomerCreatedEvent"));
    });
});

                
var app = builder.Build();

//app.UseHttpLogging();
app.AddCustomerEndpoints();
app.Run();

