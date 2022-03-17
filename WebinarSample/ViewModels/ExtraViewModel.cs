using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm.POCO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows;
using Webinar.DAL;

namespace Webinar.ViewModels
{
    [POCOViewModel]
    public class ExtraViewModel : INotifyPropertyChanged
    {
        public virtual ObservableCollection<TrackViewModel> Tracks { get; set; } = new ObservableCollection<TrackViewModel>();
        public virtual ObservableCollection<TrackViewModel> SelectedTracks { get; set; } = new ObservableCollection<TrackViewModel>();

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
            WaitIndicatorText = "Bezig met het laden van data";
            IsWaitIndicatorVisible = true;
            Title = "Extra";
            LoadTracks();

            ViewInjectionManager.Default
                .RegisterNavigatedEventHandler(this, () =>
                {
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

        //[ServiceProperty(SearchMode = ServiceSearchMode.PreferParents)]
        //protected virtual INotifyIconService NotificationIconService { get { return null; } }

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
            INotification notification = NotificationService.CreatePredefinedNotification("WESP Control Center", "Neque porro quisquam est qui dolorem ipsum quia dolor sit amet, consectetur, adipisci velit", $"Tijd die eronder ook al staat: {DateTime.Now}");
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

        private async void LoadTracks()
        {
            var tracks = TrackRepository.Instance.GetAll();

            await Application.Current.Dispatcher.BeginInvoke(new Action(() =>
            {
                foreach (var track in tracks)
                {
                    Tracks.Add(TrackViewModel.Create(track));
                }
            }));

            IsWaitIndicatorVisible = false;
        }

        public bool CanDeleteRow(TrackViewModel currentItem)
        {
            return currentItem != null && currentItem.TrackId != 0;
        }

        public void DeleteRow(TrackViewModel currentItem)
        {
            if (SelectedTracks.Count > 0)
            {
                if (MessageBoxService.ShowMessage(
                    "Weet je zeker dat je alle geselecteerde tracks wilt verwijderen?",
                    "Let op!",
                    MessageButton.YesNo,
                    MessageIcon.Question,
                    MessageResult.No) ==
                MessageResult.Yes)
                {
                    var selectedTrackList = new List<TrackViewModel>(SelectedTracks);
                    foreach (var track in selectedTrackList)
                    {
                        DeleteTrack(track);
                    }
                }
            }
            else
            {
                if (MessageBoxService.ShowMessage(
                    $"Weet je zeker dat je de track {currentItem.Name}{(currentItem.Composer == null ? string.Empty : $" door {currentItem.Composer}")} wilt verwijderen?",
                    "Let op!",
                    MessageButton.YesNo,
                    MessageIcon.Question,
                    MessageResult.No) ==
                MessageResult.Yes)
                {
                    DeleteTrack(currentItem);
                }
            }
        }

        private void DeleteTrack(TrackViewModel track)
        {
            Tracks.Remove(track);
            TrackRepository.Instance.Delete(track.TrackId);
        }

    }
}
