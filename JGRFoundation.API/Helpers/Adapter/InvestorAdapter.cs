using JGRFoundation.Shared.Entities;

namespace JGRFoundation.API.Helpers.Adapter
{
    public class InvestorAdapter : IInvestorDto
    {
        private Investor _Investor;

        public InvestorAdapter(Investor Investor)
        {
            _Investor = Investor;
        }

        public string Name
        {
            get { return _Investor.InvestorReference; }
            set { _Investor.InvestorReference = value; }
        }

        public int RPower
        {
            get { return _Investor.RatedPower * 20; }
            set { _Investor.RatedPower = value * 20; }
        }
    }
}
