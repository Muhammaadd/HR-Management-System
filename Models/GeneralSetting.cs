namespace HRSystem.Models
{
    public class GeneralSetting
    {
        public GeneralSetting()
        {
            DaysOff = new List<WeeklyHoliday>();
        }
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        public int ValueOfExtra { get; set; }
        public int ValueOfDiscount { get; set; }
        public virtual List<WeeklyHoliday>? DaysOff { get; set; }
    }
}
