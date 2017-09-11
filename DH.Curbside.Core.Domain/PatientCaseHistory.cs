using System;

namespace DH.Curbside.Core.Domain
{
    public class PatientCaseHistory
    {
        public int PatientCaseHistoryId { get; set; }

        public int PatientCaseId { get; set; }

        public int CaseStatusId { get; set; }

        public DateTime? CreatedOn { get; set; }

        public string CreatedBy { get; set; }

        public virtual PatientCase PatientCase { get; set; }

        public virtual Status Status { get; set; }
    }
}
