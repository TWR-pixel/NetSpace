using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.IdentityModel.Tokens;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration
    .SetBasePath(builder.Environment.ContentRootPath)
    .AddJsonFile("ocelot.json", false, true);

builder.Services.AddControllers();
builder.Services.AddOcelot();
builder.Services.AddOpenApi();

//builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
//    .AddJwtBearer(options =>
//    {
//options.TokenValidationParameters = new TokenValidationParameters
//{
//    ValidateIssuer = true,
//    ValidIssuer = "AuthOptions.ISSUER",
//    ValidateAudience = true,
//    ValidAudience = "AuthOptions.AUDIENCE",
//    ValidateLifetime = true,
//    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("string")),
//    ValidateIssuerSigningKey = true
//};
//    });

builder.Services
     .AddAuthentication(sharedOptions =>
     {
         sharedOptions.DefaultScheme = "dynamic";
         sharedOptions.DefaultChallengeScheme = "dynamic";
     })
     .AddPolicyScheme("dynamic", "Cookie or JWT", options =>
     {
         options.ForwardDefaultSelector = ctx =>
         {
             string? authHeader = ctx.Request.Headers.Authorization.FirstOrDefault();

             if (authHeader is null)
                 return "Google";

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
             ValidAudience = builder.Configuration["JWT:ValidAudience"],
             ValidIssuer = builder.Configuration["JWT:ValidIssuer"],
             ClockSkew = TimeSpan.Zero,
             IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Secret"]))
         };
     })
     .AddCookie("Google", options =>
     {
     })
    .AddOpenIdConnect(options =>
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
//builder.Services.AddAuthentication(x =>
//{
//})
//            .AddJwtBearer(x =>
//            {
//                x.RequireHttpsMetadata = false;
//                x.SaveToken = true;
//                x.TokenValidationParameters = new TokenValidationParameters
//                {
//                    ValidateIssuerSigningKey = true,
//                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("string")),
//                    ValidateIssuer = false,
//                    ValidateAudience = false
//                };
//            });


//builder.Services.AddAuthentication(options =>
//{
//    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
//    options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
//})
//.AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
//{
//    options.Cookie.Name = "oidc";
//    options.Cookie.SameSite = SameSiteMode.None;
//    options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
//    options.Cookie.IsEssential = true;
//})
//.AddOpenIdConnect(options =>
//{
//    options.NonceCookie.SecurePolicy = CookieSecurePolicy.Always;
//    options.CorrelationCookie.SecurePolicy = CookieSecurePolicy.Always;
//    options.SignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
//    options.GetClaimsFromUserInfoEndpoint = true;
//    options.AuthenticationMethod = OpenIdConnectRedirectBehavior.RedirectGet;

//    options.ResponseMode = OpenIdConnectResponseMode.FormPost;

//    options.Authority = "https://accounts.google.com";
//    options.ClientId = builder.Configuration["Google:ClientId"];
//    options.ClientSecret = builder.Configuration["Google:Secret"];

//    options.ResponseType = OpenIdConnectResponseType.Code;
//    options.UsePkce = true;
//    options.CallbackPath = builder.Configuration["Google:CallbackPath"];
//    options.SaveTokens = true;
//    options.GetClaimsFromUserInfoEndpoint = true;

//    options.Scope.Add("openid");
//    options.Scope.Add("email");
//    options.Scope.Add("phone");
//    options.Scope.Add("profile");

//    options.Events = new OpenIdConnectEvents
//    {
//        OnRedirectToIdentityProviderForSignOut = context =>
//        {
//            context.Response.Redirect(builder.Configuration["Google:RedirectOnSignOut"]);
//            context.HandleResponse();

//            return Task.CompletedTask;
//        },

//        OnRemoteFailure = context =>
//        {
//            context.Response.Redirect("/error");
//            context.HandleResponse();
//            return Task.FromResult(0);
//        },
//    };
//});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHsts();
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

await app.UseOcelot();

app.Run();