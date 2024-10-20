using CarRentalSystem.Application.Bases;
using CarRentalSystem.Application.Contracts.Service;
using CarRentalSystem.Application.Request;
using CarRentalSystem.Domain.Entity;
using CarRentalSystem.Domain.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarRentalSystem.Api.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/user")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpGet("{UserId:Guid}")]
        public async Task<IActionResult> GetUserById(Guid UserId)
        {
            var user = await _userService.GetByIdAsync(UserId);
            if (user != null)
            {
                return Ok(user);
            }
            else
            {
                return NotFound();
            }
        }
        [HttpPost("update/{userId:Guid}")]
        public async Task<BaseResponse<UserEntity>> UpdateProfile(RegisterRequest registerRequest, Guid userId)
        {
            return await _userService.UpdateAsync(registerRequest, userId);
        }
        [HttpPost("request-verification/{userId:Guid}")]
        public async Task<BaseResponse<UserEntity>> RequestVerification(Guid userId)
        {
            return await _userService.RequestVerificationAsync(userId);
        }
    }
}
