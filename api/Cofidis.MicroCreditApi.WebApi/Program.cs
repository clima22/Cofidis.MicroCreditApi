using Cofidis.MicroCreditApi.WebApi;
using Cofidis.MicroCreditApi.Core.Application;
using Cofidis.MicroCreditApi.Infra.ExternalApi;
using Cofidis.MicroCreditApi.Infra.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//log
var logger = builder.Services.ConfigureLogs(builder.Configuration, builder.Logging);

//Infrastructure - ExternalApis
builder.Services.ConfigureExternalApis();

//Infrastructure - Repository / Database
builder.Services.ConfigureRepositories(builder.Configuration);

//Core - Application
builder.Services.ConfigureApplication();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Logger.LogInformation($"{ApiVersion.AppName} is starting :: AppVersion: {ApiVersion.Version}");

app.Lifetime.ApplicationStarted.Register(() => app.Logger.LogInformation($"{ApiVersion.AppName} is running"));

app.Lifetime.ApplicationStopping.Register(() => app.Logger.LogInformation($"{ApiVersion.AppName} is stopping"));

app.Lifetime.ApplicationStopped.Register(() => app.Logger.LogInformation($"{ApiVersion.AppName} stopped"));

app.Run();
