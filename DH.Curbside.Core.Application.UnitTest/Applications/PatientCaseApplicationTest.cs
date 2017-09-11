using AutoMapper;
using DH.Curbside.Core.Application.PatientCase;
using DH.Curbside.Core.Application.PatientCase.ViewModels;
using DH.Curbside.Core.Domain;
using DH.Curbside.Core.Persistence.RepositoryInterfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DH.Curbside.Core.Application.UnitTest.Applications
{
    [TestClass]
    public class PatientCaseApplicationTest
    {
        #region "Common test class level functionality"

        private static PatientCaseApplication _patientCaseApplication;
        private static Mock<IPatientCaseRepository> _patientCaseRepository;
        private static Mock<IProviderRepository> _providerRepository;
        private static Mock<IPatientCaseStatusRepository> _patientCaseStatusRepositoryMock;
        private static Mock<IMapper> _mapper;

        [ClassInitialize]
        public static void ClassInit(TestContext context)
        {
            _patientCaseRepository = new Mock<IPatientCaseRepository>();
            _providerRepository = new Mock<IProviderRepository>();
            _patientCaseStatusRepositoryMock = new Mock<IPatientCaseStatusRepository>();
            _mapper = new Mock<IMapper>();

            // PatientCase
            var patientCase = new Domain.PatientCase
            {
                PatientCaseId = 1,
                PatientCaseGuid = new Guid("90C641E8-E12F-406A-BB31-9557CE7D9F66"),
                CaseTitle = "Case 1",
                CaseDescription = "Case 1 Description",
                ProviderId = new Guid("DD2F863A-D7D9-4DF6-B1F5-4F570185185D"),
                CaseStatus = "Submit",
                CreatedOn = DateTime.Parse("08/05/2017"),
                Provider = new Provider
                {
                    ProviderId = new Guid("DD2F863A-D7D9-4DF6-B1F5-4F570185185D"),
                    TenantId = new Guid("80C641E8-E12F-406A-BB31-9557CE7D9F66"),
                    FirstName = "RK",
                    LastName = "K",
                    Phone = "1234567891",
                    Email = "rk@prokarma.com",
                    LoginSourceSystem = "Phone",
                    CreatedOn = DateTime.Parse("08/05/2017"),
                    Tenant = new Tenant
                    {
                        TenantId = new Guid("80C641E8-E12F-406A-BB31-9557CE7D9F66"),
                        TenantName = "DignityHealth"
                    }
                }
            };

            var patientCaseInfo = new List<PatientCaseInfo>
            {
                new PatientCaseInfo {
                    PatientCaseId = 1,
                    TenantId = new Guid("80C641E8-E12F-406A-BB31-9557CE7D9F66"),
                    PatientCaseGuid = new Guid("90C641E8-E12F-406A-BB31-9557CE7D9F66"),
                    CaseTitle = "Case 1",
                    CaseDescription = "Case 1 Description",
                    ProviderId = new Guid("DD2F863A-D7D9-4DF6-B1F5-4F570185185D"),
                    CaseStatus = "Submit",
                    CreatedOn = DateTime.Parse("08/05/2017"),
                    MedicalCaseAttachnments = null
                }
            };

            var patientCases = new List<Domain.PatientCase> { patientCase };

            // Get All
            IQueryable<Domain.PatientCase> queryablePatientCases = patientCases.AsQueryable();
            _patientCaseRepository.Setup(x => x.All()).Returns(queryablePatientCases);

            // FineBy
            _patientCaseRepository.Setup(x => x.FindBy(It.IsAny<int>())).Returns(patientCase);

            // Provider
            var provider = new Provider
            {
                ProviderId = new Guid("DD2F863A-D7D9-4DF6-B1F5-4F570185185D"),
                TenantId = new Guid("80C641E8-E12F-406A-BB31-9557CE7D9F66"),
                FirstName = "RK",
                LastName = "K",
                Phone = "1234567891",
                Email = "rk@prokarma.com",
                LoginSourceSystem = "Phone",
                CreatedOn = DateTime.Parse("08/05/2017"),
                Tenant = new Tenant
                {
                    TenantId = new Guid("80C641E8-E12F-406A-BB31-9557CE7D9F66"),
                    TenantName = "DignityHealth"
                },
                PatientCases = new List<Domain.PatientCase>
                {
                    patientCase
                }
            };

            var providers = new List<Provider> { provider };

            // Get All
            IQueryable<Provider> queryableProviders = providers.AsQueryable();
            _providerRepository.Setup(x => x.All()).Returns(queryableProviders);

            // FineBy
            _providerRepository.Setup(x => x.FindBy(It.IsAny<int>())).Returns(provider);

            // Add PatientCase
            _patientCaseRepository.Setup(x => x.Add(It.IsAny<Domain.PatientCase>())).Returns(Task.FromResult(1));

            var mockMapper = new Mock<IMapper>();
            mockMapper.Setup(x => x.Map<List<Domain.PatientCase>, List<PatientCaseInfo>>(It.IsAny<List<Domain.PatientCase>>())).Returns(patientCaseInfo);

            _patientCaseApplication = new PatientCaseApplication(_patientCaseRepository.Object, _mapper.Object, _providerRepository.Object, _patientCaseStatusRepositoryMock.Object);
        }

        #endregion

        #region GetPatientCase

        [TestMethod]
        public void GetPatientCase_WithValidData()
        {
            const string tenantId = "80C641E8-E12F-406A-BB31-9557CE7D9F66";
            const string providerId = "DD2F863A-D7D9-4DF6-B1F5-4F570185185D";

            //Act
            PatientCaseViewModel patientCaseViewModel = _patientCaseApplication.GetPatientCase(tenantId, providerId);

            //Assert - Need to change condition
            Assert.IsTrue(patientCaseViewModel.PatientCases.Count == 0, "PatientCase not found for Valid Search");
        }

        [TestMethod]
        public void GetPatientCase_WithInValidData()
        {
            const string tenantId = "80C641E8-E12F-406A-BB31-9557CE7D9F66";
            const string providerId = "4D8D4947-EBE5-4648-801C-D1033331C683";

            //Act
            PatientCaseViewModel patientCaseViewModel = _patientCaseApplication.GetPatientCase(tenantId, providerId);

            //Assert
            Assert.IsTrue(patientCaseViewModel.PatientCases.Count == 0, "PatientCase found for InValid Search");
        }

        #endregion
    }
}
