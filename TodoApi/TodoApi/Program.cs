using Microsoft.EntityFrameworkCore;
using TodoApi.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<TodoDb>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("TodoContext")));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
var app= builder.Build();

app.MapGet("/", () => "Hello World!");

app.MapGet("/todoitems", async (TodoDb db) =>
    await db.Todos.Select(x=> new TodoItemDTO(x)).ToListAsync());


app.MapGet("/todoitems/{id}", async (int id, TodoDb db) =>
await db.Todos.FindAsync(id) is Todo todo
        ? Results.Ok(new TodoItemDTO (todo))
        : Results.NotFound()

);

app.MapPost("/todoitems", async (TodoItemDTO todoItemDTO, TodoDb db) =>
{
    var todoItem = new Todo
    {
        IsCompleted = todoItemDTO.IsCompleted,
        Name=   todoItemDTO.Name
    };
    db.Todos.Add(todoItem);
    await db.SaveChangesAsync();
    return Results.Created($"/todoitems/{todoItem.Id}", new TodoItemDTO (todoItem));
});

app.MapPut("/todoitems/{id}", async (int id, TodoItemDTO todoItemDTO, TodoDb db) =>
{
   var todo=   await db.Todos.FindAsync(id);
    if(todo is null) return Results.NotFound();
    todo.Name = todoItemDTO.Name;
    todo.IsCompleted = todoItemDTO.IsCompleted;
    await db.SaveChangesAsync();
    return Results.NoContent();
});

app.MapDelete("/todoitems/{id}", async (int id,TodoItemDTO todoItemDTO, TodoDb db) =>
{
    var todo = await db.Todos.FindAsync(id);
    if (todo is null) return Results.NotFound();

    todo.Name = todoItemDTO.Name;
    todo.IsCompleted = todoItemDTO.IsCompleted;

    await db.SaveChangesAsync();
    return Results.NoContent();
});
app.Run();