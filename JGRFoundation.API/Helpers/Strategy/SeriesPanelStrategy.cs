using JGRFoundation.Shared.DTOs;

namespace JGRFoundation.API.Helpers.Strategy
{
    public class SeriesPanelStrategy : IPanelInstallationStrategy
    {
        public int CalculateInstallation(InstallationPanelDTO installationPanelDTO)
        {
            return (int)Math.Ceiling(installationPanelDTO.demandWatts / installationPanelDTO.wattsByPanel);            
        }
    }
}
