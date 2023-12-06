using Microsoft.Net.Http.Headers;
using SynapseAviationApp.Server.Database;
using SynapseAviationApp.Server.Services.Implementation;
using SynapseAviationApp.Server.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);

//Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddTransient<IExchangeRateService, ExchangeRateService>();
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<IFileSystemService, FileSystemService>();
var app = builder.Build();


app.UseDefaultFiles();
app.UseStaticFiles();

app.UseCors(options => options
        .AllowAnyMethod().AllowAnyHeader().AllowCredentials()
        .WithOrigins("https://localhost:4200", "http://localhost:4200")
    );

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapFallbackToFile("/index.html");

app.Run();
