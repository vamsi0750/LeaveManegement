namespace LeaveManegementApi.Dto
{
    public class LoginDto
    {
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Token { get; set; } = string.Empty;
        public int Role { get; set; }
    }
}
