using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using WebStore.DomainNew.Dto.Identity;
using WebStore.DomainNew.Entities;
using WebStore.Interfaces;

namespace WebStore.Clients
{
    public class UsersClient : BaseClient, IUsersClient
    {
        public UsersClient(IConfiguration configuration) : base(configuration)
        {
            ServiceAddress = "api/users";
        }

        protected sealed override string ServiceAddress { get; }

        #region IUserStore
        public async Task<string> GetUserIdAsync(
            User user, 
            CancellationToken cancel)
        {
            var url = $"{ServiceAddress}/userId";
            var result = await PostAsync(url, user);
            return await result.Content.ReadAsAsync<string>(cancel);
        }

        public async Task<string> GetUserNameAsync(
            User user, 
            CancellationToken cancel)
        {
            var url = $"{ServiceAddress}/userName";
            var result = await PostAsync(url, user);
            var ret = await result.Content.ReadAsAsync<string>(cancel);
            return ret;
        }

        public Task SetUserNameAsync(
            User user, 
            string userName, 
            CancellationToken cancel)
        {
            user.UserName = userName;
            var url = $"{ServiceAddress}/userName/{userName}";
            return PostAsync(url, user);
        }

        public async Task<string> GetNormalizedUserNameAsync(
            User user, 
            CancellationToken cancel)
        {
            var url = $"{ServiceAddress}/normalUserName";
            var result = await PostAsync(url, user);
            return await result.Content.ReadAsAsync<string>(cancel);
        }

        public Task SetNormalizedUserNameAsync(
            User user, 
            string normalizedName, 
            CancellationToken cancel)
        {
            user.NormalizedUserName = normalizedName;
            var url = $"{ServiceAddress}/normalUserName/{normalizedName}";
            return PostAsync(url, user);
        }

        public async Task<IdentityResult> CreateAsync(
            User user, 
            CancellationToken cancel)
        {
            var url = $"{ServiceAddress}/user";
            var result = await PostAsync(url, user);
            var ret = await result.Content.ReadAsAsync<bool>(cancel);
            return ret ? IdentityResult.Success : IdentityResult.Failed();
        }


        public async Task<IdentityResult> UpdateAsync(
            User user, 
            CancellationToken cancel)
        {
            var url = $"{ServiceAddress}/user";
            var result = await PutAsync(url, user);
            var ret = await result.Content.ReadAsAsync<bool>(cancel);
            return ret ? IdentityResult.Success : IdentityResult.Failed();
        }

        public async Task<IdentityResult> DeleteAsync(
            User user, 
            CancellationToken cancel)
        {
            var url = $"{ServiceAddress}/user/delete";
            var result = await PostAsync(url, user);
            var ret = await result.Content.ReadAsAsync<bool>(cancel);
            return ret ? IdentityResult.Success : IdentityResult.Failed();
        }

        public Task<User> FindByIdAsync(
            string userId, 
            CancellationToken cancel)
        {
            var url = $"{ServiceAddress}/user/find/{userId}";
            return GetAsync<User>(url);
        }

        public async Task<User> FindByNameAsync(
            string normalizedUserName, 
            CancellationToken cancel)
        {
            var url = $"{ServiceAddress}/user/normal/{normalizedUserName}";
            var result = await GetAsync<User>(url);
            return result;
        }

        #endregion

        #region IUserRoleStore

        public Task AddToRoleAsync(
            User user, string roleName, 
            CancellationToken cancel)
        {
            var url = $"{ServiceAddress}/role/{roleName}";
            return PostAsync(url, user);
        }

        public Task RemoveFromRoleAsync(
            User user, 
            string roleName, 
            CancellationToken cancel)
        {
            var url = $"{ServiceAddress}/delete/{roleName}";
            return PostAsync(url, user);
        }

        public async Task<IList<string>> GetRolesAsync(
            User user, 
            CancellationToken cancel)
        {
            var url = $"{ServiceAddress}/roles";
            var result = await PostAsync(url, user);
            return await result.Content.ReadAsAsync<IList<string>>(cancel);
        }

        public async Task<bool> IsInRoleAsync(
            User user, 
            string roleName, 
            CancellationToken cancel)
        {
            var url = $"{ServiceAddress}/inrole/{roleName}";
            var result = await PostAsync(url, user);
            return await result.Content.ReadAsAsync<bool>(cancel);
        }

        public async Task<IList<User>> GetUsersInRoleAsync(
            string roleName, 
            CancellationToken cancel)
        {
            var url = $"{ServiceAddress}/usersInRole/{roleName}";
            return await GetAsync<List<User>>(url);
        }

        #endregion

        #region IUserPasswordStore
        public async Task SetPasswordHashAsync(
            User user, 
            string passwordHash, 
            CancellationToken cancel)
        {
            user.PasswordHash = passwordHash;
            var url = $"{ServiceAddress}/setPasswordHash";
            await PostAsync(
                url, 
                new PasswordHashDto 
                { 
                    User = user, 
                    Hash = passwordHash 
                });
        }

