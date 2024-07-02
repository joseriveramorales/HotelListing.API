// IoC container. This allows me to use things like dependency injection eventually.
// More services can be introduced later

using HoteListing.API.Configurations;
using HoteListing.API.Contracts;
using HoteListing.API.Data;
using HoteListing.API.DbConnectionTester;
using HoteListing.API.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using System.Configuration;

var builder = WebApplication.CreateBuilder(args);


var connectionString = builder.Configuration.GetConnectionString("HotelListingDBConnectionString");
builder.Services.AddDbContext<HotelListingDBContext>(options => {
    options.UseSqlServer(connectionString);
});

// Optional: Register a test service to check the database connection.
builder.Services.AddScoped<IDatabaseConnectionTester, DatabaseConnectionTester>();


// Identity Core is a standard commonly used library
// This manages users, passwords, profile data, roles, claims, token, email confirmation and other
// Identity Core supports external login providers that include Facebook,
// Google, Microsoft Account and Twitter

//  Typically a SQL Server DB is used to store user data, alternatively,
//  another persistent store can be used, for example, Azure Table Storage..

// Here Im registering APIUser my Subclass of IdentityUser
builder.Services.AddIdentityCore<APIUser>()
    .AddRoles<IdentityRole>()  
    .AddEntityFrameworkStores<HotelListingDBContext>();


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

builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<ICountriesRepository, CountriesRepository>();
builder.Services.AddScoped<IHotelsRepository, HotelsRepository>();
builder.Services.AddScoped<IAuthManager, AuthManager>();

var app = builder.Build();


//Test the connection when the application starts.

using (var scope = app.Services.CreateScope())
{
    var tester = scope.ServiceProvider.GetRequiredService<IDatabaseConnectionTester>();
    tester.TestConnection();
}


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
