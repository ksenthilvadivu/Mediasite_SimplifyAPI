using DH.Curbside.Infrastructure.MediaSite.Model;

namespace DH.Curbside.Core.Application.Mediasite.ViewModels
{
    /// <summary>
    /// MediaSite Presentation Model
    /// </summary>
    public class PresentationModel
    {
        public Presentation videoPresentation { get; set; }

        /// <summary>
        /// Presentation Model Constructor
        /// </summary>
        public PresentationModel()
        {
            videoPresentation = new Presentation();
        }
    }
}