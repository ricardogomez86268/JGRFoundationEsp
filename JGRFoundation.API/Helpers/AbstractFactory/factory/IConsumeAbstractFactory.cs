using JGRFoundation.API.Helpers.AbstractFactory.PhotovoltaicEquipment.concrete;

namespace JGRFoundation.API.Helpers.AbstractFactory.factory
{
    public interface IConsumeAbstractFactory
    {
        public BatteryAbstract createBattery();
        public InvestorAbstract createInvestor();
        public PanelAbstract createPanel();
    }
}
