using Microsoft.EntityFrameworkCore;
using SmartSchool.Data.Context; 
using SmartSchool.Data;
using SmartSchool.Data.Repository;
using SmartSchool.Data.Repository.Interface;
using System.Reflection;
using Microsoft.OpenApi.Models;



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

            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());    

            //Injetando um Repositório           
            builder.Services.AddScoped<IAlunoRepository, AlunoRepository>();
            builder.Services.AddScoped<IProfessorRepository, ProfessorRepository>();

            builder.Services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("smartschoolapi",
                    new Microsoft.OpenApi.Models.OpenApiInfo()
                    {
                        Title = "SmartSchool API",
                        Version = "1.0",
                        TermsOfService = new Uri("http://termodeservicosmartschool.com"),
                        Description = "Api para obter dados de professores e alunos da SmartSchool (escola fictícia para estudo de API)",
                        License = new Microsoft.OpenApi.Models.OpenApiLicense
                        {
                            Name = "SmartSchool License",
                            Url = new Uri("http://smartschoolfic.com")
                        },
                        Contact = new OpenApiContact
                        {
                            Name = "Responsável pelo contato",
                            Email = "responsavel.contato@smartschool-fic.com",
                            Url = new Uri("http://responsavelcontato.com")
                        }

                    }); ;;

                var xmlComentsFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlComentsFullPath = Path.Combine(AppContext.BaseDirectory, xmlComentsFile);

                options.IncludeXmlComments(xmlComentsFullPath);
            });

            builder.Services.AddControllers()
                 .AddNewtonsoftJson(options =>
                 {
                     options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                     options.SerializerSettings.DateFormatString = "dd/MM/yyyy";
                 });

            var app = builder.Build();

            app.UseSwagger()
                .UseSwaggerUI(options =>
                {
                    options.SwaggerEndpoint("/swagger/smartschoolapi/swagger.json", "smartschoolapi");
                    options.RoutePrefix = "";
                });          
            



            // Middleware para roteamento e endpoints
            app.MapControllers();

            app.Run();
        }
    }
}