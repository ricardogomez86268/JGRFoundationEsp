using System.ComponentModel.DataAnnotations;

namespace JGRFoundation.Shared.Entities
{
    public class Panel
    {
        public int Id { get; set; }

        [Display(Name = "Referencia panel")]
        [MaxLength(100, ErrorMessage = "El campo {0} debe tener máximo {1} caractéres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string PanelReference { get; set; } = null!;

        [DisplayFormat(DataFormatString = "{0:N2}")]
        [Display(Name = "Potencia")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public int Power { get; set; } = 0;
    }
}
