using System;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Practices.Unity;
using Tasker.Common.Abstraction;
using Tasker.QuartzAdapter;
using Tasker.QuartzAdapter.Unity;
using Tasker.WebApi.UnityImpl;

namespace Tasker.WebApi
{
    public class Startup
    {
        public IConfigurationRoot Configuration { get; set; }

        public Startup(IHostingEnvironment env)
        {
            // Set up configuration sources.
            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json");

            if (env.IsEnvironment("Development"))
            {
                // This will push telemetry data through Application Insights pipeline faster, allowing you to view results immediately.
                builder.AddApplicationInsightsSettings(developerMode: true);
            }
            builder.AddEnvironmentVariables();
            Configuration = builder.Build().ReloadOnChanged("appsettings.json");
        }

        // This method gets called by the runtime. Use this method to add services to the container
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddApplicationInsightsTelemetry(Configuration);
            services.AddMvc();
            var container = InitContainer().Value;
            container.Populate(services);
            //container.Resolve<IScopedInstance<ActionContext>>();
            return container.Resolve<IServiceProvider>();


        }
        private static Lazy<IUnityContainer> InitContainer()
        {
            var unityContainer = new UnityContainer();
            //taskları burada register etmeliyiz..!
            unityContainer.RegisterType<ITask, NullTask.NullTask>("NullTask");
            unityContainer.AddNewExtension<TaskUnityExtension>();
            unityContainer.RegisterType<ITaskScheduler, QuartzTaskSchedulerImpl>();
            unityContainer.AddNewExtension<TaskerQuartzUnityExtension>();
            return new Lazy<IUnityContainer>(() => unityContainer);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();
            app.UseIISPlatformHandler();
            app.UseApplicationInsightsRequestTelemetry();
            app.UseApplicationInsightsExceptionTelemetry();
            app.UseStaticFiles();
            app.UseMvc();
        }

        // Entry point for the application.
        public static void Main(string[] args) => WebApplication.Run<Startup>(args);
    }
}
