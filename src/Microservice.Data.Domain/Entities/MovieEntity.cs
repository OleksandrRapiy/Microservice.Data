using Microservice.Data.Domain.Entites.Base;

namespace Microservice.Data.Domain.Entites
{
    public class MovieEntity: BaseEntity<int>
    {
        public string Name { get; set; }
        public string Author { get; set; }

        public MovieEntity()
        { }

        public MovieEntity(string name, string author)
        {
            Name = name;
            Author = author;
        }
    }
}
