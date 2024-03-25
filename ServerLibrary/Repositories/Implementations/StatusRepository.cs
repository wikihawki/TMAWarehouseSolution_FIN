using BaseLibrary.Entities;
using BaseLibrary.Responses;
using Microsoft.EntityFrameworkCore;
using ServerLibrary.Data;
using ServerLibrary.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerLibrary.Repositories.Implementations
{
    public class StatusRepository(AppDbContext appDbContext) : IGenericRepositoryInterface<Status>
    {
        public async Task<GeneralResponse> DeleteById(int id)
        {
            var dep = await appDbContext.Statuses.FindAsync(id);
            if (dep is null) return NotFound();
            appDbContext.Statuses.Remove(dep);
            await Commit();
            return Success();
        }

        public async Task<List<Status>> GetAll() => await appDbContext.Statuses.ToListAsync();


        public async Task<Status> GetById(int id) => await appDbContext.Statuses.FindAsync(id);
        public async Task<GeneralResponse> Insert(Status item)
        {

            appDbContext.Statuses.Add(item);
            await Commit();
            return Success();
        }

        public async Task<GeneralResponse> Update(Status item)
        {
            var dep = await appDbContext.Statuses.FindAsync(item.Id);
            if (dep is null) return NotFound();
            await Commit();
            return Success();
        }

        private static GeneralResponse NotFound() => new(false, "Sorry department not found");
        private static GeneralResponse Success() => new(true, "Process completed");
        private async Task Commit() => await appDbContext.SaveChangesAsync();


    }
}
