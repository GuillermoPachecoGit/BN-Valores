using Android.App;
using Android.Content;
using Firebase.Messaging;
using Android.Support.V4.App;
using Android.Util;
using Xamarin.Forms.PlatformConfiguration;
using Android.Graphics;

namespace BNV.Droid
{
    [Service]
    [IntentFilter(new[] { "com.google.firebase.MESSAGING_EVENT" })]
    public class MyFirebaseMessagingService : FirebaseMessagingService
    {

        internal static readonly string CHANNEL_ID = "my_notification_channel";
        internal static readonly int NOTIFICATION_ID = 100;

        const string TAG = "MyFirebaseMsgService";
        public override void OnMessageReceived(RemoteMessage message)
        {
            Log.Debug(TAG, "From: " + message.From);
            var body = message.GetNotification().Body;
            Log.Debug(TAG, "Notification Message Body: " + body);
            SendNotification(body);
        }

        void SendNotification(string messageBody)
        {
            var intent = new Intent(this, typeof(MainActivity));
            intent.AddFlags(ActivityFlags.ClearTop);

            var pendingIntent = PendingIntent.GetActivity(this, 0, intent,
                                                          PendingIntentFlags.OneShot);
            var notificationBuilder = new NotificationCompat.Builder(this, CHANNEL_ID)
                                      .SetSmallIcon(Resource.Drawable.ic_notification)
                                      .SetContentTitle("BN Valores")
                                      .SetContentText(messageBody)
                                      .SetAutoCancel(true)
                                      .SetContentIntent(pendingIntent)
                                      .SetChannelId(CHANNEL_ID);

            var notificationManager = NotificationManagerCompat.From(this);
            notificationManager.Notify(0, notificationBuilder.Build());
        }
    }
}