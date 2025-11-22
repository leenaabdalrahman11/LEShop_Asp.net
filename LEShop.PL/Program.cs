
using LEShop.DL.Data;
using Microsoft.AspNetCore.Localization;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Globalization;

namespace LEShop.PL
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();
            builder.Services.AddLocalization(options => options.ResourcesPath = "");
            //builder.Services.AddDbContext<ApplicationDbContext>(options =>
            //options.UseSqlServer(builder.Configuration["ConnectionStrings:DefaultConnection"]));

            //builder.Services.AddDbContext<ApplicationDbContext>(options =>
            //options.UseSqlServer(builder.Configuration.GetSection("ConnectionStrings")["DefaultConnection"]));
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            const string defaultCulture = "en";
            var supportedCultures = new[] {
                 new CultureInfo(defaultCulture),
                new CultureInfo("ar"),
            };
            builder.Services.Configure<RequestLocalizationOptions>(options =>
            {
                options.DefaultRequestCulture = new RequestCulture(defaultCulture);
                options.SupportedCultures = supportedCultures;
                options.SupportedUICultures = supportedCultures;
                options.RequestCultureProviders.Clear();
                options.RequestCultureProviders.Add(new QueryStringRequestCultureProvider
                {
                    QueryStringKey = "lang"
                });
            });
            builder.Services.AddSwaggerGen();

            var app = builder.Build();
            app.UseRequestLocalization(app.Services.GetRequiredService<IOptions<RequestLocalizationOptions>>().Value);
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {

                app.MapOpenApi();
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
