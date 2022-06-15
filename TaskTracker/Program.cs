using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TaskTracker.Data;
using Microsoft.OpenApi.Models;
using System.Reflection;
using TaskTracker.Services.TaskServices;
using TaskTracker.Middleware;
using NLog;
using NLog.Web;

var logger = LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
logger.Debug("init main");

try
{
    var builder = WebApplication.CreateBuilder(args);

    //Подключение контекста БД
    builder.Services.AddDbContext<AppDbContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

    builder.Services.AddControllers()
        .AddNewtonsoftJson(opt => opt.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

    //Настройка логирования
    builder.Logging.ClearProviders();
    builder.Logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
    builder.Host.UseNLog();

    //Настройка сваггера
    builder.Services.AddSwaggerGen(options =>
    {
        options.SwaggerDoc("v1", new OpenApiInfo { Title = "Task Tracker", Version = "v1" });

        var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
        options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
    });

    //Автомаппер
    builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

    //Подключение DI (можно вынести в отдельный файл)
    builder.Services.AddTransient<ITaskService, TaskService>();

    //StartUp
    var app = builder.Build();

    //Подключение сваггера на дев среде
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI(options =>
        {
            options.SwaggerEndpoint("/swagger/v1/swagger.json", "Task Tracker v1");
            options.RoutePrefix = string.Empty;
        });
    }

    app.UseStaticFiles();

    app.UseRouting();

    //Подключение собственных Middleware
    app.UseMiddleware<RequestResponseLoggingMiddleware>();
    app.UseMiddleware<ErrorHandlerMiddleware>();

    app.UseEndpoints(endpoints =>
    {
        endpoints.MapDefaultControllerRoute();
    });

    app.Run();
}
catch (Exception ex)
{
    logger.Error(ex.Message, "Stopped program because of exception");
    throw;
}
finally
{
    LogManager.Shutdown();
}