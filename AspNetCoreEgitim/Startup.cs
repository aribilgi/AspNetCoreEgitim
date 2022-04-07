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
    public class Startup // Bu class projenin genel iþlevlerinin yer aldýðý classtýr
    {
        public Startup(IConfiguration configuration) // Bu metottaki parametre ve içindeki eþitleme DI yani dependency injection kullanýlarak yapýlýyor.
        // Dependency Inversion : Baðýmlýlýklarýn tersine çevrilmesi prensibi
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; } // Projenin temel ayarlarýnýn yapýlandýrýldýðý nesne, bu nesne new lenmeden DI yöntemiyle yukardaki kurucu metotta dolduruluyor

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services) // .net core da iþlemler servisler üzerinden yürütülüyor. Bu metot bu servislerin ayarlandýðý metot.
        {
            // Burada hangi servisleri kullanacaksak bunlarý aþaðýdaki gibi services. diyerek eklememiz gerekiyor.Bu sayede gereksiz servisler kullanýlmayacaðý için uygulama performansý artýyor.
            services.AddControllersWithViews(); // Bu satýr uygulamada controller ve view yapýsýnýn kullanýlabilmesini saðlar, aksi halde Mvc sistemi çalýþmaz.
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {   // Burasý uygulamanýn genel ayarlarýnýn yapýlandýrýldýðý metot
            if (env.IsDevelopment()) // uygulamanýn çalýþtýðý ortamýn geliþtirme ortamý olduðunu ayarlar. Isproduction() ise uygulamayý canlýda çalýþtýracaðýmýz zaman kullanýlýyor
            {
                app.UseDeveloperExceptionPage(); // Eðer uygulama geliþtirme ortamýnda çalýþýyorsa hatalar detaylý hata sayfasý olarak gösterilsin
            }
            else // Eðer projeyi canlýya aldýysak
            {
                app.UseExceptionHandler("/Home/Error"); // Bu adresteki hata sayfasýný kullanýcýlara göster
                app.UseHsts();
            }
            app.UseHttpsRedirection(); // uygulama https ye yönlendirme kullansýn
            app.UseStaticFiles(); // uygulamada wwwroot da css, js vb statik dosyalar kullanýlabilsin

            app.UseRouting(); // uygulama routing kullansýn

            app.UseAuthorization();  // uygulama oturum açmayý desteklesin

            app.UseEndpoints(endpoints =>
            {
                // uygulamada kullanýlacak url routing mekanizmasý. Önceki Mvc yapýsýndaki route config sýnýfýndaki ayarlar artýk burada
                endpoints.MapControllerRoute(
                    name: "default", // Varsayýlan standart Url Routing yapýsý aþaðýdaki
                    pattern: "{controller=Home}/{action=Index}/{id?}"); // Eðer adres çubuðundan herhangi bir controller yazýlmamýþsa Home controller ý, action yazýlmamýþsa Index actionunu çalýþtýr
            });
        }
    }
}
