using NLog;
using NLog.Web;

var logger = NLog.LogManager.Setup()
                            .LoadConfigurationFromAppSettings()
                            .GetCurrentClassLogger();
logger.Debug("init main");
try {
    var builder = WebApplication.CreateBuilder(args);

    // Add services to the container.
    builder.Services.AddControllers();
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
    builder.Logging.ClearProviders();
    builder.Logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
    builder.Host.UseNLog();

    builder.Services.AddCors(o =>
    {
        o.AddPolicy("CorsPolicy", builder =>
         builder.AllowAnyOrigin()
         .AllowAnyMethod()
         .AllowAnyHeader());
    });

    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
    }
    app.UseSwagger();
    app.UseSwaggerUI();

    app.UseCors("CorsPolicy");

    app.UseHttpsRedirection();

    app.UseAuthorization();

    app.MapControllers();

    app.Run();

}

catch (Exception exception){
    logger.Error(exception, "Stopped because of exception");
    throw;
}
finally
{
    NLog.LogManager.Shutdown();
}
