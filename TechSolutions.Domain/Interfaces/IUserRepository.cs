using System.Linq.Expressions;
using TechSolutions.Domain.Entities;

namespace TechSolutions.Domain.Interfaces;

public interface IUserRepository
{
    Task<IEnumerable<User>> GetAll(Expression<Func<User, bool>> filter = null);
    Task<User> GetById(Guid id);
    Task<User> GetByEmail(string email);
    Task<User> GetByCpf(string cpf);
    Task<User> Add(User user);
    Task Update(User user);
    Task Delete(User user);
}
