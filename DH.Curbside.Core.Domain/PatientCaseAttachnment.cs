using Newtonsoft.Json;

namespace DH.Curbside.Core.Domain
{
    /// <summary>
    /// Patient case attachment class
    /// </summary>
    public class PatientCaseAttachment
    {
        public int PatientCaseAttachmentId { get; set; }

        public int PatientCaseId { get; set; }
      
        public int MediaAttachmentId { get; set; }
       
        public virtual MediaAttachment MediaAttachment { get; set; }

        [JsonIgnore]
        public virtual PatientCase PatientCase { get; set; }
    }
}
