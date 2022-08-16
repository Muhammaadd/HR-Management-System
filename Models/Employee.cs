using System.ComponentModel;

namespace HRSystem.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string ContactNumber { get; set; }
        [DataType(DataType.Time)]

        public TimeSpan Start { get; set; }
        [DataType(DataType.Time)]

        public TimeSpan End { get; set; }
        [Column(TypeName = "money")]
        public double NetSalary { get; set; }
        public string Nationality { get; set; }

        public string NationalId { get; set; }
        public string Gender { get; set; }
        [Column(TypeName = "datetime")]
        [DataType(DataType.Date)]
        public DateTime ContractDate { get; set; }
        [Column(TypeName = "datetime")]
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }
        [ForeignKey("Department")]
        public int DeptId { get; set; }
        public virtual Department? Department { get; set; }
        public virtual List<Attendance>? Attendance { get; set; }
        public virtual List<ExceptionAttendance>? Exceptions { get; set; }
    }
}
