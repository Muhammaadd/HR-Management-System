namespace HRSystem.Models
{
    public class WeeklyHoliday
    {
        [ForeignKey("GeneralSetting")]
        public int GeneralId { get; set; }
        public string Day { get; set; }
        public virtual GeneralSetting? GeneralSetting { get; set; }
    }
}
