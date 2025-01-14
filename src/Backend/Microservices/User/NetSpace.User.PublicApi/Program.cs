using NetSpace.User.Application.Common.Extensions;
using NetSpace.User.Infrastructure.Common.Extensions;
using NetSpace.User.PublicApi.Common;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
builder.ConfigureSerilog();

builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen();

var connectionString = builder.Configuration.GetConnectionString("PostgreSql");

builder.Services.AddApplicationLayer();
builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.ConfigureAuthorization();

var app = builder.Build();
app.UseSerilogRequestLogging();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
    app.MapSwagger();
}

app.UseHsts();
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
