using System.Text.Json.Serialization;

namespace LeaveManegementApi.Models.Movies
{
    public class Actor
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
        public List<ActorMovie> actorMovies { get; set; }


    }
}
