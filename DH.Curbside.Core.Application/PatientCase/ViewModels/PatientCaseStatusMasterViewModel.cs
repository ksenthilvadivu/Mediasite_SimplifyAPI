using System.Collections.Generic;

namespace DH.Curbside.Core.Application.PatientCase.ViewModels
{
    public class PatientCaseStatusMasterViewModel
    {      
        public PatientCaseStatusMasterViewModel()
        {
            CaseStatuses = new List<PatientCaseStatusDescription>();
        }
        
        public IList<PatientCaseStatusDescription> CaseStatuses { get; set; }
    }
}
