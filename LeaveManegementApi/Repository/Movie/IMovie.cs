using LeaveManegementApi.Models;
namespace LeaveManegementApi.Repository.Movie
{
    public interface IMovie
    {
        Task<List<Models.Movies.Movie>> GetAllMovies();
        Task<List<Models.Movies.Movie>> GetAllMoviesByActor(int id);
        Task<List<Models.Movies.Movie>> GetAllMoviesByProducor(int id);
        Task<Models.Movies.Movie> GetAllMoviesById(int id);
    }
}
