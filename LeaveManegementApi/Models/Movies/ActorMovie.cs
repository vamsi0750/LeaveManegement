using System.Text.Json.Serialization;

namespace LeaveManegementApi.Models.Movies
{
    public class ActorMovie
    {
        public int Id { get; set; }
        public Movie Movie { get; set; }
        public Actor Actor  { get; set; }
    }
}
