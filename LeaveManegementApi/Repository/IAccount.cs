using LeaveManegementApi.Models;

namespace LeaveManegementApi.Repository
{
    public interface IAccount
    {
        Task<List<User>> GetAllUsers();
        Task<string> UserRegistration(UserRegistration userRegistration);
        Task<string> Login(UserLogin userLogin);
        Task<string> Verify(string token);
        Task<string> ForgotPassword(string email);
        Task<string> ResetPassword(ResetPassword resetPassword);
    }
}
