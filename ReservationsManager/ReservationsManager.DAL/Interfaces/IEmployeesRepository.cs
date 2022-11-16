using ReservationsManager.Domain.Models;

namespace ReservationsManager.DAL.Interfaces
{
    public interface IEmployeesRepository: IGenericRepository<Employee>
    {
        public Task<Employee> GetByUsernameAsync(string username);
    }
}
