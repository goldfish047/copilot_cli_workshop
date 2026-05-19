using System.ComponentModel.DataAnnotations;

namespace Chinook.Web.Models
{
    public class Genre
    {
        [Key]
        public int GenreId { get; set; }
        public string Name { get; set; }
    }
}
