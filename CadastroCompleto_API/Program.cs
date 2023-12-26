// Importa o namespace para usar o Entity Framework Core
using CadastroCompleto_API.Data;
using Microsoft.EntityFrameworkCore;

// Define o namespace do seu projeto
namespace CadastroCompleto_API
{
    // Classe Program que contém o ponto de entrada principal da aplicação
    public class Program
    {
        // Método Main é o ponto de entrada da aplicação
        public static void Main(string[] args)
        {
            // Cria um construtor de aplicativo Web com os argumentos de linha de comando
            var builder = WebApplication.CreateBuilder(args);

            // Adiciona os serviços de Controllers ao container de DI (Injeção de Dependência)
            // Isso habilita o uso de Controllers para tratar requisições HTTP
            builder.Services.AddControllers();

            // Adiciona o serviço de contexto do Entity Framework ao container de DI
            // Configura o contexto para usar o SQL Server com a string de conexão definida no arquivo appsettings.json
            // "YourConnectionString" é uma chave que você deverá substituir pelo nome da sua chave de conexão no appsettings.json
            builder.Services.AddDbContext<CadastroContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("ConexaoAzureSQL")));

            // Adiciona serviços necessários para habilitar o Endpoint API Explorer
            // Isso é útil para a geração da documentação da API com o Swagger
            builder.Services.AddEndpointsApiExplorer();
            // Adiciona o serviço de geração do Swagger para documentar a API
            builder.Services.AddSwaggerGen();

            // Constrói a aplicação
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Minha API V1");
            });

            app.UseHttpsRedirection();

            // Adiciona middleware para autorização de requisições
            app.UseAuthorization();

            // Mapeia os Controllers para agirem como endpoints para as requisições HTTP
            app.MapControllers();

            // Inicia a aplicação e passa a ouvir por requisições HTTP
            app.Run();
        }
    }
}
