using ikubINFO.Api.Logging;
using ikubINFO.Entity.Dtos;
using ikubINFO.Service.Authorization;
using ikubINFO.Utility.StaticData;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace ikubINFO.Api.Controllers
{
    [Route(StaticData.API_CONTROLLER_ROUTE)]
    [ApiController]
    public class AuthorizationController : ControllerBase
    {
        private readonly ILoggerManager _logger;
        private readonly IAuthorizationService _authorizationService;
        public AuthorizationController(ILoggerManager logger, IAuthorizationService authorizationService)
        {
            _logger = logger;
            _authorizationService = authorizationService;
        }

        //POST : /token
        [HttpPost(StaticData.TOKEN)]
        public async Task<TokenResult> Token(JwtTokenInfo request)
        {
            if (request.Grant_type == StaticData.GRANT_TYPE_PASSWORD)
            {
                var response = await _authorizationService.Token(request);
                _logger.LogInfo($"{DateTime.UtcNow.ToString("hh:mm:ss")}: AccessToken generated for {request.email}");
                return await Task.FromResult(response);
            }
            else if (request.Grant_type == StaticData.GRANT_TYPE_REFRESH_TOKEN)
            {
                var response = await _authorizationService.Token(request);
                _logger.LogInfo($"{DateTime.UtcNow.ToString("hh:mm:ss")}: RefreshTOken generated for {request.email}");
                return response;
            }
            _logger.LogInfo($"{DateTime.UtcNow.ToString("hh:mm:ss")}: {StaticData.GRANT_TYPE_NOT_SUPPORTED} for {request.email}");
            throw new InvalidOperationException(StaticData.GRANT_TYPE_NOT_SUPPORTED);
        }
    }
}
