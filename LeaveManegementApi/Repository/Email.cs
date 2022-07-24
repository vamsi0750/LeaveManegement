using LeaveManegementApi.Data;
using LeaveManegementApi.Models;
using Microsoft.EntityFrameworkCore;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace LeaveManegementApi.Repository
{
    public class Email : IEmail
    {
        public IConfiguration configuration;
        private readonly LeaveManagementDBContext _leaveManagementDBContext;

        public Email(IConfiguration iconfig, LeaveManagementDBContext leaveManagementDBContext)
        {
            configuration = iconfig;
            _leaveManagementDBContext = leaveManagementDBContext;
        }
        public async Task<bool> SendEmail()
        {
            var apiKey = configuration.GetSection("SendGridKey").Value;
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress(configuration.GetSection("Sender").Value);
            var subject = "Sending with SendGrid is Fun";
            var to = new EmailAddress("vamsikrishnanagisetty0750@gmail.com");
            var htmlContent = "<strong>and easy to do anywhere, even with C#</strong>";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, null, htmlContent);
            var response = await client.SendEmailAsync(msg);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> RegistartionEmail(User user)
        {
            var apiKey = configuration.GetSection("SendGridKey").Value;
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress(configuration.GetSection("Sender").Value);
            var to = new EmailAddress(user.Email, user.Name);
            var templateId = await _leaveManagementDBContext.SendGridTemplates.Where(x=>x.Id == 1).Select(x => x.SendGridId).FirstOrDefaultAsync();
            var msg = MailHelper.CreateSingleTemplateEmail(from, to, templateId, user);
            var response = await client.SendEmailAsync(msg);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> ForgotPasswordEmail(User user)
        {
            var apiKey = configuration.GetSection("SendGridKey").Value;
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress(configuration.GetSection("Sender").Value);
            var to = new EmailAddress(user.Email, user.Name);
            var templateId = await _leaveManagementDBContext.SendGridTemplates.Where(x => x.Id == 2).Select(x => x.SendGridId).FirstOrDefaultAsync();
            var msg = MailHelper.CreateSingleTemplateEmail(from, to, templateId, user);
            var response = await client.SendEmailAsync(msg);
            return response.IsSuccessStatusCode;
        }
    }
}
