using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace SalesWebMvc.Models
{
    public class DepartmentIdModel
    {
        public int DepartmentId { get; set; }

        [ValidateNever]
        public Department? Department { get; set; }
    }
}
