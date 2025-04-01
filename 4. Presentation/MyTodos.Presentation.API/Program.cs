using Microsoft.EntityFrameworkCore;
using MyTodos.Application.Business.Configuration;
using MyTodos.Infrastructure.DataAccess.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.RegisterTodoApplicationServices();

if (builder.Environment.IsDevelopment())
    builder.Services.RegisterSqliteDataAccessServices(builder.Configuration);
else
    builder.Services.RegisterSqlServerDataAccessServices(builder.Configuration);

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
// if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

// Should normally happen in seperate migration runner or something like that
using (var serviceScope = app.Services.CreateScope())
{
    var context = serviceScope.ServiceProvider.GetRequiredService<TodoDbContext>();
    context.Database.Migrate();
}

app.Run();