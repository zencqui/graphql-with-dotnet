using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using GraphiQl;
using GraphQL;
using GraphQL.Server.Ui.Playground;
using GraphQL.Types;
using GraphqlSample.GraphTypes;
using GraphqlSample.Models;
using GraphqlSample.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

namespace GraphqlSample
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(op => op.EnableEndpointRouting = false).AddNewtonsoftJson();
            services.AddHttpContextAccessor();
            services.AddSingleton<ContextServiceLocator>();
            services.AddTransient<IUserService, UserService>();
            services.AddSingleton<IDocumentExecuter, DocumentExecuter>();
            services.AddSingleton<EventBookingQuery>();
            services.AddTransient<UserGraphType>();
            services.AddSingleton<EventBookingMutation>();
            services.AddTransient<UserInputGraphType>();
            
            var sp = services.BuildServiceProvider();
            services.AddSingleton<ISchema>(
                new EventBookingSchema(new FuncDependencyResolver(type => sp.GetService(type))));

            var settings = Configuration.GetSection(nameof(EventBookingDatabaseSettings));
            services.Configure<EventBookingDatabaseSettings>(settings);
            services.AddSingleton<IEventBookingDatabaseSettings>(sp =>
                sp.GetRequiredService<IOptions<EventBookingDatabaseSettings>>().Value);

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseGraphiQl();
            app.UseGraphQLPlayground(new GraphQLPlaygroundOptions
            {
                Path = "/ui/playground"
            });

            app.UseMvc();
        }
    }
}
