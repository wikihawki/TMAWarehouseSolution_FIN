using BaseLibrary.Entities;
using BaseLibrary.Responses;
using Microsoft.EntityFrameworkCore;
using ServerLibrary.Data;
using ServerLibrary.Repositories.Contracts;


namespace ServerLibrary.Repositories.Implementations
{
    public class ItemGroupRepository(AppDbContext appDbContext) : IGenericRepositoryInterface<ItemGroup>
    {
        public async Task<GeneralResponse> DeleteById(int id)
        {
            var dep = await appDbContext.ItemGroups.FindAsync(id);
            if (dep is null) return NotFound();
            appDbContext.ItemGroups.Remove(dep);
            await Commit();
            return Success();
        }

        public async Task<List<ItemGroup>> GetAll() => await appDbContext.ItemGroups.ToListAsync();

        public async Task<ItemGroup> GetById(int id) => await appDbContext.ItemGroups.FindAsync(id);
        public async Task<GeneralResponse> Insert(ItemGroup item)
        {
            if (!await CheckName(item.Name!)) return new GeneralResponse(false, "ItemGroup already added");
            appDbContext.ItemGroups.Add(item);
            await Commit();
            return Success();
        }

        public async Task<GeneralResponse> Update(ItemGroup item)
        {
            var dep = await appDbContext.ItemGroups.FindAsync(item.Id);
            if (dep is null) return NotFound();
            dep.Name = item.Name;
            await Commit();
            return Success();
        }

        private static GeneralResponse NotFound() => new(false, "Sorry department not found");
        private static GeneralResponse Success() => new(true, "Process completed");
        private async Task Commit() => await appDbContext.SaveChangesAsync();

        private async Task<bool> CheckName(string name)
        {
            var item = await appDbContext.ItemGroups.FirstOrDefaultAsync(x => x.Name!.ToLower().Equals(name.ToLower()));
            return item is null;
        }
    }
    
    
}
