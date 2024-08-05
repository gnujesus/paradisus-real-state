﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RealStateApp.Core.Application.Interfaces.Services;
using RealStateApp.Core.Application.ViewModels.UserModels;
using RealStateApp.Core.Application.Helpers;
using RealStateApp.Middlewares;
using RealStateApp.Core.Application.Services.MainServices;
using RealStateApp.Core.Application.Enums;
using RealStateApp.Core.Application.DataTransferObjects.Account;

namespace RealStateApp.Controllers
{
    public class LoginController : Controller
    {
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IUserService _userService;

        public LoginController(IHttpContextAccessor contextAccessor, IUserService userService)
        {
            _contextAccessor = contextAccessor;
            _userService = userService;
        }

        #region Index
        [ServiceFilter(typeof(LoginAuthorize))]
        public IActionResult Index()
        {
            return View(new LoginViewModel());
        }

        [ServiceFilter(typeof(LoginAuthorize))]
        [HttpPost]
        public async Task<IActionResult> Index(LoginViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            AuthenticationResponse userVm = await _userService.LoginAsync(vm);

            if (userVm != null && userVm.HasError != true)
            {
                if (userVm.Roles.Any(r => r == "Developer"))
                {
                    vm.HasError = true;
                    vm.Error = "Los usuarios desarrolladores no se pueden registrar en la webApp";
                    return View(vm);
                    
                }
                else
                {
                    HttpContext.Session.Set<AuthenticationResponse>("user", userVm);
                    return RedirectToRoute(new { controller = "Home", action = "Index" });
                }
            }
            else
            {
                vm.HasError = userVm.HasError;
                vm.Error = userVm.Error;
                return View(vm);
            }
        }

        #endregion

        public async Task<IActionResult> LogOut()
        {
            await _userService.SignOutAsync();
            HttpContext.Session.Remove("user");
            return RedirectToRoute(new { controller = "Login", action = "Index" });
        }


        #region Register
        [ServiceFilter(typeof(LoginAuthorize))]
        public IActionResult Register()
        {
            return View(new SaveUserViewModel());
        }

        [ServiceFilter(typeof(LoginAuthorize))]
        [HttpPost]
        public async Task<IActionResult> Register(SaveUserViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            if (vm.ImageFile != null && vm.ImageFile.Length > 0)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await vm.ImageFile.CopyToAsync(memoryStream);
                    vm.Image = memoryStream.ToArray();
                    //vm.EmailVerified = "True";
                }
            }

            var origin = Request.Headers["origin"];
            RegisterResponse response = await _userService.RegisterAsync(vm, origin);


            if (response.HasError)
            {
                vm.HasError = response.HasError;
                vm.Error = response.Error;
                return View(vm);
            }

            return RedirectToRoute(new { controller = "Login", action = "Index" });
        }

        [ServiceFilter(typeof(LoginAuthorize))]
        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            string response = await _userService.ConfirmEmailAsync(userId, token);
            return View("ConfirmEmail", response);
        }
        #endregion


        #region Forgot Password

        [ServiceFilter(typeof(LoginAuthorize))]
        public IActionResult ForgotPassword()
        {
            return View(new ForgotPasswordViewModel());
        }

        [ServiceFilter(typeof(LoginAuthorize))]
        [HttpPost]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            var origin = Request.Headers["origin"];
            ForgotPasswordResponse response = await _userService.ForgotPasswordAsync(vm, origin);

            if (response.HasError)
            {
                vm.HasError = response.HasError;
                vm.Error = response.Error;
                return View(vm);
            }
            return RedirectToRoute(new { controller = "Login", action = "Index" });
        }

        #endregion


        #region ResetPassword

        [ServiceFilter(typeof(LoginAuthorize))]
        public IActionResult ResetPassword(string token)
        {
            return View(new ResetPasswordViewModel { Token = token });
        }

        [ServiceFilter(typeof(LoginAuthorize))]
        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            ResetPasswordResponse response = await _userService.ResetPasswordAsync(vm);
            if (response.HasError)
            {
                vm.HasError = response.HasError;
                vm.Error = response.Error;
                return View(vm);
            }
            return RedirectToRoute(new { controller = "Login", action = "Index" });
        }

        #endregion

        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
