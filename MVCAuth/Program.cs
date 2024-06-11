using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MVCAuth.Core.Services.Interface;
using MVCAuth.Core.Services;
using MVCAuth.Data;
using Microsoft.AspNetCore.Authentication.Cookies;


var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;


builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;

})
    //Google Authentication
    .AddGoogle(googleOptions =>
    {
        googleOptions.ClientId = builder.Configuration.GetSection("GoogleKeys:ClientId").Value;
        googleOptions.ClientSecret = builder.Configuration.GetSection("GoogleKeys:ClientSecret").Value;

    })
    //Facebook Authentication
    .AddFacebook(facebookOptions =>
    {
        facebookOptions.AppId = builder.Configuration["FacebookKeys:AppId"];
        facebookOptions.ClientSecret = builder.Configuration["FacebookKeys:ClientSecret"];
    })
    //GitHub Authentication
    .AddGitHub(options =>
    {
        options.ClientSecret = builder.Configuration["GithubKeys:ClientSecret"];
        options.ClientId = builder.Configuration["GithubKeys:ClientId"];
        options.CallbackPath = "/signin-oidc";
        options.Scope.Add("user:email");
    })
    //Twitter Authentication
    .AddTwitter(twitterOptions =>
    { 
        twitterOptions.ConsumerKey = builder.Configuration.GetSection("TwitterAuthSetting:ApiKey").Value;
        twitterOptions.ConsumerSecret = builder.Configuration.GetSection("TwitterAuthSetting:ApiSecret").Value;

        twitterOptions.CallbackPath = new PathString("/signin-twitter");
    });



var connectionString = builder.Configuration.GetConnectionString("MVCAuthContextConnection") 
    ?? throw new InvalidOperationException("Connection string 'MVCAuthContextConnection' not found.");

builder.Services.AddDbContext<MVCAuthContext>(options =>
       options.UseSqlServer(connectionString, b => b.MigrationsAssembly(typeof(MVCAuthContext).Assembly.FullName)));

builder.Services.AddDefaultIdentity<IdentityUser>(options =>
       options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<MVCAuthContext>();

// Add services to the container.
builder.Services.AddControllersWithViews()
    .AddJsonOptions(options =>
    {
        // A property naming policy, or null to leave property names unchanged.
        options.JsonSerializerOptions.PropertyNamingPolicy = null;
    });
builder.Services.AddScoped<IEmployeeService, EmployeeService>();    
builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();
app.MapControllers();

app.Run();
