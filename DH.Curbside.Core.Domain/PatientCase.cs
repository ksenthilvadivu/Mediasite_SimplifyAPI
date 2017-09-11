using System;
using System.Collections.Generic;

namespace DH.Curbside.Core.Domain
{
    /// <summary>
    /// Patient case entity class
    /// </summary>
    public class PatientCase
    {
        public PatientCase()
        {
            PatientCaseHistories = new HashSet<PatientCaseHistory>();
            PatientCaseReviews = new HashSet<PatientCaseReview>();
            PatientCaseAttachments = new HashSet<PatientCaseAttachment>();
        }

        public int PatientCaseId { get; set; }

        public Guid PatientCaseGuid { get; set; }

        public string CaseTitle { get; set; }

        public string CaseDescription { get; set; }

        public Guid ProviderId { get; set; }

        public string CaseStatus { get; set; }

        public DateTime CreatedOn { get; set; }

        public virtual ICollection<PatientCaseHistory> PatientCaseHistories { get; set; }

        public virtual ICollection<PatientCaseReview> PatientCaseReviews { get; set; }

        public virtual ICollection<PatientCaseAttachment> PatientCaseAttachments { get; set; }

        public virtual Provider Provider { get; set; }
    }
}
