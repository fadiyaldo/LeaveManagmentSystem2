using System.ComponentModel.DataAnnotations;




namespace LeaveManagmentSystem2.web.Models.LeaveTypes
{
    public class LeaveTypeEditVM
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        [Display(Name = "Number of Days")]
        public int NumberOfDays { get; set; }
    }
}
