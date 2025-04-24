using System.ComponentModel.DataAnnotations;

namespace MyGestor.Domain.Entities
{
    public class Cliente
    {
        public int Id { get; set; }

        [Required]
        public string Nome { get; set; }
        public string? Email { get; set; }
        public string Telefone { get; set; }
    }
}
