// Importa o namespace para usar o Entity Framework Core
using CadastroCompleto_API.Data;
using Microsoft.EntityFrameworkCore;

// Define o namespace do seu projeto
namespace CadastroCompleto_API
{
    // Classe Program que cont�m o ponto de entrada principal da aplica��o
    public class Program
    {
        // M�todo Main � o ponto de entrada da aplica��o
        public static void Main(string[] args)
        {
            // Cria um construtor de aplicativo Web com os argumentos de linha de comando
            var builder = WebApplication.CreateBuilder(args);

            // Adiciona os servi�os de Controllers ao container de DI (Inje��o de Depend�ncia)
            // Isso habilita o uso de Controllers para tratar requisi��es HTTP
            builder.Services.AddControllers();

            // Adiciona o servi�o de contexto do Entity Framework ao container de DI
            // Configura o contexto para usar o SQL Server com a string de conex�o definida no arquivo appsettings.json
            // "YourConnectionString" � uma chave que voc� dever� substituir pelo nome da sua chave de conex�o no appsettings.json
            builder.Services.AddDbContext<CadastroContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("ConexaoAzureSQL")));

            // Adiciona servi�os necess�rios para habilitar o Endpoint API Explorer
            // Isso � �til para a gera��o da documenta��o da API com o Swagger
            builder.Services.AddEndpointsApiExplorer();
            // Adiciona o servi�o de gera��o do Swagger para documentar a API
            builder.Services.AddSwaggerGen();

            // Constr�i a aplica��o
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Minha API V1");
            });

            app.UseHttpsRedirection();

            // Adiciona middleware para autoriza��o de requisi��es
            app.UseAuthorization();

            // Mapeia os Controllers para agirem como endpoints para as requisi��es HTTP
            app.MapControllers();

            // Inicia a aplica��o e passa a ouvir por requisi��es HTTP
            app.Run();
        }
    }
}
