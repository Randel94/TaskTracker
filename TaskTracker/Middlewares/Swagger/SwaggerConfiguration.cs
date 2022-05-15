using Microsoft.OpenApi.Models;

namespace TaskTracker.Middlewares.Swagger
{
    public static class SwaggerConfiguration
    {
        public static void ConfigureSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Task Tracker", Version = "v1" });
                var xmlFiles = Directory.GetFiles(AppContext.BaseDirectory, "*.xml", SearchOption.TopDirectoryOnly)
                    .ToList();
                xmlFiles.ForEach(xmlFile => c.IncludeXmlComments(xmlFile));
            });
        }
    }
}
