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
app.MapPut("/todo-items/{id}", async (int id, ToDoItem item, ToDoDbContext db) =>
{
    var todoItem = await db.ToDoItem.FindAsync(id);
    if (todoItem is null)
    {
        return Results.NotFound();
    }
    todoItem.Task = item.Task;
    todoItem.IsComplete = item.IsComplete;
    await db.SaveChangesAsync();
    return Results.NoContent();
});
app.MapDelete("/todo-items/{id}", async (int id, ToDoDbContext db) =>
{
    var item = await db.ToDoItem.FindAsync(id);
    if (item == null)
    {
        return Results.NotFound();
    }
    db.ToDoItem.Remove(item);
    await db.SaveChangesAsync();
    return Results.NoContent();
});

app.Run();
