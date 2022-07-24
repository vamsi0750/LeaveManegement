
namespace LeaveManegementApi.Models
{
    public class ResponceModel
    {
        public ResponceModel(bool isSuccess, string responceMessage, object data)
        {
            IsSuccess = isSuccess;
            ResponceMessage = responceMessage;
            Data = data;
        }
        public bool IsSuccess { get; set; }
        public string ResponceMessage { get; set; }
        public object Data { get; set; }
    }
}
