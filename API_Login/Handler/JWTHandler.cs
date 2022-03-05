using Application_Common;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace API_Login.Handler
{
    public class JWTHandler
    {
        public string GenerateJSONWebToken(string LoginName, string LoginPassword)
        {
            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(APISetting.Jwt.Key));
            SigningCredentials signCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            JwtSecurityToken token = new JwtSecurityToken(
                issuer: APISetting.Jwt.Issuer,
                audience: APISetting.Jwt.Issuer,
                claims: new List<Claim>(), // claims (are used to filter the data)
                expires: DateTime.Now.AddMinutes(60),
                signingCredentials: signCredentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public bool AuthenticateUser(string LoginName, string LoginPassword)
        {
            // Validate the User Credentials using LDAP / Database
            if (LoginName == "admin")
            {
                return true;
                //user = new UserModel { Username = "######", Password = "######" };
            }
            return false;
        }
    }
}