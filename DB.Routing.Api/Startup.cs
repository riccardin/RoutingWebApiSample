using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using DB.Routing.Api.Contracts;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.AspNetCore.Diagnostics;
using NLog.Extensions.Logging;
using NLog.Web;
using AspNetCoreRateLimit;

namespace DB.Routing.Api
{
    public class Startup
    {
       public Startup(IConfiguration configuration)
        {

          
          Configuration = configuration;
            
        }

        public IConfiguration  Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddMvc(setupAction=> {

                setupAction.ReturnHttpNotAcceptable = true; //Will return 406 Status code (Not acceptable) if Header Accept is different to application/json
                setupAction.OutputFormatters.Add(new XmlDataContractSerializerOutputFormatter());  // To suppor XML as output beside of Json
            });

            services.AddScoped<IShardInfoRepository, ShardInfoRepository>(); //Dependency Injection 
            services.AddHttpCacheHeaders
                (
                        (expirationModelOptions) =>{ expirationModelOptions.MaxAge = 600; },
                        (validationModelOptions)=> { validationModelOptions.AddMustRevalidate = true;  }
            );

            services.AddMemoryCache(); //Memory cache is used the the .NetCoreRateLimit library to persist the rules
            services.Configure<IpRateLimitOptions>((options) =>

                options.GeneralRules = new System.Collections.Generic.List<RateLimitRule>() {

                    //This rule defines the client can access to all endpoints maximum 3 times in a period of 5 minutes.
                    //new RateLimitRule{

                    //    Endpoint="*",
                    //    Limit=3,
                    //    Period="5m"
                    //}

                     new RateLimitRule{

                        Endpoint="*",
                        Limit=1000,
                        Period="5m"
                    },
                     //Limit the number of request to 2 request each 10 seconds
                      new RateLimitRule{

                        Endpoint="*",
                        Limit=2,
                        Period="10s"
                    }




                }

            );

            //Is important to register these Services as singlenton since we don't want to store the rules for each request.
            services.AddSingleton<IRateLimitCounterStore, MemoryCacheRateLimitCounterStore>();
            services.AddSingleton<IIpPolicyStore, MemoryCacheIpPolicyStore>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            env.ConfigureNLog("nlog.config");
            //loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            //loggerFactory.AddDebug(LogLevel.Information);
            loggerFactory.AddNLog();
            

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else  //Global Exception handler
            {
                app.UseExceptionHandler(appBuilder =>
                {
                    appBuilder.Run(async context =>
                    {
                        var exceptionHandlerFeature=context.Features.Get<IExceptionHandlerFeature>();
                        if (exceptionHandlerFeature != null) {

                            var logger = loggerFactory.CreateLogger("Global exception logger");
                            logger.LogError(500,
                                  exceptionHandlerFeature.Error,
                                    exceptionHandlerFeature.Error.Message);


                          
                        }

                        context.Response.StatusCode = 500;
                        await context.Response.WriteAsync("An unexpected fault happened. Try again later.");

                    });


                });
            }
            //another alternative of using autommaper is the use of "Json .Net" and decorate the properties as JsonProperties to serialize only the attributes 
            //we want to return as JSON
            AutoMapper.Mapper.Initialize(cfg =>
            {
              cfg.CreateMap<Models.ShardInformation, Models.ShardInformationDto>();

            });

            //app.UseStaticFiles();

            app.UseIpRateLimiting();

            app.UseHttpCacheHeaders(); //Is important to add this before the MVC middleware of this way the Cache validation can happen before the request reaches the controller
                                        //and the response will not be generated
            app.UseMvc();
        }
    }
}
