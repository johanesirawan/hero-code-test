using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SimpleBookingWidget.Services;
using SimpleBookingWidget.Sessions;
using System;

namespace SimpleBookingWidget
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDistributedMemoryCache();

            services.AddSession(options =>
            {
                options.Cookie.Name = "SimpleBookingWidget.Sessions";
                options.IdleTimeout = TimeSpan.FromHours(6);
                options.Cookie.IsEssential = true;
            });

            services.AddControllersWithViews();

            services.AddHttpContextAccessor();
            services.AddWebOptimizer(pipeline =>
            {
                pipeline.AddCssBundle("/lib/sweetalert2/sweetalert2.css", "node_modules/sweetalert2/dist/sweetalert2.css");
                pipeline.AddJavaScriptBundle("/lib/sweetalert2/sweetalert2.js", "node_modules/sweetalert2/dist/sweetalert2.all.js");
                pipeline.AddCssBundle("/lib/kendo-ui-core/kendo.ui.core.web.css", "lib/kendo-ui-core/css/web/*.css");
                pipeline.AddJavaScriptBundle("/lib/kendo-ui-core/kendo.ui.core.all.js", "lib/kendo-ui-core/js/kendo.ui.core.js");
                pipeline.AddCssBundle("/lib/intl-tel-input/intlTelInput.css", "node_modules/intl-tel-input/build/css/intlTelInput.min.css");
                pipeline.AddJavaScriptBundle("/lib/intl-tel-input/intlTelInput.js", "node_modules/intl-tel-input/build/js/intlTelInput.min.js", "node_modules/intl-tel-input/build/js/utils.js");
            });

            SetupIoC(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseWebOptimizer();

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseSession();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }

        private void SetupIoC(IServiceCollection services)
        {
            services.AddSingleton<IMySession, MySession>();
            services.AddSingleton<IPaxSession, MySession>();
            services.AddSingleton<IBookingSession, MySession>();

            #region Services
            services.AddScoped<IHeroApiService, HeroApiService>();
            services.AddTransient<IPaxService, PaxService>();
            services.AddTransient<IBookingService, BookingService>();

            #endregion Services
        }
    }
}
