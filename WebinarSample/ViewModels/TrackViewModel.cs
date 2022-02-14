﻿using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm.POCO;
using System;
using System.ComponentModel;

namespace Webinar.ViewModels
{
    [POCOViewModel]
    public class TrackViewModel : IEditableObject
    {
        private TrackInfo track;

        public virtual int TrackId { get; set; }

        public virtual string Name { get; set; }

        public virtual string Composer { get; set; }

        [ServiceProperty(SearchMode = ServiceSearchMode.PreferParents)]
        protected virtual IMessageBoxService MessageBoxService { get { return null; } }

        protected TrackViewModel() : this(new TrackList()[15])
        {
        }

        protected TrackViewModel(TrackInfo track)
        {
            if(track == null)
                throw new ArgumentNullException("track", "track is null.");
            Load(track);
        }

        public static TrackViewModel Create() { return ViewModelSource.Create(() => new TrackViewModel()); }

        public static TrackViewModel Create(TrackInfo track)
        { return ViewModelSource.Create(() => new TrackViewModel(track)); }

        public bool CanResetName() { return track != null && !String.IsNullOrEmpty(Name); }

        public void ResetName()
        {
            if(track != null)
            {
                if(MessageBoxService.ShowMessage(
                        "Are you sure you want to reset the Name value?",
                        "Question",
                        MessageButton.YesNo,
                        MessageIcon.Question,
                        MessageResult.No) ==
                    MessageResult.Yes)
                    Name = string.Empty;
            }
        }

        private void Load(TrackInfo track)
        {
            this.track = track;
            this.TrackId = track.TrackId;
            this.Name = track.Name;
            this.Composer = track.Composer;
        }

        void IEditableObject.BeginEdit()
        {
        }

        void IEditableObject.EndEdit()
        {
            if(!string.Equals(Name, track.Name))
                track.Name = Name;
            if(!string.Equals(Composer, track.Composer))
                track.Composer = Composer;
            if(TrackId != track.TrackId)
                track.TrackId = TrackId;
        }

        void IEditableObject.CancelEdit() { Load(this.track); }

        public void Save() { ((IEditableObject)this).EndEdit(); }
        public void Revert() { ((IEditableObject)this).CancelEdit(); }
    }
}