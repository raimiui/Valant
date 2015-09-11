namespace Valant.Model
{
    public class Notification
    {
        public NotificationCode Code { get; set; }
    }

    public enum NotificationCode
    {
        ItemHasBeenTakenOut = 0,
        Expired
    }
}
