using DevExpress.Mvvm.UI;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Windows;

namespace Webinar
{
    [Guid("5A430E6E-2E66-4A46-BBFA-A5CC432673A0"), ComVisible(true)]
    public class CustomNotificationActivator : ToastNotificationActivator
    {
        public override void OnActivate(string arguments, Dictionary<string, string> data)
        {
            Application.Current.Dispatcher
                .Invoke(() => {
                    MainWindow.NotificationCallback();
                });
        }
    }
}
