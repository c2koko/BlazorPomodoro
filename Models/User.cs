using System.ComponentModel.DataAnnotations;

namespace BlazorWeb.Models
{
    public class User
    {
        [Key]
        public string Name { get; set; } = string.Empty;
        public int Streak { get; set; } = 0;
        public DateTime LastOpen { get; set; }
        public List<Note> Notes { get; set; } = new List<Note>();
    }
}