using BasketAPI.Controllers;
using BasketAPI.Interfaces;
using BasketAPI.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BasketAPI
{
    public class InMemoryBasketRepository : IBasketRepository
    {
        private readonly List<Basket> _baskets = new List<Basket>();

        public Basket FindById(Guid basketId)
        {
            return _baskets.SingleOrDefault(b => b.Id == basketId);
        }

        public Basket Add(Guid ownerId)
        {
            var basket = new Basket(Guid.NewGuid(), ownerId);
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
            services.AddSingleton<InMemoryBasketRepository>();
            services.AddScoped<IBasketRepository>(s => s.GetService<InMemoryBasketRepository>());
            services.AddSwaggerGen(c => { c.SwaggerDoc("v1", new Info() {Title = "BasketAPI", Version = "v1"}); });
            services.AddScoped(s => new TokenGenerator(Configuration["Auth:TokenSecret"]));

            ConfigureAuthentication(services);
        }

        public void ConfigureAuthentication(IServiceCollection services)
        {
            var tokenSecret = Encoding.ASCII.GetBytes(Configuration["Auth:TokenSecret"]);

            services.AddAuthentication(c =>
                {
                    c.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    c.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(c =>
                {
                    c.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(tokenSecret),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });
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

            app.UseSwagger();
            app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "BasketAPI v1"); });

            app.UseAuthentication();

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}