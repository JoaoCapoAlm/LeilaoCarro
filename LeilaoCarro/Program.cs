using System.Reflection;
using Hangfire;
using Hangfire.SQLite;
using LeilaoCarro.Data;
using LeilaoCarro.Interfaces;
using LeilaoCarro.Jobs;
using LeilaoCarro.Middlewares;
using LeilaoCarro.Models;
using LeilaoCarro.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddDbContext<LeilaoContext>(options =>
{
    options.UseSqlite(builder.Configuration.GetConnectionString("sqlitePath"));
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.TryAddScoped<CarroService>();
builder.Services.TryAddScoped<LanceService>();
builder.Services.TryAddScoped<UsuarioService>();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "Leilão de carros",
        Version = "v1"
    });

    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFile));
});

builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("EmailSettings"));
builder.Services.AddHangfire(configuration => configuration
    .UseSimpleAssemblyNameTypeSerializer()
    .UseRecommendedSerializerSettings()
    .UseSQLiteStorage(builder.Configuration.GetConnectionString("sqlitePath")));

builder.Services.AddHangfireServer();

var app = builder.Build();
app.UseExceptionMiddleware();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
        options.RoutePrefix = string.Empty;
    });
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.UseHangfireDashboard();

var recurringJobManager = app.Services.GetRequiredService<IRecurringJobManager>();

// Agendar o job para enviar o e-mail todo dia às 8 horas da manhã
recurringJobManager.AddOrUpdate<LeiloarCarroJob>(
    "finalizar-lances",
    emailService => emailService.LeiloarCarros(),
    "0 8 * * *"
);

app.Run();
