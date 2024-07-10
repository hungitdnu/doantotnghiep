namespace HeThongDangKyDuHoc.Models
{
    public class CustomNotification
    {
        public string Message { get; set; }
        public string Type { get; set; }
        public CustomNotification(string message, string type)
        {
            Message = message;
            Type = type;
        }
        public static CustomNotification Success(string message)
        {
            return new CustomNotification(message, "success");
        }
        public static CustomNotification Error(string message)
        {
            return new CustomNotification(message, "danger");
        }
        public static CustomNotification Warning(string message)
        {
            return new CustomNotification(message, "warning");
        }
    }
}
