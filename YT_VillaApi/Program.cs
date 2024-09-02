
using Serilog;
using YT_VillaApi.Logging;

namespace YT_VillaApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            //Log.Logger = new LoggerConfiguration().MinimumLevel.Debug()
            //    .WriteTo.File("log/villalogs.txt",rollingInterval:RollingInterval.Day).CreateLogger();

            //builder.Host.UseSerilog();

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            // builder.Services.AddSingleton--> created when app starts that object will be used when app requests an implemenation
            //builder.services.AddScoped--> for every request  creates an object
            //builder.Services.AddTransient-->evry time the object acceseed ex-> for 10 requests 10 objects are created and assign that where its needed
            builder.Services.AddSingleton<ILogging, Logging>();


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
