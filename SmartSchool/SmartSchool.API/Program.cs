using Microsoft.EntityFrameworkCore;
using SmartSchool.API.Data;
using SmartSchool.Data;

namespace SmartSchool.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Conex�o com o banco de dados
            string connectionString = builder.Configuration.GetConnectionString("SqlConnection");

            var SqlConnectionConfiguration = new SqlConfiguration(connectionString);

            builder.Services.AddSingleton(SqlConnectionConfiguration);

            builder.Services.AddDbContext<SmartContext>(options =>
                options.UseSqlServer(SqlConnectionConfiguration.ConnectionString));

            builder.Services.AddControllers();

            var app = builder.Build();

            // Middleware para roteamento e endpoints
            app.MapControllers();

            app.Run();
        }
    }
}