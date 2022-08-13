using System.ComponentModel.DataAnnotations;

namespace YourSoft.BLL.Models.Sample
{
    public class CreateSampleDto
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public DateTime Date { get; set; }
        public bool IsActive { get; set; }
    }
}
