namespace LeaveManegementApi.Models.Movies
{
    public class Movie
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public List<ActorMovie> ActorMovies { get; set; } 
        public string Description { get; set; } = string.Empty;
        public Producer Producer { get; set; }
        public string ImageUrl { get; set; }
        public DateTime ReleseDate { get; set; }
        public DateTime DateOfBirth { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
    }
}
