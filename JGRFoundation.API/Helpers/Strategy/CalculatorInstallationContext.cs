using JGRFoundation.Shared.DTOs;

namespace JGRFoundation.API.Helpers.Strategy
{
    public class CalculatorInstallationContext
    {
        private IPanelInstallationStrategy _panelInstallationStrategy;

        public CalculatorInstallationContext(IPanelInstallationStrategy panelInstallationStrategy)
        {
            _panelInstallationStrategy = panelInstallationStrategy;
        }
        public int executeStrategy(InstallationPanelDTO installationPanelDTO)
        {
            return _panelInstallationStrategy.CalculateInstallation(installationPanelDTO);
        }
    }
}
