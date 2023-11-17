using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SchoolProject.Data.Entities.Identity;
using SchoolProject.Data.Helpers;
using SchoolProject.Infrustructure.IRepositories;
using SchoolProject.Service.Abstractions;
using System.Collections.Concurrent;
using System.IdentityModel.Tokens.Jwt;

using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace SchoolProject.Service.Implemintations
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly JwtSetting _jwtsettings;
        private readonly ConcurrentDictionary<string, RefreshToken> _userrefreshtoken;
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly UserManager<User> _usermanager;
        public AuthenticationService(JwtSetting jwtsettings, IRefreshTokenRepository refreshTokenRepository, UserManager<User> usermanager)
        {
            _jwtsettings = jwtsettings;
            _userrefreshtoken = new ConcurrentDictionary<string, RefreshToken>();
            _refreshTokenRepository = refreshTokenRepository;
            _usermanager = usermanager;
        }

        // generate jwttoken expires after time and make refreshtoken to has the ability to regenrate anothertoken
        public async Task<JwtAuthResult> GetjwtToken(User user)
        {
            var (JwtToken, AccessToken) = await Generatetoken(user); //generate token
            var RefreshToken = GetRefreshToken(user.UserName);  // generate refreshtoken

            var userRefreshToken = new UserRefreshToken()
            {
                UserId = user.Id,
                JwtId = JwtToken.Id,
                AddedTime = DateTime.Now,
                ExpiryDate = DateTime.Now.AddDays(_jwtsettings.RefreshTokenExpireDate),  // refreshtoken date expires of user
                IsUsed = true,
                IsRevoked = false,
                Token = AccessToken,
                RefreshToken = RefreshToken.RefreshTokenString,
            };
            var result = await _refreshTokenRepository.AddAsync(userRefreshToken); // add details of token in database in table refreshtoken

            var response = new JwtAuthResult();
            response.AccessToken = AccessToken;
            response.RefreshToken = RefreshToken;
            return response;
        }

        private async Task<(JwtSecurityToken, string)> Generatetoken(User user)
        {
            var userRoles = await _usermanager.GetRolesAsync(user);
            var claims = GenerateClaims(user, userRoles.ToList());
            var JwtToken = new JwtSecurityToken
            (
                issuer: _jwtsettings.Issuer,
                audience: _jwtsettings.Audience,
                claims: claims,
                expires: DateTime.Now.AddDays(_jwtsettings.AccessTokenExpireDate),  //time avaliabeltoken for user to stay in signin not refreshtoken
                signingCredentials: new SigningCredentials
                                    (new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_jwtsettings.Secret)), SecurityAlgorithms.HmacSha256)
            );
            var AccessToken = new JwtSecurityTokenHandler().WriteToken(JwtToken);  //make jwttoken a string
            return (JwtToken, AccessToken);
        }
        private List<Claim> GenerateClaims(User user, List<string> roles)
        {
            var claims = new List<Claim>()
            {
                new Claim(nameof(UserClaimModel.UserId),user.Id.ToString()),
                new Claim(nameof(UserClaimModel.phoneNumber),user.PhoneNumber),
                new Claim(ClaimTypes.NameIdentifier,user.UserName),
                new Claim(ClaimTypes.Name,user.UserName),
                new Claim(ClaimTypes.Email,user.Email),
                //new Claim(nameof(UserClaimModel.UserName),user.UserName),
                //new Claim(nameof(UserClaimModel.Email),user.Email),
            };
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }
            return claims;
        }

        private RefreshToken GetRefreshToken(string username)
        {
            var refreshtoken = new RefreshToken()
            {
                UserName = username,
                ExpireAt = DateTime.Now.AddDays(_jwtsettings.RefreshTokenExpireDate), // time for refreshtoken to be avaliable to make token regenrate
                RefreshTokenString = GenerateRefreshToken()
            };
            // used to add refreshtoken if not exists or update it if exists
            _userrefreshtoken.AddOrUpdate(refreshtoken.RefreshTokenString, refreshtoken, (s, t) => refreshtoken);
            return refreshtoken;
        }
        private string GenerateRefreshToken()
        {
            var randomnumber = new byte[32]; // Create an array of bytes to store the random number.
            var randomnumbergenerate = RandomNumberGenerator.Create(); // Create an instance of RandomNumberGenerator to generate random bytes.
            randomnumbergenerate.GetBytes(randomnumber); // Fill the byte array with random bytes.
            return Convert.ToBase64String(randomnumber); // Convert the byte array to a Base64-encoded string.
        }



        public JwtSecurityToken ReadJwtToken(string accessToken)  // make jwttoken string as jwtsecuritytoken that has all claims , etc
        {
            if (string.IsNullOrEmpty(accessToken))
            {
                throw new ArgumentNullException(nameof(accessToken));
            }
            var handler = new JwtSecurityTokenHandler();
            var response = handler.ReadJwtToken(accessToken);  //used to readtoken as jwtsecuritytoken that has payload,algorithmssecurity and header
            return response;
        }

        // make validation on token and refresh to check if it valid to make token or not
        public async Task<(string, DateTime?)> ValidateTokenDetails(JwtSecurityToken jwtToken, string accessToken, string refreshToken)
        {
            if (jwtToken == null || !jwtToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256))  //check algorithm of token same or not
            {
                return ("AlgorithmIsWrong", null);
            }
            if (jwtToken.ValidTo > DateTime.UtcNow)  //check accesstoken expired date 
            {
                return ("TokenIsNotExpired", null);
            }

            // get userid from claimstoken from accesstoken 
            var userId = jwtToken.Claims.FirstOrDefault(x => x.Type == nameof(UserClaimModel.UserId).ToString()).Value;

            // get refreshtoken according to accesstoken,refreshtoken,userid
            var userRefreshToken = await _refreshTokenRepository.GetTableNoTracking().FirstOrDefaultAsync
                                                      (x => x.Token == accessToken && x.RefreshToken == refreshToken
                                                       && x.UserId == int.Parse(userId));
            if (userRefreshToken == null)
            {
                return ("RefreshTokenIsNotFound", null);
            }

            if (userRefreshToken.ExpiryDate < DateTime.UtcNow)    //check refreshtoken expiredate
            {
                userRefreshToken.IsRevoked = true;
                userRefreshToken.IsUsed = false;
                await _refreshTokenRepository.UpdateAsync(userRefreshToken);
                return ("RefreshTokenIsExpired", null);
            }
            var expirydate = userRefreshToken.ExpiryDate;   // updaterefreshtoken state
            return (userId, expirydate);
        }

        // after we check for that token is expired but refreshtoken is  not we regenrate token and safe refreshtoken as it
        public async Task<JwtAuthResult> GenerateRefreshToken(User user, JwtSecurityToken jwtToken, string refreshToken, DateTime? expiryDate)
        {
            var (JwtSecurityToken, AccessToken) = await Generatetoken(user);  // normal generatetoken

            var response = new JwtAuthResult();
            response.AccessToken = AccessToken;

            var refreshTokenResult = new RefreshToken();
            refreshTokenResult.UserName = jwtToken.Claims.FirstOrDefault(x => x.Type == nameof(UserClaimModel.UserId).ToString()).Value;// هنجيبة من jwttoken عادى خالص
            refreshTokenResult.ExpireAt = (DateTime)expiryDate;
            refreshTokenResult.RefreshTokenString = refreshToken;

            response.RefreshToken = refreshTokenResult;
            return response;
        }

        // has an endpoint for itself check token is expired and not for refreshtoken
        public async Task<string> ValidateToken(string accessToken)
        {
            var handler = new JwtSecurityTokenHandler();
            var parameters = new TokenValidationParameters
            {
                ValidateIssuer = _jwtsettings.ValidateIssuer,
                ValidIssuers = new[] { _jwtsettings.Issuer },
                ValidateIssuerSigningKey = _jwtsettings.ValidateIssuerSigningKey,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_jwtsettings.Secret)),
                ValidAudience = _jwtsettings.Audience,
                ValidateAudience = _jwtsettings.ValidateAudience,
                ValidateLifetime = _jwtsettings.ValidateLifeTime,
            };
            try
            {
                var validator = handler.ValidateToken(accessToken, parameters, out SecurityToken validatedToken);
                if (validator == null)
                {
                    return "InvalidToken";
                }

                return "NotExpired";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
