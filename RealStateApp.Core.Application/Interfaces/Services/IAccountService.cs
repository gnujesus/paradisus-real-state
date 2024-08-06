using RealStateApp.Core.Application.DataTransferObjects.Account;
using RealStateApp.Core.Application.ViewModels.UserModels;
using RealStateApp.Core.Domain.Entities;

namespace RealStateApp.Core.Application.Interfaces.Services
{
    public interface IAccountService
    {

        #region General Methods
        Task<AuthenticationResponse> AuthenticateAsync(AuthenticationRequest request);
        Task<string> ConfirmAccountAsync(string userId, string token);
        Task<ForgotPasswordResponse> ForgotPasswordAsync(ForgotPasswordRequest request, string origin);
        Task<ResetPasswordResponse> ResetPasswordAsync(ResetPasswordRequest request);
        Task SignOutAsync();
        Task<SaveUserViewModel> UpdateUserAsync(SaveUserViewModel model);
        Task<SaveUserViewModel> GetUserForUsername(string username);
        Task ActivateDeactivateUserAsync(string userId, bool isActive);
        Task<UserStatisticsViewModel> GetUserStatisticsAsync();
        #endregion

        #region Client Methods
        Task<RegisterResponse> RegisterClientUserAsync(RegisterRequest request, string origin);
        #endregion

        #region Agent Methods
        Task<RegisterResponse> RegisterAgentUserAsync(RegisterRequest request);
        Task<IEnumerable<Agent>> GetAgentsAsync();
        Task<Agent> GetAgentByIdAsync(string id);
        Task<Agent> GetAgentByNameAsync(string name);
        Task<Agent> UpdateAgentAsync(string id, bool status);
        #endregion

        #region Developers Methods
        Task<RegisterResponse> RegisterDeveloperUserAsync(RegisterRequest request);
        Task<IEnumerable<UserViewModel>> GetDevelopersAsync();
        #endregion

        #region Admins Methods
        Task<RegisterResponse> RegisterAdminUserAsync(RegisterRequest request);
        Task<IEnumerable<UserViewModel>> GetAdminsAsync();
        #endregion

    }
}
