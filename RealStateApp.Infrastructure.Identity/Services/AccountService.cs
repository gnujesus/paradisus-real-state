using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using RealStateApp.Core.Application.DataTransferObjects.Account;
using RealStateApp.Core.Application.Enums;
using RealStateApp.Core.Application.Interfaces.Services;
using RealStateApp.Core.Application.ViewModels.UserModels;
using RealStateApp.Core.Domain.Settings;
using RealStateApp.Infrastructure.Identity.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace RealStateApp.Infrastructure.Identity.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IEmailService _emailService;
        private readonly JWTSettings _jwtSettings;

        public AccountService(UserManager<ApplicationUser> userManager, 
            SignInManager<ApplicationUser> signInManager, 
            IEmailService emailService,
            IOptions<JWTSettings> jwtSettings)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailService = emailService;
            _jwtSettings = jwtSettings.Value;
        }

        #region General Methods

        // gnu: Modified this to make it returna SaveUserViewModel. Needed to make the update method.
        public async Task<SaveUserViewModel> GetUserByIdAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);

            var rolesList = await _userManager.GetRolesAsync(user).ConfigureAwait(false);

            var UserModel = new SaveUserViewModel
            {
                Id = userId,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Phone = user.PhoneNumber,
                Image = user.Image,
                Email = user.Email,
                UserName = user.UserName,
                Password = user.PasswordHash,
                IsActive = user.IsActive,
                Type_User = rolesList.ToList()[0],
                EmailVerified = user.Email
            };

            if (user == null)
            {
                throw new Exception("User not found");
            }

            return UserModel;
        }

        public async Task<SaveUserViewModel> GetUserForUsername(string username)
        {
            var user = await _userManager.FindByNameAsync(username);

            if (user == null)
            {
                throw new Exception("User not found");
            }

            var saveUserViewModel = new SaveUserViewModel
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                UserName = user.UserName,
                Phone = user.PhoneNumber,
                Image = user.Image
            };

            return saveUserViewModel;
        }

        public async Task<AuthenticationResponse> AuthenticateAsync(AuthenticationRequest request)
        {
            AuthenticationResponse response = new();

            var user = await _userManager.FindByNameAsync(request.UserName);
            if (user == null)
            {
                response.HasError = true;
                response.Error = $"No Accounts registered with {request.UserName}";
                return response;
            }

            var result = await _signInManager.PasswordSignInAsync(user.UserName, request.Password, false, lockoutOnFailure: false);
            if (!result.Succeeded)
            {
                response.HasError = true;
                response.Error = $"Invalid credentials for {request.UserName}";
                return response;
            }
            if (!user.EmailConfirmed)
            {
                response.HasError = true;
                response.Error = $"Account no confirmed for {request.UserName}";
                return response;
            }
            if (!user.IsActive)
            {
                response.HasError = true;
                response.Error = $"Account is not active for {request.UserName}";
                return response;
            }

            response.Id = user.Id;
            response.Email = user.Email;
            response.UserName = user.UserName;

            var rolesList = await _userManager.GetRolesAsync(user).ConfigureAwait(false);

            response.Roles = rolesList.ToList();
            response.IsActive = user.EmailConfirmed;

            JwtSecurityToken jwtSecurityToken = await GenerateJWToken(user);
            response.JWToken = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);

            var refreshToken = GenerateRefreshToken();
            response.RefreshToken = refreshToken.Token;

            return response;
        }

        public async Task SignOutAsync()
        {
            await _signInManager.SignOutAsync();
        }

        #region JWT Methods
        private async Task<JwtSecurityToken> GenerateJWToken(ApplicationUser user)
        {
            var userClaims = await _userManager.GetClaimsAsync(user);
            var roles = await _userManager.GetRolesAsync(user);

            var roleClaims = new List<Claim>();

            foreach (var role in roles)
            {
                roleClaims.Add(new Claim("roles", role));
            }

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName!),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim("uid", user.Id)
            }
            .Union(userClaims)
            .Union(roleClaims);

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha512);

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_jwtSettings.DurationInMinutes),
                signingCredentials: signingCredentials);

            return jwtSecurityToken;
        }

        private RefreshToken GenerateRefreshToken()
        {
            return new RefreshToken
            {
                Token = "",
                Expires = DateTime.UtcNow.AddDays(7),
                Created = DateTime.UtcNow,
            };
        }

        private string RandomTokenString()
        {
            var randomBytes = new byte[40];

            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomBytes);
                return Convert.ToBase64String(randomBytes).Replace("-", "");
            }
        }
        #endregion

        public async Task<string> ConfirmAccountAsync(string userId, string token)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return $"No accounts registered with this user";
            }
            user.IsActive = true;
            token = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(token));
            var result = await _userManager.ConfirmEmailAsync(user, token);
            if (result.Succeeded)
            {
                return $"Account confirmed for {user.Email}. You can now use the app";
            }
            else
            {
                return $"An error occurred while confirming {user.Email}.";
            }
        }

        public async Task<ForgotPasswordResponse> ForgotPasswordAsync(ForgotPasswordRequest request, string origin)
        {
            ForgotPasswordResponse response = new()
            {
                HasError = false
            };

            var user = await _userManager.FindByNameAsync(request.UserName);

            if (user == null)
            {
                response.HasError = true;
                response.Error = $"No Accounts registered with {request.UserName}";
                return response;
            }

            var verificationUri = await SendForgotPasswordUri(user, origin);

            await _emailService.SendAsync(new Core.Application.DataTransferObjects.Email.EmailRequest()
            {
                To = user.Email,
                Body = $"Please reset your account visiting this URL {verificationUri}",
                Subject = "reset password"
            });


            return response;
        }

        public async Task<ResetPasswordResponse> ResetPasswordAsync(ResetPasswordRequest request)
        {
            ResetPasswordResponse response = new()
            {
                HasError = false
            };

            var user = await _userManager.FindByEmailAsync(request.Email);

            if (user == null)
            {
                response.HasError = true;
                response.Error = $"No Accounts registered with {request.Email}";
                return response;
            }

            request.Token = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(request.Token));
            var result = await _userManager.ResetPasswordAsync(user, request.Token, request.Password);

            if (!result.Succeeded)
            {
                response.HasError = true;
                response.Error = $"An error occurred while reset password";
                return response;
            }

            return response;
        }

        private async Task<string> SendVerificationEmailUri(ApplicationUser user, string origin)
        {
            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
            var route = "Login/ConfirmEmail";
            var Uri = new Uri(string.Concat($"{origin}/", route));
            var verificationUri = QueryHelpers.AddQueryString(Uri.ToString(), "userId", user.Id);
            verificationUri = QueryHelpers.AddQueryString(verificationUri, "token", code);

            return verificationUri;
        }
        private async Task<string> SendForgotPasswordUri(ApplicationUser user, string origin)
        {
            var code = await _userManager.GeneratePasswordResetTokenAsync(user);
            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
            var route = "Login/ResetPassword";
            var Uri = new Uri(string.Concat($"{origin}/", route));
            var verificationUri = QueryHelpers.AddQueryString(Uri.ToString(), "token", code);

            return verificationUri;
        }

        public async Task<SaveUserViewModel> UpdateUserAsync(SaveUserViewModel model)
        {
            var user = await _userManager.FindByIdAsync(model.Id.ToString());

            if (user == null)
            {
                throw new Exception("User not found");
            }

            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.Email = model.Email;
            user.UserName = model.UserName;
            user.PhoneNumber = model.Phone;
            user.IsActive = model.IsActive;
            user.Image = model.Image ?? user.Image;

            // Encriptar la contraseña si se proporciona una nueva
            if (!string.IsNullOrEmpty(model.Password))
            {
                user.PasswordHash = _userManager.PasswordHasher.HashPassword(user, model.Password);
            }

            var result = await _userManager.UpdateAsync(user);

            if (!result.Succeeded)
            {
                throw new Exception("Error updating user");
            }

            return new SaveUserViewModel
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                UserName = user.UserName,
                Phone = user.PhoneNumber,
                Image = user.Image
            };
        }

        public async Task ActivateDeactivateUserAsync(string userId, bool isActive)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                throw new Exception("User not found");
            }

            user.IsActive = isActive;
            var result = await _userManager.UpdateAsync(user);

            if (!result.Succeeded)
            {
                throw new Exception("Error updating user");
            }
        }


        public async Task<UserStatisticsViewModel> GetUserStatisticsAsync()
        {
            var activeClientsCount = await GetUsersCountByRoleAndStatusAsync(Roles.Client.ToString(), true);
            var inactiveClientsCount = await GetUsersCountByRoleAndStatusAsync(Roles.Client.ToString(), false);
            var activeAgentsCount = await GetUsersCountByRoleAndStatusAsync(Roles.Agent.ToString(), true);
            var inactiveAgentsCount = await GetUsersCountByRoleAndStatusAsync(Roles.Agent.ToString(), false);
            var activeDevelopersCount = await GetUsersCountByRoleAndStatusAsync(Roles.Developer.ToString(), true);
            var inactiveDevelopersCount = await GetUsersCountByRoleAndStatusAsync(Roles.Developer.ToString(), false);

            return new UserStatisticsViewModel
            {
                ActiveClientsCount = activeClientsCount,
                InactiveClientsCount = inactiveClientsCount,
                ActiveAgentsCount = activeAgentsCount,
                InactiveAgentsCount = inactiveAgentsCount,
                ActiveDevelopersCount = activeDevelopersCount,
                InactiveDevelopersCount = inactiveDevelopersCount
            };
        }

        private async Task<int> GetUsersCountByRoleAndStatusAsync(string role, bool isActive)
        {
            var usersInRole = await _userManager.GetUsersInRoleAsync(role);
            return usersInRole.Count(u => u.IsActive == isActive);
        }

        #endregion

        #region Client Methods
        public async Task<RegisterResponse> RegisterClientUserAsync(RegisterRequest request, string origin)
        {
            RegisterResponse response = new()
            {
                HasError = false
            };

            var userWithSameUserName = await _userManager.FindByNameAsync(request.UserName);
            if (userWithSameUserName != null)
            {
                response.HasError = true;
                response.Error = $"username '{request.UserName}' is already taken.";
                return response;
            }

            var userWithSameEmail = await _userManager.FindByEmailAsync(request.Email);
            if (userWithSameEmail != null)
            {
                response.HasError = true;
                response.Error = $"Email '{request.Email}' is already registered.";
                return response;
            }

            var user = new ApplicationUser
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                UserName = request.UserName,
                PhoneNumber = request.Phone,
                Image = request.Image,

            };

            var result = await _userManager.CreateAsync(user, request.Password);
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, Roles.Client.ToString());
                var verificationUri = await SendVerificationEmailUri(user, origin);
                await _emailService.SendAsync(new Core.Application.DataTransferObjects.Email.EmailRequest()
                {
                    To = user.Email,
                    Body = $"Please confirm your account visiting this URL {verificationUri}",
                    Subject = "Confirm registration"
                });
            }
            else
            {
                response.HasError = true;
                response.Error = $"An error occurred trying to register the user.";
                return response;
            }

            return response;
        }



        #endregion

        #region Agents Methods
        public async Task<RegisterResponse> RegisterAgentUserAsync(RegisterRequest request)
        {
            RegisterResponse response = new()
            {
                HasError = false
            };

            var userWithSameUserName = await _userManager.FindByNameAsync(request.UserName);
            if (userWithSameUserName != null)
            {
                response.HasError = true;
                response.Error = $"username '{request.UserName}' is already taken.";
                return response;
            }

            var userWithSameEmail = await _userManager.FindByEmailAsync(request.Email);
            if (userWithSameEmail != null)
            {
                response.HasError = true;
                response.Error = $"Email '{request.Email}' is already registered.";
                return response;
            }

            var user = new ApplicationUser
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                UserName = request.UserName,
                PhoneNumber = request.Phone,
                Image = request.Image,
                IsActive = false
            };

            var result = await _userManager.CreateAsync(user, request.Password);
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, Roles.Agent.ToString());
            }
            else
            {
                response.HasError = true;
                response.Error = $"An error occurred trying to register the user.";
                return response;
            }

            return response;
        }

        public async Task<IEnumerable<UserViewModel>> GetAgentsAsync()
        {
            var agents = await _userManager.GetUsersInRoleAsync(Roles.Agent.ToString());
            return agents.OrderBy(a => a.LastName).Select(a => new UserViewModel
            {
                Id = a.Id,
                FirstName = a.FirstName,
                LastName = a.LastName,
                Username = a.UserName,
                Email = a.Email,
                Phone = a.PhoneNumber,
                IsActive = a.IsActive,
            }).ToList();
        }

        public async Task<UserViewModel> GetAgentByIdAsync(string id)
        {
            var agent = await _userManager.Users
                .FirstOrDefaultAsync(u => u.Id == id);

            if (agent == null)
            {
                throw new Exception("Agent not found");
            }

            var roles = await _userManager.GetRolesAsync(agent);
            if (!roles.Contains(Roles.Agent.ToString()))
            {
                throw new Exception("That user is not an agent");
            }

            return new UserViewModel
            {
                Id = agent.Id,
                FirstName = agent.FirstName,
                LastName = agent.LastName,
                Email = agent.Email,
                Phone = agent.PhoneNumber,
            };
        }

        public async Task<UserViewModel> GetAgentByNameAsync(string name)
        {
            var agent = await _userManager.Users
                .FirstOrDefaultAsync(u => (u.FirstName + " " + u.LastName) == name);

            if (agent == null)
            {
                throw new Exception("Agent not found");
            }

            var roles = await _userManager.GetRolesAsync(agent);
            if (!roles.Contains(Roles.Agent.ToString()))
            {
                throw new Exception("User is not an agent");
            }

            return new UserViewModel
            {
                Id = agent.Id,
                FirstName = agent.FirstName,
                LastName = agent.LastName,
                Email = agent.Email,
                IsActive = agent.IsActive
            };
        }

        public async Task<UserViewModel> UpdateAgentAsync(string id, bool status)
        {
            var agent = await _userManager.FindByIdAsync(id);

            if (agent is null)
            {
                throw new Exception("Agent not found");
            }

            agent.IsActive = status;

            var result = await _userManager.UpdateAsync(agent);

            if (!result.Succeeded)
            {
                throw new Exception("Error updating agent");
            }

            return new UserViewModel
            {
                Id = agent.Id,
                FirstName = agent.FirstName,
                LastName = agent.LastName,
                Email = agent.Email,
                IsActive = agent.IsActive
            };
        }
        #endregion

        #region Developers Methods

        public async Task<RegisterResponse> RegisterDeveloperUserAsync(RegisterRequest request)
        {
            RegisterResponse response = new()
            {
                HasError = false
            };

            var userWithSameUserName = await _userManager.FindByNameAsync(request.UserName);
            if (userWithSameUserName != null)
            {
                response.HasError = true;
                response.Error = $"username '{request.UserName}' is already taken.";
                return response;
            }

            var userWithSameEmail = await _userManager.FindByEmailAsync(request.Email);
            if (userWithSameEmail != null)
            {
                response.HasError = true;
                response.Error = $"Email '{request.Email}' is already registered.";
                return response;
            }

            var user = new ApplicationUser
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                UserName = request.UserName,
                PhoneNumber = request.Phone,
                Image = request.Image,
                IsActive = true // Developer user starts active
            };

            var result = await _userManager.CreateAsync(user, request.Password);
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, Roles.Developer.ToString());
            }
            else
            {
                response.HasError = true;
                response.Error = $"An error occurred trying to register the user.";
                return response;
            }

            return response;
        }

        public async Task<IEnumerable<UserViewModel>> GetDevelopersAsync()
        {
            var developers = await _userManager.GetUsersInRoleAsync(Roles.Developer.ToString());
            return developers.OrderBy(d => d.LastName).Select(d => new UserViewModel
            {
                Id = d.Id,
                FirstName = d.FirstName,
                LastName = d.LastName,
                Email = d.Email,
                Username = d.UserName,
                IsActive = d.IsActive,
                Phone = d.PhoneNumber,
                Image = d.Image
            }).ToList();
        }

        #endregion

        #region Admins Methods

        public async Task<RegisterResponse> RegisterAdminUserAsync(RegisterRequest request)
        {
            RegisterResponse response = new()
            {
                HasError = false
            };

            var userWithSameUserName = await _userManager.FindByNameAsync(request.UserName);
            if (userWithSameUserName != null)
            {
                response.HasError = true;
                response.Error = $"username '{request.UserName}' is already taken.";
                return response;
            }

            var userWithSameEmail = await _userManager.FindByEmailAsync(request.Email);
            if (userWithSameEmail != null)
            {
                response.HasError = true;
                response.Error = $"Email '{request.Email}' is already registered.";
                return response;
            }

            var user = new ApplicationUser
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                UserName = request.UserName,
                PhoneNumber = request.Phone,
                Image = request.Image,
                IsActive = true // Admin user starts active
            };

            var result = await _userManager.CreateAsync(user, request.Password);
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, Roles.Admin.ToString());
            }
            else
            {
                response.HasError = true;
                response.Error = $"An error occurred trying to register the user.";
                return response;
            }

            return response;
        }
        public async Task<IEnumerable<UserViewModel>> GetAdminsAsync()
        {
            var admins = await _userManager.GetUsersInRoleAsync(Roles.Admin.ToString());
            return admins.OrderBy(a => a.LastName).Select(a => new UserViewModel
            {
                Id = a.Id,
                FirstName = a.FirstName,
                LastName = a.LastName,
                Email = a.Email,
                Username = a.UserName,
                Phone = a.PhoneNumber,
                Image = a.Image
            }).ToList();
        }

        #endregion

    }
}
