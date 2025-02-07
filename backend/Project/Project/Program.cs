
using Microsoft.EntityFrameworkCore;
using Project.Models;

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

            

            builder.Services.AddControllers();
            builder.Services.AddDbContext<ProjectContext>(options =>
              options.UseSqlServer(builder.Configuration.GetConnectionString("MyDBConn")));
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

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
