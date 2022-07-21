using LeaveManegementApi.Data;
using LeaveManegementApi.Helper;
using LeaveManegementApi.Models;
using Microsoft.EntityFrameworkCore;

namespace LeaveManegementApi.Repository
{
    public class Account : IAccount
    {
        private readonly LeaveManagementDBContext _leaveManagementDBContext;
        public Account(LeaveManagementDBContext leaveManagementDBContext)
        {
            _leaveManagementDBContext = leaveManagementDBContext;
        }

        public async Task<List<User>> GetAllUsers()
        {
            return await _leaveManagementDBContext.Users.ToListAsync();
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
                PasswordSalt = passwordSalt
            };
            var result = await _leaveManagementDBContext.Users.AddAsync(user);
            await _leaveManagementDBContext.SaveChangesAsync();
            return "User Created successfult";
        }
        
    }
}
