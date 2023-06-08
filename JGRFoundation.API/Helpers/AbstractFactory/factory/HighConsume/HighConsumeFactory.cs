using JGRFoundation.API.Helpers.AbstractFactory.PhotovoltaicEquipment.concrete;
using JGRFoundation.API.Helpers.AbstractFactory.PhotovoltaicEquipment.concrete.HighConsume;

namespace JGRFoundation.API.Helpers.AbstractFactory.factory.HighConsume
{
    public class HighConsumeFactory : IConsumeAbstractFactory
    {
        public BatteryAbstract createBattery()
        {
            return new BatteryHighConsume();
        }

        public InvestorAbstract createInvestor()
        {
            return new InvestorHighConsume();
        }

        public PanelAbstract createPanel()
        {
            return new PanelHighConsume();
        }
    }
}
