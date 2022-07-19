using NLog;
using NLog.Web;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PackyAPI.Data;
using PackyAPI.Configurations;
using PackyAPI.Repository;
using PackyAPI.IRepository;
using Microsoft.AspNetCore.Identity;
using PackyAPI;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using PackyAPI.Services;

var logger = NLog.LogManager.Setup()
                            .LoadConfigurationFromAppSettings()
                            .GetCurrentClassLogger();
logger.Debug("init main");
try {
    var builder = WebApplication.CreateBuilder(args);

    // Add services to the container.
    builder.Services.AddControllers();
    

    // Add DbContext and Database Connection
    builder.Services.AddDbContext<DatabaseContext>(options => options.UseSqlServer(
    builder.Configuration.GetConnectionString("DefaultConnection")
    ));

    // Add Authentication and Authorization
    builder.Services.AddAuthentication();
    builder.Services.AddIdentity<ApiUser, IdentityRole>()
        .AddEntityFrameworkStores<DatabaseContext>()
        .AddDefaultTokenProviders();


    builder.Services.AddEndpointsApiExplorer();

    // Add Swagger
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddSwaggerGen();

    // Add NLog
    builder.Logging.ClearProviders();
    builder.Logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
    builder.Host.UseNLog();

    // Add CORS
    builder.Services.AddCors(o =>
    {
        o.AddPolicy("CorsPolicy", builder =>
         builder.AllowAnyOrigin()
         .AllowAnyMethod()
         .AllowAnyHeader());
    });

    // Add AutoMapper
    builder.Services.AddAutoMapper(typeof(MapperInitializer));
    builder.Services.AddScoped<IUnitofWork, UnitofWork>();
    builder.Services.AddScoped<IAuthManager, AuthManager>();
    builder.Services.AddControllers().AddNewtonsoftJson(op =>
                           op.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

    // Add JWT
    var jwtSettings = builder.Configuration.GetSection("Jwt");
    var key = Environment.GetEnvironmentVariable("KEY");
    builder.Services.AddAuthentication(o =>
    {
        o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    }
    )
        .AddJwtBearer(o =>
        {
            o.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = jwtSettings.GetSection("Issuer").Value,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key))
            };
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
