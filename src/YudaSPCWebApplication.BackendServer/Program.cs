using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Serilog;
using YudaSPCWebApplication.BackendServer.Data;
using YudaSPCWebApplication.BackendServer.Data.Entities;
using FluentValidation.AspNetCore;
using YudaSPCWebApplication.ViewModels.System;
using FluentValidation;
using YudaSPCWebApplication.BackendServer.IdentityServer;
using YudaSPCWebApplication.BackendServer.Services;
using Microsoft.AspNetCore.Identity.UI.Services;

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .WriteTo.Console()
    .CreateLogger();

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
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddFluentValidationClientsideAdapters();
builder.Services.AddValidatorsFromAssemblyContaining<RoleVmValidator>();

builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer(); 
builder.Services.AddSwaggerGen(); 

// Register DbInitializer (can remain Transient)
builder.Services.AddTransient<DbInitializer>();
builder.Services.AddTransient<IEmailSender, EmailSenderSevice>();

builder.Services.AddRazorPages();

//builder.Services.AddAuthentication()
//        .AddCookie(IdentityConstants.ApplicationScheme);


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
    //options.UserInteraction.LoginUrl = "/Account/Login";
    //options.UserInteraction.LogoutUrl = "/Account/Logout";
    //options.UserInteraction.ErrorUrl = "/Home/Error";
    //options.Authentication = new AuthenticationOptions
    //{
    //    CookieLifetime = TimeSpan.FromHours(10),
    //    CookieSlidingExpiration = true,
    //};
})
.AddInMemoryApiResources(Config.ApiResources)
.AddInMemoryClients(Config.Clients)
.AddInMemoryIdentityResources(Config.IdentityResources)
.AddAspNetIdentity<User>();


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

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseIdentityServer();
app.UseAuthorization();

app.UseSwagger();
app.UseSwaggerUI(c => {
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Quality Management System API V1");
});

app.UseEndpoints(endpoints => {
    _ = endpoints.MapDefaultControllerRoute();
    _ = endpoints.MapRazorPages();
});

app.Run();
