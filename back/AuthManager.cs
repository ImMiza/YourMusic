using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using yourmusic.Context;
using System.Linq;
using yourmusic.Models;

namespace yourmusic.Auth
{
    public class AuthManager : IAuth
    {

        private readonly string _key;
        
        public AuthManager(string key) 
        {
            this._key = key;
        }

        public string Connection(ContextDatabase database, string username, string password)
        {
            if(!GetUser(database, username, password)) {
                return null;
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.ASCII.GetBytes(_key);
            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(new Claim[] 
                {
                    new Claim(ClaimTypes.Name, username)
                }),
                Expires = System.DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        private bool GetUser(ContextDatabase database, string username, string password) 
        {
            return !database.Users.Where(user => user.Username == username).IsNullOrEmpty();
        }
    }
}