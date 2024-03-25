
using BaseLibrary.DTOs;
using BaseLibrary.Entities;
using BaseLibrary.Responses;


namespace ServerLibrary.Repositories.Contracts
{
    public interface IItemRequestsManagement
    {
        Task<List<ManageItemRequest>> GetRequests();
        Task<GeneralResponse> UpdateRequest(ManageItemRequest request);
        Task<GeneralResponse> InsertRequest(ManageItemRequest request);
        Task<List<Status>> GetStatuses();
    }
}
