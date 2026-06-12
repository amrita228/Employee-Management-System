using System.ComponentModel.DataAnnotations;

namespace EmployeeManagementAPI.Models
{
    public class Department
    {
        [Key]
        public int DepartmentId { get; set; }

        [Required]
        public string DepartmentName { get; set; }

        public ICollection<Employee>? Employees { get; set; }
    }
}