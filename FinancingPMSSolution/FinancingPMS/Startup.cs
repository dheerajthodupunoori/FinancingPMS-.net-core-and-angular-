using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Reflection;
using System.IO;
using FinancingPMS.Interfaces;
using FinancingPMS.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using FinancingPMS.ServiceDependencies;
using Microsoft.AspNetCore.Http;
using FinancingPMS.Middlewares;

namespace FinancingPMS
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
            services.AddControllers();

            services.AddSingleton<IConfiguration>(Configuration);

            services.AddSingleton<IRegistration, RegistrationService>();

            services.AddSingleton<IAzureOperations, AzureOperations>();

            services.AddSingleton<ILoginService, LoginService>();

            services.AddSingleton<IFirmService, FirmService>();

            services.AddTransient<INotificationService, NotificationService>();

            services.AddScoped<ICustomerRegistrationService, CustomerRegistrationService>();

            services.AddAppConfiguration(Configuration);

            services.AddCors(options =>
            {
                options.AddPolicy("AllowOrigin",
                    builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            });

            services.AddMvc(options => options.EnableEndpointRouting = false);

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("Version1", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Contact = new Microsoft.OpenApi.Models.OpenApiContact()
                    {
                        Email = "dheeraj.thodupunoori01@gmail.com",
                        Name = "Dheeraj Thodupunuri",
                    },
                    Description = "Financing PMS API",
                    Title = "FinancingPMS",
                    Version = "V1"
                });

                //Including XML comments.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                options.IncludeXmlComments(xmlPath);
            });


            services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
           .AddJwtBearer(options =>
           {
               options.TokenValidationParameters = new TokenValidationParameters
               {
                   ValidateIssuer = true,
                   ValidateAudience = true,
                   ValidateLifetime = true,
                   ValidateIssuerSigningKey = true,

                   ValidIssuer = "",
                   ValidAudience = "",
                   IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("superSecretKey@345"))
               };
           });
            services.AddApplicationInsightsTelemetry();

            //services.AddHttpClient();

            services.AddHttpClient("NotificationClient", notificationClient =>
           {
               notificationClient.BaseAddress =new Uri("http://localhost:5000");
               notificationClient.DefaultRequestHeaders.Clear();

           });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseStaticFiles();

            //app.UseCors(builder => builder.WithOrigkins("http://localhost:4200"));

            app.UseCors("AllowOrigin");

            app.UseMvc();

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/Version1/swagger.json", "Version1");
                c.RoutePrefix = String.Empty;
            });

            

            app.Map("/swagger", (appBuilder) =>
           {
               appBuilder.Run(async (context) =>
               {
                   await context.Response.WriteAsync("<h1 style='text-align:center;position:center'>Please navigate to localhost</h1>");
               });
           });
            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseHttpRequestTimeMiddleware();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
