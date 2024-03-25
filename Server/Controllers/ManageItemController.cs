using BaseLibrary.DTOs;
using BaseLibrary.Entities;
using Microsoft.AspNetCore.Mvc;
using ServerLibrary.Repositories.Contracts;


namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ManageItemController(IItemsManagement itemManagementInterface) : ControllerBase
    {
        [HttpGet("items")]
        public async Task<IActionResult> GetWarehouseItems()
        {
            var users = await itemManagementInterface.GetWarehouseItems();
            if (users == null) return NotFound();
            return Ok(users);
        }
        [HttpPost("update-item")]
        public async Task<IActionResult> UpdateWarehouseItem(ManageItem item)
        {
            if (item == null) return BadRequest("Model is empty");
            var users = await itemManagementInterface.UpdateWarehouseItem(item);
            if (users == null) return NotFound();
            return Ok(users);
        }



        [HttpPost("create-item")]
        public async Task<IActionResult> CreateAsync(ManageItem item)
        {
            if (item == null) return BadRequest("Model is empty");
            var users = await itemManagementInterface.InsertWarehouseItem(item);
            if (users == null) return NotFound();
            return Ok(users);

        }


        [HttpGet("units")]
        public async Task<IActionResult> GetUnits()
        {
            var users = await itemManagementInterface.GetUnits();
            if (users == null) return NotFound();
            return Ok(users);
        }

        [HttpGet("itemGroups")]
        public async Task<IActionResult> GetItemGroups()
        {
            var users = await itemManagementInterface.GetItemGroups();
            if (users == null) return NotFound();
            return Ok(users);
        }

        [HttpDelete("delete-item/{id}")]
        public async Task<IActionResult> DeleteItem(int id)
        {
            var result = await itemManagementInterface.DeleteItem(id);
            return Ok(result);
        }



    }
}
