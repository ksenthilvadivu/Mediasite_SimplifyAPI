using DH.Curbside.Infrastructure.MediaSite.Model;
using System.Collections.Generic;

namespace DH.Curbside.Core.Application.Mediasite.ViewModels
{
    public class MediasiteModel
    {
        public IList<Presentation> videoPresentation { get; set; }

        public MediasiteModel()
        {
            videoPresentation = new List<Presentation>();
        }
    }
}
