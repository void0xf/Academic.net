using Microsoft.EntityFrameworkCore;
using WebAppActions.Models;
using WebAppActions.Models.Repositories;
using WebAppActions.Models.Services;
using AutoMapper;
using Microsoft.AspNetCore.Identity;

namespace WebAppActions
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllersWithViews();
            builder.Services.AddTransient<IBookRepository, BookRepository>();
            builder.Services.AddTransient<IBookService, BookService>();

            builder.Services.AddAutoMapper(typeof(Program));

            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseLazyLoadingProxies().UseSqlServer(
                    builder.Configuration.GetConnectionString("DefaultConnection")));

                    
            builder.Services.AddDefaultIdentity<User>(options => options.SignIn.RequireConfirmedAccount = true)
            .AddEntityFrameworkStores<ApplicationDbContext>();

            var app = builder.Build();

            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            
            app.MapRazorPages();

            app.Run();
        }
    }
}
