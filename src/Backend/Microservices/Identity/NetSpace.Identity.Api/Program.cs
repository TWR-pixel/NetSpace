using HealthChecks.UI.Client;
using Microsoft.AspNetCore.HttpOverrides;
using NetSpace.Identity.Api.Common;
using NetSpace.Identity.Application.Common.Extensions;
using NetSpace.Identity.Infrastructure.Common.Extensions;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
builder.ConfigureSerilog();

builder.Services.AddControllers();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins", builder =>
    {
        builder.AllowAnyOrigin();
        builder.AllowAnyHeader();
        builder.AllowAnyMethod();
    });
});

builder.Services.AddHealthCheck(builder.Configuration);
builder.Services.AddOpenApi();
builder.Services.ConfigureAuthenticationSwagger();
builder.Services.AddApplicationLayer();

builder.Services.AddInfrastructure(builder.Configuration);

builder.Services
    .ConfigureAuthentication(builder.Configuration)
    .ConfigureAuthorization();

var app = builder.Build();

app.UseSerilogRequestLogging();
app.UseCors("AllowAllOrigins");
app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
});

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.MapSwagger();
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.UseHealthChecks("/health", new Microsoft.AspNetCore.Diagnostics.HealthChecks.HealthCheckOptions
{
    Predicate = _ => true,
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});

app.MapControllers();

app.Run();
