using Infra.Ioc;
using WebUI.Common.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddProjectServices(builder.Configuration);
builder.Services.AddWebUiServices(builder.Configuration);

var app = builder.Build();

await app.MigrateAndSeedDatabaseAsync();
app.UseWebUiPipeline();

app.Run();
