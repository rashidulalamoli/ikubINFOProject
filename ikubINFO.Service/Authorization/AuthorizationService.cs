using ikubINFO.Entity.Dtos;
using ikubINFO.Repository.CustomRepositories.User;
using ikubINFO.Utility.PasswordHelper;
using ikubINFO.Utility.StaticData;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ikubINFO.Service.Authorization
{

    public class AuthorizationService : IAuthorizationService
    {
        private readonly IUserRepository _repository;
        private readonly IPasswordHasher _hasher;
        private readonly IConfiguration _configuration;
        public AuthorizationService(IUserRepository repository, IPasswordHasher hasher, IConfiguration configuration)
        {
            _repository = repository;
            _hasher = hasher;
            _configuration = configuration;
        }
        public async Task<TokenResult> Token(JwtTokenInfo request)
        {
            if (request.Grant_type == StaticData.GRANT_TYPE_PASSWORD)
            {
                var response = await BuildToken(request);

                return await Task.FromResult(response);
            }
            else if (request.Grant_type == StaticData.GRANT_TYPE_REFRESH_TOKEN)
            {
                var response = BuildRefreshToken(request.Refreshtoken);
                return response;
            }

            return new(null, null, null, StatusCodes.Status404NotFound, StaticData.GRANT_TYPE_NOT_SUPPORTED);
        }
        private async Task<TokenResult> BuildToken(JwtTokenInfo request)
        {
            var info = await GetTokenInfo(request.Password, request.email);
            if (info.StatusCode == StatusCodes.Status200OK)
            {
                var token = await GenerateToken(request.email, info.UserName);
                return new(new JwtSecurityTokenHandler().WriteToken(token), token.ValidTo, null, StatusCodes.Status200OK, StaticData.SUCCESS);
            }
            return new(null, null, "", info.StatusCode, info.Message);
        }
        private TokenResult BuildRefreshToken(string token)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration[StaticData.JWT_KEY]));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var handler = new JwtSecurityTokenHandler();
            var refreshToken = handler.ReadToken(token) as JwtSecurityToken;
            var claims = refreshToken.Claims.ToList();
            var newToken = new JwtSecurityToken(_configuration[StaticData.SERVICE_BASE], _configuration[StaticData.ORIGINS], claims, expires: DateTime.UtcNow.AddHours(8), signingCredentials: credentials);
            return new(new JwtSecurityTokenHandler().WriteToken(newToken), newToken.ValidTo, null, StatusCodes.Status200OK, StaticData.SUCCESS);
        }
        private async Task<JwtSecurityToken> GenerateToken(string email, string userName)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration[StaticData.JWT_KEY]));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            IEnumerable<Claim> claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, email, JwtSecurityTokenHandler.JsonClaimTypeProperty),
                new Claim(JwtRegisteredClaimNames.UniqueName, userName, JwtSecurityTokenHandler.JsonClaimTypeProperty),
            };
            var token = new JwtSecurityToken(_configuration[StaticData.SERVICE_BASE], _configuration[StaticData.ORIGINS], claims, expires: DateTime.UtcNow.AddHours(8), signingCredentials: credentials);
            return await Task.FromResult(token);
        }
        private async Task<TokenInfo> GetTokenInfo(string password, string email)
        {
            var user = await _repository.GetUserByEmail(email);
            if (user is null)
            {
                return new("", "", StatusCodes.Status404NotFound, StaticData.USER_NOT_FOUND);
            }

            bool isPasswordMatched = MatchPassword(password, user.PasswordHash);
            if (isPasswordMatched)
            {
                return new(user.UserName, user.UserGuid, StatusCodes.Status200OK, StaticData.PASSWORD_MATCHED);
            }
            else
            {
                return new("", "", StatusCodes.Status401Unauthorized, StaticData.PASSWORD_MISMATCHED);

            }

        }
        private bool MatchPassword(string password, string hash)
        {
            var checkPassword = _hasher.Check(hash, password);
            return checkPassword.Verified;
        }
    }
}