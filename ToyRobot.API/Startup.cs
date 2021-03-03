using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToyRobot.Logic;
using ToyRobot.Logic.Commands;
using ToyRobot.Logic.Interfaces;

namespace ToyRobot.API
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
            services.AddScoped<IRobot, Robot>(s => new Robot(false));
            services.AddSingleton<ITabletop, Tabletop>(s => new Tabletop(4, 4));
            services.AddScoped<IPlaceCommand, PlaceCommand>();
            services.AddScoped<IRobotCommander, RobotCommander>();
            services.AddScoped<IMoveCommand, MoveCommand>();
            services.AddScoped<ILeftCommand, LeftCommand>();
            services.AddScoped<IRightCommand, RightCommand>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
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
        }
    }
}
