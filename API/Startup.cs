using Core.Tools.SmsHandler.Abstract;
using Core.Tools.SmsHandler.Implementation.ATLSms.Concreate;
using DataAccess.Contexts;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
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

namespace API
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

            services.AddHttpClient();

            //Context
            services.AddDbContext<ATLSmsContext>(option => 
            option.UseSqlServer(Configuration.GetConnectionString("LocalPC"), x => x.MigrationsAssembly("DataAccess")));

            // SMS Service
            var atlsmsConfig = Configuration.GetSection("ATLSms").Get<ATLSmsConfiguration>();
            services.AddSingleton(atlsmsConfig);
            services.AddTransient<ISMSService, ATLSms>();
            services.AddTransient<IATLSmsService, ATLSms>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
