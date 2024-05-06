using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using WebApiSample.Model;
using System.Globalization;
using System.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : Controller
    {
        private readonly IConfiguration configuration;
        private readonly ILogger<AuthController> _logger;
        public AuthController(IConfiguration configuration, ILogger<AuthController> logger)
        {
            this.configuration = configuration;
            _logger = logger;
        }
        [HttpPost("Login")]
        public async Task<UserToken> Login()
        {
            //Demo用自動帶入Admin
            var _userInfo = new UserInfo()
            {
                UserID = "Admin",
                Password = "Admin"
            };
            UserToken userToken = await GetToken(_userInfo);

            return userToken;
        }
        private async Task<UserToken> GetToken(UserInfo userInfo)
        {
            //驗證方式純為Demo用
            if (userInfo.UserID == "Admin")
            {
                userInfo.Role = "Admin";
                return await Task.FromResult(BuildToken(userInfo));
            }
            return null;
        }

        /// <summary>
        /// 建立Token
        /// </summary>
        /// <param name="userInfo"></param>
        /// <returns></returns>
        private UserToken BuildToken(UserInfo userInfo)
        {
            //記在jwt payload中的聲明，可依專案需求自訂Claim
            string claimuser = userInfo.UserID;

            var claims = new List<Claim>()
            {
                new Claim(JwtRegisteredClaimNames.NameId, claimuser),
                new Claim(JwtRegisteredClaimNames.Name, claimuser),
                new Claim(ClaimTypes.Role,userInfo.Role)
            };
            //取得對稱式加密 JWT Signature 的金鑰
            var key = new Microsoft.IdentityModel.Tokens.
                SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["jwt:Key"]));

            var credential = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            //設定token有效期限
            DateTime expireTime = DateTime.Now.AddMinutes(double.Parse(configuration["jwt:expireTime"]));

            //產生token
            JwtSecurityTokenHandler jwtSecurityTokenHandler = new();
            JwtSecurityToken jwtSecurityToken = new(
                issuer: null,
                audience: null,
                claims: claims,
                expires: expireTime,
                signingCredentials: credential
                );
            string jwtToken = jwtSecurityTokenHandler.WriteToken(jwtSecurityToken);

            //建立UserToken物件後回傳client
            UserToken userToken = new()
            {
                StatusCode = System.Net.HttpStatusCode.OK,
                token = jwtToken,
                ExpireTime = Convert.ToDateTime(expireTime.ToString(), new CultureInfo("zh-TW")).ToString("yyyy-MM-dd HH:mm:ss"),
            };
            return userToken;
        }
    }
}
