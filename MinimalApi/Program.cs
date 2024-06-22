using Microsoft.EntityFrameworkCore;
using MinimalApi;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<ToDoDbContext>(
    options => options.UseInMemoryDatabase("ToDO"));
//Get Method
var app = builder.Build();
app.MapGet("/todo-items", async (ToDoDbContext db) =>
    await db.ToDoItem.ToListAsync());
app.MapGet("/todo-items/{id}", async (int id, ToDoDbContext db) =>
    await db.ToDoItem.FindAsync(id));
app.MapPost("todo-items", async (ToDoItem item, ToDoDbContext db) =>
{
    await db.ToDoItem.AddAsync(item);
    await db.SaveChangesAsync();
    return Results.Created($"/todo-items/{item.Id}", item);
});

app.Run();
