using Android.App;
using Android.Content;
using Android.Graphics;
using Android.Support.V4.App;
using Android.Widget;
using TaskStackBuilder = Android.Support.V4.App.TaskStackBuilder;

namespace AlarmSample
{
    [BroadcastReceiver]
    public class AlarmBroadcast : BroadcastReceiver
    {
        public override void OnReceive(Context context, Intent intent)
        {
            PendingIntent pi = PendingIntent.GetActivity(context, 0, intent, 0);
            AlarmManager manager = (AlarmManager)context.GetSystemService(Android.Content.Context.AlarmService);
            manager.Cancel(pi);

            // When the user clicks the notification, SecondActivity will start up.
            Intent resultIntent = new Intent(context, typeof(MainActivity));

            // Construct a back stack for cross-task navigation:
            TaskStackBuilder stackBuilder = TaskStackBuilder.Create(context);
            stackBuilder.AddParentStack(Java.Lang.Class.FromType(typeof(MainActivity)));
            stackBuilder.AddNextIntent(resultIntent);

            // Create the PendingIntent with the back stack:            
            PendingIntent resultPendingIntent = stackBuilder.GetPendingIntent(0, (int)PendingIntentFlags.UpdateCurrent);

            //Build Notification
            NotificationCompat.Builder builder = new NotificationCompat.Builder(context)
                .SetAutoCancel(true)
                .SetDefaults((int)NotificationDefaults.Sound)
                .SetContentIntent(resultPendingIntent)
                .SetContentTitle("Notification ALARM!!!")
                .SetSmallIcon(Resource.Drawable.ic_stat_button_click)
                .SetContentText(string.Format("You are OUT!!."));

            //Send Notification
            NotificationManager nf = (NotificationManager)context.GetSystemService(Context.NotificationService);
            nf.Notify(1000, builder.Build());

            MainActivity.CloseThis();
        }
    }
}