using B2B_SiteListing.Service.Models;

namespace B2B_SiteListing.Service.Contracts
{
    public interface ILogInDetailsService
    {
        Task<Guid> AddLogInDetails(LogInDetailsViewModel model);
        Task UpdateLogInDetails(LogInDetailsViewModel model, string userId);
        Task DeleteLogInDetails(string userId);
    }
}
