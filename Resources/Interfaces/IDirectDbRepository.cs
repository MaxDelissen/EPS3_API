using System.Linq.Expressions;
using Resources.Models.DbModels;

namespace Resources.Interfaces;

public interface IDirectDbRepository<E> where E : Entity
{
    public void Create(E entity);
    public void Delete(E entity);
    public List<E> GetAll(); // technically not usefull because of GetWhere(), but nice to have
    public List<E> GetWhere(Expression<Func<E, bool>> where);
    public void Update(E entity);
}