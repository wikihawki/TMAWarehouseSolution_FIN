using BaseLibrary.DTOs;
using BaseLibrary.Entities;
using BaseLibrary.Responses;


namespace ClientLibrary.Services.Contracts
{
    public interface IManageItemRequestService
    {
        Task<List<ManageItemRequest>> GetRequests(string baseUrl);
        Task<GeneralResponse> UpdateRequest(ManageItemRequest request, string baseUrl);
        Task<GeneralResponse> InsertRequest(ManageItemRequest request, string baseUrl);
        Task<List<Status>> GetStatuses(string baseUrl);
    }
}
