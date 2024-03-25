using BaseLibrary.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServerLibrary.Repositories.Contracts;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ManageRequestController(IItemRequestsManagement itemRequestManagementInterface) : ControllerBase
    {
        [HttpGet("requests")]
        public async Task<IActionResult> GetRequests()
        {
            var users = await itemRequestManagementInterface.GetRequests();
            if (users == null) return NotFound();
            return Ok(users);
        }
        [HttpPost("update-request")]
        public async Task<IActionResult> UpdateRequest(ManageItemRequest request)
        {
            if (request == null) return BadRequest("Model is empty");
            var users = await itemRequestManagementInterface.UpdateRequest(request);
            if (users == null) return NotFound();
            return Ok(users);
        }



        [HttpPost("create-request")]
        public async Task<IActionResult> CreateAsync(ManageItemRequest request)
        {
            if (request == null) return BadRequest("Model is empty");
            var users = await itemRequestManagementInterface.InsertRequest(request);
            if (users == null) return NotFound();
            return Ok(users);

        }


        [HttpGet("statuses")]
        public async Task<IActionResult> GetStatuses()
        {
            var users = await itemRequestManagementInterface.GetStatuses();
            if (users == null) return NotFound();
            return Ok(users);
        }
    }
}
