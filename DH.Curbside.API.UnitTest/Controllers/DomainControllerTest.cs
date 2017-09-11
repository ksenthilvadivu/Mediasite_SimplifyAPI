using DH.Curbside.API.Controllers;
using DH.Curbside.Core.Application.Exceptions;
using DH.Curbside.Core.Application.WhitelistDomain;
using DH.Curbside.Core.Application.WhitelistDomain.ViewModels;
using DH.Curbside.Core.Enterprise.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Hosting;

namespace DH.Curbside.API.UnitTest.Controllers
{
    [TestClass]
    public class DomainControllerTest
    {
        private string _tenantId = new Guid().ToString();
        private Mock<IWhitelistDomainApplication> _whitelistDomainApplicationMock;
        private WhitelistDomainController _whitelistDomainContoller;
        private static RequestDomainViewModel _requestDomainViewModelTest1 = new RequestDomainViewModel { AppId = 1, DomainName = "DomainNameTest1" };
        private static RequestDomainViewModel _requestDomainViewModelTest2 = new RequestDomainViewModel { AppId = 2, DomainName = "DomainNameTest2" };
        private static List<RequestDomainViewModel> _domainViewModelsListTest1 = new List<RequestDomainViewModel> { _requestDomainViewModelTest1, _requestDomainViewModelTest2 };
        

        [TestInitialize]
        public void TestInit()
        {
            _whitelistDomainApplicationMock = new Mock<IWhitelistDomainApplication>();
            _whitelistDomainContoller = new WhitelistDomainController(_whitelistDomainApplicationMock.Object);

            _whitelistDomainApplicationMock.Setup(u => u.AddDomain(_domainViewModelsListTest1, _tenantId)).Returns(Task.FromResult(true));

            _whitelistDomainContoller.Request = new System.Net.Http.HttpRequestMessage();
            _whitelistDomainContoller.Request.Properties.Add(HttpPropertyKeys.HttpConfigurationKey,
                                              new HttpConfiguration());
        }

        [TestMethod()]
        public void VerifyIfValidDataPassedToAPICallCreateDomainsThenReturnSuccessMessage()
        {
            var result = _whitelistDomainContoller.CreateDomains(_domainViewModelsListTest1, _tenantId);
            var resultString = result.Result.Content.ReadAsStringAsync();
            var objectsResult = JObject.Parse(resultString.Result);
            var resultStatusCode = result.Result.StatusCode;

            Assert.AreEqual(((int)ResponseCodes.WHITELISTDOMAIN_ADDED).ToString(), (string)objectsResult["Code"],
                                                "Expected response code is not matching with actual result");
            Assert.AreEqual("Whitelist Domain successfully added", (string)objectsResult["Message"],
                                                "Expected Message is not matching with actual message");

            Assert.AreEqual(HttpStatusCode.OK, resultStatusCode,
                                                "Expected status is not equal to actual");
        }

        [TestMethod]
        public void VerifyIfAnyExceptionRaisesInCreateDomainsAPIThenCaughtInCatchBlockAndRetnrnedErrorMessage()
        {
            _domainViewModelsListTest1[0].DomainName = "InvalidDomain";
            _whitelistDomainApplicationMock.Setup
                (
                u => u.AddDomain(_domainViewModelsListTest1, _tenantId))
                .Throws(new Exception("Test Exception")
                );

            var result = _whitelistDomainContoller.CreateDomains(_domainViewModelsListTest1, _tenantId);
            var resultString = result.Result.Content.ReadAsStringAsync();
            var resultStatusCode = result.Result.StatusCode;
            var objectsResult = JObject.Parse(resultString.Result);
        
            Assert.AreEqual(((int)ResponseCodes.INTERNAL_SEREVR_ERROR).ToString(), (string)objectsResult["Code"],
                                                "Expected response code is not matching with actual result");
            Assert.AreEqual("An unexpected error occurred. Please contact administrator", (string)objectsResult["Message"],
                                                "Expected Message is not matching with actual message");
            Assert.AreEqual(HttpStatusCode.NotFound, resultStatusCode,
                                            "Expected status is not equal to actual");
        }

        [TestMethod]
        public void VerifyAddingWhitelistDomainsAPIThrowsInvalidTenantIdExceptionIfInvalidTenantIdGiven()
        {
            _tenantId = "InvalidTenantId";
            _whitelistDomainApplicationMock
                .Setup(u => u.AddDomain(_domainViewModelsListTest1, _tenantId))
                .Throws(new InvalidTenantIdException());
            var result = _whitelistDomainContoller.CreateDomains(_domainViewModelsListTest1, _tenantId);
            var resultString = result.Result.Content.ReadAsStringAsync();
            var objectsResult = JObject.Parse(resultString.Result);
            var resultStatusCode = result.Result.StatusCode;
         
            Assert.AreEqual(HttpStatusCode.BadRequest, resultStatusCode,
                                             "Expected status is not equal to actual");
            Assert.AreEqual(((int)ResponseCodes.INVALID_TENANT_ID).ToString(), (string)objectsResult["Code"],
                                                "Expected response code is not matching with actual result");
            Assert.AreEqual("Invalid tenant id", (string)objectsResult["Message"],
                                                "Expected Message is not matching with actual message");
        }

        [TestMethod]
        public void VerifyAddingWhitelistDomainsAPIThrowsInvalidJsonExceptionIfInvalidRequestObjectGiven()
        {
            _domainViewModelsListTest1[0] = null;
            _whitelistDomainApplicationMock
                .Setup(u => u.AddDomain(_domainViewModelsListTest1, _tenantId))
                .Throws(new InvalidJsonException());
            var result = _whitelistDomainContoller.CreateDomains(_domainViewModelsListTest1, _tenantId);
            var resultString = result.Result.Content.ReadAsStringAsync();
            var objectsResult = JObject.Parse(resultString.Result);
            var resultStatusCode = result.Result.StatusCode;

            Assert.AreEqual(HttpStatusCode.BadRequest, resultStatusCode,
                                             "Expected status is not equal to actual");
            Assert.AreEqual(((int)ResponseCodes.INVALID_JSON_DATA).ToString(), (string)objectsResult["Code"],
                                                "Expected response code is not matching with actual result");
            Assert.AreEqual("Invalid json data", (string)objectsResult["Message"],
                                                "Expected Message is not matching with actual message");
        }      
    }
}
