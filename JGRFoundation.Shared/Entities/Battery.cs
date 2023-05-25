using System.ComponentModel.DataAnnotations;

namespace JGRFoundation.Shared.Entities
{
    public class Battery
    {
        public int Id { get; set; }

        [Display(Name = "Referencia bateria")]
        [MaxLength(100, ErrorMessage = "El campo {0} debe tener máximo {1} caractéres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string BatteryReference { get; set; } = null!;

        [DisplayFormat(DataFormatString = "{0:N2}")]
        [Display(Name = "Voltage")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public int Voltage { get; set; } = 0;

        [DisplayFormat(DataFormatString = "{0:N2}")]
        [Display(Name = "Capacidad amperaje por hora")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public int CapacityAmperageHour { get; set; } = 0;


    }
}
