using Databaze_tabulky;

namespace Projekt_csharp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            var connectionString = "Data Source=C:\\Users\\judis\\AppData\\Local\\prihlasky.db;";
            //builder.Configuration.GetConnectionString("DefaultConnection");
            string assebly_path = "C:\\Users\\judis\\source\\repos\\Projekt_csharp\\Databaze_tabulky\\bin\\Debug\\net8.0\\Databaze_tabulky.dll";
            builder.Services.AddSingleton(new MyORM(connectionString,assebly_path));

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}