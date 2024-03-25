using BaseLibrary.Entities;
using BaseLibrary.Responses;
using Microsoft.EntityFrameworkCore;
using ServerLibrary.Data;
using ServerLibrary.Repositories.Contracts;


namespace ServerLibrary.Repositories.Implementations
{
    public class UnitRepository(AppDbContext appDbContext) : IGenericRepositoryInterface<Unit>
    {
        public async Task<GeneralResponse> DeleteById(int id)
        {
            var dep = await appDbContext.Units.FindAsync(id);
            if (dep is null) return NotFound();
            appDbContext.Units.Remove(dep);
            await Commit();
            return Success();
        }

        public async Task<List<Unit>> GetAll() => await appDbContext.Units.ToListAsync();


        public async Task<Unit> GetById(int id) => await appDbContext.Units.FindAsync(id);
        public async Task<GeneralResponse> Insert(Unit item)
        {
            if (!await CheckName(item.Name!)) return new GeneralResponse(false, "Unit already added");
            appDbContext.Units.Add(item);
            await Commit();
            return Success();
        }

        public async Task<GeneralResponse> Update(Unit item)
        {
            var dep = await appDbContext.Units.FindAsync(item.Id);
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
            var item = await appDbContext.Units.FirstOrDefaultAsync(x => x.Name!.ToLower().Equals(name.ToLower()));
            return item is null;
        }
    }
}
