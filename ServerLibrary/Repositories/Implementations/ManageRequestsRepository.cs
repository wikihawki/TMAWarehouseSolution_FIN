
using Azure.Core;
using BaseLibrary.DTOs;
using BaseLibrary.Entities;
using BaseLibrary.Responses;
using Microsoft.EntityFrameworkCore;
using ServerLibrary.Data;
using ServerLibrary.Helpers;

using ServerLibrary.Repositories.Contracts;
using System.Security.Cryptography.X509Certificates;

namespace ServerLibrary.Repositories.Implementations
{
    public class ManageRequestsRepository(AppDbContext appDbContext) : IItemRequestsManagement
    {
        //Dodaje 
        private async Task<List<Unit>> Units() => await appDbContext.Units.AsNoTracking().ToListAsync();
        private async Task<List<Status>> Statuses() => await appDbContext.Statuses.AsNoTracking().ToListAsync();
        private async Task<List<ItemGroup>> ItemGroups() => await appDbContext.ItemGroups.AsNoTracking().ToListAsync();
        private async Task<List<ItemRequest>> GetItemRequests() => await appDbContext.Requests.Include(gd => gd.Item).AsNoTracking().ToListAsync();

        public async Task<List<ManageItemRequest>> GetRequests()
        {
            var allRequests = await GetItemRequests();
            var allStatuses = await GetStatuses();
            var allItemGroups = await ItemGroups();
            var allUnits = await Units();

            if (allRequests.Count == 0 || allStatuses.Count == 0 || allUnits.Count == 0 || allItemGroups.Count ==0) return [];

            var requests = new List<ManageItemRequest>();
            foreach (var request in allRequests)
            {
                var itemGroup = allItemGroups.FirstOrDefault(u => u.Id == request?.Item.ItemGroup);
                var status = allStatuses.FirstOrDefault(s => s.Id == request.Status);
                var unit = allUnits.FirstOrDefault(r => r.Id == request!.Unit);
                Console.WriteLine(request);
                requests.Add(new ManageItemRequest() { RequestId = request.RequestId,ItemId= request.ItemId, EmployeeName = request.EmployeeName, ItemName= itemGroup?.Name, Unit= unit?.Name, Quantity= request.Quantity, UAH = request.UAH, Comment= request.Comment,Status = status?.Name  });
            }
            return requests;
        }

        public async Task<GeneralResponse> UpdateRequest(ManageItemRequest request)
        {
            var order = await appDbContext.Requests.FindAsync(request.RequestId);
            var status = (await Statuses()).FirstOrDefault(s => s.Name!.Equals(request.Status));
            
            if (order is null) return NotFound();
            order.Status = status?.Id;
            order.Comment= request.Comment;

            var item = await appDbContext.Items.FindAsync(request.ItemId);
            if (item is null) return new(false, "Sorry, but connected item was not found in warehouse");
            item.Quantity = item.Quantity - request.Quantity;
            if(item.Quantity<= 0) return new(false, "Sorry, but this request cannot be accepted because there is no sufficient items in warehouse");


            await appDbContext.SaveChangesAsync();
            return Success();
        }

        public async Task<GeneralResponse> InsertRequest(ManageItemRequest request)
        {
            var itemGroup = (await ItemGroups()).FirstOrDefault(i => i.Name!.Equals(request.ItemName));
            var unit = (await Units()).FirstOrDefault(u => u.Name!.Equals(request.Unit));
            var status = await Statuses();

            if (itemGroup is null || unit is null ) return new(false, "Sorry, some error occured");
            var databaseRequest= new ItemRequest() { EmployeeName=request.EmployeeName, ItemId= request.ItemId, Unit = unit.Id, Quantity = request.Quantity, UAH = request.UAH, Comment= request.Comment, Status = 1};
            appDbContext.Requests.Add(databaseRequest);
            await appDbContext.SaveChangesAsync();
            return Success();


        }

        public async Task<List<Status>> GetStatuses() => await Statuses();
        private static GeneralResponse NotFound() => new(false, "Sorry, error occured");
        private static GeneralResponse Success() => new(true, "Process completed");


    }
}
