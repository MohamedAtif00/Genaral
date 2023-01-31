using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MoviesSites.Areas.Identity.Data;
using MoviesSites.Data;
using MoviesSites.Models;

namespace MoviesSites
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
                        var connectionString = builder.Configuration.GetConnectionString("IdentityDbContextConnection") ?? throw new InvalidOperationException("Connection string 'IdentityDbContextConnection' not found.");

                builder.Services.AddDbContext<IdentityDbContext>(options =>
                options.UseSqlServer(connectionString));

            builder.Services.AddDbContext<MoviesDbContext>(options =>
            options.UseSqlServer(connectionString));


            builder.Services.AddDefaultIdentity<MoviesSitesUser>(options => options.SignIn.RequireConfirmedAccount = false)
                .AddEntityFrameworkStores<IdentityDbContext>();

            // Add services to the container.
            builder.Services.AddControllersWithViews();

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
                        app.UseAuthentication();;

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Movies}/{action=Index}/{id?}");

            app.Run();
        }
    }
}