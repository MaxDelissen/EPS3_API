using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Resources.Interfaces;

namespace DAL;

public class DirectDbRepository<E> : IDirectDbRepository<E> where E : Entity
{
    private readonly AppDbContext _context;
    protected DbSet<E> _dbSet;

    public DirectDbRepository(AppDbContext context)
    {
        _context = context;
        _dbSet = context.Set<E>();
    }

    public void Create(E entity)
    {
        _context.Add(entity);
        _context.SaveChanges();
    }

    public void Delete(E entity)
    {
        _context.Remove(entity);
        _context.SaveChanges();
    }

    public List<E> GetAll() => _dbSet.ToList();

    public List<E> GetWhere(Expression<Func<E, bool>> where) => _dbSet.Where(where).ToList();

    public void Update(E entity)
    {
        _context.Update(entity);
        _context.SaveChanges();
    }
}