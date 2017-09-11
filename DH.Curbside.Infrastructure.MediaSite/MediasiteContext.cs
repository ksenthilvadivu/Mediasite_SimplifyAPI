using System.Text;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using DH.Curbside.Infrastructure.MediaSite.Mappings;
using DH.Curbside.Infrastructure.MediaSite.Model;
using System.Collections.Generic;
using System.Configuration;

namespace DH.Curbside.Infrastructure.MediaSite
{
    public class MediasiteContext : BaseContext
    {
        public MediasiteContext()
            : base()
        {

        }

        /// <summary>
        /// Get Presentations
        /// </summary>
        /// <param name="searchPresentation">Search Presentation</param>
        /// <param name="recordCount">Records Count</param>
        /// <param name="pageNumber">Page Number</param>
        /// <returns></returns>
        public List<Presentation> GetPresentations(string searchPresentation, int skip, int top)
        {
            List<Presentation> presentationList = null;
            var request = new RestRequest("Presentations", Method.GET);
            StringBuilder paramBuilder = new StringBuilder();
            if (!string.IsNullOrEmpty(searchPresentation))
            {
                paramBuilder.AppendFormat("Title eq '{0}'", searchPresentation);
                paramBuilder.AppendFormat(" or Description eq '{0}'", searchPresentation);
                //paramBuilder.AppendFormat(" or Tags eq '{0}'", searchPresentation);
            }

            if (paramBuilder.Length > 0)
            {
                request.AddParameter("$filter", paramBuilder.ToString());
            }

            request.AddParameter("$select", "full");
            request.AddParameter("$orderby", "RecordDate");
            request.AddParameter("$top", top);
            request.AddParameter("$skip", skip);

            var presentations = ClientManager.Client.Execute<GenericResponse<PresentationFullRepresentation>>(request);


            if (presentations.Data?.value != null)
            {
                IEnumerable<Presentation> prDetails = presentations.Data.value.Select(i => MapToModel(i));
                presentationList = prDetails.ToList();
                //Removing authentication ticket
                /*
                foreach (Presentation pre in presentationList)
                {
                    string authTicket = RequestAuthTicket("Apptest", pre.Id, 1000);
                    pre.AuthorizationTicket = authTicket;
                    pre.ThumbnailUrl += "?AuthTicket=" + authTicket;
                    pre.PlaybackUrl = _mediasiteConn.EndpointAddress + @"Play/" + pre.Id + "?AuthTicket=" + authTicket;
                }
                */

            }
            //return presentationList;
            List<Presentation> sortedpresentationList = presentationList.OrderByDescending(x => x.RecordDate).ToList();
            //return presentationList;
            return sortedpresentationList;
        }

        /// <summary>
        /// Request Auth Ticket
        /// </summary>
        /// <param name="presentationId">Resource Id</param>
        /// <param name="minutesToLive">Minutes To Live</param>
        /// <returns>Authorization ticket value</returns>
        public string RequestAuthTicket(string presentationId, int minutesToLive)
        {
            var authResponse = this.RequestAuthorizationTicket(presentationId, minutesToLive);
            return authResponse.TicketId;
        }

        /// <summary>
        /// Get single object of presentation
        /// </summary>
        /// <param name="presentationId">Presentation Id</param>        
        /// <returns>Presentation Object</returns>
        public Presentation GetPresentation(string presentationId)
        {
            var authTicketInfo = RequestAuthTicket(presentationId, 1);
            var presentations = GetPresentations(null, 0, 0);
            var presentation = (from defaultItem in presentations
                                where (defaultItem.Id == presentationId)
                                select defaultItem).FirstOrDefault();
            presentation.AuthorizationTicket = authTicketInfo;

            return presentation;
        }

        #region Private Methods

        /// <summary>
        /// Map to model
        /// </summary>
        /// <param name="presentationDetails">PresentationFullRepresentation Object</param>
        /// <returns></returns>
        private Presentation MapToModel(PresentationFullRepresentation presentationDetails)
        {
            return new Presentation()
            {
                Id = presentationDetails.Id,
                Name = presentationDetails.Title,
                Description = presentationDetails.Description,
                ThumbnailUrl = presentationDetails.ThumbnailUrl,
                Duration = presentationDetails.Duration,
                RecordDate = presentationDetails.RecordDate,
                PrimaryPresenter = presentationDetails.PrimaryPresenter,
                Owner = presentationDetails.Owner

            };
        }

        /// <summary>
        /// Request Auth Ticket
        /// </summary>
        /// <param name="presentationId">Resource Id</param>
        /// <param name="minutesToLive">Minutes To Live</param>
        /// <returns>AuthResponse Object</returns>
        private AuthResponse RequestAuthorizationTicket(string presentationId, int minutesToLive)
        {
            AuthorizationTicket ticket = new AuthorizationTicket
            {
                Username = ConfigurationManager.AppSettings["MS_UserName"],
                ResourceId = presentationId,
                MinutesToLive = minutesToLive
            };

            var request = new RestRequest("AuthorizationTickets", Method.POST);
            request.RequestFormat = DataFormat.Json;
            request.AddJsonBody(ticket);

            var ticketResponse = ClientManager.Client.Execute<GenericResponse<PresentationFullRepresentation>>(request);
            var obj = JObject.Parse(ticketResponse.Content);

            return JsonConvert.DeserializeObject<AuthResponse>(obj.ToString());
        }

        #endregion
    }
}
