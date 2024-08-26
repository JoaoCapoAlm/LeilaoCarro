using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LeilaoCarro.Models
{
    public class Usuario
    {
        [Required, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required, MaxLength(250)]
        public required string Nome { get; set; }
        [Required, MaxLength(14)]
        public required string Documento { get; set; }
        public string? Email { get; set; }
        public DateOnly? DataNascimento { get; set; }
        public virtual IEnumerable<Lance> Lances { get; set; }
        public virtual IEnumerable<UsuarioEndereco> UsuarioEnderecos { get; set; }
    }
}
