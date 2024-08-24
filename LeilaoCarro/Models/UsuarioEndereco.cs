using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LeilaoCarro.Models
{
    public class UsuarioEndereco
    {
        [Required, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public int IdUsuario { get; set; }
        public virtual Usuario Usuario { get; set; }
        [Required, MaxLength(8)]
        public required string Cep { get; set; }
        [Required]
        public byte IdEstado { get; set; }
        public virtual Estado Estado { get; set; }
        [Required]
        public required string Cidade { get; set; }
        [Required]
        public required string Logradouro { get; set; }
        public int Numero { get; set; }
        public string? Complemento { get; set; }
        [Required]
        public bool Ativo { get; set; }
    }
}
