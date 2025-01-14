using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.IdentityModel.Tokens;
using NetSpace.ApiGateway.Common;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Serilog;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.ConfigureSerilog();

builder.Configuration
    .SetBasePath(builder.Environment.ContentRootPath)
    .AddJsonFile("ocelot.json", false, true)
    .AddJsonFile("ocelot.production.json", false, true);

builder.Services.AddControllers();
builder.Services.AddOcelot();
builder.Services.AddOpenApi();

builder.Services
     .AddAuthentication(sharedOptions =>
     {
         sharedOptions.DefaultScheme = "dynamic";
         sharedOptions.DefaultChallengeScheme = "dynamic";
     })
     .AddPolicyScheme("dynamic", "OpenId by cookies or JWT", options =>
     {
         options.ForwardDefaultSelector = ctx =>
         {
             string? authHeader = ctx.Request.Headers.Authorization.FirstOrDefault();

             if (authHeader is null)
                 return OpenIdConnectDefaults.AuthenticationScheme;

             return JwtBearerDefaults.AuthenticationScheme;
         };
     })
     .AddJwtBearer(options =>
     {
         options.SaveToken = true;
         options.RequireHttpsMetadata = false;
         options.TokenValidationParameters = new TokenValidationParameters()
         {
             ValidateIssuer = true,
             ValidateAudience = true,
             ValidAudience = builder.Configuration["JwtAuth:ValidAudience"],
             ValidIssuer = builder.Configuration["JwtAuth:ValidIssuer"],
             ClockSkew = TimeSpan.Zero,
             IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtAuth:Secret"]))
         };
     })
     .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
     {
     })
     .AddOpenIdConnect(OpenIdConnectDefaults.AuthenticationScheme, options =>
    {
        options.NonceCookie.SecurePolicy = CookieSecurePolicy.Always;
        options.CorrelationCookie.SecurePolicy = CookieSecurePolicy.Always;
        options.SignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
        options.GetClaimsFromUserInfoEndpoint = true;
        options.AuthenticationMethod = OpenIdConnectRedirectBehavior.RedirectGet;

        options.ResponseMode = OpenIdConnectResponseMode.FormPost;

        options.Authority = "https://accounts.google.com";
        options.ClientId = builder.Configuration["Google:ClientId"];
        options.ClientSecret = builder.Configuration["Google:Secret"];

        options.ResponseType = OpenIdConnectResponseType.Code;
        options.UsePkce = true;
        options.CallbackPath = builder.Configuration["Google:CallbackPath"];
        options.SaveTokens = true;
        options.GetClaimsFromUserInfoEndpoint = true;

        options.Scope.Add("openid");
        options.Scope.Add("email");
        options.Scope.Add("phone");
        options.Scope.Add("profile");

        options.Events = new OpenIdConnectEvents
        {
            OnRedirectToIdentityProviderForSignOut = context =>
            {
                context.Response.Redirect(builder.Configuration["Google:RedirectOnSignOut"]);
                context.HandleResponse();

                return Task.CompletedTask;
            },

            OnRemoteFailure = context =>
            {
                context.Response.Redirect("/error");
                context.HandleResponse();
                return Task.FromResult(0);
            },
        };
    });

var app = builder.Build();
app.UseSerilogRequestLogging();

app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
});

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHsts();
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.MapGet("/check", () =>
{
    return "Ok";
});


await app.UseOcelot();

app.Run();