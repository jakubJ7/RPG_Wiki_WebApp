using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RPG_Wiki_WebApp.Data;
using RPG_Wiki_WebApp.Models;
using Microsoft.AspNetCore.Identity;
using RPG_Wiki_WebApp.Models.Chat;
namespace RPG_Wiki_WebApp
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddDbContext<RPG_Wiki_WebAppContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("RPG_Wiki_WebAppContext") ?? throw new InvalidOperationException("Connection string 'RPG_Wiki_WebAppContext' not found.")));

            builder.Services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/Account/Login";
                options.AccessDeniedPath = "/Account/AccessDenied";
            });

            builder.Services.AddDbContext<RPG_Wiki_WebAppContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("RPG_Wiki_WebAppContext")));

            // Identity
            builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<RPG_Wiki_WebAppContext>()
                .AddDefaultTokenProviders();

            // dla tesów 
            builder.Services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 4;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
            });


            // Add services to the container.
            builder.Services.AddControllersWithViews();

            // SignalR
            builder.Services.AddSignalR();

            var app = builder.Build();

            // Dane
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                SeedData.Initialize(services);
            }

            // Roles
            using (var scope = app.Services.CreateScope())
            {
                await SeedData.SeedRolesAsync(scope.ServiceProvider);
            }

            

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

            app.UseAuthentication(); // authent
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHub<ChatHub>("/chatHub"); // Mapa Hubu
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });

            app.Run();
        }
    }
}
