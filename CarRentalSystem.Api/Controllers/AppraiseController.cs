using CarRentalSystem.Application.Bases;
using CarRentalSystem.Application.Contracts.Service;
using CarRentalSystem.Domain.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace CarRentalSystem.Api.Controllers
{
    [Route("api/appraiser")]
    [ApiController]
    [Authorize]
    public class AppraiseController : ControllerBase
    {
        private readonly IAppraiseService _appraiseService;
        private readonly IFinesService _finesService;

        public AppraiseController(IAppraiseService appraiseService, IFinesService finesService)
        {
            _appraiseService = appraiseService;
            _finesService = finesService;
        }

        [HttpGet("")]
        public async Task<IActionResult> GetAllRequestReturn([FromQuery] BaseFilteration filter)
        {
            var bookingHistoryRes = await _appraiseService.GetAllRequestReturn(filter);
            if (bookingHistoryRes.Items.Count() > 0)
                return Ok(bookingHistoryRes);
            return NoContent();
        }
        [HttpPost("add/fines-request")]
        public async Task<IActionResult> AddFinesRequests([FromBody] List<FinesRequest> finesRequests)
        {
            bool isAdded = await _finesService.AddFinesRange(finesRequests);
            return isAdded ? Ok("Add fines request successfully") : BadRequest("Add fines requests failed");
        }
    }
}
