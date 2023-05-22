using JGRFoundation.Shared.Responses;

namespace JGRFoundation.API.Services
{
    public interface IApiService
    {
        Task<Response> GetListAsync<T>(string servicePrefix, string controller);
    }
}
