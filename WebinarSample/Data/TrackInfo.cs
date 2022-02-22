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

    public int Id { get { return _id; } }

    private string _name;
    public string Name
    {
        get { return _name; }
        set
        {
            if (_name == value)
                return;
            _name = value;
            OnPropertyChanged("Name");
        }
    }

    private int? _albumId;
    public int? AlbumId
    {
        get { return _albumId; }
        set
        {
            if (_albumId == value)
                return;
            _albumId = value;
            OnPropertyChanged("AlbumId");
        }
    }

    private int _mediaTypeId;
    public int MediaTypeId
    {
        get { return _mediaTypeId; }
        set
        {
            if (_mediaTypeId == value)
                return;
            _mediaTypeId = value;
            OnPropertyChanged("MediaTypeId");
        }
    }

    private int? _genreId;
    public int? GenreId
    {
        get { return _genreId; }
        set
        {
            if (_genreId == value)
                return;
            _genreId = value;
            OnPropertyChanged("GenreId");
        }
    }

    private string _composer;
    public string Composer
    {
        get { return _composer; }
        set
        {
            if (_composer == value)
                return;
            _composer = value;
            OnPropertyChanged("Composer");
        }
    }

    private int _milliseconds;
    public int Milliseconds
    {
        get { return _milliseconds; }
        set
        {
            if (_milliseconds == value)
                return;
            _milliseconds = value;
            OnPropertyChanged("Milliseconds");
        }
    }

    private int? _bytes;
    public int? Bytes
    {
        get { return _bytes; }
        set
        {
            if (_bytes == value)
                return;
            _bytes = value;
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