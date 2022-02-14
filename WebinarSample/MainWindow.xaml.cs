using DevExpress.Data;
using DevExpress.Xpf.Core;
using System.Windows;

namespace Webinar
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : DXWindow
    {
        public MainWindow() { InitializeComponent(); }

        public static void NotificationCallback() { MessageBox.Show("callback triggered!"); }

        public static string ApplicationID
        {
            get { return $"InteractiveNotifications_{(AssemblyInfo.VersionShort.Replace(".", "_"))}"; }
        }

        public static string ApplicationName { get { return "Interactive Notifications"; } }
    }
}

