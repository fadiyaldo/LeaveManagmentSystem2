using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LeaveManagmentSystem2.web.Data
{
    public class LeaveType
    {
        public int Id { get; set; }

        [Column(TypeName = "nvarchar(150)")]
        public string Name { get; set; }

        [Display(Name = "Number of Days")]
        public int NumberOfDays { get; set; }
    }
}
