using MassTransit;
using Microsoft.AspNetCore.HttpOverrides;
using NetSpace.EmailSender.Api.Common;
using NetSpace.EmailSender.Application;
using NetSpace.EmailSender.Application.Consumers;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.ConfigureSerilog();
builder.Services.AddControllers();
builder.Services.AddOpenApi();

builder.Services.Configure<EmailSenderOptions>(
        builder
            .Configuration
            .GetSection(nameof(EmailSenderOptions))
    );

builder.Services.AddMassTransit(configure =>
{
    configure.AddConsumers(typeof(SendEmailMessageConsumer).Assembly);
    configure.AddConsumer<SendEmailMessageConsumer>();

    configure.UsingRabbitMq((context, configurator) =>
    {
        configurator.Host(builder.Configuration.GetSection("RabbitMQ")["Host"], h =>
        {
            h.Username(builder.Configuration.GetSection("RabbitMQ")["UserName"] ?? "guest");
            h.Password(builder.Configuration.GetSection("RabbitMQ")["Password"] ?? "guest");
        });

        configurator.ConfigureEndpoints(context);
    });
});


builder.Services.AddScoped<IEmailSender, SmtpEmailSender>();

var app = builder.Build();

app.UseSerilogRequestLogging();
app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
});
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
