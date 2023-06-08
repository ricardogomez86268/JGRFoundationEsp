using JGRFoundation.API.Helpers.AbstractFactory.PhotovoltaicEquipment.concrete;
using JGRFoundation.API.Helpers.AbstractFactory.PhotovoltaicEquipment.concrete.LowConsume;

namespace JGRFoundation.API.Helpers.AbstractFactory.factory.LowConsume
{
    public class LowConsumeFactory : IConsumeAbstractFactory
    {
        public BatteryAbstract createBattery()
        {
            return new BatteryLowConsume();
        }

        public InvestorAbstract createInvestor()
        {
            return new InvestorLowConsume();
        }

        public PanelAbstract createPanel()
        {
            return new PanelLowConsume();
        }
    }
}
