using DH.Curbside.Core.Application.Mediasite.ViewModels;

namespace DH.Curbside.Core.Application.Mediasite
{
    public interface IMediasiteApplication
    {
        /// <summary>
        /// Get list of Mediasite videos
        /// </summary>
        /// <param name="tenantId">Tenant Id</param>
        /// <param name="searchCriteria">Search Criteria</param>
        /// <param name="top">fetch top records</param>
        /// <param name="skip">skip records</param>
        /// <returns>List of Mediasite videos</returns>
        MediasiteModel GetMediasiteVideos(string tenantId, string searchCriteria, int skip, int top);

        string GetAuthorizationTicket(string presentationId, int minutesToLive);

        /// <summary>
        /// Get single object for presentation
        /// </summary>
        /// <param name="presentationId">Presentation Id</param>        
        /// <returns>PresentationModel Object</returns>
        PresentationModel GetPresentation(string presentationId);
    }
}
