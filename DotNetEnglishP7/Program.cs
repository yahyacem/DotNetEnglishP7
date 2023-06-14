using Dot.Net.WebApi.Data;
using Dot.Net.WebApi.Domain;
using Dot.Net.WebApi.Repositories;
using DotNetEnglishP7;
using DotNetEnglishP7.Repositories;
using DotNetEnglishP7.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Setup connection to database
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<LocalDbContext>(options =>
    options.UseSqlServer(connectionString));

// Setup Identity
builder.Services.AddAuthentication();
builder.Services.ConfigureIdentity();

builder.Host.UseSerilog((context, configuration) => 
    configuration.WriteTo.Console(restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Information,
    outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {Message:lj}{NewLine}{Exception}")
    .WriteTo.File("DotNetEnglishP7-API.log",
    outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {Message:lj}{NewLine}{Exception}",
    rollingInterval: RollingInterval.Day,
    restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Information));


builder.Services.AddMvc(options =>
{
    options.SuppressAsyncSuffixInActionNames = false;
}).AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add services and repositories
builder.Services.AddScoped<ITradeService, TradeService>();
builder.Services.AddScoped<IRuleService, RuleService>();
builder.Services.AddScoped<IRatingService, RatingService>();
builder.Services.AddScoped<ICurveService, CurveService>();
builder.Services.AddScoped<IBidListService, BidListService>();
builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddScoped<ITradeRepository, TradeRepository>();
builder.Services.AddScoped<IRuleRepository, RuleRepository>();
builder.Services.AddScoped<IRatingRepository, RatingRepository>();
builder.Services.AddScoped<IBidListRepository, BidListRepository>();
builder.Services.AddScoped<ICurveRepository, CurveRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

var app = builder.Build();

//Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseSerilogRequestLogging();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();