using FluentValidation;
using SharpGrip.FluentValidation.AutoValidation.Mvc.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;
using Serilog;
using YudaSPCWebApplication.BackendServer.Data;
using YudaSPCWebApplication.BackendServer.Data.Entities;
using YudaSPCWebApplication.BackendServer.IdentityServer;
using YudaSPCWebApplication.BackendServer.Services;
using YudaSPCWebApplication.ViewModels.System;
using Microsoft.OpenApi.Models;


Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .WriteTo.Console()
    .CreateLogger();
JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
var builder = WebApplication.CreateBuilder(args);
builder.Host.UseSerilog();

// Add DB context
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

//builder.Services.AddDefaultIdentity<User>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<ApplicationDbContext>();
// Add Identity
//builder.Services.AddIdentity<User, Role>()
//    .AddEntityFrameworkStores<ApplicationDbContext>()
//    .AddDefaultTokenProviders();

//builder.Services.AddDefaultIdentity<User>(options => options.SignIn.RequireConfirmedAccount = true)
//    .AddEntityFrameworkStores<ApplicationDbContext>();

// MVC / OpenAPI
builder.Services.AddControllers();
builder.Services.AddValidatorsFromAssemblyContaining<RoleValidator>();
//builder.Services.AddValidatorsFromAssemblyContaining<UserValidator>();
builder.Services.AddFluentValidationAutoValidation(configuration =>
{
    configuration.EnableBodyBindingSourceAutomaticValidation = true;
    configuration.EnableFormBindingSourceAutomaticValidation = true;
});

//builder.Services.AddOpenApi();
//builder.Services.AddEndpointsApiExplorer(); 

// Register DbInitializer (can remain Transient)
builder.Services.AddTransient<DbInitializer>();
builder.Services.AddTransient<IEmailSender, EmailSenderService>();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "Quality Management System API", Version = "v1" });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Type = SecuritySchemeType.OAuth2,  
        BearerFormat = "JWT",
        Flows = new OpenApiOAuthFlows
        {
            Implicit = new OpenApiOAuthFlow
            {
                AuthorizationUrl = new Uri("/connect/authorize", UriKind.Relative),
                TokenUrl = new Uri("/connect/token", UriKind.Relative),
                Scopes = new Dictionary<string, string>
                {
                    {"api.qms", "QMS API"}
                },
                
            }
        }
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            ["api.qms"] 
        }
    });

    //c.AddSecurityRequirement(doc => new OpenApiSecurityRequirement
    //{
    //    {
    //        new OpenApiSecuritySchemeReference("Bearer"),
    //        new List<string>{"api.qms"}
    //    }x
    //});
});
 

builder.Services.AddRazorPages(options => {
    _ = options.Conventions.AddAreaFolderRouteModelConvention("Identity", "/Account/", model =>
    {
        foreach (var selector in model.Selectors)
        {
            var attributeRouteModel = selector.AttributeRouteModel;
            attributeRouteModel?.Order = -1;
            _ = (attributeRouteModel?.Template = attributeRouteModel?.Template?["Identity".Length..]);
        }
    });
});

builder.Services.AddAuthentication(options => {
        options.DefaultAuthenticateScheme = "Bearer";
        options.DefaultChallengeScheme = "Bearer";
    })
    .AddLocalApi("Bearer", options =>
    {
        options.ExpectedScope = "api.qms";
    });


builder.Services.AddAuthorizationBuilder()
    .AddPolicy("Bearer", policy =>
    {
        _ = policy.AddAuthenticationSchemes("Bearer")
            .RequireAuthenticatedUser()
            .RequireAssertion(ctx =>
                {
                    var scopes = ctx.User.FindAll("scope").Select(c => c.Value);
                    return scopes.Any(s => s.Split(' ', StringSplitOptions.RemoveEmptyEntries)
                                             .Contains("api.qms"));
                })
           .Build();
    });

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSwaggerUI", policy =>
    {
        policy
            .WithOrigins(
                "https://localhost:7022"  
            )
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials();  
    });
});

// Register Identity once
builder.Services.AddIdentity<User, Role>(options => {
    // configure options if needed
})
.AddEntityFrameworkStores<ApplicationDbContext>()
.AddDefaultTokenProviders();

// IdentityServer configuration stays
builder.Services.AddIdentityServer(options =>
{
    options.Events.RaiseErrorEvents = true;
    options.Events.RaiseInformationEvents = true;
    options.Events.RaiseFailureEvents = true;
    options.Events.RaiseSuccessEvents = true;
    options.EmitStaticAudienceClaim = true;
})
.AddInMemoryApiResources(Config.ApiResources)
.AddInMemoryApiScopes(Config.ApiScopes)
.AddInMemoryClients(Config.Clients)
.AddInMemoryIdentityResources(Config.IdentityResources)
.AddAspNetIdentity<User>()
.AddDeveloperSigningCredential();


var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        Log.Information("Seeding database...");
        var dbInitializer = services.GetRequiredService<DbInitializer>();
        dbInitializer.Seed().Wait();
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred seeding the DB.");
    }
}

//// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.MapOpenApi();
//}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();


app.UseCors("AllowSwaggerUI");  

app.UseIdentityServer();
app.UseAuthentication();
app.UseAuthorization();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.OAuthClientId("swagger");
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Quality Management System API V1");
    c.OAuthScopes("api.qms");
    c.OAuthUsePkce();
});

app.UseEndpoints(endpoints => {
    _ = endpoints.MapDefaultControllerRoute();
    _ = endpoints.MapRazorPages();
});

app.Run();
