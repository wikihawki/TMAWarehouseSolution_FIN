

using BaseLibrary.Entities;
using BaseLibrary.Responses;
using Microsoft.EntityFrameworkCore;
using ServerLibrary.Data;
using ServerLibrary.Repositories.Contracts;

namespace ServerLibrary.Repositories.Implementations
{
    public class ItemRepository(AppDbContext appDbContext) : IGenericRepositoryInterface<Item>
    {
        public async Task<GeneralResponse> DeleteById(int id)
        {
            var dep = await appDbContext.Items.FindAsync(id);
            if (dep is null) return NotFound();
            appDbContext.Items.Remove(dep);
            await Commit();
            return Success();
        }

        public async Task<List<Item>> GetAll() => await appDbContext.Items.ToListAsync();


        public async Task<Item> GetById(int id) => await appDbContext.Items.FindAsync(id);
        public async Task<GeneralResponse> Insert(Item item)
        {

            appDbContext.Items.Add(item);
            await Commit();
            return Success();
        }

        public async Task<GeneralResponse> Update(Item item)
        {
            var dep = await appDbContext.Items.FindAsync(item.ItemId);
            if (dep is null) return NotFound();
            await Commit();
            return Success();
        }

        private static GeneralResponse NotFound() => new(false, "Sorry department not found");
        private static GeneralResponse Success() => new(true, "Process completed");
        private async Task Commit() => await appDbContext.SaveChangesAsync();


    }
}
