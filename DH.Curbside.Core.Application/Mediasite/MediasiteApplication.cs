using DH.Curbside.Core.Application.Mediasite.ViewModels;
using DH.Curbside.Infrastructure.MediaSite;
using AutoMapper;

namespace DH.Curbside.Core.Application.Mediasite
{
    public class MediasiteApplication : IMediasiteApplication
    {
        private readonly IMapper _mapper;

        /// <summary>
        /// WhitelistManager Constructor
        /// </summary>
        /// <param name="whitelistRepository">WhitelistUserRepository Interface</param>
        /// <param name="mapper">Mapper Interface</param>
        public MediasiteApplication(IMapper mapper)
        {
            _mapper = mapper;
        }

        /// <summary>
        /// Get list of Mediasite videos
        /// </summary>
        /// <param name="tenantId">Tenant Id</param>
        /// <param name="searchCriteria">Search Criteria</param>
        /// <param name="skip">skip records</param>
        /// <param name="top">fetch top records</param>
        /// <returns>List of Mediasite videos</returns>
        public MediasiteModel GetMediasiteVideos(string tenantId, string searchCriteria, int skip, int top)
        {
            MediasiteContext contextObj = new MediasiteContext();
            var videoList = contextObj.GetPresentations(searchCriteria, skip, top);

            var msModel = new MediasiteModel();
            if (videoList != null)
            {
                foreach (var video in videoList)
                {
                    msModel.videoPresentation.Add(video);
                }
            }

            return msModel;
        }

        /// <summary>
        /// Get Authorization Ticket Value
        /// </summary>
        /// <param name="presentationId">Presentation Id</param>
        /// <param name="minutesToLive"></param>
        /// <returns></returns>
        public string GetAuthorizationTicket(string presentationId, int minutesToLive)
        {
            MediasiteContext contextObj = new MediasiteContext();
            string authTicket = contextObj.RequestAuthTicket(presentationId, minutesToLive);

            return authTicket;
        }

        /// <summary>
        /// Get Presentation
        /// </summary>
        /// <param name="presentationId">Presentation Id</param>
        /// <returns>PresentationModel Object</returns>
        public PresentationModel GetPresentation(string presentationId)
        {
            MediasiteContext contextObj = new MediasiteContext();
            return new PresentationModel() { videoPresentation = contextObj.GetPresentation(presentationId) };
        }
    }
}
