using EFCollectionUpdateIssue.Console.Data;
using EFCollectionUpdateIssue.Console.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;

// init app
var builder = Host.CreateApplicationBuilder();
var configuration = builder.Configuration;

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlite(configuration.GetConnectionString("DefaultConnection"), o => o.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName));
});

using IHost app = builder.Build();

// init and seed database
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    await dbContext.Database.MigrateAsync();
    if (!dbContext.Orders.Any())
    {
        // create sample order with 1 item
        var newOrder = Order.Create(Guid.Parse("a54fc7b9-1093-49d2-8885-eb7d6cc4bd21"), new[]
        {
            OrderItem.Create("Steak", 1)
        });

        await dbContext.Orders.AddAsync(newOrder);
        await dbContext.SaveChangesAsync();
    }
}

// update order items logic - failing!
using (var scope = app.Services.CreateScope())
{
    // get db context
    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

    // get order to change
    var order = await dbContext.Orders
        .Include(x => x.OrderItems)
        .SingleAsync(x => x.Id == Guid.Parse("a54fc7b9-1093-49d2-8885-eb7d6cc4bd21"));

    // replace items (clean and add range)
    order.SetOrderItemsCollection(new[]
    {
        OrderItem.Create("salad", 1),
        OrderItem.Create("beer", 1),
    });

    // set as update
    dbContext.Orders.Update(order);

    // save fails!
    await dbContext.SaveChangesAsync();
}