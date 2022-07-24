namespace LeaveManegementApi.Models
{
    public class SendGridTemplates
    {
        public int Id { get; set; }
        public string SendGridId { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }
}
