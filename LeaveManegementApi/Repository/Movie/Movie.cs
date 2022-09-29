using LeaveManegementApi.Data;
using Microsoft.EntityFrameworkCore;

namespace LeaveManegementApi.Repository.Movie
{
    public class Movie : IMovie
    {
        private readonly LeaveManagementDBContext _leaveManagementDBContext;

        public Movie(LeaveManagementDBContext leaveManagementDBContext)
        {
            _leaveManagementDBContext = leaveManagementDBContext;
        }

        public async Task<List<Models.Movies.Movie>> GetAllMovies()
        {
            return await _leaveManagementDBContext.Movies
                .Include(x=>x.Producer).Include(x=>x.Actors).ToListAsync();
        }

        public Task<List<Models.Movies.Movie>> GetAllMoviesByActor(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Models.Movies.Movie> GetAllMoviesById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Models.Movies.Movie>> GetAllMoviesByProducor(int id)
        {
            throw new NotImplementedException();
        }
    }
}
