// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT License.

using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.Identity.Web;
using Microsoft.Identity.Web.UI;

namespace Orleans.ShoppingCart.Silo;

public sealed class Startup
{
    private readonly IConfiguration _configuration;
    private readonly IWebHostEnvironment _environment;

    public Startup(IConfiguration configuration, IWebHostEnvironment environment)
    {
        _configuration = configuration;
        _environment = environment;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddMudServices();

        // Only add authentication if not in development or if explicitly configured
        var enableAuth = !_environment.IsDevelopment() || 
                        _configuration.GetValue<bool>("EnableAuthentication", false);

        if (enableAuth)
        {
            services.AddAuthentication(OpenIdConnectDefaults.AuthenticationScheme)
                    .AddMicrosoftIdentityWebApp(
                options =>
                {
                    _configuration.Bind("AzureAdB2C", options);

                    static void ConfigCookies(params CookieBuilder[] cookies)
                    {
                        foreach (var cookie in cookies)
                        {
                            cookie.SameSite = SameSiteMode.None;
                            cookie.SecurePolicy = CookieSecurePolicy.Always;
                            cookie.IsEssential = true;
                        }
                    }

                    ConfigCookies(options.NonceCookie, options.CorrelationCookie);
                },
                subscribeToOpenIdConnectMiddlewareDiagnosticsEvents: true);

            services.AddControllersWithViews()
                .AddMicrosoftIdentityUI();
        }
        else
        {
            // Add minimal authentication for development
            services.AddAuthentication("Cookies")
                .AddCookie("Cookies");
            
            services.AddControllersWithViews();
        }

        services.AddRazorPages();
        services.AddServerSideBlazor();

        services.AddHttpContextAccessor();
        services.AddSingleton<ShoppingCartService>();
        services.AddSingleton<InventoryService>();
        services.AddSingleton<ProductService>();
        services.AddScoped<ComponentStateChangedObserver>();
        services.AddSingleton<ToastService>();
        services.AddLocalStorageServices();
        services.AddApplicationInsights("Silo");
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        app.UseForwardedHeaders();
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        else
        {
            app.UseExceptionHandler("/Error");
            app.UseHsts();
        }

        var enableAuth = !env.IsDevelopment() || 
                        _configuration.GetValue<bool>("EnableAuthentication", false);

        if (enableAuth)
        {
            app.UseRewriter(new RewriteOptions().Add(context =>
            {
                if (context.HttpContext.Request.Path == "/MicrosoftIdentity/Account/SignedOut")
                {
                    var host = context.HttpContext.Request.Host;
                    var url = $"{context.HttpContext.Request.Scheme}://{host}";
                    context.HttpContext.Response.Redirect(url);
                }
            }));
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();
        app.UseRouting();
        app.UseAuthentication();
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
            endpoints.MapBlazorHub();
            endpoints.MapFallbackToPage("/_Host");
        });
    }
}