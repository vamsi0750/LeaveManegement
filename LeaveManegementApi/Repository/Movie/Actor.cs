using LeaveManegementApi.Data;
using Microsoft.EntityFrameworkCore;

namespace LeaveManegementApi.Repository.Movie
{
    public class Actor : IActor
    {
        private readonly LeaveManagementDBContext _leaveManagementDBContext;
        public Actor(LeaveManagementDBContext leaveManagementDBContext)
        {
            _leaveManagementDBContext = leaveManagementDBContext;
        }
        public async Task<List<Models.Movies.Actor>> Actors()
        {
            return await _leaveManagementDBContext.Actors.ToListAsync();
        }

        Task<Models.Movies.Actor> IActor.Actor()
        {
            throw new NotImplementedException();
        }
    }
}
