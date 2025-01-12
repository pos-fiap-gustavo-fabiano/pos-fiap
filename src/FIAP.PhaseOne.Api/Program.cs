using FIAP.PhaseOne.Infra;
using FIAP.PhaseOne.Api;
using FIAP.PhaseOne.Application.Shared;
using FIAP.PhaseOne.Api.Middleware;

using FIAP.PhaseOne.Api.Config;
using Prometheus;
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddInfraServices(builder.Configuration);
builder.Services.AddApplicationService();
builder.Services.AddApiService();
builder.Services.AddMetricsConfig();

builder.Services.AddCors(policy =>
{
    policy.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
    });
});
var app = builder.Build();
app.MapPrometheusScrapingEndpoint();

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseMiddleware<ExceptionHandlerMiddleware>();

app.MapControllers();

app.UseDeveloperExceptionPage();

app.Services.CreateScope().ServiceProvider.UpdateMigrate();
app.UseSwagger();
app.UseSwaggerUI();

var httpRequestsByStatus = Metrics.CreateCounter("http_requests_by_status", 
    "Total de requisições HTTP por status de resposta", 
    new CounterConfiguration
    {
        LabelNames = new[] { "status_code" }
    });

app.Use(async (context, next) =>
{
    await next();

    // Obtenha o status da resposta
    var statusCode = context.Response.StatusCode.ToString();

    // Incremente o contador com o status da resposta
    httpRequestsByStatus.WithLabels(statusCode).Inc();
});

app.UseRouting();
app.Run();
