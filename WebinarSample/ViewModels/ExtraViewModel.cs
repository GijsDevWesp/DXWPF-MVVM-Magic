using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm.POCO;
using DevExpress.Xpf.Core;
using System;
using System.ComponentModel;
using System.Threading.Tasks;

namespace Webinar.ViewModels
{
    [POCOViewModel]
    public class ExtraViewModel : INotifyPropertyChanged
    {
        public virtual TrackList Tracks { get; set; }

        public virtual string Title { get; set; }

        bool _isWaitIndicatorVisible;
        string _waitIndicatorText;

        public bool IsWaitIndicatorVisible
        {
            get
            {
                return _isWaitIndicatorVisible;
            }

            set
            {
                _isWaitIndicatorVisible = value;
                RaisePropertyChanged("IsWaitIndicatorVisible");
            }
        }

        public string WaitIndicatorText
        {
            get
            {
                return _waitIndicatorText;
            }

            set
            {
                _waitIndicatorText = value;
                RaisePropertyChanged("WaitIndicatorText");
            }
        }

        protected ExtraViewModel()
        {
            Title = "Extra";
            WaitIndicatorText = "Bezig met het laden van data";
            LoadTracks();

            ViewInjectionManager.Default
                .RegisterNavigatedEventHandler(this, () => {
                    ViewInjectionManager.Default.Navigate(Regions.Navigation, NavigationKey.Extra);
                });
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void RaisePropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public static ExtraViewModel Create() { return ViewModelSource.Create(() => new ExtraViewModel()); }

        [ServiceProperty(SearchMode = ServiceSearchMode.PreferParents)]
        protected virtual INotifyIconService NotificationIconService { get { return null; } }

        [ServiceProperty(SearchMode = ServiceSearchMode.PreferParents)]
        protected virtual IMessageBoxService MessageBoxService { get { return null; } }

        //[ServiceProperty(Key = "ServiceWithCustomNotifications")]
        //protected virtual INotificationService CustomNotificationService { get { return null; } }
        [ServiceProperty(SearchMode = ServiceSearchMode.PreferParents)]
        protected virtual INotificationService NotificationService { get { return null; } }

        public void ShowErrorMessage()
        {
            MessageBoxService.ShowMessage("Er heeft een fout plaatsgevonden, zie chart", "Error", MessageButton.OK);
            ViewInjectionManager.Default.Navigate(Regions.Navigation, NavigationKey.Chart);
        }

        public void ShowNotification()
        {
            INotification notification = NotificationService.CreatePredefinedNotification("WESP Control Center", "Neque porro quisquam est qui dolorem ipsum quia dolor sit amet, consectetur, adipisci velit", string.Format("Tijd die eronder ook al staat: {0}", DateTime.Now));
            notification.ShowAsync().ContinueWith(OnNotificationClicked, TaskScheduler.FromCurrentSynchronizationContext());
        }

        //public void ShowCustomNotification()
        //{
        //    CustomNotificationViewModel vm = ViewModelSource.Create(() => new CustomNotificationViewModel());
        //    vm.Caption = "Custom Notificatie titel";
        //    vm.Content = "Er is een probleem in ...";
        //    vm.Time = String.Format("Tijd: {0}", DateTime.Now);

        //    INotification notification = CustomNotificationService.CreateCustomNotification(vm);
        //    notification.ShowAsync().ContinueWith(OnNotificationClicked, TaskScheduler.FromCurrentSynchronizationContext());
        //}

        private void OnNotificationClicked(Task<NotificationResult> task)
        {
            if (task.Result == NotificationResult.Activated)
            {
                ViewInjectionManager.Default.Navigate(Regions.Navigation, NavigationKey.Chart);
            }
        }

        private async void LoadTracks() {
            IsWaitIndicatorVisible = true;
            Task task = Task.Factory.StartNew(() =>
            {
                Tracks = new TrackList();
            });
            await task;
            IsWaitIndicatorVisible = false;
        }

    }
}
