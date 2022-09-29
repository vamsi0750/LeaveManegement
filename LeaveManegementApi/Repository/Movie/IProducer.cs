namespace LeaveManegementApi.Repository.Movie
{
    public interface IProducer
    {
        Task<List<Models.Movies.Producer>> producers();
        Task<Models.Movies.Producer> producer(int id);

    }
}
