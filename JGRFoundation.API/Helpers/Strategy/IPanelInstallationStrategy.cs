using JGRFoundation.Shared.DTOs;

namespace JGRFoundation.API.Helpers.Strategy
{
    public interface IPanelInstallationStrategy
    {
        int CalculateInstallation(InstallationPanelDTO installationPanelDTO);
    }
}
