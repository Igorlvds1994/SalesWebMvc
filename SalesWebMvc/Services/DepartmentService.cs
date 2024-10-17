using Microsoft.EntityFrameworkCore;
using SalesWebMvc.Data;
using SalesWebMvc.Models;
using System.Threading.Tasks;

namespace SalesWebMvc.Services
{
    public class DepartmentService
    {
        private readonly SalesWebMvcContext _context;

        // Use um nome de parâmetro mais claro
        public DepartmentService(SalesWebMvcContext context)
        {
            _context = context;
        }

        // Método assíncrono para obter todos os departamentos, ordenados pelo nome
        public async Task<List<Department>> FindAllAsync()
        {
            return await _context.Department.OrderBy(x => x.Name).ToListAsync(); // Corrigido para "Departments"
        }

        // Método assíncrono para encontrar um departamento por ID
        public async Task<Department> FindByIdAsync(int id)
        {
            return await _context.Department.FirstOrDefaultAsync(x => x.Id == id); // Corrigido para "Departments"
        }
    }
}
