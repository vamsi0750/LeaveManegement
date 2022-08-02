using LeaveManegementApi.Data;
using LeaveManegementApi.Dto;
using LeaveManegementApi.Helper;
using LeaveManegementApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace LeaveManegementApi.Repository
{
    public class Account : IAccount
    {
        private readonly LeaveManagementDBContext _leaveManagementDBContext;
        private readonly IEmail _email;
        private readonly IConfiguration _configuration;

        public Account(LeaveManagementDBContext leaveManagementDBContext,IEmail email, IConfiguration configuration)
        {
            _leaveManagementDBContext = leaveManagementDBContext;
            _email = email;
            _configuration = configuration;
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

        public async Task<List<LoginDto>> GetAllUsers()
        {
            return await _leaveManagementDBContext.Users.Select(x => new LoginDto()
            {
                Name = x.Name,
                Role = x.Role,
                Email = x.Email
            }).ToListAsync();
        }

        public async Task<ResponceModel> Login(UserLogin userLogin)
        {
            var user = await _leaveManagementDBContext.Users.FirstOrDefaultAsync(x => x.Email == userLogin.Email);
            if (user == null)
            {
                return new ResponceModel( false,"User Not Found",null);
            }
            if (!CreateHash.VerifyPassworHash(userLogin.Password, user.PasswordHash, user.PasswordSalt))
            {
                return new ResponceModel(false, "Password In Coreect", null);
            }
            if (user.VerifiedAt == null)
            {
                return new ResponceModel(false, "User Not Verified", null);
            }
            var token = CreateJwtToken(user);
            var vamsiDto = new LoginDto()
            {
                Name = user.Name,
                Email = user.Email,
                Token = token,
                Role = user.Role
            };
            var ss = new ResponceModel(true, "Login Success", vamsiDto);
            return new ResponceModel(true,"Login Success", new { token= token});
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

        public async Task<ResponceModel> UserRegistration(UserRegistration userRegistration)
        {
            if(_leaveManagementDBContext.Users.Any(u=>u.Email == userRegistration.Email))
            {
                return new ResponceModel(true, "User Already Exist", null);
            }
            CreateHash.CreatePassworHash(userRegistration.Password, out byte[] passwordHash, out byte[] passwordSalt);
            var user = new User()
            {
                Name = userRegistration.Name,
                Email = userRegistration.Email,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                VerificationToken = CreateHash.CreateRandomToken(),
                Role = 1
            };
            var result = await _leaveManagementDBContext.Users.AddAsync(user);
            await _leaveManagementDBContext.SaveChangesAsync();
            var s = await _email.RegistartionEmail(user);
            return new ResponceModel(true, "User Created Succesfully, please verify email address", null);

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
        public  string CreateJwtToken(User user)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration.GetSection("JWTConfig:Key").Value);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new System.Security.Claims.ClaimsIdentity(new[]
                {
                    new System.Security.Claims.Claim(ClaimTypes.Name, user.Name),
                    new System.Security.Claims.Claim(ClaimTypes.Email, user.Email),
                    new System.Security.Claims.Claim(ClaimTypes.Role, user.Role.ToString()),
                }),
                Expires = DateTime.UtcNow.AddHours(12),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Audience = _configuration.GetSection("JWTConfig:AudiEnce").Value,
                Issuer = _configuration.GetSection("JWTConfig:Issuer").Value
            };
            var token = jwtTokenHandler.CreateToken(tokenDescriptor);
            return jwtTokenHandler.WriteToken(token);
        }
    }
}
