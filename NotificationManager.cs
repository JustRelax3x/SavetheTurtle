using Unity.Notifications.Android;
using UnityEngine;

public class NotificationManager : MonoBehaviour
{

    private void OnEnable()
    {
        AndroidNotificationCenter.CancelAllDisplayedNotifications();
    }
    public void OnUse()
    {
        
        //Create a notification channel
        var channel = new AndroidNotificationChannel()
        {
            Id = "channel_id",
            Name = "Notification Channel",
            Importance = Importance.Default,
            Description = "Reminder notifications",
        };
        AndroidNotificationCenter.RegisterNotificationChannel(channel);


        //Create a notification
        var notification = new AndroidNotification();
        notification.Title = Assets.SimpleLocalization.LocalizationManager.Localize("darew");
        notification.Text = Assets.SimpleLocalization.LocalizationManager.Localize("reaget");
        notification.FireTime = System.DateTime.Now.AddHours(24);
        notification.SmallIcon = "icon_0";


        var id = AndroidNotificationCenter.SendNotification(notification, "channel_id");

        if (AndroidNotificationCenter.CheckScheduledNotificationStatus(id) == NotificationStatus.Scheduled)
        {
            AndroidNotificationCenter.CancelAllNotifications();
            AndroidNotificationCenter.SendNotification(notification, "channel_id");
        }
    }
}
