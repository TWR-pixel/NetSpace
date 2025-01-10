using Microsoft.AspNetCore.HttpOverrides;
using NetSpace.Community.Application.Common.Extensions;
using NetSpace.Community.Infrastructure.Common.Extensions;
using Prometheus;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen();

var connectionString = builder.Configuration.GetConnectionString("PostgreSql");

var redisInstanceName = builder.Configuration["RedisInstanceName"] ?? "";
var redisConnectionString = builder.Configuration.GetConnectionString("Redis") ?? "";

builder.Services.AddApplicationLayer();
builder.Services.AddInfrastructure(connectionString, redisInstanceName, redisConnectionString);

var app = builder.Build();
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
