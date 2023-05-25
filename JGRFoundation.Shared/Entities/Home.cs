using System.ComponentModel.DataAnnotations;

namespace JGRFoundation.Shared.Entities
{
    public class Home
    {
        public int Id { get; set; }
        [Display(Name = "Coordenada")]
        [MaxLength(50, ErrorMessage = "El campo {0} debe tener máximo {1} caractéres.")]
        public string Coordinate { get; set; }=string.Empty;
        public User? User { get; set; }

        public string? UserId { get; set; }

        public ICollection<HomeDetail>? HomeDetails { get; set; }

        [DisplayFormat(DataFormatString = "{0:N0}")]
        [Display(Name = "Líneas")]
        public int Lines => HomeDetails == null ? 0 : HomeDetails.Count;

        [DisplayFormat(DataFormatString = "{0:N2}")]
        [Display(Name = "Cantidad electrodomesticos")]
        public float Quantity => HomeDetails == null ? 0 : HomeDetails.Sum(sd => sd.QuantityByAppliance);

        [DisplayFormat(DataFormatString = "{0:N2}")]
        [Display(Name = "Total consumo")]
        public decimal TotalConsumption => HomeDetails == null ? 0 : HomeDetails.Sum(sd => sd.ApplianceConsumption);
    }
}
