using TaskManagement.Api.Extensions;
using TaskManagement.Application.Abstractions.Identity;
using TaskManagement.Application.Extensions;
using TaskManagement.Infrastructure.Extensions;

namespace TaskManagement.Api
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddPresentation(builder.Configuration)
                .AddApplication()
                .AddInfrastructure(builder.Configuration);

            var app = builder.Build();




            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger(); // run Middleware that make documentarion file(Json) available to use
                app.UseSwaggerUI();
                await app.SeedDatabaseAsync();
            }
            app.UseExceptionHandler();
            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
