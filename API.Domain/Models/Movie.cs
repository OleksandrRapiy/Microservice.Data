using API.Domain.Models.Base;

namespace API.Domain.Models
{
    public class Movie: Entity<int>
    {
        public string Name { get; set; }
        public string Author { get; set; }
    }
}
