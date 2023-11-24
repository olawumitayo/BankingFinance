using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using TheCoreBanking.Finance.Data;
using TheCoreBanking.Finance.Models;

namespace TheCoreBanking.Finance.Services
{
    public interface IIdentityTransaction
    {
        Task<IdentityResult> Add(ApplicationUser appUser, string password);
        Task<ApplicationUser> FindByNameAsync(string userName);
        Task<bool> CheckPasswordAsync(ApplicationUser user, string password);
        Task<ApplicationUser> FindByLoginAsync(string loginProvider, string providerKey);
        Task<IdentityResult> CreateAsync(ApplicationUser user);
        Task<IdentityResult> CreateAsync(ApplicationUser user, string password);
        Task<IdentityResult> AddLoginAsync(ApplicationUser user, UserLoginInfo login);
        Task<SignInResult> PasswordSignInAsync(ApplicationUser user, string password, bool isPersistent, bool lockoutOnFailure);
        Task<SignInResult> PasswordSignInAsync(string userName, string password, bool isPersistent, bool lockoutOnFailure);
        Task<ApplicationUser> GetTwoFactorAuthenticationUserAsync();
        
        Task<SignInResult> TwoFactorAuthenticatorSignInAsync(string code, bool isPersistent, bool rememberClient);
        string GetUserId(ClaimsPrincipal principal);
        Task SignInAsync(ApplicationUser user, bool isPersistent, string authenticationMethod = null);
        Task SignInAsync(ApplicationUser user, AuthenticationProperties authenticationProperties, string authenticationMethod = null);
        Task<string> GenerateEmailConfirmationTokenAsync(ApplicationUser user);
        Task<SignInResult> TwoFactorRecoveryCodeSignInAsync(string recoveryCode);
        AuthenticationProperties ConfigureExternalAuthenticationProperties(string provider, string redirectUrl, string userId = null);

        Task<ApplicationUser> FindByEmailAsync(string email);
        Task<IdentityResult> ResetPasswordAsync(ApplicationUser user, string token, string newPassword);
        Task<ExternalLoginInfo> GetExternalLoginInfoAsync(string expectedXsrf = null);
        Task<SignInResult> ExternalLoginSignInAsync(string loginProvider, string providerKey, bool isPersistent, bool bypassTwoFactor);
        Task<SignInResult> ExternalLoginSignInAsync(string loginProvider, string providerKey, bool isPersistent);
        Task<string> GeneratePasswordResetTokenAsync(ApplicationUser user);
        Task<bool> IsEmailConfirmedAsync(ApplicationUser user);
        Task<ApplicationUser> FindByIdAsync(string userId);
        Task<IdentityResult> ConfirmEmailAsync(ApplicationUser user, string token);
    }
    public class IdentityTransaction : IIdentityTransaction
    {

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signManager;
        private readonly RoleManager<IdentityRole> _roleManager;


        public IdentityTransaction(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, SignInManager<ApplicationUser> signManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _signManager = signManager;

        }
        public async Task<IdentityResult> Add(ApplicationUser applicationUser, string password)
        {
            return await _userManager.CreateAsync(applicationUser, password);
        }

        public async Task<ApplicationUser> FindByNameAsync(string userName)
        {
            return await _userManager.FindByNameAsync(userName);
        }

        public async Task<bool> CheckPasswordAsync(ApplicationUser user, string password)
        {
            return await _userManager.CheckPasswordAsync(user, password);
        }

        public async Task<ApplicationUser> FindByLoginAsync(string loginProvider, string providerKey)
        {
            return await _userManager.FindByLoginAsync(loginProvider, providerKey);
        }
        public async Task<IdentityResult> CreateAsync(ApplicationUser user)
        {
            return await _userManager.CreateAsync(user);
        }

        public async Task<IdentityResult> CreateAsync(ApplicationUser user, string password)
        {
            return await _userManager.CreateAsync(user, password);
        }
        public async Task<IdentityResult> AddLoginAsync(ApplicationUser user, UserLoginInfo login)
        {
            return await _userManager.AddLoginAsync(user, login);
        }

