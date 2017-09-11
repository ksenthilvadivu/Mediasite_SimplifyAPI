using System;

namespace DH.Curbside.Infrastructure.MediaSite.Model
{
    public class Presentation
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string PrimaryPresenter { get; set; }

        public string Owner { get; set; }

        public string ThumbnailUrl { get; set; }

        public int Duration { get; set; }

        public DateTime? RecordDate { get; set; }

        public string AuthorizationTicket { get; set; }

        //public string PlaybackUrl { get; set; }        
        //setting playback url
        public string PlaybackUrl
        {
            get { return @"http://ms.barrowmedia.com/mediasite/play/" + Id; }
        }
    }
}
