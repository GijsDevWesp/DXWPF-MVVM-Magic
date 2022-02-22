using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm.POCO;
using System;
using Webinar.DAL;

namespace Webinar.ViewModels
{
    [POCOViewModel]
    public class TrackListViewModel
    {
        public virtual TrackList Tracks { get; set; }

        protected TrackListViewModel()
        {
            Tracks = new TrackList();

            ViewInjectionManager.Default
                .RegisterNavigatedEventHandler(this, () => {
                    ViewInjectionManager.Default.Navigate(Regions.Navigation, NavigationKey.Dashboard);
                });
        }

        public static TrackListViewModel Create() { return ViewModelSource.Create(() => new TrackListViewModel()); }

        [ServiceProperty(SearchMode = ServiceSearchMode.PreferParents)]
        protected virtual IDocumentManagerService DocumentManagerService { get { return null; } }

        public void EditTrack(object trackObject)
        {
            var track = trackObject as TrackInfo;
            var document = DocumentManagerService.CreateDocument("TrackView", TrackViewModel.Create(track));
            document.Show();
        }

        public bool CanDeleteRow(object currentItem)
        {
            return currentItem != null;
        }

        public void DeleteRow(object currentItem)
        {
            var track = currentItem as TrackInfo;
            Tracks.Remove(track);

            TrackRepository tracks = new TrackRepository();
            tracks.Delete(track.Id);
        }
    }
}