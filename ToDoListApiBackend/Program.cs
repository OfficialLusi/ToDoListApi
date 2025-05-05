using ToDoListApiBackend.Application.Interfaces;
using ToDoListApiBackend.Infrastructure.Repositories;

namespace ToDoListApiBackend
{
    public class Program
    {
        private static void Main(string[] args)
        {
            // creating builder
            var builder = WebApplication.CreateBuilder(args);

            // getting db infos by appsettings.json
            string dbPath = builder.Configuration.GetSection("Repository")["DbPath"];
            string connectionString = builder.Configuration.GetConnectionString("Default");
            string creationFilePath = builder.Configuration.GetSection("Repository")["CreationScript"];

            // Add services to the container.
            builder.Services.AddControllers();

            // repository registration
            builder.Services.AddTransient<IRepositoryService, RepositoryService>(provider =>
            {
                ILogger<RepositoryService> repoLogger = provider.GetRequiredService<ILogger<RepositoryService>>();
                return new RepositoryService(repoLogger, dbPath, creationFilePath, connectionString);
            });



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

            app.MapControllers();

            app.Run();
        }
    }
}