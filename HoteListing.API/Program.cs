// IoC container. This allows me to use things like dependency injection eventually.
// More services can be introduced later

using HoteListing.API.Configurations;
using HoteListing.API.Data;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);


var connectionString = builder.Configuration.GetConnectionString("HotelListingDBConnectionString");
builder.Services.AddDbContext<HotelListingDBContext>(options => {
    options.UseSqlServer(connectionString);
});


// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// If a Cors policy isnt used, when we run our API it would only be accesible in the same server where it's running
// This would defeat the purpose of hosting the API in the internet

builder.Services.AddCors(option =>
{
    option.AddPolicy("AllowAll",
        b => b.AllowAnyHeader()
        .AllowAnyOrigin()
        .AllowAnyMethod());

});

// I want to use Serilog, Serilog Sinks and Expressions

// I want to use Serilog, I create an instance of the builder (ctx), and the logger configuration (lc)
// I ask the logger to Write to console, and read from the builder's Configuration (appsettings.json).
builder.Host.UseSerilog((ctx, lc) => lc.WriteTo.Console().ReadFrom.Configuration(ctx.Configuration));

// After I added the Nuget for Automapper and created my MapperConfig, 
// proceed to inject the MapperConfig into my Services using AddAutoMapper()
builder.Services.AddAutoMapper(typeof(MapperConfig));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// This is oging to add default request logging. 
// Log type of requests comming in and how long they took to respond.
app.UseSerilogRequestLogging();

app.UseHttpsRedirection();

//Uncomment the line below to enable use of the API with Functions

//app.UseCors("AllowAll");

app.UseAuthorization();

app.MapControllers();

app.Run();
