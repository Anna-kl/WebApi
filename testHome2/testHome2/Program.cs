using Microsoft.EntityFrameworkCore;
using TestHomeWork.Models;
using testHome2.Controllers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddCors(o => o.AddPolicy("CorsPolicy", builder =>
{
    builder.
    AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin();
}));
builder.Services.AddControllersWithViews()
    .AddNewtonsoftJson(options =>
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<EFDataContext>(options =>
options.UseNpgsql("Host=postgres78.1gb.ru;Username=xgb_ocpio;Password=ZPyDk7j-cfHA;Database=xgb_ocpio;Port=5432"), ServiceLifetime.Transient);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
app.UseAuthorization();
app.UseCors("CorsPolicy");

app.MapControllers();

app.Run();
