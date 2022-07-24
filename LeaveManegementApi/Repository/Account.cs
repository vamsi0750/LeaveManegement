using LeaveManegementApi.Data;
using LeaveManegementApi.Helper;
using LeaveManegementApi.Models;
using Microsoft.EntityFrameworkCore;

namespace LeaveManegementApi.Repository
{
    public class Account : IAccount
    {
        private readonly LeaveManagementDBContext _leaveManagementDBContext;
        private readonly IEmail _email;

        public Account(LeaveManagementDBContext leaveManagementDBContext,IEmail email)
        {
            _leaveManagementDBContext = leaveManagementDBContext;
            _email = email;
        }

        public async Task<string> ForgotPassword(string email)
        {
            var user = await _leaveManagementDBContext.Users.FirstOrDefaultAsync(x => x.Email == email);
            if (user == null) return "User Not Found";
            user.PasswordResetToken = CreateHash.CreateRandomToken();
            user.ResetTokenExpiriesAt = DateTime.Now.AddDays(1);
            await _leaveManagementDBContext.SaveChangesAsync();
            var isEmailSent = await _email.ForgotPasswordEmail(user);
            if (!isEmailSent)
            {
                return "Something went wrong , please try after some time";
            }
            return "Please Check Your Email to Reset Password";
        }

        public async Task<List<User>> GetAllUsers()
        {
            return await _leaveManagementDBContext.Users.ToListAsync();
        }

        public async Task<string> Login(UserLogin userLogin)
        {
            var user = await _leaveManagementDBContext.Users.FirstOrDefaultAsync(x => x.Email == userLogin.Email);
            if (user == null)
            {
                return "User Not Found";
            }
            if (!CreateHash.VerifyPassworHash(userLogin.Password, user.PasswordHash, user.PasswordSalt))
            {
                return "Password In Coreect";
            }
            if (user.VerifiedAt == null)
            {
                return "User Not Verified";
            }
            return "Loged In Succesfully";
        }

        public async Task<string> ResetPassword(ResetPassword resetPassword)
        {
            var user = await _leaveManagementDBContext.Users.FirstOrDefaultAsync(x => x.PasswordResetToken == resetPassword.Token);
            if (user == null || user.ResetTokenExpiriesAt < DateTime.Now) return "InValid Token";
            CreateHash.CreatePassworHash(resetPassword.Password, out byte[] passwordHash, out byte[] passwordSalt);
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;
            user.PasswordResetToken = null;
            user.ResetTokenExpiriesAt = null;
            await _leaveManagementDBContext.SaveChangesAsync();
            return "Password Successfully Reseted";
        }

        public async Task<string> UserRegistration(UserRegistration userRegistration)
        {
            if(_leaveManagementDBContext.Users.Any(u=>u.Email == userRegistration.Email))
            {
                return "User Already Exist";
            }
            CreateHash.CreatePassworHash(userRegistration.Password, out byte[] passwordHash, out byte[] passwordSalt);
            var user = new User()
            {
                Name = userRegistration.Name,
                Email = userRegistration.Email,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                VerificationToken = CreateHash.CreateRandomToken()
            };
            var result = await _leaveManagementDBContext.Users.AddAsync(user);
            await _leaveManagementDBContext.SaveChangesAsync();
            var s = await _email.RegistartionEmail(user);
            return "User Created Succesfully, please verify email address";
        }

        public async Task<string> Verify(string token)
        {
            var user  = await _leaveManagementDBContext.Users.FirstOrDefaultAsync(u=>u.VerificationToken == token);
            if(user == null)
            {
                return "InValid Token";
            }
            user.VerifiedAt = DateTime.Now;
            await _leaveManagementDBContext.SaveChangesAsync();
            return "User Verifed";
        }
    }
}
