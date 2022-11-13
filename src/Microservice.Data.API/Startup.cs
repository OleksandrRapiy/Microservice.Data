using IdentityServer4.AccessTokenValidation;
using MediatR;
using Microservice.Data.Application.Interfaces;
using Microservice.Data.Infrastructure.Repositories;
using Microservice.Data.Persistence.Context;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Microservice.Data.API
{
    public class Startup
    {
        public Startup(IWebHostEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();

            Configuration = builder.Build();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var paths = Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory, "*Application.dll").ToList();
            paths.AddRange(Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory, "*Infrastructure.dll").ToList());

            var assemblies = paths.Select(path => Assembly.Load(AssemblyName.GetAssemblyName(path))).ToArray();

            services.AddControllers();

            var postgreSqlDbConnection = Configuration.GetSection("DbConnection").Value;
            if (postgreSqlDbConnection is null) throw new ArgumentNullException(nameof(postgreSqlDbConnection));

            services.AddDbContext<ApplicationContext>(config =>
            {
                config.UseNpgsql(postgreSqlDbConnection, options => options.EnableRetryOnFailure());
            });

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddMediatR(assemblies);

            services.AddAutoMapper(assemblies);

            services.AddRouting(options => options.LowercaseUrls = true);


            #region Auth
            services.AddAuthentication(options =>
             {
                 options.DefaultScheme = IdentityServerAuthenticationDefaults.AuthenticationScheme;
                 options.DefaultAuthenticateScheme = IdentityServerAuthenticationDefaults.AuthenticationScheme;
                 options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
             })
            .AddIdentityServerAuthentication(options =>
            {
                options.Authority = Configuration["Identity:Authority"];
                options.ApiName = Configuration["Identity:ApiName"];
                options.TokenRetriever = request =>
                {
                    request.Cookies.TryGetValue("access_token", out var cookiesToken);
                    if (request.Headers.TryGetValue("Authorization", out var headerToken))
                        headerToken = headerToken.ToString().Split(' ').Last();

                    var resp = cookiesToken ?? headerToken;

                    return resp;
                };
            });

            // You create policy if token have required scope
            services.AddAuthorization(options =>
            {
                options.AddPolicy("microservice.data.api",
                    policy => policy.RequireScope("microservice.data.api"));
            });

            #endregion

            #region Swagger 

            services.AddSwaggerGen(options =>
            {

                options.SwaggerDoc("v1", new OpenApiInfo { Title = "Microservice.Data.API", Version = "v1" });

                var xml = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xml);

                options.IncludeXmlComments(xmlPath);

                options.AddServer(new OpenApiServer()
                {
                    Url = "https://localhost:5003"
                });


                options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.OAuth2,
                    Flows = new OpenApiOAuthFlows
                    {
                        Password = new OpenApiOAuthFlow
                        {
                            TokenUrl = new Uri($"https://localhost:5001/connect/token"),
                            AuthorizationUrl = new Uri($"https://localhost:5001/connect/authorize"),
                        },
                    }
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Id = "oauth2",
                                Type = ReferenceType.SecurityScheme
                            }
                        },
                        new List<string>()
                    }
                });
            });

            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Microservice.Data.API v1"));

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
