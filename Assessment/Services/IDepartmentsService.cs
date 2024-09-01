namespace Assessment.Services
{
    public interface IDepartmentsService
    {
        Task<IEnumerable<Department>> GetAll();
        Task<Department> GetById(int id);
        Task<Department> Add(Department dept);
        Department Update(Department dept);
        Department Delete(Department dept);
    }
}