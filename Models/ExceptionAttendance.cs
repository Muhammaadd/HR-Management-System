namespace HRSystem.Models
{
    public class ExceptionAttendance
    {
        public int Id { get; set; }
        [DataType(DataType.Date)]
        [CheckValidDate]
        public DateTime Date { get; set; }
        [DataType(DataType.Time)]
        [ExceptionsCheckTime]
        public TimeSpan Start { get; set; }
        [DataType(DataType.Time)]
        [ExceptionsCheckTime]
        public TimeSpan End { get; set; }
        [ForeignKey("Employee")]
        public int? EmployeeId { get; set; }
        public virtual Employee? Employee { get; set; }
    }
}
