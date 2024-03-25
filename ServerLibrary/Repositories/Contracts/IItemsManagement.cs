
using BaseLibrary.DTOs;
using BaseLibrary.Entities;
using BaseLibrary.Responses;


namespace ServerLibrary.Repositories.Contracts
{
    public interface IItemsManagement
    {
        Task<List<ManageItem>> GetWarehouseItems();
        Task<GeneralResponse> UpdateWarehouseItem(ManageItem item);
        Task<GeneralResponse> InsertWarehouseItem(ManageItem item);
        Task<List<Unit>> GetUnits();
        Task<List<ItemGroup>> GetItemGroups();
        Task<GeneralResponse> DeleteItem(int id);
    }
}
