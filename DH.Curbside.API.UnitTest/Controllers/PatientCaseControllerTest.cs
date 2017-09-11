using DH.Curbside.API.Controllers;
using DH.Curbside.Core.Application.Exceptions;
using DH.Curbside.Core.Application.PatientCase;
using DH.Curbside.Core.Application.PatientCase.ViewModels;
using DH.Curbside.Core.Domain;
using DH.Curbside.Core.Enterprise.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Hosting;

namespace DH.Curbside.API.UnitTest.Controllers
{
    [TestClass]
    public class PatientCaseControllerTest
    {
        #region "Common test class level functionality"
        static string _tenantId = "80C641E8-E12F-406A-BB31-9557CE7D9F66";
        static string _providerId = "4D8D4947-EBE5-4648-801C-D1033331C683";
        string _caseId = "EC88A504-9BEA-4DD7-9DF9-311193F16ECG";
        static Guid _patientCaseGuid = Guid.NewGuid();

        private static PatientCaseController _patientCaseController;
        private static Mock<IPatientCaseApplication> _patientCaseApplicationMock;

        private static PatientCaseViewModel _patientCaseViewModelTest1 = new PatientCaseViewModel
        {
            PatientCases = {
                               new PatientCaseInfo{ PatientCaseId = 1, PatientCaseGuid = Guid.NewGuid(), TenantId = Guid.NewGuid(), CaseTitle = "Case 1", CaseDescription = "Case 1 Description", ProviderId = Guid.NewGuid(), CaseStatus = "submit", CreatedOn = DateTime.Parse("08/05/2017")},
                               new PatientCaseInfo{ PatientCaseId = 2, PatientCaseGuid = Guid.NewGuid(), TenantId = Guid.NewGuid(), CaseTitle = "Case 2", CaseDescription = "Case 2 Description", ProviderId = Guid.NewGuid(), CaseStatus = "submit", CreatedOn = DateTime.Parse("08/05/2017")}
                           }
        };

        private static List<PatientCaseViewModel> _patientCaseViewModelListTest1 = new List<PatientCaseViewModel> { _patientCaseViewModelTest1 };
        private static PatientCaseAttachment _patientCaseAttachment = new PatientCaseAttachment { };
        private static RequestPatientCaseViewModel _requestPatientCaseViewModel = new RequestPatientCaseViewModel
        {
            CaseDescription = "CaseDescTest1",
            CaseStatus = "CaseStatusTest1",
            CaseTitle = "CaseTitleTest1",
            ProviderId = new Guid(_providerId),
            PatientCaseAttachments = null
        };

        private static PatientCaseInfo _patientCaseInfo = new PatientCaseInfo
        {
            PatientCaseId = 1, PatientCaseGuid = _patientCaseGuid, TenantId = new Guid(_tenantId), CaseTitle = "Case 1", CaseDescription = "Case 1 Description", ProviderId = Guid.NewGuid(), CaseStatus = "submit", CreatedOn = DateTime.Parse("08/05/2017")
        };

        private static List<RequestPatientCaseViewModel> _requestPatientCaseViewModelList = new List<RequestPatientCaseViewModel> { _requestPatientCaseViewModel };

