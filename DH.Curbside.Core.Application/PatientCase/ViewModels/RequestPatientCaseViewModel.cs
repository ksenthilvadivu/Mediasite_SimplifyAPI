using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DH.Curbside.Core.Application.PatientCase.ViewModels
{
    public class RequestPatientCaseViewModel
    {
        [Required]
        public string CaseTitle { get; set; }

        public string CaseDescription { get; set; }

        public Guid ProviderId { get; set; }

        [Required]
        public string CaseStatus { get; set; }

        public DateTime CreatedOn { get; set; }

        public ICollection<Domain.PatientCaseAttachment> PatientCaseAttachments { get; set; }
    }
}


