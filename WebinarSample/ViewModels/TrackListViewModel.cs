using System.Collections.ObjectModel;
using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm.POCO;
using Webinar.DAL;

namespace Webinar.ViewModels
{
    [POCOViewModel]
    public class TrackListViewModel
    {
        public virtual ObservableCollection<TrackViewModel> Tracks { get; set; } = new ObservableCollection<TrackViewModel>();

        protected TrackListViewModel()
        {
            foreach (var track in TrackRepository.Instance.GetAll())
            {
                Tracks.Add(TrackViewModel.Create(track));
            }

            ViewInjectionManager.Default
                .RegisterNavigatedEventHandler(this, () => {
                    ViewInjectionManager.Default.Navigate(Regions.Navigation, NavigationKey.Dashboard);
                });
        }

        public static TrackListViewModel Create() { return ViewModelSource.Create(() => new TrackListViewModel()); }

        [ServiceProperty(SearchMode = ServiceSearchMode.PreferParents)]
        protected virtual IDocumentManagerService DocumentManagerService { get { return null; } }

        public void EditTrack(TrackViewModel trackVM)
        {
            trackVM.SetParentViewModel(this);
            var document = DocumentManagerService.CreateDocument("TrackView", trackVM);
            document.Show();
        }

        public bool CanDeleteRow(TrackViewModel currentItem)
        {
            return currentItem != null && currentItem.TrackId != 0;
        }

        public void DeleteRow(TrackViewModel currentItem)
        {
            Tracks.Remove(currentItem);

            TrackRepository.Instance.Delete(currentItem.TrackId);
        }
    }
}