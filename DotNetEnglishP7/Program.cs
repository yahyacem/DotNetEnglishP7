using Dot.Net.WebApi.Data;
using Dot.Net.WebApi.Repositories;
using DotNetEnglishP7.Repositories;
using DotNetEnglishP7.Services;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<LocalDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddMvc(options =>
{
    options.SuppressAsyncSuffixInActionNames = false;
}).AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix);
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddTransient<ICurveService, CurveService>();
builder.Services.AddTransient<IBidListRepository, BidListRepository>();
builder.Services.AddTransient<ICurveRepository, CurveRepository>();
builder.Services.AddTransient<IUserRepository, UserRepository>();

var app = builder.Build();

//Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
