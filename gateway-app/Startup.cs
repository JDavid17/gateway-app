using gateway_app.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gateway_app
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
            services.AddDbContext<GatewayAppContext>(options => options.UseInMemoryDatabase("GatewayApp"));
            services.AddControllers().AddNewtonsoftJson(options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, GatewayAppContext context)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            if (!context.Gateways.Any())
            {
                Random rnd = new Random();

                context.Gateways.AddRange(new List<Gateway>()
                {
                    new Gateway() {Name = "CISCO 01", SerialNumber = "1234567", Ipv4 = "10.6.100.66", 
                        Peripherals = new List<Peripheral>() 
                        {
                            new Peripheral() {Date = DateTime.UtcNow, Status = true, Vendor = "Logitec", UID = rnd.Next(100000, 1000000)},
                            new Peripheral() {Date = DateTime.UtcNow, Status = true, Vendor = "Dell", UID = rnd.Next(100000, 1000000)},
                            new Peripheral() {Date = DateTime.UtcNow, Status = false, Vendor = "Asus", UID = rnd.Next(100000, 1000000)}
                        } },
                    new Gateway() {Name = "CISCO 02", SerialNumber = "1234565432", Ipv4 = "10.6.100.67"},
                    new Gateway() {Name = "CISCO 03", SerialNumber = "12345654321", Ipv4 = "10.6.100.68"},
                    new Gateway() {Name = "CISCO 04", SerialNumber = "123454321", Ipv4 = "10.6.100.69"},
                    new Gateway() {Name = "CISCO 05", SerialNumber = "123456765432", Ipv4 = "10.6.100.670"}
                });
            }

            context.SaveChanges();
        }
    }
}
