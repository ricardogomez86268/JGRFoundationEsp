
namespace JGRFoundation.API.Helpers.AbstractFactory.PhotovoltaicEquipment.concrete
{
    public abstract class PanelAbstract
    {
        public int Id;
        public string PanelReference;
        public int Power;
        public int getId()
        {
            return Id;
        }
        public void setId(int Id)
        {
            this.Id = Id;
        }
        public string getPanelReference()
        {
            return PanelReference;
        }
        public void setPanelReference(string PanelReference)
        {
            this.PanelReference = PanelReference;
        }
        public int getPower()
        {
            return Power;
        }
        public void setPower(int Power)
        {
            this.Power = Power;
        }

        public abstract string SelectPanel();
    }
}
