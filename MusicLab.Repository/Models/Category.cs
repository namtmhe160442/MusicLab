using System.ComponentModel.DataAnnotations;

namespace MusicLab.Repository.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [Required, MaxLength(100)]
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public virtual ICollection<SongCategory> SongCategories { get; set; }
    }
}
