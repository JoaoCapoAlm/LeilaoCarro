using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LeilaoCarro.Models
{
    public class Carro
    {
        [Required, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public required string Marca { get; set; }
        [Required]
        public required string Modelo { get; set; }
        public string? Placa { get; set; }
        public short? Ano { get; set; }
        public DateTime? DataHoraLeiloado { get; set; }
        [Required]
        public decimal LanceInicial { get; set; }
        public int? IdLance { get; set; }
        public virtual Lance? Lance { get; set; }
        public DateTime DataHoraCadastrado { get; set; }
        public virtual IEnumerable<Lance> Lances { get; set; }
    }
}
