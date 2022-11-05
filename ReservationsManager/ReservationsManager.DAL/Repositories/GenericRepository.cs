using EFCoreMappingApp;
using Microsoft.EntityFrameworkCore;
using ReservationsManager.DAL.Interfaces;
using ReservationsManager.Domain.Models;
using System.Linq.Expressions;

namespace ReservationsManager.DAL.Repositories
{
    public abstract class GenericRepository<T> : IGenericRepository<T> where T : Entity
    {
        protected readonly RezervationsDbContext _context;

        public GenericRepository(RezervationsDbContext context) => 
            _context = context;

        public async Task AddAsync(T entity) => 
            await _context.Set<T>().AddAsync(entity);

        public async Task AddRangeAsync(IEnumerable<T> entities) => 
            await _context.Set<T>().AddRangeAsync(entities);

        public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> expression) =>
            await _context.Set<T>().Where(expression).ToListAsync();

        public async Task<IEnumerable<T>> GetAllAsync() =>
            await _context.Set<T>().ToListAsync();

        public async Task<T> GetByIdAsync(int id) =>
            await _context.Set<T>().FindAsync(id);

        public void Update(T entity)
        {
            _context.Set<T>().Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }
        public void Remove(T entity) => 
            _context.Set<T>().Remove(entity);

        public async Task SaveAsync() =>
            await _context.SaveChangesAsync();
    }
}