        /////
        public async Task<SignInResult> PasswordSignInAsync(ApplicationUser user,  string password, bool isPersistent, bool lockoutOnFailure)
        {
            return await _signManager.PasswordSignInAsync(user, password, isPersistent, lockoutOnFailure);
        }
        public async Task<SignInResult> PasswordSignInAsync(string userName, string password, bool isPersistent, bool lockoutOnFailure)
        {
            return await _signManager.PasswordSignInAsync(userName, password, isPersistent, lockoutOnFailure);
        }

        public async Task<ApplicationUser> GetTwoFactorAuthenticationUserAsync()
        {
            return await _signManager.GetTwoFactorAuthenticationUserAsync();
        }
      
        public string GetUserId(ClaimsPrincipal principal)
        {
            return _userManager.GetUserId(principal);
        }       
        public async Task<SignInResult> TwoFactorAuthenticatorSignInAsync(string code, bool isPersistent, bool rememberClient)
        {
            return await _signManager.TwoFactorAuthenticatorSignInAsync(code, isPersistent, rememberClient);
        }
        public async Task<SignInResult> TwoFactorRecoveryCodeSignInAsync(string recoveryCode)
        {
            return await _signManager.TwoFactorRecoveryCodeSignInAsync(recoveryCode);
        }
        public async Task<string> GenerateEmailConfirmationTokenAsync(ApplicationUser user)
        {
            return await _userManager.GenerateEmailConfirmationTokenAsync(user);
        }
        public async Task SignInAsync(ApplicationUser user, AuthenticationProperties authenticationProperties, string authenticationMethod = null)
        {
            await _signManager.SignInAsync(user, authenticationProperties, authenticationMethod);
        }
        public async Task SignInAsync(ApplicationUser user, bool isPersistent, string authenticationMethod = null)
        {
            await _signManager.SignInAsync(user, isPersistent, authenticationMethod);
        }

        public AuthenticationProperties  ConfigureExternalAuthenticationProperties(string provider, string redirectUrl, string userId = null)
        {
           return _signManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl, userId);
        }
        public async Task<ExternalLoginInfo> GetExternalLoginInfoAsync(string expectedXsrf = null)
        {
            return await _signManager.GetExternalLoginInfoAsync(expectedXsrf);
        }
      
        public async Task<SignInResult> ExternalLoginSignInAsync(string loginProvider, string providerKey, bool isPersistent)
        {
            return await _signManager.ExternalLoginSignInAsync(loginProvider, providerKey, isPersistent);
        }
        public async Task<SignInResult> ExternalLoginSignInAsync(string loginProvider, string providerKey, bool isPersistent, bool bypassTwoFactor)
        {
            return await _signManager.ExternalLoginSignInAsync(loginProvider, providerKey, isPersistent, bypassTwoFactor);
        }
        public async Task<ApplicationUser> FindByIdAsync(string userId) {
            return await _userManager.FindByIdAsync(userId);
        }

        public async Task<IdentityResult> ConfirmEmailAsync(ApplicationUser user, string token)
        {
            return await _userManager.ConfirmEmailAsync(user, token);
        }
        public async Task<bool> IsEmailConfirmedAsync(ApplicationUser user)
        {
            return await _userManager.IsEmailConfirmedAsync(user);
        }
        public async Task<string> GeneratePasswordResetTokenAsync(ApplicationUser user)
        {
            return await _userManager.GeneratePasswordResetTokenAsync(user);
        }
        public async Task<ApplicationUser> FindByEmailAsync(string email)
        {
            return await _userManager.FindByEmailAsync(email);
        }
        public async Task<IdentityResult> ResetPasswordAsync(ApplicationUser user, string token, string newPassword)
        {
            return await _userManager.ResetPasswordAsync(user, token, newPassword);
        }
    }
}

