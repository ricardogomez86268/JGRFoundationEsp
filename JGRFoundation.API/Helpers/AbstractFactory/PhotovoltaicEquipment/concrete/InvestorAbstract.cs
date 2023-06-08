
namespace JGRFoundation.API.Helpers.AbstractFactory.PhotovoltaicEquipment.concrete
{
    public abstract class InvestorAbstract
    {
        public int Id;
        public string InvestorReference;
        public int RatedPower;

        public int getId()
        {
            return Id;
        }
        public void setId(int Id)
        {
            this.Id = Id;
        }
        public string getInvestorReference()
        {
            return InvestorReference;
        }
        public void setInvestorReference(string InvestorReference)
        {
            this.InvestorReference = InvestorReference;
        }
        public int getRatedPower()
        {
            return RatedPower;
        }
        public void setRatedPower(int RatedPower)
        {
            this.RatedPower = RatedPower;
        }
        public abstract string SelectInvestor();
    }
}
