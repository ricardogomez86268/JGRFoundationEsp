using System.ComponentModel.DataAnnotations;

namespace JGRFoundation.Shared.Entities
{
    public class Appliance
    {
        public int Id { get; set; }

        [Display(Name = "Electrodomestico")]
        [MaxLength(100, ErrorMessage = "El campo {0} debe tener máximo {1} caractéres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string Name { get; set; } = null!;


        [DisplayFormat(DataFormatString = "{0:N2}")]
        [Display(Name = "Consumo promedio diario")]
        public decimal AverageDailyConsumption { get; set; } = (decimal)0;
    }
}
