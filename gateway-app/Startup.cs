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
            services.AddControllers();
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
                context.Gateways.AddRange(new List<Gateway>()
                {
                    new Gateway() {Name = "CISCO 01", SerialNumber = Guid.NewGuid().ToString(), Ipv4 = "10.0.0.1"},
                    new Gateway() {Name = "CISCO 02", SerialNumber = Guid.NewGuid().ToString(), Ipv4 = "10.0.0.2"},
                    new Gateway() {Name = "CISCO 03", SerialNumber = Guid.NewGuid().ToString(), Ipv4 = "10.0.0.3"},
                    new Gateway() {Name = "CISCO 04", SerialNumber = Guid.NewGuid().ToString(), Ipv4 = "10.0.0.4"},
                    new Gateway() {Name = "CISCO 05", SerialNumber = Guid.NewGuid().ToString(), Ipv4 = "10.0.0.5"}
                });
            }

            context.SaveChanges();
        }
    }
}
