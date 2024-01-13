using HoteListing.API.Contracts;
using HoteListing.API.Data;
using Microsoft.EntityFrameworkCore;

namespace HoteListing.API.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly HotelListingDBContext _context;
        
        public GenericRepository(HotelListingDBContext context)
        {
            _context = context;
        }

        public async Task<T> AddAsync(T entity)
        {
            await _context.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteAsync(int? id)
        {
            if (id is null) throw new ArgumentNullException(nameof(id));
            var entity = await GetAsync(id) ?? throw new ArgumentNullException("Record does not exist.");
            _context.Set<T>().Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> Exists(int id)
        {
            return await GetAsync(id) != null;
        }

        public async Task<List<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<T> GetAsync(int? id)
        {
            return id is null ? null : await _context.Set<T>().FindAsync(id);
        }

        public async Task UpdateAsync(T entity)
        {
            _context.Update(entity);
            await _context.SaveChangesAsync();
        }
    }
}
