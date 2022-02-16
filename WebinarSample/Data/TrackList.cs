using System.Collections.ObjectModel;
using Webinar.DAL;

public class TrackList : ObservableCollection<TrackInfo>
{

    public TrackList()
    {
        TrackRepository tracks = new TrackRepository();

        foreach (var t in tracks.GetAll())
        {
            Add(new TrackInfo(t.TrackId, t.Name, t.AlbumId, t.MediaTypeId, t.GenreId, t.Composer, t.Milliseconds, t.Bytes));
        }
    }
}