using LeaveManegementApi.Models;

namespace LeaveManegementApi.Repository
{
    public interface IEmail
    {
        Task<bool> SendEmail();
        Task<bool> RegistartionEmail(User user);
        Task<bool> ForgotPasswordEmail(User user);
    }
}
