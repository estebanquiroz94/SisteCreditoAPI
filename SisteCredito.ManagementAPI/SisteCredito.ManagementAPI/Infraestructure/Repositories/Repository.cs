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
            return await _context.Set<T>().ToList();
        }

        public async Task<T> GetById(int id)
        {
            return await _context.Set<T>().Find(id);
        }

        public async Task Add(T entity)
        {
            await _context.Set<T>().Add(entity);
            await _context.SaveChanges();
        }

        public async Task Update(T entity)
        {
            _context.Set<T>().Update(entity);
            await _context.SaveChanges();
        }

        public async Task Delete(int id)
        {
            var entity = await _context.Set<T>().Find(id);
            _context.Set<T>().Remove(entity);
            await _context.SaveChanges();
        }

        public Task<List<T>> GetByDateRange()
        {
            throw new NotImplementedException();
        }
    }
}
