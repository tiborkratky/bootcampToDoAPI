using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Bootcamp.ToDoList.Backend.Entities.DTO;
using Bootcamp.ToDoList.Backend.Entities.Models;

namespace Bootcamp.ToDoList.Backend.Services.Interfaces
{
    public interface IItemService
    {
        Task<ItemDto> CreateItemAsync(Guid listId, ItemModel model, CancellationToken ct = default);
        Task<ItemDto> GetItemAsync(Guid itemId, CancellationToken ct = default);
        Task<List<ItemDto>> GetAllItemsAsync(Guid listId, int? pageSize = null, CancellationToken ct = default);
        Task<ItemDto> UpdateItemAsync(Guid itemId, ItemModel model, CancellationToken ct = default);
        Task DeleteItemAsync(Guid itemId, CancellationToken ct = default);
        Task<ItemDto> UpdateStatusAsync(Guid itemId, CancellationToken ct = default);
    }
}