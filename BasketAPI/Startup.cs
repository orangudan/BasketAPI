using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BasketAPI.Controllers;
using BasketAPI.Interfaces;
using BasketAPI.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace BasketAPI
{

    public class InMemoryBaskets : IBasketRepository
    {
        private readonly List<Basket> _baskets = new List<Basket>();

        public Basket FindById(Guid id)
        {
            return _baskets.SingleOrDefault(b => b.Id == id);
        }

        public Basket Add()
        {
            var basket = new Basket { Id = Guid.NewGuid() };
            _baskets.Add(basket);
            return basket;
        }

        public void Remove(Basket basket)
        {
            _baskets.Remove(basket);
        }
    }

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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddSingleton<InMemoryBaskets>();
            services.AddScoped<IBasketRepository>(s => s.GetService<InMemoryBaskets>());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
