using LeaveManegementApi.Data;
using Microsoft.EntityFrameworkCore;

namespace LeaveManegementApi.Repository.Movie
{
    public class Producer : IProducer
    {
        private readonly LeaveManagementDBContext _leaveManagementDBContext;
        public Producer(LeaveManagementDBContext leaveManagementDBContext)
        {
            _leaveManagementDBContext = leaveManagementDBContext;
        }
        public Task<Models.Movies.Producer> producer(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Models.Movies.Producer>> producers()
        {
            return await _leaveManagementDBContext.producers.ToListAsync();
        }
    }
}
