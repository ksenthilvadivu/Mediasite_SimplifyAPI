using System;

namespace DH.Curbside.Core.Application.PatientCase.ViewModels
{
    public class CreatePatientCaseResponseViewModel
    {
        public int CaseId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string status { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
