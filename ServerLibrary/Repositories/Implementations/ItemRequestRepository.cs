using BaseLibrary.Entities;
using BaseLibrary.Responses;
using Microsoft.EntityFrameworkCore;
using ServerLibrary.Data;
using ServerLibrary.Repositories.Contracts;


namespace ServerLibrary.Repositories.Implementations
{
    public class ItemRequestRepository(AppDbContext appDbContext) : IGenericRepositoryInterface<ItemRequest>
    {
    
        public async Task<GeneralResponse> DeleteById(int id)
        {
            var dep = await appDbContext.Requests.FindAsync(id);
            if (dep is null) return NotFound();
            appDbContext.Requests.Remove(dep);
            await Commit();
            return Success();
        }

        public async Task<List<ItemRequest>> GetAll() => await appDbContext.Requests.Include(gd => gd.Item).ToListAsync();


        public async Task<ItemRequest> GetById(int id) => await appDbContext.Requests.FindAsync(id);
        public async Task<GeneralResponse> Insert(ItemRequest request)
        {

            appDbContext.Requests.Add(request);
            await Commit();
            return Success();
        }

        public async Task<GeneralResponse> Update(ItemRequest request)
        {
            var dep = await appDbContext.Requests.FindAsync(request.RequestId);
            if (dep is null) return NotFound();
            await Commit();
            return Success();
        }

        private static GeneralResponse NotFound() => new(false, "Sorry department not found");
        private static GeneralResponse Success() => new(true, "Process completed");
        private async Task Commit() => await appDbContext.SaveChangesAsync();

    }
}
