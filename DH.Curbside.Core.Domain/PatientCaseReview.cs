using System;

namespace DH.Curbside.Core.Domain
{
    public class PatientCaseReview
    {
        public int PatientCaseReviewId { get; set; }

        public int PatientCaseId { get; set; }

        public int ReviewerProviderId { get; set; }

        public string ReviewNotes { get; set; }

        public string DiagnosisCategory { get; set; }

        public string ReviewStatus { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime UpdatedOn { get; set; }

        public virtual PatientCase PatientCase { get; set; }
    }
}
