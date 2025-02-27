using ApiConciertosAnitaAWS.Data;
using ApiConciertosAnitaAWS.Helpers;
using ApiConciertosAnitaAWS.Models;
using ApiConciertosAnitaAWS.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;

namespace ApiConciertosAnitaAWS;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container
    public void ConfigureServices(IServiceCollection services)
    {
        string jsonSecrets = 
            HelperSecretManager.GetSecretsAsync()
            .GetAwaiter().GetResult();
        KeysModel keysModel = 
            JsonConvert.DeserializeObject<KeysModel>(jsonSecrets);

        services.AddSingleton<KeysModel>(x => keysModel);

        string connectionString = keysModel.MySql;
            //Configuration.GetConnectionString("MySql");
        services.AddTransient<RepositoryEventos>();
        services.AddDbContext<EventosContext>
            (options => options.UseMySql(connectionString,
            ServerVersion.AutoDetect(connectionString)));

        services.AddCors(options =>
        {
            options.AddPolicy("AllowOrigin", x => x.AllowAnyOrigin());
        });

        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "Api Eventos Anita AWS",
                Version = "v1"
            });
        });

        services.AddControllers();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseCors(options => options.AllowAnyOrigin());
        app.UseSwagger();
        app.UseSwaggerUI(options =>
        {
            options.SwaggerEndpoint(url: "swagger/v1/swagger.json",
                "ApiEventosAnitaAWS");
            options.RoutePrefix = "";
        });

        app.UseHttpsRedirection();

        app.UseRouting();

        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
            endpoints.MapGet("/", async context =>
            {
                await context.Response.WriteAsync("Welcome to running ASP.NET Core on AWS Lambda");
            });
        });
    }
}