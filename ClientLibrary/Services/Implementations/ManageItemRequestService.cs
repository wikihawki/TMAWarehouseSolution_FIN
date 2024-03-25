using BaseLibrary.DTOs;
using BaseLibrary.Entities;
using BaseLibrary.Responses;
using ClientLibrary.Helpers;
using ClientLibrary.Services.Contracts;
using System.Net.Http.Json;

namespace ClientLibrary.Services.Implementations
{
    public class ManageItemRequestService(GetHttpClient getHttpClient) : IManageItemRequestService
    {
        public async Task<List<ManageItemRequest>> GetRequests(string baseUrl)
        {
            var httpClient = await getHttpClient.GetPrivateHttpClient();
            var result = await httpClient.GetFromJsonAsync<List<ManageItemRequest>>($"{baseUrl}/requests");
            return result!;
        }

        public async Task<List<Status>> GetStatuses(string baseUrl)
        {
            var httpClient = await getHttpClient.GetPrivateHttpClient();
            var result = await httpClient.GetFromJsonAsync<List<Status>>($"{baseUrl}/statuses");
            return result!;
        }

        public async Task<GeneralResponse> InsertRequest(ManageItemRequest request, string baseUrl)
        {
            var httpClient = await getHttpClient.GetPrivateHttpClient();
            var result = await httpClient.PostAsJsonAsync($"{baseUrl}/create-request", request);
            if (!result.IsSuccessStatusCode) return new GeneralResponse(false, "Error occured");

            return await result.Content.ReadFromJsonAsync<GeneralResponse>()!;
        }

        public async Task<GeneralResponse> UpdateRequest(ManageItemRequest request, string baseUrl)
        {
            var httpClient = await getHttpClient.GetPrivateHttpClient();
            var result = await httpClient.PostAsJsonAsync($"{baseUrl}/update-request", request);
            if (!result.IsSuccessStatusCode) return new GeneralResponse(false, "Error occured");

            return await result.Content.ReadFromJsonAsync<GeneralResponse>()!;
        }
    }
}
