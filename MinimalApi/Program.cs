using Microsoft.EntityFrameworkCore;
using MinimalApi;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<ToDoDbContext>(
    options => options.UseInMemoryDatabase("ToDO"));
//Get Method
var app = builder.Build();
app.MapGet("/todo-items", async (ToDoDbContext db) =>
    await db.ToDoItem.ToListAsync());
app.MapGet("/todo-items/{id}", async (ToDoDbContext db, int id) =>
    await db.ToDoItem.FindAsync(id));


app.Run();
