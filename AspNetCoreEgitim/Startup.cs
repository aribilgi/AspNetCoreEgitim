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
    public class Startup // Bu class projenin genel işlevlerinin yer aldığı classtır
    {
        public Startup(IConfiguration configuration) // Bu metottaki parametre ve içindeki eşitleme DI yani dependency injection kullanılarak yapılıyor.
        // Dependency Inversion : Bağımlılıkların tersine çevrilmesi prensibi
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; } // Projenin temel ayarlarının yapılandırıldığı nesne, bu nesne new lenmeden DI yöntemiyle yukardaki kurucu metotta dolduruluyor

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services) // .net core da işlemler servisler üzerinden yürütülüyor. Bu metot bu servislerin ayarlandığı metot.
        {
            // Burada hangi servisleri kullanacaksak bunları aşağıdaki gibi services. diyerek eklememiz gerekiyor.Bu sayede gereksiz servisler kullanılmayacağı için uygulama performansı artıyor.
            services.AddControllersWithViews(); // Bu satır uygulamada controller ve view yapısının kullanılabilmesini sağlar, aksi halde Mvc sistemi çalışmaz.
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {   // Burası uygulamanın genel ayarlarının yapılandırıldığı metot
            if (env.IsDevelopment()) // uygulamanın çalıştığı ortamın geliştirme ortamı olduğunu ayarlar. Isproduction() ise uygulamayı canlıda çalıştıracağımız zaman kullanılıyor
            {
                app.UseDeveloperExceptionPage(); // Eğer uygulama geliştirme ortamında çalışıyorsa hatalar detaylı hata sayfası olarak gösterilsin
            }
            else // Eğer projeyi canlıya aldıysak
            {
                app.UseExceptionHandler("/Home/Error"); // Bu adresteki hata sayfasını kullanıcılara göster
                app.UseHsts();
            }
            app.UseHttpsRedirection(); // uygulama https ye yönlendirme kullansın
            app.UseStaticFiles(); // uygulamada wwwroot da css, js vb statik dosyalar kullanılabilsin

            app.UseRouting(); // uygulama routing kullansın

            app.UseAuthorization();  // uygulama oturum açmayı desteklesin

            app.UseEndpoints(endpoints =>
            {
                // uygulamada kullanılacak url routing mekanizması. Önceki Mvc yapısındaki route config sınıfındaki ayarlar artık burada
                endpoints.MapControllerRoute(
                    name: "default", // Varsayılan standart Url Routing yapısı aşağıdaki
                    pattern: "{controller=Home}/{action=Index}/{id?}"); // Eğer adres çubuğundan herhangi bir controller yazılmamışsa Home controller ı, action yazılmamışsa Index actionunu çalıştır
            });
        }
    }
}
