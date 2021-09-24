using ikubINFO.Api.Logging;
using ikubINFO.Entity.Dtos;
using ikubINFO.Service.User;
using ikubINFO.Utility.StaticData;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ikubINFO.Api.Controllers
{
    [Route(StaticData.API_CONTROLLER_ROUTE), Authorize]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ILoggerManager _logger;
        private readonly IUserService _service;
        public UserController(ILoggerManager logger, IUserService service)
        {
            _logger = logger;
            _service = service;
        }
        // GET: api/<UserController>
        [HttpGet]
        public async Task<IEnumerable<UserDto>> GetUsersAsync()
        {
            var users = await _service.GetUsersAsync();

            _logger.LogInfo($"{DateTime.UtcNow.ToString("hh:mm:ss")}: Retrieved {users.Count()} users");

            return users;
        }

        // GET api/<UserController>/5
        [HttpGet(StaticData.ID)]
        public async Task<ActionResult<UserDto>> GetUserAsync(string id)
        {
            var user = await _service.GetUserAsync(id);

            if (user is null)
            {
                return NotFound();
            }
            _logger.LogInfo($"{DateTime.UtcNow.ToString("hh:mm:ss")}: Retrieved {user.FullName}");
            return user;
        }

        // POST api/<UserController>
        [HttpPost, AllowAnonymous]
        public async Task<Status> CreateUserAsync(CreateUserDto userDto)
        {
            var status = await _service.AddUser(userDto);
            if (status.StatusCode == StatusCodes.Status200OK)
            {
                _logger.LogInfo($"{DateTime.UtcNow.ToString("hh:mm:ss")}: Added {userDto.FullName}");
            }
            else
            {
                _logger.LogInfo($"{DateTime.UtcNow.ToString("hh:mm:ss")}: User add failed because {status.Message}");
            }
            return status;

        }

        // PUT api/<UserController>/5
        [HttpPut(StaticData.ID)]
        public async Task<Status> UpdateUserAsync(string id, UpdateUserDto userDto)
        {
            var status = await _service.UpdateUser(id, userDto);
            if (status.StatusCode == StatusCodes.Status200OK)
            {
                _logger.LogInfo($"{DateTime.UtcNow.ToString("hh:mm:ss")}: Updated {userDto.FullName}");
            }
            else
            {
                _logger.LogInfo($"{DateTime.UtcNow.ToString("hh:mm:ss")}: User update failed because {status.Message}");
            }
            return status;
        }

        // DELETE api/<UserController>/5
        [HttpDelete(StaticData.ID)]
        public async Task<Status> DeleteUserAsync(string id)
        {
            var status = await _service.DeleteUser(id);
            if (status.StatusCode == StatusCodes.Status200OK)
            {
                _logger.LogInfo($"{DateTime.UtcNow.ToString("hh:mm:ss")}: User deleted");
            }
            else
            {
                _logger.LogInfo($"{DateTime.UtcNow.ToString("hh:mm:ss")}: User delete failed because {status.Message}");
            }
            return status;
        }
    }
}
