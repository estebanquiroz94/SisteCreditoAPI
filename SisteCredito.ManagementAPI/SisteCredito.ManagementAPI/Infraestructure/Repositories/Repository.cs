using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Crypto;
using SisteCredito.ManagementAPI.Infraestructure.Data;
using System.Linq;
using static SisteCredito.ManagementAPI.Domain.Interfaces.IRepository;

namespace SisteCredito.ManagementAPI.Infraestructure.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly SisteCreditoContext _context;

        public Repository(SisteCreditoContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<T> GetById(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task Add(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Update(T entity)
        {
            _context.Set<T>().Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var entity = await _context.Set<T>().FindAsync(id);
            _context.Set<T>().Remove(entity);
            await _context.SaveChangesAsync();
        }

        public Task<List<T>> GetByDateRange()
        {
            throw new NotImplementedException();
        }
    }
}
