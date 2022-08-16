namespace HRSystem.Models
{
    public class Attendance
    {
        public int Id { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime Date { get; set; }
        public TimeSpan Start { get; set; }
        public TimeSpan End { get; set; }
        public bool Absent { get; set; }
        public int BonusHours { get; set; }
        public int DiscountHours { get; set; }
        [ForeignKey("Employee")]
        public int? EmpId { get; set; }
        public virtual Employee? Employee { get; set; }
    }
}
