using System.Collections.Generic;

namespace DH.Curbside.Core.Domain
{
    /// <summary>
    /// Media attachment class
    /// </summary>
    public class MediaAttachment
    {
        public MediaAttachment()
        {
            PatientCaseAttachments = new HashSet<PatientCaseAttachment>();
        }

        public int MediaAttachmentId { get; set; }

        public byte[] AttachmentThumbnail { get; set; }

        public string AttachmentSourceId { get; set; }

        public string AttachmentSourceSystem { get; set; }

        public string AttachmentURL { get; set; }

        public virtual ICollection<PatientCaseAttachment> PatientCaseAttachments { get; set; }
    }
}
