using Microsoft.AspNetCore.Http.Features;
using Microsoft.EntityFrameworkCore;
using TransactionsTestApi.Data;
using TransactionsTestApi.Extensions;
using TransactionsTestApi.Services;
using TransactionsTestApi.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ITransactionsService, TransactionsLoadService>();

builder.Services.AddDbContext<AppDbContext>(
    opt => opt.UseNpgsql(builder.Configuration.GetConnectionString("Database")));

builder.Services.Configure<FormOptions>(x =>
{
    var limit = builder.Configuration["MultipartBodyLengthLimit"];

    if (!string.IsNullOrEmpty(limit))
    {
        x.MultipartBodyLengthLimit = long.Parse(limit);
    }
});

var app = builder.Build();
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.ApplyMigrations();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
