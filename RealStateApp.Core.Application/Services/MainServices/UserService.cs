using AutoMapper;
using Microsoft.AspNetCore.Http;
using RealStateApp.Core.Application.DataTransferObjects.Account;
using RealStateApp.Core.Application.Enums;
using RealStateApp.Core.Application.Interfaces.Services;
using RealStateApp.Core.Application.ViewModels.UserModels;

namespace RealStateApp.Core.Application.Services.MainServices
{
    public class UserService : IUserService
    {
        private readonly IAccountService _accountService;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IMapper _mapper;

        public UserService(IAccountService accountService, IMapper mapper, IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
            _accountService = accountService;
            _mapper = mapper;
        }

        public async Task<AuthenticationResponse> LoginAsync(LoginViewModel vm)
        {
            AuthenticationRequest loginRequest = _mapper.Map<AuthenticationRequest>(vm);
            AuthenticationResponse userResponse = await _accountService.AuthenticateAsync(loginRequest);
            return userResponse;
        }
        public async Task SignOutAsync()
        {
            await _accountService.SignOutAsync();
        }

        public async Task<RegisterResponse> RegisterAsync(SaveUserViewModel vm, string origin)
        {
            RegisterRequest registerRequest = _mapper.Map<RegisterRequest>(vm);

            if(vm.Type_User == Roles.Client.ToString())
            {
                return await _accountService.RegisterClientUserAsync(registerRequest, origin);
            }
            else if (vm.Type_User == Roles.Developer.ToString())
            {

                return await _accountService.RegisterDeveloperUserAsync(registerRequest);
            }
            else
            {
                return await _accountService.RegisterAgentUserAsync(registerRequest);
            }
        }

        public async Task<string> ConfirmEmailAsync(string userId, string token)
        {
            return await _accountService.ConfirmAccountAsync(userId, token);
        }

        public async Task<ForgotPasswordResponse> ForgotPasswordAsync(ForgotPasswordViewModel vm, string origin)
        {
            ForgotPasswordRequest forgotRequest = _mapper.Map<ForgotPasswordRequest>(vm);
            return await _accountService.ForgotPasswordAsync(forgotRequest, origin);
        }

        public async Task<ResetPasswordResponse> ResetPasswordAsync(ResetPasswordViewModel vm)
        {
            ResetPasswordRequest resetRequest = _mapper.Map<ResetPasswordRequest>(vm);
            return await _accountService.ResetPasswordAsync(resetRequest);
        }

        public async Task<SaveUserViewModel> GetUserByUsernameAsync(string username)
        {
            SaveUserViewModel user = await _accountService.GetUserForUsername(username);
            return user;
        }

        public async Task<SaveUserViewModel> UpdateUserAsync(SaveUserViewModel model)
        {
            SaveUserViewModel user = await _accountService.UpdateUserAsync(model);
            return user;
        }

        public async Task<SaveUserViewModel> GetUserByIdAsync(string userId)
        {
            return await _accountService.GetUserByIdAsync(userId);
        }

        public async Task ActivateDeactivateUserAsync(string userId, bool IsActivate)
        {
            await _accountService.ActivateDeactivateUserAsync(userId, IsActivate);
        }


        public async Task<IEnumerable<UserViewModel>> GetAllAgentsAsync()
        {
            return await _accountService.GetAgentsAsync();
        }

        public async Task<IEnumerable<UserViewModel>> GetAllAdminsAsync()
        {
            return await _accountService.GetAdminsAsync();
        }

        public async Task<IEnumerable<UserViewModel>> GetAllDevelopersAsync()
        {
            return await _accountService.GetDevelopersAsync();
        }

        public async Task<UserStatisticsViewModel> GetUserStatisticsAsync()
        {
            return await _accountService.GetUserStatisticsAsync();
        }

    }
}
