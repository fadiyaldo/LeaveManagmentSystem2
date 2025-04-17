using System.ComponentModel.DataAnnotations;

namespace LeaveManagmentSystem2.web.Models.LeaveTypes
{
    public class LeaveTypeCreateVM
    {
        [Required]
        [Length(4, 150, ErrorMessage = "Name must be between 4 and 150 characters")]
        public string Name { get; set; } = string.Empty;

        [Required]
        [Range(1, 25)]
        [Display(Name = "Number of Days")]
        public int NumberOfDays { get; set; }
    }
}
