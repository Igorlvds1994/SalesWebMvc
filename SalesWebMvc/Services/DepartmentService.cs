using SalesWebMvc.Data;
using SalesWebMvc.Models;

namespace SalesWebMvc.Services
{
    public class DepartmentService
    {
        private readonly SalesWebMvcContext _context;

        public DepartmentService (SalesWebMvcContext contex)
        {
            _context = contex;
        }
        public List<Department> FindAll()
        {
            return _context.Department.OrderBy( x => x.Name).ToList();
        }
        public Department FindById(int id)
        {
            return _context.Department.FirstOrDefault(x => x.Id == id);
        }

    }
}
