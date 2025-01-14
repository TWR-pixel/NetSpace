using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using NetSpace.Identity.Application.User;
using System.Security.Claims;
using System.Text;

namespace NetSpace.Identity.Api.Common;

public static class AuthServiceCollectionExtensions
{
    public static IServiceCollection ConfigureAuthenticationSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen(c =>
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
        return services;
    }

    public static IServiceCollection ConfigureAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAuthentication()
            .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidAudience = configuration["JwtAuth:ValidAudience"],
                    ValidIssuer = configuration["JwtAuth:ValidIssuer"],
                    ClockSkew = TimeSpan.Zero,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtAuth:Secret"] ?? "default"))
                };
            });

        services.AddAuthentication(options =>
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
            options.ClientId = configuration["Google:ClientId"];
            options.ClientSecret = configuration["Google:Secret"];

            options.ResponseType = OpenIdConnectResponseType.Code;
            options.UsePkce = true;
            options.CallbackPath = configuration["Google:CallbackPath"];
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
                    context.Response.Redirect(configuration["Google:RedirectOnSignOut"] ?? "default");
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



        return services;
    }

    public static IServiceCollection ConfigureAuthorization(this IServiceCollection services)
    {
        services.AddAuthorizationBuilder()
            .AddPolicy(AuthConstants.AdminPolicy, policy =>
            {
                policy.RequireClaim(ClaimTypes.Role, UserRoles.Admin);
                policy.RequireRole(UserRoles.Admin);
                policy.RequireAuthenticatedUser();
            })
            .AddPolicy(AuthConstants.ModeratorPolicy, policy =>
            {
                policy.RequireClaim(ClaimTypes.Role, UserRoles.Moderator);
                policy.RequireRole(UserRoles.Moderator);
                policy.RequireAuthenticatedUser();
            });


        return services;
    }
}
