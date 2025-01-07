using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen();
builder.Services.AddAuthentication();

var authenticationBuilder = builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
});

authenticationBuilder
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
