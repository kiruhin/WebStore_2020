using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using WebStore.DomainNew.Entities;
using WebStore.Interfaces;

namespace WebStore.Services
{
    public class CustomUserStore :
        IUserStore<User>,
        IUserLoginStore<User>,
        IUserRoleStore<User>,
        IUserClaimStore<User>,
        IUserPasswordStore<User>,
        IUserTwoFactorStore<User>,
        IUserEmailStore<User>,
        IUserPhoneNumberStore<User>,
        IUserLockoutStore<User>
    {
        private readonly IUsersClient _client;
 
        public CustomUserStore(IUsersClient client)
        {
            _client = client;
        }
 
        public void Dispose()
        {
            _client.Dispose();
        }
 
        public Task<string> GetUserIdAsync(User user, CancellationToken cancellationToken)
        {
            return _client.GetUserIdAsync(user, cancellationToken);
        }
 
        public Task<string> GetUserNameAsync(User user, CancellationToken cancellationToken)
        {
            return _client.GetUserNameAsync(user, cancellationToken);
        }
 
        public Task SetUserNameAsync(User user, string userName, CancellationToken cancellationToken)
        {
            return _client.SetUserNameAsync(user, userName, cancellationToken);
        }
 
        public Task<string> GetNormalizedUserNameAsync(User user, CancellationToken cancellationToken)
        {
            return _client.GetNormalizedUserNameAsync(user, cancellationToken);
        }
 
        public Task SetNormalizedUserNameAsync(User user, string normalizedName, CancellationToken cancellationToken)
        {
            return _client.SetNormalizedUserNameAsync(user, normalizedName, cancellationToken);
        }
 
        public Task<IdentityResult> CreateAsync(User user, CancellationToken cancellationToken)
        {
            return _client.CreateAsync(user, cancellationToken);
        }
 
        public Task<IdentityResult> UpdateAsync(User user, CancellationToken cancellationToken)
        {
            return _client.UpdateAsync(user, cancellationToken);
        }
 
        public Task<IdentityResult> DeleteAsync(User user, CancellationToken cancellationToken)
        {
            return _client.DeleteAsync(user, cancellationToken);
        }
 
        public Task<User> FindByIdAsync(string userId, CancellationToken cancellationToken)
        {
            return _client.FindByIdAsync(userId, cancellationToken);
        }
 
        public Task<User> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
        {
            return _client.FindByNameAsync(normalizedUserName, cancellationToken);
        }
 
        public Task AddToRoleAsync(User user, string roleName, CancellationToken cancellationToken)
        {
            return _client.AddToRoleAsync(user, roleName, cancellationToken);
        }
 
        public Task RemoveFromRoleAsync(User user, string roleName, CancellationToken cancellationToken)
        {
            return _client.RemoveFromRoleAsync(user, roleName, cancellationToken);
        }
 
        public Task<IList<string>> GetRolesAsync(User user, CancellationToken cancellationToken)
        {
            return _client.GetRolesAsync(user, cancellationToken);
        }
 
        public Task<bool> IsInRoleAsync(User user, string roleName, CancellationToken cancellationToken)
        {
            return _client.IsInRoleAsync(user, roleName, cancellationToken);
        }
 
        public Task<IList<User>> GetUsersInRoleAsync(string roleName, CancellationToken cancellationToken)
        {
            return _client.GetUsersInRoleAsync(roleName, cancellationToken);
        }
 
        public async Task SetPasswordHashAsync(User user, string passwordHash, CancellationToken cancellationToken)
        {
            await _client.SetPasswordHashAsync(user, passwordHash, cancellationToken);
        }
 
        public Task<string> GetPasswordHashAsync(User user, CancellationToken cancellationToken)
        {
            return _client.GetPasswordHashAsync(user, cancellationToken);
        }
 
        public Task<bool> HasPasswordAsync(User user, CancellationToken cancellationToken)
        {
            return _client.HasPasswordAsync(user, cancellationToken);
        }
 
        public Task<IList<Claim>> GetClaimsAsync(User user, CancellationToken cancellationToken)
        {
            return _client.GetClaimsAsync(user, cancellationToken);
        }
 
        public Task AddClaimsAsync(User user, IEnumerable<Claim> claims, CancellationToken cancellationToken)
        {
            return _client.AddClaimsAsync(user, claims, cancellationToken);
        }
 
        public Task ReplaceClaimAsync(User user, Claim claim, Claim newClaim, CancellationToken cancellationToken)
        {
            return _client.ReplaceClaimAsync(user, claim, newClaim, cancellationToken);
        }
 
        public Task RemoveClaimsAsync(User user, IEnumerable<Claim> claims, CancellationToken cancellationToken)
        {
            return _client.RemoveClaimsAsync(user, claims, cancellationToken);
        }
 
        public Task<IList<User>> GetUsersForClaimAsync(Claim claim, CancellationToken cancellationToken)
        {
            return _client.GetUsersForClaimAsync(claim, cancellationToken);
        }
 
        public Task SetTwoFactorEnabledAsync(User user, bool enabled, CancellationToken cancellationToken)
        {
            return _client.SetTwoFactorEnabledAsync(user, enabled, cancellationToken);
        }
 
        public Task<bool> GetTwoFactorEnabledAsync(User user, CancellationToken cancellationToken)
        {
            return _client.GetTwoFactorEnabledAsync(user, cancellationToken);
        }
 
        public Task SetEmailAsync(User user, string email, CancellationToken cancellationToken)
        {
            return _client.SetEmailAsync(user, email, cancellationToken);
        }
 
        public Task<string> GetEmailAsync(User user, CancellationToken cancellationToken)
        {
            return _client.GetEmailAsync(user, cancellationToken);
        }
 
        public Task<bool> GetEmailConfirmedAsync(User user, CancellationToken cancellationToken)
        {
            return _client.GetEmailConfirmedAsync(user, cancellationToken);
        }
 
        public Task SetEmailConfirmedAsync(User user, bool confirmed, CancellationToken cancellationToken)
        {
            return _client.SetEmailConfirmedAsync(user, confirmed, cancellationToken);
        }
 
        public Task<User> FindByEmailAsync(string normalizedEmail, CancellationToken cancellationToken)
        {
            return _client.FindByEmailAsync(normalizedEmail, cancellationToken);
        }
 
        public Task<string> GetNormalizedEmailAsync(User user, CancellationToken cancellationToken)
        {
            return _client.GetNormalizedEmailAsync(user, cancellationToken);
        }
 
        public Task SetNormalizedEmailAsync(User user, string normalizedEmail, CancellationToken cancellationToken)
        {
            return _client.SetNormalizedEmailAsync(user, normalizedEmail, cancellationToken);
        }
 
        public Task SetPhoneNumberAsync(User user, string phoneNumber, CancellationToken cancellationToken)
        {
            return _client.SetPhoneNumberAsync(user, phoneNumber, cancellationToken);
        }
 
        public Task<string> GetPhoneNumberAsync(User user, CancellationToken cancellationToken)
        {
            return _client.GetPhoneNumberAsync(user, cancellationToken);
        }
 
        public Task<bool> GetPhoneNumberConfirmedAsync(User user, CancellationToken cancellationToken)
        {
            return _client.GetPhoneNumberConfirmedAsync(user, cancellationToken);
        }
 
        public Task SetPhoneNumberConfirmedAsync(User user, bool confirmed, CancellationToken cancellationToken)
        {
            return _client.SetPhoneNumberConfirmedAsync(user, confirmed, cancellationToken);
        }
 
        public Task AddLoginAsync(User user, UserLoginInfo login, CancellationToken cancellationToken)
        {
            return _client.AddLoginAsync(user, login, cancellationToken);
        }
 
        public Task RemoveLoginAsync(User user, string loginProvider, string providerKey, CancellationToken cancellationToken)
        {
            return _client.RemoveLoginAsync(user, loginProvider, providerKey, cancellationToken);
        }
 
        public Task<IList<UserLoginInfo>> GetLoginsAsync(User user, CancellationToken cancellationToken)
        {
            return _client.GetLoginsAsync(user, cancellationToken);
        }
 
        public Task<User> FindByLoginAsync(string loginProvider, string providerKey, CancellationToken cancellationToken)
        {
            return _client.FindByLoginAsync(loginProvider, providerKey, cancellationToken);
        }
 
        public Task<DateTimeOffset?> GetLockoutEndDateAsync(User user, CancellationToken cancellationToken)
        {
            return _client.GetLockoutEndDateAsync(user, cancellationToken);
        }
 
        public Task SetLockoutEndDateAsync(User user, DateTimeOffset? lockoutEnd, CancellationToken cancellationToken)
        {
            return _client.SetLockoutEndDateAsync(user, lockoutEnd, cancellationToken);
        }
 
        public Task<int> IncrementAccessFailedCountAsync(User user, CancellationToken cancellationToken)
        {
            return _client.IncrementAccessFailedCountAsync(user, cancellationToken);
        }
 
        public Task ResetAccessFailedCountAsync(User user, CancellationToken cancellationToken)
        {
            return _client.ResetAccessFailedCountAsync(user, cancellationToken);
        }
 
        public Task<int> GetAccessFailedCountAsync(User user, CancellationToken cancellationToken)
        {
            return _client.GetAccessFailedCountAsync(user, cancellationToken);
        }
 
        public Task<bool> GetLockoutEnabledAsync(User user, CancellationToken cancellationToken)
        {
            return _client.GetLockoutEnabledAsync(user, cancellationToken);
        }
 
        public Task SetLockoutEnabledAsync(User user, bool enabled, CancellationToken cancellationToken)
        {
            return _client.SetLockoutEnabledAsync(user,enabled, cancellationToken);
        }
    }
}
