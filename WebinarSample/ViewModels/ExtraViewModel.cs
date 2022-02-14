using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm.POCO;
using DevExpress.Xpf.Core;
using System;

namespace Webinar.ViewModels
{
    [POCOViewModel]
    public class ExtraViewModel
    {
        public virtual TrackList Tracks { get; set; }

        public virtual string Title { get; set; }

        protected ExtraViewModel()
        {
            Tracks = new TrackList();
            Title = "Extra";

            ViewInjectionManager.Default
                .RegisterNavigatedEventHandler(this, () => {
                    ViewInjectionManager.Default.Navigate(Regions.Navigation, NavigationKey.Extra);
                });
        }

        public static ExtraViewModel Create() { return ViewModelSource.Create(() => new ExtraViewModel()); }

        [ServiceProperty(SearchMode = ServiceSearchMode.PreferParents)]
        protected virtual INotifyIconService NotificationIconService { get { return null; } }

        [ServiceProperty(SearchMode = ServiceSearchMode.PreferParents)]
        protected virtual IMessageBoxService MessageBoxService { get { return null; } }

        [ServiceProperty(SearchMode = ServiceSearchMode.PreferParents)]
        protected virtual INotificationService CustomNotificationService { get { return null; } }

        public void IconLeftClick() { MessageBoxService.ShowMessage("test berichtje", "Error", MessageButton.OK); }

        public void ShowNotification()
        {
            INotification notification = CustomNotificationService.CreatePredefinedNotification($"Time: {DateTime.Now}", "Custom Notificatie je kan typen wat je wilt 123", string.Empty);
            notification.ShowAsync();
        }
    }
}
