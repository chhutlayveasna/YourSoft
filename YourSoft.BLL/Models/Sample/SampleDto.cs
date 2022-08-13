using System.ComponentModel.DataAnnotations;

namespace YourSoft.BLL.Models.Sample
{
    public class SampleDto
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public DateTime Date { get; set; }
        public bool IsActive { get; set; }
    }
}
