namespace JGRFoundation.API.Helpers.AbstractFactory.PhotovoltaicEquipment.concrete.LowConsume
{
    public class BatteryLowConsume : BatteryAbstract
    {
        public override string SelectBattery()
        {
            return "Seleccion bateria bajo voltaje";
        }
    }
}
