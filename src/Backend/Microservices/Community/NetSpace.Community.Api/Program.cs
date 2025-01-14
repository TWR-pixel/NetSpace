using Microsoft.AspNetCore.HttpOverrides;
using NetSpace.Community.Api.Common;
using NetSpace.Community.Application.Common.Extensions;
using NetSpace.Community.Infrastructure.Common.Extensions;
using Prometheus;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.ConfigureSerilog();
builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen();

var connectionString = builder.Configuration.GetConnectionString("PostgreSql");

var redisInstanceName = builder.Configuration["RedisInstanceName"] ?? "";

builder.Services.AddApplicationLayer();
builder.Services.AddInfrastructure(connectionString, redisInstanceName, builder.Configuration);

var app = builder.Build();

app.UseSerilogRequestLogging();
app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
});

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapSwagger();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseHttpMetrics(options =>
{
    options.AddCustomLabel("host", context => context.Request.Host.Host);
});

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
