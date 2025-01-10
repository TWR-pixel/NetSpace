using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using NetSpace.Identity.Api.Common;
using NetSpace.Identity.Application.Common.Extensions;
using NetSpace.Identity.Application.User;
using NetSpace.Identity.Infrastructure.Common.Extensions;
using System.Security.Claims;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

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

builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "NetSpace api",
        Version = "v1"
    });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please insert JWT with Bearer into field",
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement {
    {
        new OpenApiSecurityScheme
        {
            Reference = new OpenApiReference
            {
                Type = ReferenceType.SecurityScheme,
                Id = "Bearer"
            }
        },
        Array.Empty<string>()
    }
    });
});
builder.Services.AddApplicationLayer();

var connectionString = builder.Configuration.GetConnectionString("PostgreSql");
builder.Services.AddInfrastructure(connectionString);

builder.Services.AddAuthentication()
            .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
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
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtAuth:Secret"] ?? "default"))
                };
            });

builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
})
.AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
 {
     options.Cookie.Name = "oidc";
     options.Cookie.SameSite = SameSiteMode.None;
     options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
     options.Cookie.IsEssential = true;
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
            context.Response.Redirect(builder.Configuration["Google:RedirectOnSignOut"] ?? "default");
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
builder.Services.AddAuthorizationBuilder()
    .AddPolicy(AuthConstants.AdminPolicy, policy =>
    {
        policy.RequireClaim(ClaimTypes.Role, UserRoles.Admin);
    });

var app = builder.Build();
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

app.MapControllers();

app.Run();
