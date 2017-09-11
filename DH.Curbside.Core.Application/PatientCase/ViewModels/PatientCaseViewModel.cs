using System.Collections.Generic;

namespace DH.Curbside.Core.Application.PatientCase.ViewModels
{
    public class PatientCaseViewModel
    {
        public PatientCaseViewModel()
        {
            PatientCases = new List<PatientCaseInfo>();
        }

        public IList<PatientCaseInfo> PatientCases { get; set; }
    }
}
