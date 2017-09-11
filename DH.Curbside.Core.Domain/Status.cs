using System.Collections.Generic;

namespace DH.Curbside.Core.Domain
{
    public class Status
    {
        public Status()
        {
            CaseHistories = new HashSet<PatientCaseHistory>();
        }

        public int StatusId { get; set; }

        public string StatusName { get; set; }

        public string StatusDescription { get; set; }

        public virtual ICollection<PatientCaseHistory> CaseHistories { get; set; }
    }
}
