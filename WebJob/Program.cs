using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WebJob;

var builder = WebApplication.CreateBuilder(args);

// Adicione o background service
builder.Services.AddHostedService<MyBackgroundJob>();

var app = builder.Build();

// Configurações adicionais...

app.Run();
