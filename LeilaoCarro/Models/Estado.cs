using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LeilaoCarro.Models
{
    public class Estado
    {
        [Required, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public byte Id { get; set; }
        [Required, MaxLength(30)]
        public required string Nome { get; set; }
        [Required, MaxLength(2)]
        public required string Sigla { get; set; }
        public virtual IEnumerable<UsuarioEndereco> UsuarioEndereco { get; set; }
    }
}
