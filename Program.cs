using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Builder;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<CoffeeDbContext>();
var app = builder.Build();

app.UseStaticFiles();

app.MapGet("/", () => Results.Redirect("/products.html"));

using (var scope = app.Services.CreateScope()) {
    var db = scope.ServiceProvider.GetRequiredService<CoffeeDbContext>();
    db.Database.EnsureCreated();
}


app.MapGet("/api/products", (CoffeeDbContext db) => db.Products.ToList());

app.MapPost("/api/products", (Products newProduct, CoffeeDbContext db) => {
    db.Products.Add(newProduct);
    db.SaveChanges();
    return Results.Ok(newProduct);
});

app.MapPut("/api/products/{id}", (int id, Products updateProduct, CoffeeDbContext db) => {
    var product = db.Products.Find(id);
    if (product is null) {
        return Results.NotFound($"Sorry, product with ID {id} was not found");
    }
    product.Name = updateProduct.Name;
    product.Price = updateProduct.Price;
    product.Category = updateProduct.Category;
    db.SaveChanges();
    return Results.Ok(product);
});

app.MapDelete("/api/products/{id}", (int id, CoffeeDbContext db) => {
    var product = db.Products.Find(id);
    if (product is null) {
        return Results.NotFound($"Sorry, product ID {id} was not found");
    }
    db.Products.Remove(product);
    db.SaveChanges();
    return Results.Ok(product);
});


app.MapGet("/api/orders", (CoffeeDbContext db) => db.Orders.ToList());

app.MapPost("/api/orders", (Orders newOrder, CoffeeDbContext db) => {
    db.Orders.Add(newOrder);
    db.SaveChanges();
    return Results.Ok(newOrder);
});

app.MapPut("/api/orders/{id}", (int id, Orders updateOrder, CoffeeDbContext db) => {
    var order = db.Orders.Find(id);
    if (order is null) {
        return Results.NotFound($"Sorry, order with ID {id} was not found!");
    }
    order.CustomerName = updateOrder.CustomerName;
    order.ItemOrdered = updateOrder.ItemOrdered;
    order.Quantity = updateOrder.Quantity;
    db.SaveChanges();
    return Results.Ok(order);
});

app.MapDelete("/api/orders/{id}", (int id, CoffeeDbContext db) => {
    var order = db.Orders.Find(id);
    if (order is null) {
        return Results.NotFound($"Sorry, order ID {id} was not found!");
    }
    db.Orders.Remove(order);
    db.SaveChanges();
    return Results.Ok(order);
});

app.Run();