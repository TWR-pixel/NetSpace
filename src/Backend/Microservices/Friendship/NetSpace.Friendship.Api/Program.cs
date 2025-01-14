using Microsoft.AspNetCore.HttpOverrides;
using NetSpace.Friendship.Api.Common;
using NetSpace.Friendship.Application.Common.Extensions;
using NetSpace.Friendship.Infrastructure;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
builder.ConfigureSerilog();

builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

builder.Services.AddOpenApi();

builder.Services.AddApplicationServices(builder.Configuration);
builder.Services.AddInfrastructure(builder.Configuration.GetSection("Neo4j"));

var app = builder.Build();

app.UseSerilogRequestLogging();
app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.MapSwagger();
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
