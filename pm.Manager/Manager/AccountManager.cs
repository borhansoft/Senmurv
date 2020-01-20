using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using pm;
using pm.Bus.BusHelpers;
using pm.Core.Helpers;
using pm.Data.Models;

namespace pm.Manager.Manager
{
    public interface IAccountManager
    {
        string Login(string nationalCode, string password);
    }

    public class AccountManager : IAccountManager
    {
        private IUserBus _userBus;
        private IClaimJwtBus _claimJwtBus;

        public AccountManager(IUserBus userBus, IClaimJwtBus claimJwt)
        {
            _userBus = userBus;
            _claimJwtBus = claimJwt;
        }

        public string Login(string nationalCode, string password)
        {
            if (string.IsNullOrEmpty(nationalCode) || string.IsNullOrEmpty(password))
            {
                return null;
            }

            User user = _userBus.GetBynationalCode(nationalCode);

            bool result = AuthHelper.Authenticate(user, password);

            if (!result)
                return null;

            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.UTF8.GetBytes(SettingsHelper.Secret);
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Email, user.Id.ToString()),
                    new Claim(ClaimTypes.Role, "mahmood rules")
                };

                var credentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature);
                //add many role the user have (later)

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(claims),
                    SigningCredentials = credentials,
                    Expires = DateTime.UtcNow.AddDays(7)

                };

                var token = tokenHandler.CreateToken(tokenDescriptor);
                var tokenString = tokenHandler.WriteToken(token);

                //AccountHelper.CreatJwt(user);

                // ClaimJwt claimJwt = 
                _claimJwtBus.SaveJwt(tokenString, user.Id);
                return tokenString;
            }
            catch (Exception e)
            {
                var error = e.Message;
                return null;
            }



        }

    }
}
