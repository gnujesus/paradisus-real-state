using RealStateApp.Core.Application.DataTransferObjects.Account;
using RealStateApp.Core.Application.ViewModels.UserModels;
using RealStateApp.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealStateApp.Core.Application.Interfaces.Services
{
    public interface IUserService
    {
        Task<string> ConfirmEmailAsync(string userId, string token);
        Task<ForgotPasswordResponse> ForgotPasswordAsync(ForgotPasswordViewModel vm, string origin);
        Task<AuthenticationResponse> LoginAsync(LoginViewModel vm);
        Task<RegisterResponse> RegisterAsync(SaveUserViewModel vm, string origin);
        Task<ResetPasswordResponse> ResetPasswordAsync(ResetPasswordViewModel vm);
        Task SignOutAsync();
        Task<SaveUserViewModel> GetUserByUsernameAsync(string username);
        Task<SaveUserViewModel> UpdateUserAsync(SaveUserViewModel model);
        Task<SaveUserViewModel> GetUserByIdAsync(string userId);
        Task ActivateDeactivateUserAsync(string userId, bool IsActivate);
        Task<IEnumerable<UserViewModel>> GetAllAgentsAsync();
        Task<IEnumerable<UserViewModel>> GetAllAdminsAsync();
        Task<IEnumerable<UserViewModel>> GetAllDevelopersAsync();
        Task<UserStatisticsViewModel> GetUserStatisticsAsync();
    }
}
