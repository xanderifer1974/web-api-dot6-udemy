using Microsoft.EntityFrameworkCore;
using SmartSchool.Data.Context; 
using SmartSchool.Data;
using SmartSchool.Data.Repository;
using SmartSchool.Data.Repository.Interface;

namespace SmartSchool.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Conexão com o banco de dados
            string connectionString = builder.Configuration.GetConnectionString("SqlConnection");

            var SqlConnectionConfiguration = new SqlConfiguration(connectionString);

            builder.Services.AddSingleton(SqlConnectionConfiguration);

            builder.Services.AddDbContext<SmartContext>(options =>
                options.UseSqlServer(SqlConnectionConfiguration.ConnectionString));

            //Injetando um Repositório
            builder.Services.AddScoped<IBaseRepository, BaseRepository>();
            builder.Services.AddScoped<IAlunoRepository, AlunoRepository>();
            builder.Services.AddScoped<IProfessorRepository, ProfessorRepository>();

            builder.Services.AddControllers();

            var app = builder.Build();

            // Middleware para roteamento e endpoints
            app.MapControllers();

            app.Run();
        }
    }
}