        [TestInitialize]
        public void TestInit()
        {
            _patientCaseApplicationMock = new Mock<IPatientCaseApplication>();

            _patientCaseApplicationMock.Setup(x => x.GetPatientCase(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(new PatientCaseViewModel
                {
                    PatientCases = {
                                        new PatientCaseInfo{ PatientCaseId = 1, PatientCaseGuid = Guid.NewGuid(), TenantId = Guid.NewGuid(), CaseTitle = "Case 1", CaseDescription = "Case 1 Description", ProviderId = Guid.NewGuid(), CaseStatus = "submit", CreatedOn = DateTime.Parse("08/05/2017")},
                                        new PatientCaseInfo{ PatientCaseId = 2, PatientCaseGuid = Guid.NewGuid(), TenantId = Guid.NewGuid(), CaseTitle = "Case 2", CaseDescription = "Case 2 Description", ProviderId = Guid.NewGuid(), CaseStatus = "submit", CreatedOn = DateTime.Parse("08/05/2017")}
                                   }
                });

            _patientCaseApplicationMock.Setup(x => x.GetSinglePatientCase(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(new PatientCaseInfo { PatientCaseId = 1, PatientCaseGuid = Guid.NewGuid(), TenantId = Guid.NewGuid(), CaseTitle = "Case 1", CaseDescription = "Case 1 Description", ProviderId = Guid.NewGuid(), CaseStatus = "submit", CreatedOn = DateTime.Parse("08/05/2017") });

            
            _patientCaseApplicationMock.Setup(x => x.GetSinglePatientCase(_tenantId, _caseId)).Returns(_patientCaseInfo);

            _patientCaseApplicationMock.Verify();

            _patientCaseController = new PatientCaseController(_patientCaseApplicationMock.Object);
            _patientCaseController.Request = new System.Net.Http.HttpRequestMessage();
            _patientCaseController.Request.Properties.Add(HttpPropertyKeys.HttpConfigurationKey,
                                              new HttpConfiguration());
        }

        //[ClassCleanup]
        //public static void ClassCleanup()
        //{
        //    _patientCaseController.Dispose();
        //}

        #endregion

        #region GetPatientCase

        [TestMethod]
        public void GetPatientCase_Valid_TenantId_And_ProviderId()
        {
            // Arrange

            _patientCaseApplicationMock.Setup(x => x.GetPatientCase(_tenantId, _providerId))
                .Returns(new PatientCaseViewModel
                {
                    PatientCases = {
                                        new PatientCaseInfo{ PatientCaseId = 1, PatientCaseGuid = Guid.NewGuid(), TenantId = Guid.NewGuid(), CaseTitle = "Case 1", CaseDescription = "Case 1 Description", ProviderId = Guid.NewGuid(), CaseStatus = "submit", CreatedOn = DateTime.Parse("08/05/2017")},
                                        new PatientCaseInfo{ PatientCaseId = 2, PatientCaseGuid = Guid.NewGuid(), TenantId = Guid.NewGuid(), CaseTitle = "Case 2", CaseDescription = "Case 2 Description", ProviderId = Guid.NewGuid(), CaseStatus = "submit", CreatedOn = DateTime.Parse("08/05/2017")}
                                   }
                });

            _patientCaseController.Request = new HttpRequestMessage
            {
                Properties = { { HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration() } }
            };

            // Act
            HttpResponseMessage patientCase = _patientCaseController.GetPatientCase(_tenantId, _providerId);

            // Assert
            Assert.IsTrue(patientCase.StatusCode == HttpStatusCode.OK && ((CurbsideResponse<PatientCaseViewModel>)(((ObjectContent)(patientCase.Content)).Value)).Code == 2000, "PatientCase record found for invalid tenantId and providerId");
        }     


        [TestMethod]
        public void VerifyGetPatientCaseAPICaughtCatchCodeWhileExceptionThrows()
        {
            _providerId = "Exception";
            _patientCaseApplicationMock.Setup
                (
                u => u.GetPatientCase(_tenantId, _providerId))
                .Throws(new Exception("Test Exception")
                );

            var result = _patientCaseController.GetPatientCase(_tenantId, _providerId);
            var resultString = result.Content.ReadAsStringAsync();
            var objectsResult = JObject.Parse(resultString.Result);
            var resultStatusCode = result.StatusCode;
            Assert.AreEqual(HttpStatusCode.InternalServerError, resultStatusCode,
                                                "Expected status is not equal to actual");
            Assert.AreEqual(((int)ResponseCodes.INTERNAL_SEREVR_ERROR).ToString(), objectsResult["Code"].ToString(),
                                                "Expected response code is not matching with actual result");
            Assert.AreEqual("An unexpected error occurred. Please contact administrator", objectsResult["Message"].ToString(),
                                                "Expected Message is not matching with actual message");
        }

        [TestMethod]
        public void VerifyGetPatientCaseAPICaughtInvalidTenantIdExceptionWhilePassedInvalidTenantId()
        {
            _tenantId = "InvalidId";
            _patientCaseApplicationMock.Setup
                (
                u => u.GetPatientCase(_tenantId, _providerId))
                .Throws(new InvalidTenantIdException()
                );

            var result = _patientCaseController.GetPatientCase(_tenantId, _providerId);
            var resultString = result.Content.ReadAsStringAsync();
            var objectsResult = JObject.Parse(resultString.Result);
            var resultStatusCode = result.StatusCode;
            Assert.AreEqual(HttpStatusCode.BadRequest, resultStatusCode,
                                                "Expected status is not equal to actual");
            Assert.AreEqual(((int)ResponseCodes.INVALID_TENANT_ID).ToString(), objectsResult["Code"].ToString(),
                                                "Expected response code is not matching with actual result");
            Assert.AreEqual("Invalid tenant id", objectsResult["Message"].ToString(),
                                                "Expected Message is not matching with actual message");
        }

        [TestMethod]
        public void VerifyGetPatientCaseAPICaughtInvalidProviderIdExceptionIfPassedInvalidProviderId()
        {
            _providerId = "";
            _patientCaseApplicationMock.Setup
                (
                u => u.GetPatientCase(_tenantId, _providerId))
                .Throws(new InvalidProviderIdException()
                );

            var result = _patientCaseController.GetPatientCase(_tenantId, _providerId);
            var resultString = result.Content.ReadAsStringAsync();
            var objectsResult = JObject.Parse(resultString.Result);
            var resultStatusCode = result.StatusCode;
            Assert.AreEqual(HttpStatusCode.BadRequest, resultStatusCode,
                                                "Expected status is not equal to actual");
            Assert.AreEqual(((int)ResponseCodes.INVALID_PROVIDER_ID).ToString(), objectsResult["Code"].ToString(),
                                                "Expected response code is not matching with actual result");
            Assert.AreEqual("Invalid provider id", objectsResult["Message"].ToString(),
                                                "Expected Message is not matching with actual message");
        }

        [TestMethod]
        public void VerifyGetPatientCaseAPICaughtPatientCaseNotAvailableExceptionIfDataNotFound()
        {
            _providerId = "NotExistsID";
            _patientCaseApplicationMock.Setup
                (
                u => u.GetPatientCase(_tenantId, _providerId))
                .Throws(new PatientCaseNotAvailableException()
                );

            var result = _patientCaseController.GetPatientCase(_tenantId, _providerId);
            var resultString = result.Content.ReadAsStringAsync();
            var objectsResult = JObject.Parse(resultString.Result);
            var resultStatusCode = result.StatusCode;
            Assert.AreEqual(HttpStatusCode.NotFound, resultStatusCode,
                                                "Expected status is not equal to actual");
            Assert.AreEqual(((int)ResponseCodes.PATIENTCASES_NOT_AVAILABLE).ToString(), objectsResult["Code"].ToString(),
                                                "Expected response code is not matching with actual result");
            Assert.AreEqual("Patient cases not found with the given details", objectsResult["Message"].ToString(),
                                                "Expected Message is not matching with actual message");
        }

        //  ----------------------------------------------------------------------------------------------------------

  

        [TestMethod]
        public void VerifyGetSinglePatientCaseAPIReturnedValidCaseDetailsWhileSendingValidRequest()
        {
            var result = _patientCaseController.GetSinglePatientCase(_tenantId, _caseId);
            var resultString = result.Content.ReadAsStringAsync();
            var objectsResult = JObject.Parse(resultString.Result);
            var resultList = objectsResult["Data"];
            Assert.AreEqual(((int)ResponseCodes.OK).ToString(), objectsResult["Code"].ToString(),
                                                   "Expected status code is not equal to actual value");
            Assert.AreEqual(_patientCaseInfo.PatientCaseId.ToString(), resultList["PatientCaseId"].ToString(),
                                                    "Expected PatientCaseId is not equal to actual value");
            Assert.AreEqual(_patientCaseInfo.PatientCaseGuid.ToString(), resultList["PatientCaseGuid"].ToString(),
                                                    "Expected PatientCaseGuid is not equal to actual value");
            Assert.AreEqual(_patientCaseInfo.TenantId.ToString(), resultList["TenantId"].ToString(),
                                                    "Expected TenantId is not equal to actual value");
            Assert.AreEqual(_patientCaseInfo.ProviderId.ToString(), resultList["ProviderId"].ToString(),
                                                    "Expected ProviderId is not equal to actual value");
            Assert.AreEqual(_patientCaseInfo.CreatedOn.ToString(), resultList["CreatedOn"].ToString(),
                                                    "Expected CreatedOn is not equal to actual value");
            Assert.AreEqual(_patientCaseInfo.CaseTitle, resultList["CaseTitle"].ToString(),
                                                    "Expected CaseTitle is not equal to actual value");
            Assert.AreEqual(_patientCaseInfo.CaseStatus, resultList["CaseStatus"].ToString(),
                                                    "Expected CaseStatus is not equal to actual value");
            Assert.AreEqual(_patientCaseInfo.CaseDescription, resultList["CaseDescription"].ToString(),
                                                    "Expected CaseDescription is not equal to actual value");    
        }

        [TestMethod]
        public void VerifyGetSinglePatientCaseAPICaughtCatchCodeWhileExceptionThrows()
        {
            _caseId = "Exception";
            _patientCaseApplicationMock.Setup
                (
                u => u.GetSinglePatientCase(_tenantId, _caseId))
                .Throws(new Exception("Test Exception")
                );

            var result = _patientCaseController.GetSinglePatientCase(_tenantId, _caseId);
            var resultString = result.Content.ReadAsStringAsync();
            var objectsResult = JObject.Parse(resultString.Result);
            var resultStatusCode = result.StatusCode;
            Assert.AreEqual(HttpStatusCode.InternalServerError, resultStatusCode,
                                                "Expected status is not equal to actual");
            Assert.AreEqual(((int)ResponseCodes.INTERNAL_SEREVR_ERROR).ToString(), objectsResult["Code"].ToString(),
                                                "Expected response code is not matching with actual result");
            Assert.AreEqual("An unexpected error occurred. Please contact administrator", objectsResult["Message"].ToString(),
                                                "Expected Message is not matching with actual message");
        }

        [TestMethod]
        public void VerifyGetSinglePatientCaseAPICaughtInvalidTenantIdExceptionWhilePassedInvalidTenantId()
        {
            _tenantId = "InvalidId";
            _patientCaseApplicationMock.Setup
                (
                u => u.GetSinglePatientCase(_tenantId, _caseId))
                .Throws(new InvalidTenantIdException()
                );

            var result = _patientCaseController.GetSinglePatientCase(_tenantId, _caseId);
            var resultString = result.Content.ReadAsStringAsync();
            var objectsResult = JObject.Parse(resultString.Result);
            var resultStatusCode = result.StatusCode;
            Assert.AreEqual(HttpStatusCode.BadRequest, resultStatusCode,
                                                "Expected status is not equal to actual");
            Assert.AreEqual(((int)ResponseCodes.INVALID_TENANT_ID).ToString(), objectsResult["Code"].ToString(),
                                                "Expected response code is not matching with actual result");
            Assert.AreEqual("Invalid tenant id", objectsResult["Message"].ToString(),
                                                "Expected Message is not matching with actual message");
        }

        [TestMethod]
        public void VerifyGetSinglePatientCaseAPICaughtInvalidCaseIdExceptionIfPassedInvalidCaseId()
        {
            _caseId = "";
            _patientCaseApplicationMock.Setup
                (
                u => u.GetSinglePatientCase(_tenantId, _caseId))
                .Throws(new InvalidCaseIdException()
                );

            var result = _patientCaseController.GetSinglePatientCase(_tenantId, _caseId);
            var resultString = result.Content.ReadAsStringAsync();
            var objectsResult = JObject.Parse(resultString.Result);
            var resultStatusCode = result.StatusCode;
            Assert.AreEqual(HttpStatusCode.BadRequest, resultStatusCode,
                                                "Expected status is not equal to actual");
            Assert.AreEqual(((int)ResponseCodes.INVALID_CASE_ID).ToString(), objectsResult["Code"].ToString(),
                                                "Expected response code is not matching with actual result");
            Assert.AreEqual("Invalid case id", objectsResult["Message"].ToString(),
                                                "Expected Message is not matching with actual message");
        }

        [TestMethod]
        public void VerifyGetSinglePatientCaseAPICaughtPatientCaseNotAvailableExceptionIfDataNotFound()
        {
            _caseId = "NotExistsID";
            _patientCaseApplicationMock.Setup
                (
                u => u.GetSinglePatientCase(_tenantId, _caseId))
                .Throws(new PatientCaseNotAvailableException()
                );

            var result = _patientCaseController.GetSinglePatientCase(_tenantId, _caseId);
            var resultString = result.Content.ReadAsStringAsync();
            var objectsResult = JObject.Parse(resultString.Result);
            var resultStatusCode = result.StatusCode;
            Assert.AreEqual(HttpStatusCode.NotFound, resultStatusCode,
                                                "Expected status is not equal to actual");
            Assert.AreEqual(((int)ResponseCodes.PATIENTCASE_NOT_AVAILABLE).ToString(), objectsResult["Code"].ToString(),
                                                "Expected response code is not matching with actual result");
            Assert.AreEqual("PatientCase not found with the given details", objectsResult["Message"].ToString(),
                                                "Expected Message is not matching with actual message");
        }
        #endregion
    }
}
