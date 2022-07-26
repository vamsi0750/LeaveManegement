﻿using LeaveManegementApi.Dto;
using LeaveManegementApi.Models;

namespace LeaveManegementApi.Repository
{
    public interface IAccount
    {
        Task<List<LoginDto>> GetAllUsers();
        Task<ResponceModel> UserRegistration(UserRegistration userRegistration);
        Task<ResponceModel> Login(UserLogin userLogin);
        Task<string> Verify(string token);
        Task<ResponceModel> ForgotPassword(string email);
        Task<ResponceModel> ResetPassword(ResetPassword resetPassword);
    }
}
