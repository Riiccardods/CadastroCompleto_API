using System.ComponentModel.DataAnnotations;

namespace CadastroCompleto_API.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        [Required(ErrorMessage = "O campo de e-mail é obrigatório.")]
        [EmailAddress(ErrorMessage = "Por favor, insira um endereço de e-mail válido.")]
        public string Email { get; set; }
    }
}
