using Microsoft.OpenApi.Models;

namespace AssessmentService;

public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddCors(options =>
        {            options.AddPolicy("AllowAngularApp",                
            builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
        });
        services.AddControllers();
        services.AddControllers().AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.PropertyNamingPolicy = new LowercaseNamingPolicy();
        });
      
        //services.AddScoped<IMongoService, MongoService>();
        services.AddSingleton<RabbitMqService>();
        services.AddSwaggerGen(c =>
        {            c.SwaggerDoc("v1", new OpenApiInfo { Title = "Assessment Service API", Version = "v1" });
        });
        
    }

 
    public void Configure(IApplicationBuilder app)
    {
       app.UseHttpsRedirection();
       app.UseRouting();
       app.UseCors("AllowAngularApp");
       app.UseAuthorization();
       app.UseSwagger();

       // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),// specifying the Swagger JSON endpoint.
       app.UseSwaggerUI(c =>        {            
           c.SwaggerEndpoint("/swagger/v1/swagger.json", "Assessment Service API V1");
           c.RoutePrefix = string.Empty; // Set Swagger UI at the app's root (e.g. http://localhost:<port>/)
            });
       
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            
    }
}

