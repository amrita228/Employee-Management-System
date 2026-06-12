namespace EmployeeManagementAPI.DTOs
{
    public class EmployeeDTO
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public string Gender { get; set; }

        public DateTime DateOfBirth { get; set; }

        public int DepartmentId { get; set; }

        public int RoleId { get; set; }

        public decimal Salary { get; set; }

        public DateTime JoiningDate { get; set; }

        public string Address { get; set; }
    }
}