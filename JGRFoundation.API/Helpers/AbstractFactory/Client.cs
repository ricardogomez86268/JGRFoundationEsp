using JGRFoundation.API.Helpers.AbstractFactory.factory;
using JGRFoundation.Shared.DTOs;

namespace JGRFoundation.API.Helpers.AbstractFactory
{
    public class Client
    {
        private IConsumeAbstractFactory factory;
        public Client(IConsumeAbstractFactory factory)
        {
            this.factory = factory;
        }

        public PhotovoltaicEquipmentDTO Operation()
        {
            var photovoltaicEquipmentDTO = new PhotovoltaicEquipmentDTO();
            photovoltaicEquipmentDTO.BatteryReference = this.factory.createBattery().SelectBattery();
            photovoltaicEquipmentDTO.PanelReference = this.factory.createPanel().SelectPanel();
            photovoltaicEquipmentDTO.InvestorReference = this.factory.createInvestor().SelectInvestor();
            return photovoltaicEquipmentDTO;
        }
    }
}