        public async Task<string> GetPasswordHashAsync(
            User user, 
            CancellationToken cancel)
        {
            var url = $"{ServiceAddress}/getPasswordHash";
            var result = await PostAsync(url, user);
            return await result.Content.ReadAsAsync<string>(cancel);
        }

        public async Task<bool> HasPasswordAsync(
            User user, 
            CancellationToken cancel)
        {
            var url = $"{ServiceAddress}/hasPassword";
            var result = await PostAsync(url, user);
            return await result.Content.ReadAsAsync<bool>(cancel);
        }

        #endregion

        #region IUserClaimStore
        public async Task<IList<Claim>> GetClaimsAsync(
            User user, 
            CancellationToken cancel)
        {
            var url = $"{ServiceAddress}/getClaims";
            var result = await PostAsync(url, user);
            return await result.Content.ReadAsAsync<List<Claim>>(cancel);
        }

        public Task AddClaimsAsync(
            User user, 
            IEnumerable<Claim> claims, 
            CancellationToken cancel)
        {
            var url = $"{ServiceAddress}/addClaims";
            return PostAsync(
                url, 
                new AddClaimsDto 
                { 
                    User = user, 
                    Claims = claims 
                });
        }

        public Task ReplaceClaimAsync(
            User user, 
            Claim claim, 
            Claim newClaim, 
            CancellationToken cancel)
        {
            var url = $"{ServiceAddress}/replaceClaim";
            return PostAsync(
                url, 
                new ReplaceClaimsDto 
                { 
                    User = user, 
                    Claim = claim, 
                    NewClaim = newClaim 
                });
        }

        public Task RemoveClaimsAsync(
            User user, 
            IEnumerable<Claim> claims, 
            CancellationToken cancel)
        {
            var url = $"{ServiceAddress}/removeClaims";
            return PostAsync(
                url, 
                new RemoveClaimsDto
                { 
                    User = user, 
                    Claims = claims 
                });
        }

        public async Task<IList<User>> GetUsersForClaimAsync(
            Claim claim, 
            CancellationToken cancel)
        {
            var url = $"{ServiceAddress}/getUsersForClaim";
            var result = await PostAsync(url, claim);
            return await result.Content.ReadAsAsync<List<User>>(cancel);
        }

        #endregion

        #region IUserTwoFactorStore
        public Task SetTwoFactorEnabledAsync(
            User user, 
            bool enabled, 
            CancellationToken cancel)
        {
            user.TwoFactorEnabled = enabled;
            var url = $"{ServiceAddress}/setTwoFactor/{enabled}";
            return PostAsync(url, user);
        }

        public async Task<bool> GetTwoFactorEnabledAsync(
            User user, 
            CancellationToken cancel)
        {
            var url = $"{ServiceAddress}/getTwoFactorEnabled";
            var result = await PostAsync(url, user);
            return await result.Content.ReadAsAsync<bool>(cancel);
        }

        #endregion

        #region IUserEmailStore

        public Task SetEmailAsync(
            User user, 
            string email, 
            CancellationToken cancel)
        {
            user.Email = email;
            var url = $"{ServiceAddress}/setEmail/{email}";
            return PostAsync(url, user);
        }

        public async Task<string> GetEmailAsync(
            User user, 
            CancellationToken cancel)
        {
            var url = $"{ServiceAddress}/getEmail";
            var result = await PostAsync(url, user);
            return await result.Content.ReadAsAsync<string>(cancel);
        }

        public async Task<bool> GetEmailConfirmedAsync(
            User user, 
            CancellationToken cancel)
        {
            var url = $"{ServiceAddress}/getEmailConfirmed";
            var result = await PostAsync(url, user);
            return await result.Content.ReadAsAsync<bool>(cancel);
        }

        public Task SetEmailConfirmedAsync(
            User user, bool confirmed, 
            CancellationToken cancel)
        {
            user.EmailConfirmed = confirmed;
            var url = $"{ServiceAddress}/setEmailConfirmed/{confirmed}";
            return PostAsync(url, user);
        }

        public Task<User> FindByEmailAsync(
            string normalizedEmail, 
            CancellationToken cancel)
        {
            var url = $"{ServiceAddress}/user/findByEmail/{normalizedEmail}";
            return GetAsync<User>(url);
        }

        public async Task<string> GetNormalizedEmailAsync(
            User user, 
            CancellationToken cancel)
        {
            var url = $"{ServiceAddress}/getNormalizedEmail";
            var result = await PostAsync(url, user);
            return await result.Content.ReadAsAsync<string>(cancel);
        }

        public Task SetNormalizedEmailAsync(
            User user, 
            string normalizedEmail, 
            CancellationToken cancel)
        {
            user.NormalizedEmail = normalizedEmail;
            var url = $"{ServiceAddress}/setEmail/{normalizedEmail}";
            return PostAsync(url, user);
        }

