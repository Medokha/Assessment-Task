namespace Assessment.Services
{
    public interface IEmployeeService
    {
        Task<IEnumerable<Employee>> GetAll(int genreId = 0);
        Task<Employee> GetById(int id);
        Task<Employee> Add(Employee movie);
        Employee Update(Employee movie);
        Employee Delete(Employee movie);
    }
}