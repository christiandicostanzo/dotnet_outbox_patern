using MassTransit;
using Microsoft.AspNetCore.Http.HttpResults;
using Outbox.Api.Application;
using static MassTransit.ValidationResultExtensions;

namespace Outbox.Api.Endpoints;

public static class CustomersEndpoints
{
    public static WebApplication AddCustomerEndpoints(this WebApplication app)
    {
        var customerApi = app.MapGroup("/customers");

        customerApi
            .WithDisplayName("Customer Api");

        customerApi.MapPost("/", CreateCustomer);

        return app;

    }
    public static async Task<Results<Ok<Customer>, IResult>> CreateCustomer(
        Customer request,
        ISendEndpointProvider sendEndpointProvider,
        IBus bus,
        CancellationToken cancellation)
    {
        try
        {
            var customer = new Customer
            {
                CustomerId = Guid.NewGuid(),
                Name = request.Name,
                Email = request.Email
            };

            CustomerCreatedEvent customerEvent = new CustomerCreatedEvent
            {
                CustomerId = customer.CustomerId,
                Name = customer.Name,
                Email = customer.Email
            };

            //await sendEndpointProvider.Send<CustomerCreatedEvent>(customerEvent, cancellationToken: cancellation);
            await bus.Publish<CustomerCreatedEvent>(customerEvent, cancellation);

            return TypedResults.Ok(customer);
        }
        catch (Exception ex)
        {
            //logger.LogError(ex, "Error creating customer");
            throw;
        }
    }

}
