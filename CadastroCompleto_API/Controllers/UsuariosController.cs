// Namespaces necessários para funcionamento do Controller
using Microsoft.AspNetCore.Mvc;
using CadastroCompleto_API.Data;
using CadastroCompleto_API.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

// Declaração do namespace do controller, que deve ser o mesmo do seu projeto
namespace CadastroCompleto_API.Controllers
{
    // Atributo de rota que define o padrão de URL para acessar o Controller
    // Atributo ApiController que habilita recursos específicos para APIs no controller, como a validação automática de ModelState
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        // Contexto do Entity Framework Core para acesso ao banco de dados
        private readonly CadastroContext _context;

        #region  // Construtor que injeta a dependência do contexto do banco de dados
        public UsuariosController(CadastroContext context)
        {
            _context = context;
        }
        #endregion

        #region// Método GET para listar todos os usuários
        // Task<ActionResult<IEnumerable<Usuario>>> permite uma operação assíncrona que retorna uma ação de resultado com uma lista de usuários
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Usuario>>> GetUsuarios()
        {
            // Retorna a lista de usuários do banco de dados de forma assíncrona
            return await _context.Usuarios.ToListAsync();
        }
        #endregion

        #region// Método GET para obter um único usuário por ID
        // Parâmetro "id" é especificado na rota
        [HttpGet("{id}")]
        public async Task<ActionResult<Usuario>> GetUsuario(int id)
        {
            // Busca o usuário de forma assíncrona
            var usuario = await _context.Usuarios.FindAsync(id);

            // Se nenhum usuário for encontrado, retorna um erro 404 Not Found
            if (usuario == null)
            {
                return NotFound();
            }

            // Retorna o usuário encontrado
            return usuario;
        }
        #endregion

        #region// Método POST para criar um novo usuário
        [HttpPost]
        public async Task<ActionResult<Usuario>> PostUsuario(Usuario usuario)
        {
            // Adiciona o usuário ao contexto
            _context.Usuarios.Add(usuario);
            // Salva as alterações de forma assíncrona no banco de dados
            await _context.SaveChangesAsync();

            // Retorna um status 201 Created, incluindo a localização do novo recurso criado e o próprio recurso
            return CreatedAtAction(nameof(GetUsuario), new { id = usuario.Id }, usuario);
        }
        #endregion

        // Método PUT para atualizar um usuário existente
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUsuario(int id, Usuario usuario)
        {
            // Verifica se o ID do parâmetro é diferente do ID do usuário passado no corpo da requisição
            if (id != usuario.Id)
            {
                return BadRequest(); // Retorna um erro 400 Bad Request
            }

            // Atualiza o estado do usuário no contexto para Modified, o que indica que deve ser atualizado no banco de dados
            _context.Entry(usuario).State = EntityState.Modified;

            // Tenta salvar as alterações no banco de dados de forma assíncrona
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                // Verifica se o usuário realmente existe no banco de dados
                if (!UsuarioExists(id))
                {
                    return NotFound(); // Retorna um erro 404 Not Found
                }
                else
                {
                    throw; // Lança a exceção capturada
                }
            }

            // Se a atualização for bem-sucedida, retorna um status 204 No Content
            return NoContent();
        }

        // Método DELETE para excluir um usuário
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUsuario(int id)
        {
            // Busca o usuário de forma assíncrona
            var usuario = await _context.Usuarios.FindAsync(id);
            // Se nenhum usuário for encontrado, retorna um erro 404 Not Found
            if (usuario == null)
            {
                return NotFound();
            }

            // Remove o usuário do contexto
            _context.Usuarios.Remove(usuario);
            // Salva as alterações no banco de dados de forma assíncrona
            await _context.SaveChangesAsync();

            // Retorna um status 204 No Content
            return NoContent();
        }

        // Método auxiliar privado para verificar se um usuário existe no banco de dados
        private bool UsuarioExists(int id)
        {
            // Verifica se algum usuário no contexto tem o ID fornecido
            return _context.Usuarios.Any(e => e.Id == id);
        }
    }
}
