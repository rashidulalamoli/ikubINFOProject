using ikubINFO.Api.Logging;
using ikubINFO.Entity.Dtos;
using ikubINFO.Service.Role;
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
    public class RoleController : ControllerBase
    {
        private readonly ILoggerManager _logger;
        private readonly IRoleService _service;
        public RoleController(ILoggerManager logger, IRoleService service)
        {
            _logger = logger;
            _service = service;
        }
        // GET: api/<RoleController>
        [HttpGet]
        public async Task<IEnumerable<RoleDto>> GetRolesAsync()
        {
            var roles = await _service.GetRolesAsync();

            _logger.LogInfo($"{DateTime.UtcNow.ToString("hh:mm:ss")}: Retrieved {roles.Count()} roles");

            return roles;
        }

        // GET api/<RoleController>/5
        [HttpGet(StaticData.ID)]
        public async Task<ActionResult<RoleDto>> GetRoleAsync(string id)
        {
            var role = await _service.GetRoleAsync(id);

            if (role is null)
            {
                return NotFound();
            }
            _logger.LogInfo($"{DateTime.UtcNow.ToString("hh:mm:ss")}: Retrieved role with title {role.Title}");
            return role;
        }

        // GET api/<RoleController>/roles-with-user
        [HttpGet("roles-with-user")]
        public async Task<IEnumerable<RoleWithUserDto>> GetRolesWithUserAsync()
        {
            var roles = await _service.GetRolesWithUserAsync();

            _logger.LogInfo($"{DateTime.UtcNow.ToString("hh:mm:ss")}: Retrieved {roles.Count()} roles");

            return roles;
        }

        // GET api/<RoleController>/role-with-user/5
        [HttpGet("role-with-user/{id}")]
        public async Task<ActionResult<RoleWithUserDto>> GetRoleWithUserAsync(string id)
        {
            var role = await _service.GetRoleWithUserAsync(id);

            if (role is null)
            {
                return NotFound();
            }
            _logger.LogInfo($"{DateTime.UtcNow.ToString("hh:mm:ss")}: Retrieved role with title {role.Title}");
            return role;
        }

        // POST api/<RoleController>
        [HttpPost]
        public async Task<Status> CreateRoleAsync( CreateRoleDto roleDto)
        {
            var status = await _service.AddRole(roleDto);
            if (status.StatusCode == StatusCodes.Status200OK)
            {
                _logger.LogInfo($"{DateTime.UtcNow.ToString("hh:mm:ss")}: Added role with title {roleDto.Title}");
            }
            else
            {
                _logger.LogInfo($"{DateTime.UtcNow.ToString("hh:mm:ss")}: Role add failed because {status.Message}");
            }
            return status;

        }

        // PUT api/<RoleController>/5
        [HttpPut(StaticData.ID)]
        public async Task<Status> UpdateRoleAsync(string id, UpdateRoleDto roleDto)
        {
            var status = await _service.UpdateRole(id, roleDto);
            if (status.StatusCode == StatusCodes.Status200OK)
            {
                _logger.LogInfo($"{DateTime.UtcNow.ToString("hh:mm:ss")}: Updated role with title {roleDto.Title}");
            }
            else
            {
                _logger.LogInfo($"{DateTime.UtcNow.ToString("hh:mm:ss")}: role update failed because {status.Message}");
            }
            return status;
        }

        // DELETE api/<RoleController>/5
        [HttpDelete(StaticData.ID)]
        public async Task<Status> DeleteRoleAsync(string id, bool userDelete = false)
        {
            var status = userDelete?await _service.DeleteRoleWIthUser(id) : await _service.DeleteRole(id);
            if (status.StatusCode == StatusCodes.Status200OK)
            {
                _logger.LogInfo($"{DateTime.UtcNow.ToString("hh:mm:ss")}: Role deleted");
            }
            else
            {
                _logger.LogInfo($"{DateTime.UtcNow.ToString("hh:mm:ss")}: Role delete failed because {status.Message}");
            }
            return status;
        }
    }
}
