using LeaveManegementApi.Models;

namespace LeaveManegementApi.Repository
{
    public interface IAccount
    {
        Task<List<User>> GetAllUsers();
        Task<string> UserRegistration(UserRegistration userRegistration);
    }
}
