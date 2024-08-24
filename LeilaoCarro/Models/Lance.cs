using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LeilaoCarro.Models
{
    public class Lance
    {
        [Required, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public int IdUsuario { get; set; }
        public virtual Usuario Usuario { get; set; }
        [Required]
        public int IdCarro { get; set; }
        public virtual Carro Carro { get; set; }
        [Required]
        public decimal Valor { get; set; }
        [Required]
        public DateTime DataHoraLance { get; set; }
    }
}
