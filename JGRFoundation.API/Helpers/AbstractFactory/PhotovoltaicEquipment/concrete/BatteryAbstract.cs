
namespace JGRFoundation.API.Helpers.AbstractFactory.PhotovoltaicEquipment.concrete
{
    public abstract class BatteryAbstract
    {
        public int Id;
        public string BatteryReference;
        public int Voltage;
        public int CapacityAmperageHour;

        public int getId()
        {
            return Id;
        }
        public void setId(int Id)
        {
            this.Id = Id;
        }
        public string getBatteryReference()
        {
            return BatteryReference;
        }
        public void setBatteryReference(string BatteryReference)
        {
            this.BatteryReference = BatteryReference;
        }
        public int getVoltage()
        {
            return Voltage;
        }
        public void setVoltage(int Voltage)
        {
            this.Voltage = Voltage;
        }
        public int getCapacityAmperageHour()
        {
            return CapacityAmperageHour;
        }
        public void setCapacityAmperageHour(int CapacityAmperageHour)
        {
            this.CapacityAmperageHour = CapacityAmperageHour;
        }
        public abstract string SelectBattery();
    }
}
