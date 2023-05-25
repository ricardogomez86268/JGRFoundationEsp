using JGRFoundation.Shared.Responses;

namespace JGRFoundation.API.Helpers
{
    public interface IHomesHelper
    {
        Task<Response> ProcessHomeAsync(string email, string coordinate);
    }
}
