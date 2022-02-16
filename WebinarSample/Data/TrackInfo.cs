using System;
using System.ComponentModel;
using Webinar.DAL;

public class TrackInfo : INotifyPropertyChanged
{
    private readonly int _id;
    public TrackInfo() { }
    public TrackInfo(int id, string name, int? albumId, int mediaTypeId,
                        int? genreId, string composer, int milliseconds, int? bytes)
    {
        _id = id;
        Name = name;
        AlbumId = albumId;
        MediaTypeId = mediaTypeId;
        GenreId = genreId;
        Composer = composer;
        Milliseconds = milliseconds;
        Bytes = bytes;
    }

    string name;
    public string Name
    {
        get { return name; }
        set
        {
            if (name == value)
                return;
            name = value;
            OnPropertyChanged("Name");
        }
    }
    int? albumId;
    public int? AlbumId
    {
        get { return albumId; }
        set
        {
            if (albumId == value)
                return;
            albumId = value;
            OnPropertyChanged("AlbumId");
        }
    }
    int mediaTypeId;
    public int MediaTypeId
    {
        get { return mediaTypeId; }
        set
        {
            if (mediaTypeId == value)
                return;
            mediaTypeId = value;
            OnPropertyChanged("MediaTypeId");
        }
    }
    int? genreId;
    public int? GenreId
    {
        get { return genreId; }
        set
        {
            if (genreId == value)
                return;
            genreId = value;
            OnPropertyChanged("GenreId");
        }
    }
    string composer;
    public string Composer
    {
        get { return composer; }
        set
        {
            if (composer == value)
                return;
            composer = value;
            OnPropertyChanged("Composer");
        }
    }
    int milliseconds;
    public int Milliseconds
    {
        get { return milliseconds; }
        set
        {
            if (milliseconds == value)
                return;
            milliseconds = value;
            OnPropertyChanged("Milliseconds");
        }
    }
    int? bytes;
    public int? Bytes
    {
        get { return bytes; }
        set
        {
            if (bytes == value)
                return;
            bytes = value;
            OnPropertyChanged("Bytes");
        }
    }

    public override string ToString()
    {
        return String.Format("Name: {0}, Milliseconds: {1}, Composer: {2}",
          Name, Milliseconds, Composer);
    }

    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnPropertyChanged(string propertyName)
    {
        var trackRepo = new TrackRepository();
        var t = trackRepo.GetById(_id);
        if (t != null)
        {
            t.Name = Name;
            t.Composer = Composer;
            t.Milliseconds = Milliseconds;
            t.Bytes = Bytes;
            trackRepo.Update(t);
        }
        var handler = PropertyChanged;
        if (handler != null)
            handler(this, new PropertyChangedEventArgs(propertyName));
    }
}