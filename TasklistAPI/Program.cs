using Microsoft.EntityFrameworkCore;
using TasklistAPI.DAL.Implementation;
using TasklistAPI.DAL.Interfaces;
using TasklistAPI.Data;
using TasklistAPI.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCors(option => option.AddPolicy("PolicyOne", builder =>
{
    builder.AllowAnyOrigin()
           .WithMethods("PUT", "DELETE", "GET", "POST")
           .AllowAnyHeader()
           .WithExposedHeaders("X-Pagination")
           .SetPreflightMaxAge(TimeSpan.FromSeconds(2520));
}));
// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<TasklistContext>(options => options.UseSqlServer(
    builder.Configuration.GetConnectionString("Tasklist")
    ));
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<ITaskRepository, TaskRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("PolicyOne");

app.UseAuthorization();

app.MapControllers();

app.Run();
