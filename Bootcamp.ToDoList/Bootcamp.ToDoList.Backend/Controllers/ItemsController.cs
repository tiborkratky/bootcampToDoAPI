using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Bootcamp.ToDoList.Backend.Entities.DTO;
using Bootcamp.ToDoList.Backend.Entities.Models;
using Bootcamp.ToDoList.Backend.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Bootcamp.ToDoList.Backend.Controllers
{
    [ApiController]
    [Produces("application/json")]
    [Route("api/[controller]")]
    [Authorize]
    public class ItemsController : ControllerBase
    {
        private readonly IItemService _itemService;

        public ItemsController(IItemService itemService)
        {
            _itemService = itemService;
        }

        public const string GetItemRouteName = "getitem";

        [HttpPost("list/{listId}")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ItemDto))]
        [SwaggerOperation(
            summary: "Create item",
            description: "Create new item in the list",
            OperationId = "CreateItem",
            Tags = new[] { "Item Management" }
        )]
        public async Task<IActionResult> CreateAsync(
            [FromBody, Bind] ItemModel model,
            [Required, FromRoute(Name = "listId")] Guid listId,
            CancellationToken ct)
        {
            ItemDto itemDto = await _itemService.CreateItemAsync(listId, model, ct);

            return CreatedAtRoute(
                GetItemRouteName,
                new { item_id = itemDto.PublicId },
                itemDto);
        }

        [HttpGet("{item_id}", Name = GetItemRouteName)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ItemDto))]
        [SwaggerOperation(
            summary: "Get item",
            description: "Get the item in the list",
            OperationId = "GetItem",
            Tags = new[] { "Item Management" }
        )]
        public async Task<IActionResult> GetAsync(
            [Required, FromRoute(Name = "item_id")] Guid? itemId,
            CancellationToken ct)
        {
            ItemDto itemDto = await _itemService.GetItemAsync(itemId.Value, ct);

            return Ok(itemDto);
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<ItemDto>))]
        [SwaggerOperation(
            summary: "Get items",
            description: "Get items in the list",
            OperationId = "CreateItems",
            Tags = new[] { "Item Management" }
        )]
        public async Task<IActionResult> GetAllAsync(
            [Required, FromQuery] Guid listId,
            CancellationToken ct)
        {
            List<ItemDto> listOfItems = await _itemService.GetAllItemsAsync(listId, ct: ct);

            return Ok(listOfItems);
        }

        [HttpDelete("{item_id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ItemDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [SwaggerOperation(
            summary: "Delete item",
            description: "Delete item in the list",
            OperationId = "DeleteItem",
            Tags = new[] { "Item Management" }
        )]
        public async Task<IActionResult> DeleteAsync(
            [Required, FromRoute(Name = "item_id")] Guid? itemId,
            CancellationToken ct)
        {
            await _itemService.DeleteItemAsync(itemId.Value, ct);
            return NoContent();
        }

        [HttpPut("{item_id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ItemDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [SwaggerOperation(
            summary: "Update item",
            description: "Update item in the list",
            OperationId = "UpdateItem",
            Tags = new[] { "Item Management" }
        )]
        public async Task<IActionResult> UpdateAsync(
            [Required, FromRoute(Name = "item_id")] Guid? itemId,
            [FromBody, Bind] ItemModel model,
            CancellationToken ct)
        {
            ItemDto itemDto = await _itemService.UpdateItemAsync(itemId.Value, model, ct);
            return Ok(itemDto);
        }

        [HttpPut("status/{item_id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ItemDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [SwaggerOperation(
            summary: "Update item status",
            description: "Update item status in the list",
            OperationId = "UpdateItemStatus",
            Tags = new[] { "Item Management" }
        )]
        public async Task<IActionResult> StatusAsync(
            [Required, FromRoute(Name = "item_id")] Guid? itemId, 
            CancellationToken ct)
        {
            ItemDto itemDto = await _itemService.UpdateStatusAsync(itemId.Value, ct);
            return Ok(itemDto);
        }
    }
}
