namespace LeaveManegementApi.Repository.Movie
{
    public interface IActor
    {
        Task<List<Models.Movies.Actor>> Actors();
        Task<Models.Movies.Actor> Actor();
    }
}
