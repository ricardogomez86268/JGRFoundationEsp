using JGRFoundation.Shared.DTOs;

namespace JGRFoundation.API.Helpers.Strategy
{
    public class ParallelPanelStrategy : IPanelInstallationStrategy
    {
        public int CalculateInstallation(InstallationPanelDTO installationPanelDTO)
        {
            return (int)Math.Ceiling((installationPanelDTO.demandWatts * 2) / installationPanelDTO.wattsByPanel);
        }
    }
}
