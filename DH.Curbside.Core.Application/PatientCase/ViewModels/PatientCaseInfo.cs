using System;
using System.Collections.Generic;
using DH.Curbside.Core.Domain;

namespace DH.Curbside.Core.Application.PatientCase.ViewModels
{
    public class PatientCaseInfo
    {
        public int PatientCaseId { get; set; }

        public Guid TenantId { get; set; }

        public Guid PatientCaseGuid { get; set; }

        public string CaseTitle { get; set; }

        public string CaseDescription { get; set; }

        public Guid ProviderId { get; set; }

        public string CaseStatus { get; set; }

        public DateTime CreatedOn { get; set; }

        public ICollection<PatientCaseAttachment> MedicalCaseAttachnments { get; set; }
    }
}
