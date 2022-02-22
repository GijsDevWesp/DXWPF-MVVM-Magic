using System.Collections.Generic;
using System.Linq;

namespace Webinar.DAL
{
    public class TrackRepository : IRepository<Track>
    {
        private static TrackRepository _instance = null;
        private static readonly object _instanceLock = new object();

        public static TrackRepository Instance
        {
            get {
                lock (_instanceLock)
                {
                    if (_instance == null)
                    {
                        _instance = new TrackRepository();
                    }
                    return _instance;
                }
            }
        }

        protected TrackRepository()
        {

        }

        public IEnumerable<Track> GetAll()
        {
            using (var context = new ChinookEntities())
            {
                return context.Tracks.ToList();
            }
        }

        public Track GetById(int id)
        {
            using (var context = new ChinookEntities())
            {
                return context.Tracks.Find(id);
            }
        }

        public void Create(Track entity)
        {
            using (var context = new ChinookEntities())
            {
                context.Tracks.Add(entity);
                context.SaveChanges();
            }
        }

        public void Update(Track entity)
        {
            using (var context = new ChinookEntities())
            {
                var t = context.Tracks.Find(entity.TrackId);
                if (t != null)
                {
                    t.Name = entity.Name;
                    t.Composer = entity.Composer;
                    t.Bytes = entity.Bytes;
                    t.Milliseconds = entity.Milliseconds;
                    context.SaveChanges();
                }
            }
        }

        public void Delete(int id)
        {
            using (var context = new ChinookEntities())
            {
                var toDelete = context.Tracks.Find(id);
                context.Tracks.Remove(toDelete);
                context.SaveChanges();
            }
        }
    }
}
