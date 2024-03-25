

using Azure;
using BaseLibrary.DTOs;
using BaseLibrary.Entities;
using BaseLibrary.Responses;
using Microsoft.EntityFrameworkCore;
using ServerLibrary.Data;
using ServerLibrary.Repositories.Contracts;

namespace ServerLibrary.Repositories.Implementations
{
    public class ManageItemsRepository(AppDbContext appDbContext) : IItemsManagement
    {
        //Dodaje 
        private async Task<List<Unit>> Units() => await appDbContext.Units.AsNoTracking().ToListAsync();
        private async Task<List<ItemGroup>> ItemGroups() => await appDbContext.ItemGroups.AsNoTracking().ToListAsync();
        private async Task<List<Item>> GetItems() => await appDbContext.Items.AsNoTracking().ToListAsync();

       public async Task<List<ManageItem>> GetWarehouseItems()
        {
            var allItems = await GetItems();
            var allItemGroups = await ItemGroups();
            var allUnits = await Units();

            if (allItems.Count == 0 || allItemGroups.Count == 0 || allUnits.Count == 0) return [];

            var items = new List<ManageItem>();
            foreach (var item in allItems)
            {
                var itemGroup = allItemGroups.FirstOrDefault(u => u.Id == item.ItemGroup);
                var unit = allUnits.FirstOrDefault(r => r.Id == item!.Unit);
                items.Add(new ManageItem() { ItemId = item.ItemId, ItemGroup = itemGroup!.Name!, Unit = unit!.Name!, Quantity= item.Quantity,UAH=item.UAH,Status=item.Status, StorageLocation = item.StorageLocation , ContactPerson = item.ContactPerson });

            }
            return items;
        }

        public async Task<GeneralResponse> UpdateWarehouseItem(ManageItem item)
        {
            var itemGroup = (await ItemGroups()).FirstOrDefault(i => i.Name!.Equals(item.ItemGroup));
            var unit = (await Units()).FirstOrDefault(u => u.Name!.Equals(item.Unit));
            var warehouseItem = await appDbContext.Items.FindAsync(item.ItemId);
            if (warehouseItem is null) return NotFound();
            warehouseItem!.Unit = unit!.Id;
            warehouseItem!.ItemGroup = itemGroup!.Id;
            warehouseItem!.Quantity = item.Quantity;
            warehouseItem!.UAH = item.UAH;
            warehouseItem!.Status = item.Status;
            warehouseItem!.StorageLocation = item.StorageLocation;
            warehouseItem!.ContactPerson = item.ContactPerson;
            await appDbContext.SaveChangesAsync();
            return Success();
        }

        public async Task<GeneralResponse> InsertWarehouseItem(ManageItem item)
        {
            var itemGroup = (await ItemGroups()).FirstOrDefault(i => i.Name!.Equals(item.ItemGroup));
            var unit = (await Units()).FirstOrDefault(u => u.Name!.Equals(item.Unit));
            if (itemGroup is null || unit is null) return NotFound();
            var databaseItem = new Item() { Unit = unit.Id, ItemGroup = itemGroup.Id, Quantity = item.Quantity, UAH = item.UAH, Status = item.Status, StorageLocation = item.StorageLocation, ContactPerson = item.ContactPerson };
            appDbContext.Items.Add(databaseItem);
            await appDbContext.SaveChangesAsync();
            return Success();


        }

        public async Task<List<Unit>> GetUnits() => await Units();

        public async Task<List<ItemGroup>> GetItemGroups() => await ItemGroups();

        public async Task<GeneralResponse> DeleteItem(int id)
        {
            var item = await appDbContext.Items.FirstOrDefaultAsync(u => u.ItemId == id);
            appDbContext.Items.Remove(item!);
            await appDbContext.SaveChangesAsync();
            return Success();
        }

        private static GeneralResponse NotFound() => new(false, "Sorry, error occured");
        private static GeneralResponse Success() => new(true, "Process completed");


    }
}
