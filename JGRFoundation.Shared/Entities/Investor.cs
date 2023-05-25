using System.ComponentModel.DataAnnotations;

namespace JGRFoundation.Shared.Entities
{
    public class Investor
    {
        public int Id { get; set; }

        [Display(Name = "Referencia Inversor")]
        [MaxLength(100, ErrorMessage = "El campo {0} debe tener máximo {1} caractéres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string InvestorReference { get; set; } = null!;

        [DisplayFormat(DataFormatString = "{0:N2}")]
        [Display(Name = "Potencia nominal")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public int RatedPower { get; set; } = 0;     
    }
}