        #endregion

        #region IUserPhoneNumberStore

        public Task SetPhoneNumberAsync(
            User user, 
            string phoneNumber, 
            CancellationToken cancel)
        {
            user.PhoneNumber = phoneNumber;
            var url = $"{ServiceAddress}/setPhoneNumber/{phoneNumber}";
            return PostAsync(url, user);
        }

        public async Task<string> GetPhoneNumberAsync(
            User user, 
            CancellationToken cancel)
        {
            var url = $"{ServiceAddress}/getPhoneNumber";
            var result = await PostAsync(url, user);
            return await result.Content.ReadAsAsync<string>(cancel);
        }

        public async Task<bool> GetPhoneNumberConfirmedAsync(
            User user, 
            CancellationToken cancel)
        {
            var url = $"{ServiceAddress}/getPhoneNumberConfirmed";
            var result = await PostAsync(url, user);
            return await result.Content.ReadAsAsync<bool>(cancel);
        }

        public Task SetPhoneNumberConfirmedAsync(
            User user, 
            bool confirmed, 
            CancellationToken cancel)
        {
            user.PhoneNumberConfirmed = confirmed;
            var url = $"{ServiceAddress}/setPhoneNumberConfirmed/{confirmed}";
            return PostAsync(url, user);
        }

        #endregion

        #region IUserLoginStore

        public Task AddLoginAsync(
            User user, 
            UserLoginInfo login, 
            CancellationToken cancel)
        {
            var url = $"{ServiceAddress}/addLogin";
            return PostAsync(
                url, 
                new AddLoginDto
                { 
                    User = user, 
                    UserLoginInfo = login 
                });
        }

        public Task RemoveLoginAsync(
            User user, 
            string loginProvider, 
            string providerKey, 
            CancellationToken cancel)
        {
            var url = 
                $"{ServiceAddress}/removeLogin/{loginProvider}/{providerKey}";
            return PostAsync(url, user);
        }

        public async Task<IList<UserLoginInfo>> GetLoginsAsync(
            User user, 
            CancellationToken cancel)
        {
            var url = $"{ServiceAddress}/getLogins";
            var result = await PostAsync(url, user);
            return await result.Content
                .ReadAsAsync<List<UserLoginInfo>>(cancel);
        }

        public Task<User> FindByLoginAsync(
            string Provider, 
            string Key, 
            CancellationToken cancel)
        {
            var url = $"{ServiceAddress}/user/findbylogin/{Provider}/{Key}";
            return GetAsync<User>(url);
        }

        #endregion

        #region IUserLockoutStore

        public async Task<DateTimeOffset?> GetLockoutEndDateAsync(
            User user, 
            CancellationToken cancel)
        {
            var url = $"{ServiceAddress}/getLockoutEndDate";
            var result = await PostAsync(url, user);
            return await result.Content.ReadAsAsync<DateTimeOffset?>(cancel);
        }

        public Task SetLockoutEndDateAsync(
            User user, 
            DateTimeOffset? lockoutEnd, 
            CancellationToken cancel)
        {
            user.LockoutEnd = lockoutEnd;
            var url = $"{ServiceAddress}/setLockoutEndDate";
            return PostAsync(
                url, 
                new SetLockoutDto 
                { 
                    User = user, 
                    LockoutEnd = lockoutEnd 
                });
        }

        public async Task<int> IncrementAccessFailedCountAsync(
            User user, 
            CancellationToken cancel)
        {
            var url = $"{ServiceAddress}/IncrementAccessFailedCount";
            var result = await PostAsync(url, user);
            return await result.Content.ReadAsAsync<int>(cancel);
        }

        public Task ResetAccessFailedCountAsync(
            User user, 
            CancellationToken cancel)
        {
            var url = $"{ServiceAddress}/ResetAccessFailedCount";
            return PostAsync(url, user);
        }

        public async Task<int> GetAccessFailedCountAsync(
            User user, 
            CancellationToken cancel)
        {
            var url = $"{ServiceAddress}/GetAccessFailedCount";
            var result = await PostAsync(url, user);
            return await result.Content.ReadAsAsync<int>(cancel);
        }

        public async Task<bool> GetLockoutEnabledAsync(
            User user, 
            CancellationToken cancel)
        {
            var url = $"{ServiceAddress}/GetLockoutEnabled";
            var result = await PostAsync(url, user);
            return await result.Content.ReadAsAsync<bool>(cancel);
        }

        public async Task SetLockoutEnabledAsync(
            User user, 
            bool enabled, 
            CancellationToken cancel)
        {
            user.LockoutEnabled = enabled;
            var url = $"{ServiceAddress}/SetLockoutEnabled/{enabled}";
            await PostAsync(url, user);
        }
        #endregion

        public void Dispose()
        {
            Client.Dispose();
        }
    }
}
