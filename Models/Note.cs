using System.ComponentModel.DataAnnotations;

namespace BlazorWeb.Models
{
    public class Note
    {
        public int Id { get; set; }
        public string Content { get; set; } = string.Empty;
        public User User { get; set; } = null!;
    }
}
