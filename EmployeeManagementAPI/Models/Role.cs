using System.ComponentModel.DataAnnotations;

namespace EmployeeManagementAPI.Models
{
    public class Role
    {
        [Key]
        public int RoleId { get; set; }

        [Required]
        public string RoleName { get; set; }

        public ICollection<Employee>? Employees { get; set; }
    }
}