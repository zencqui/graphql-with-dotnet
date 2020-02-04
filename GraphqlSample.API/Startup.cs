using System;
using System.Threading.Tasks;
using GraphiQl;
using GraphQL;
using GraphQL.Http;
using GraphQL.Server;
using GraphQL.Server.Ui.Playground;
using GraphQL.Types;
using GraphQL.Validation;
using GraphQL.Server.Authorization.AspNetCore;
using GraphqlSample.API.GraphTypes;
using GraphqlSample.API.Models;
using GraphqlSample.API.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

namespace GraphqlSample.API
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
            services.AddTransient<IEventService, EventService>();
            services.AddSingleton<IDocumentExecuter, DocumentExecuter>();
            services.AddSingleton<IDocumentWriter, DocumentWriter>();
            services.AddSingleton<IDocumentValidator, DocumentValidator>();
            services.AddSingleton<EventBookingQuery>();
            services.AddSingleton<EventBookingMutation>();
            services.AddSingleton<UserGraphType>();
            services.AddSingleton<UserInputGraphType>();
            services.AddSingleton<AuthDataGraphType>();
            services.AddSingleton<LoginInputGraphType>();
            services.AddSingleton<EventGraphType>();
            services.AddSingleton<EventInputGraphType>();
            services.AddSingleton<BookingInputGraphType>();
            services.AddSingleton<BookingGraphType>();
            //services.AddTransient<IAuthorizationHandler, IsSuperUserRequirement>();

            //authorization
            services.AddTransient<IValidationRule, AuthorizationValidationRule>()
                .AddAuthorization(options =>
                {
                    options.AddPolicy("IsAdmin", p => p.RequireClaim(JwtClaimType.Role, "superuser"));
                    //options.AddPolicy("IsSuperUser", p => p.AddRequirements(new IsSuperUserRequirement()));
                });

            services.AddAuthentication(options =>
                {
                    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(x =>
                {
                    x.Authority = "http://localhost:5000";
                    x.Audience = "grapqlapiclient";
                    x.RequireHttpsMetadata = false;
                });
                //.AddCookie(o => o.Cookie.Name = "graph-auth");

            var sp = services.BuildServiceProvider();
            services.AddSingleton<ISchema>(
                new EventBookingSchema(new FuncDependencyResolver(type => sp.GetService(type))));

            var settings = Configuration.GetSection(nameof(EventBookingDatabaseSettings));
            services.Configure<EventBookingDatabaseSettings>(settings);
            services.AddSingleton<IEventBookingDatabaseSettings>(sp =>
                sp.GetRequiredService<IOptions<EventBookingDatabaseSettings>>().Value);

            // If using Kestrel:
            services.Configure<KestrelServerOptions>(options =>
            {
                options.AllowSynchronousIO = true;
            });

            // If using IIS:
            services.Configure<IISServerOptions>(options =>
            {
                options.AllowSynchronousIO = true;
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
            app.UseAuthentication();
            app.UseAuthorization();

            //app.UseGraphiQl();

            var settings = new GraphQLSettings
            {
                BuildUserContext = ctx =>
                {
                    var userContext = new GraphQLUserContext
                    {
                        User = ctx.User
                    };

                    return Task.FromResult(userContext);
                }
            };

            var rules = app.ApplicationServices.GetServices<IValidationRule>();
            settings.ValidationRules.AddRange(rules);

            app.UseMiddleware<GraphQLMiddleware>(settings);

            app.UseGraphQLPlayground(new GraphQLPlaygroundOptions
            {
                Path = "/ui/playground"
            });

            
        }

        //public static void UseGraphQLWithAuth(this IApplicationBuilder app)
        //{
        //    var settings = new GraphQLSettings
        //    {
        //        BuildUserContext = ctx =>
        //        {
        //            var userContext = new GraphQLUserContext
        //            {
        //                User = ctx.User
        //            };

        //            return Task.FromResult(userContext);
        //        }
        //    };

        //    var rules = app.ApplicationServices.GetServices<IValidationRule>();
        //    settings.ValidationRules.AddRange(rules);

        //    app.UseMiddleware<GraphQLMiddleware>(settings);
        //}
    }
}
