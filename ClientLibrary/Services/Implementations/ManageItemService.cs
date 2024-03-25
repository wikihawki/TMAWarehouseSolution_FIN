using BaseLibrary.DTOs;
using BaseLibrary.Entities;
using BaseLibrary.Responses;
using ClientLibrary.Helpers;
using ClientLibrary.Services.Contracts;
using System.Net.Http.Json;


namespace ClientLibrary.Services.Implementations
{
    public class ManageItemService(GetHttpClient getHttpClient) : IManageItemService
    {

        public async Task<List<ManageItem>> GetWarehouseItems(string baseUrl) 
        {
            var httpClient = await getHttpClient.GetPrivateHttpClient();
            var result = await httpClient.GetFromJsonAsync<List<ManageItem>>($"{baseUrl}/items");
            return result!;

        }

        public async Task<GeneralResponse> UpdateWarehouseItem(ManageItem item,string baseUrl)
        {
            var httpClient = await getHttpClient.GetPrivateHttpClient();
            var result = await httpClient.PostAsJsonAsync($"{baseUrl}/update-item", item);
            if (!result.IsSuccessStatusCode) return new GeneralResponse(false, "Error occured");

            return await result.Content.ReadFromJsonAsync<GeneralResponse>()!;

        }

        public async Task<GeneralResponse> InsertWarehouseItem(ManageItem item, string baseUrl)
        {
            var httpClient = await getHttpClient.GetPrivateHttpClient();
            var result = await httpClient.PostAsJsonAsync($"{baseUrl}/create-item",item);
            if (!result.IsSuccessStatusCode) return new GeneralResponse(false, "Error occured");

            return await result.Content.ReadFromJsonAsync<GeneralResponse>()!;

        }


        public async Task<List<Unit>> GetUnits(string baseUrl)
        {
            var httpClient = await getHttpClient.GetPrivateHttpClient();
            var result = await httpClient.GetFromJsonAsync<List<Unit>>($"{baseUrl}/units");
            return result!;
        }



        public async Task<List<ItemGroup>> GetItemGroups(string baseUrl)
        {
            var httpClient = await getHttpClient.GetPrivateHttpClient();
            var result = await httpClient.GetFromJsonAsync<List<ItemGroup>>($"{baseUrl}/itemgroups");
            return result!;
        }

        public async Task<GeneralResponse> DeleteItem(int id, string baseUrl)
        {
            var httpClient = await getHttpClient.GetPrivateHttpClient();
            var result = await httpClient.DeleteAsync($"{baseUrl}/delete-item/{id}");
            if (!result.IsSuccessStatusCode) return new GeneralResponse(false, "Error ocurred");
            return await result.Content.ReadFromJsonAsync<GeneralResponse>()!;
        }

    }
}
