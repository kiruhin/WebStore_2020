using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WebStore_2020.Infrastructure;
using WebStore_2020.Infrastructure.Interfaces;
using WebStore_2020.Infrastructure.Services;

namespace WebStore_2020
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddRouting(options => options.LowercaseUrls = true);
            services.AddMvc(options =>
            {
                options.Filters.Add(typeof(SimpleActionFilter));

                // альтернативный вариант подключения
                //options.Filters.Add(new SimpleActionFilter());
            });

            // Добавляем разрешение зависимости
            services.AddSingleton<IEmployeesService, InMemoryEmployeeService>();
            //services.AddScoped<IEmployeesService, InMemoryEmployeeService>();
            //services.AddTransient<IEmployeesService, InMemoryEmployeeService>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();

            app.Map("/index", CustomIndexHandler);

            app.UseMiddleware<TokenMiddleware>();

            UseSampleErrorCheck(app);

            //ConfigV22(app, env);

            ConfigV31(app, env);

            app.UseWelcomePage("/welcome");
          
            RunSample(app);
        }

        private void UseSampleErrorCheck(IApplicationBuilder app)
        {
            app.Use(async (context, next) =>
            {
                bool isError = false;
                // ...
                if (isError)
                {
                    await context.Response
                        .WriteAsync("Error occured. You're in custom pipeline module...");
                }
                else
                {
                    await next.Invoke();
                }
            });
        }

        private void RunSample(IApplicationBuilder app)
        {
            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("Привет из конвейера обработки запроса (метод app.Run())");
            });

        }

        private void CustomIndexHandler(IApplicationBuilder app)
        {
            app.Run(async context =>
            {
                await context.Response.WriteAsync("Index");
            });

        }

        private void ConfigV31(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseRouting();

            var helloMsg = _configuration["CustomHelloWorld"];
            helloMsg = _configuration["Logging:LogLevel:Default"];

            //app.UseMvcWithDefaultRoute();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                              // GET: /<controller>/details/{id}

                //endpoints.MapGet("/", async context => { await context.Response.WriteAsync(helloMsg); });
            });
        }

        private void ConfigV22(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // Производим конфигурацию инфраструктуры MVC
            app.UseMvc(routes =>
            {
                // Добавляем обработчик маршрута по умолчанию
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
                // Маршрут по умолчанию состоит из трёх частей разделённых “/”
                // Первой частью указывается имя контроллера,
                // второй - имя действия (метода) в контроллере,
                // третей - опциональный параметр с именем “id”
                // Если часть не указана - используются значения по умолчанию:
                // для контроллера имя “Home”,
                // для действия - “Index”
            });

        }
    }
}
