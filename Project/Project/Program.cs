
using Microsoft.EntityFrameworkCore;
using Project.Models;
using Project.Repo;
using Serilog;

namespace Project
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

           

            // Add services to the container.
            builder.Services.AddCors(options => options.AddPolicy("MyTestCors",
                            policy =>policy.AllowAnyOrigin()
                            .AllowAnyHeader().AllowAnyMethod()
                            ));

            builder.Services.AddTransient<IEquity, EquityOps>();
            builder.Services.AddTransient<IBond, BondOps>();

            builder.Services.AddControllers();
            builder.Services.AddDbContext<ProjectContext>(options =>
              options.UseSqlServer(builder.Configuration.GetConnectionString("MyDBConn")));
            
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // Serilog
            Log.Logger = new LoggerConfiguration()
            .ReadFrom.Configuration(builder.Configuration)
            .Enrich.FromLogContext()
            .WriteTo.File("logs/MyAppLog.txt")
            .CreateLogger();
            builder.Host.UseSerilog();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.UseCors("MyTestCors");

          

            app.MapControllers();

            app.Run();
        }
    }
}
