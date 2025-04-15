using System.ComponentModel.DataAnnotations;

namespace MyToDoAppSQL.Models
{
    public class Item
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string ToDo { get; set; } = string.Empty;
        [Required]
        public DateTime DueDate { get; set; } = DateTime.Now;
        [Required]
        public bool IsComplete { get; set; } = false;
    }
}
