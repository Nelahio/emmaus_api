using emmaus_api.Data;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);
var conStrBuilder = new SqlConnectionStringBuilder(
        builder.Configuration.GetConnectionString("EmmausDb"));
conStrBuilder.UserID = builder.Configuration["Connection:UserId"];
conStrBuilder.Password = builder.Configuration["Connection:UserPassword"];
var connection = conStrBuilder.ConnectionString;
// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<EmmausContext>(options => options.UseSqlServer(connection));
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
