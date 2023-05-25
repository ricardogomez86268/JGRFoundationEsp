using System.ComponentModel.DataAnnotations;

namespace JGRFoundation.Shared.Entities
{
    public class HomeDetail
    {
        public int Id { get; set; }

        public Home? Home { get; set; }

        public int HomeId { get; set; }     

        public Appliance? Appliance { get; set; }
        public int ApplianceId { get; set; }

        [DisplayFormat(DataFormatString = "{0:N2}")]
        [Display(Name = "Cantidad por electrodomestico")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public float QuantityByAppliance { get; set; }

        [DisplayFormat(DataFormatString = "{0:N2}")]
        [Display(Name = "Consumo por electrodomestico")]
        public decimal ApplianceConsumption => Appliance == null ? 0 : (decimal)QuantityByAppliance * Appliance.AverageDailyConsumption;
    }
}
