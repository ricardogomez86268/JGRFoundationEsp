
namespace JGRFoundation.API.Helpers.Builder
{
    public class DirectorQuery
    {
        public IQueryBuilerContract constructInvestors(IQueryBuilerContract builder)
        {
            builder.Update("Investors");
            return builder;
        }
        public IQueryBuilerContract constructPanels(IQueryBuilerContract builder)
        {
            builder.Update("Panels");
            return builder;
        }
        public IQueryBuilerContract constructBatteries(IQueryBuilerContract builder)
        {
            builder.Update("Batteries");
            return builder;
        }
    }
}
