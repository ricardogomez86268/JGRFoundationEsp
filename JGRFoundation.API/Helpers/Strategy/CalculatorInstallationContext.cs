using JGRFoundation.Shared.DTOs;

namespace JGRFoundation.API.Helpers.Strategy
{
    public class CalculatorInstallationContext
    {
        private IPanelInstallationStrategy _panelInstallationStrategy;        

        public void SetPanelInstallationStrategy(IPanelInstallationStrategy panelInstallationStrategy)
        {
            _panelInstallationStrategy = panelInstallationStrategy;
        }

        public int executeStrategy(InstallationPanelDTO installationPanelDTO)
        {
            if (_panelInstallationStrategy == null)
                throw new InvalidOperationException("No se ha seleccionado una estrategia de calculo.");

            return _panelInstallationStrategy.CalculateInstallation(installationPanelDTO);
        }
    }
}
