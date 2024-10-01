using FitStore.DataAccess;
using FitStore.DBConn;
using FitStore.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<ProductRepo>();
//builder.Services.Configure<MongoDbSettings>(builder.Configuration.GetSection(nameof(MongoDbSettings)));

//builder.Services.AddSingleton<IMongoClient>(serviceProvider =>
//{
//    var settings = serviceProvider.GetRequiredService<IOptions<MongoDbSettings>>().Value;
//    return new MongoClient(settings.ConnectionString);
//});

//builder.Services.AddScoped(serviceProvider =>
//{
//    var settings = serviceProvider.GetRequiredService<IOptions<MongoDbSettings>>().Value;
//    var client = serviceProvider.GetRequiredService<IMongoClient>();
//    return client.GetDatabase(settings.DatabaseName);
//});

builder.Services.AddDbContext<ApplicationDBContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//app.UseAuthorization();

app.MapControllers();

app.Run();
