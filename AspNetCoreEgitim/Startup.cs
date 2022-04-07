using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreEgitim
{
    public class Startup // Bu class projenin genel i�levlerinin yer ald��� classt�r
    {
        public Startup(IConfiguration configuration) // Bu metottaki parametre ve i�indeki e�itleme DI yani dependency injection kullan�larak yap�l�yor.
        // Dependency Inversion : Ba��ml�l�klar�n tersine �evrilmesi prensibi
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; } // Projenin temel ayarlar�n�n yap�land�r�ld��� nesne, bu nesne new lenmeden DI y�ntemiyle yukardaki kurucu metotta dolduruluyor

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services) // .net core da i�lemler servisler �zerinden y�r�t�l�yor. Bu metot bu servislerin ayarland��� metot.
        {
            // Burada hangi servisleri kullanacaksak bunlar� a�a��daki gibi services. diyerek eklememiz gerekiyor.Bu sayede gereksiz servisler kullan�lmayaca�� i�in uygulama performans� art�yor.
            services.AddControllersWithViews(); // Bu sat�r uygulamada controller ve view yap�s�n�n kullan�labilmesini sa�lar, aksi halde Mvc sistemi �al��maz.
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {   // Buras� uygulaman�n genel ayarlar�n�n yap�land�r�ld��� metot
            if (env.IsDevelopment()) // uygulaman�n �al��t��� ortam�n geli�tirme ortam� oldu�unu ayarlar. Isproduction() ise uygulamay� canl�da �al��t�raca��m�z zaman kullan�l�yor
            {
                app.UseDeveloperExceptionPage(); // E�er uygulama geli�tirme ortam�nda �al���yorsa hatalar detayl� hata sayfas� olarak g�sterilsin
            }
            else // E�er projeyi canl�ya ald�ysak
            {
                app.UseExceptionHandler("/Home/Error"); // Bu adresteki hata sayfas�n� kullan�c�lara g�ster
                app.UseHsts();
            }
            app.UseHttpsRedirection(); // uygulama https ye y�nlendirme kullans�n
            app.UseStaticFiles(); // uygulamada wwwroot da css, js vb statik dosyalar kullan�labilsin

            app.UseRouting(); // uygulama routing kullans�n

            app.UseAuthorization();  // uygulama oturum a�may� desteklesin

            app.UseEndpoints(endpoints =>
            {
                // uygulamada kullan�lacak url routing mekanizmas�. �nceki Mvc yap�s�ndaki route config s�n�f�ndaki ayarlar art�k burada
                endpoints.MapControllerRoute(
                    name: "default", // Varsay�lan standart Url Routing yap�s� a�a��daki
                    pattern: "{controller=Home}/{action=Index}/{id?}"); // E�er adres �ubu�undan herhangi bir controller yaz�lmam��sa Home controller �, action yaz�lmam��sa Index actionunu �al��t�r
            });
        }
    }
}
