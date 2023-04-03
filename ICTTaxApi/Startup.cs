//using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using ICTTaxApi.Data.Entities;
using ICTTaxApi.Filters;
using ICTTaxApi.Services;
using ICTTaxApi.Data.Repositories;

namespace ICTTaxApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; set; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers(options =>
            {
                options.Filters.Add(typeof(ExceptionFilter));
            }).AddJsonOptions(x =>
            x.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles);

            services.AddTransient<IICTTaxService, ICTTaxService>();
            services.AddTransient<IClientRepository, ClientRepository>();
            services.AddTransient<ITransactionRepository, TransactionRepository>();
            services.AddEndpointsApiExplorer();

            services.AddCors(options =>
            { 
            options.AddPolicy(
                    name: "AllowOrigin",
                    builder =>{   builder.AllowAnyOrigin().AllowAnyMethod()
                        .AllowAnyHeader();
                    });
            });

            //services.AddHostedService<ICWriteInFile>();

        services.AddResponseCaching();

            //services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer();
        //    services.AddDbContext<ICTTaxDbContext>(options =>
        //    {
        //        options.UseSqlServer(Configuration.GetConnectionString("deafultConnection"),
        //        //sqlServerOptionsAction: sqlOptions =>
        //        //{
        //        //    sqlOptions.EnableRetryOnFailure();
        //        //}), ServiceLifetime.Transient);
                   
        //});
            services.AddDbContext<ICTTaxDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("deafultConnection")),
                ServiceLifetime.Transient);


            services.AddSwaggerGen();

        services.AddAutoMapper(typeof(Startup));
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILogger<Startup> logger)
    {
        //app.UseMiddleware<LogHttpResponseMiddleware>();
        //app.UseLogReponseHttp();


        if (env.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseRouting();

            //app.UseResponseCaching(); cachear respuesta
        app.UseCors("AllowOrigin");

        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}
}
