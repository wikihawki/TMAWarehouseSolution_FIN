using BaseLibrary.DTOs;
using BaseLibrary.Entities;
using BaseLibrary.Responses;


namespace ClientLibrary.Services.Contracts
{
    public interface IManageItemService
    {
        Task<List<ManageItem>> GetWarehouseItems(string baseUrl);
        Task<GeneralResponse> UpdateWarehouseItem(ManageItem item,string baseUrl);
        Task<List<Unit>> GetUnits(string baseUrl);

        Task<List<ItemGroup>> GetItemGroups(string baseUrl);
        Task<GeneralResponse> DeleteItem(int id, string baseUrl);

        Task<GeneralResponse> InsertWarehouseItem(ManageItem item, string baseUrl);



    }
}
