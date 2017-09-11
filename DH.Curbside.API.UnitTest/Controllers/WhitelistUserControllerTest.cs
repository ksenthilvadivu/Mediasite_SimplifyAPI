using DH.Curbside.API.Controllers;
using DH.Curbside.Core.Application.Exceptions;
using DH.Curbside.Core.Application.WhitelistUser;
using DH.Curbside.Core.Application.WhitelistUser.ViewModels;
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
    public class WhitelistUserControllerTest
    {
        #region variable declaration
        private string _tenantId = new Guid().ToString();
        private int _appId = 1;
        private string _emailId = "sampleemail@test1.com";
        private Mock<IWhitelistUserApplication> _whitelistUserApplicationMock;
        private WhitelistUserController _whitelistUserContoller;
             
        static readonly UserInfo _userInfoTest1 = new UserInfo { ClientApplicationId = 1, FirstName = "FNameTest1", LastName = "LNameTest1", EmailAddress = "sampletest1@email.com", Prefix = "PrefixTest1", UserId = 1 };
        static readonly UserInfo _userInfoTest2 = new UserInfo { ClientApplicationId = 2, FirstName = "FNameTest2", LastName = "LNameTest2", EmailAddress = "sampletest2@email.com", Prefix = "PrefixTest2", UserId = 2 };
        static readonly UserViewModel _userViewModel = new UserViewModel { Users = { _userInfoTest1 , _userInfoTest2 } };


        private static readonly RequestUserViewModel _requestUserViewModelTest1 = new RequestUserViewModel { AppId = 1, FirstName = "FNameTest1", LastName = "LNameTest1", Email = "sampletest1@email.com", Invite = "InviteTest1", Prefix = "PrefixTest1" };
        private static readonly RequestUserViewModel _requestUserViewModelTest2 = new RequestUserViewModel { AppId = 2, FirstName = "FNameTest2", LastName = "LNameTest2", Email = "sampletest2@email.com", Invite = "InviteTest2", Prefix = "PrefixTest2" };
        private readonly List<RequestUserViewModel> _requestUserViewModelListTest1 = new List<RequestUserViewModel> { _requestUserViewModelTest1, _requestUserViewModelTest2 };        

        #endregion

        [TestInitialize]
        public void TestInit()
        {
            _whitelistUserApplicationMock = new Mock<IWhitelistUserApplication>();
            _whitelistUserContoller = new WhitelistUserController(_whitelistUserApplicationMock.Object);
            _whitelistUserApplicationMock.Setup(u => u.CreateWhitelistUser(_requestUserViewModelListTest1, _tenantId)).Returns(Task.FromResult(true));
            _whitelistUserApplicationMock.Setup(u => u.GetWhitelistUsers(_tenantId)).Returns(_userViewModel);
            _whitelistUserApplicationMock.Setup(u => u.GetWhitelistUser(_tenantId, _emailId)).Returns(_userInfoTest1);

            _whitelistUserApplicationMock.Setup(u => u.DeprovisionUser(_tenantId, _emailId, _appId)).Returns(Task.FromResult(true));

            _whitelistUserContoller.Request = new System.Net.Http.HttpRequestMessage();
            _whitelistUserContoller.Request.Properties.Add(HttpPropertyKeys.HttpConfigurationKey,
                                              new HttpConfiguration());
        }

        [TestMethod()]
        public void VerifyIfValidDataPassedToAPICallCreateWhitelistUserThenReturnSuccess()
        {
            var result = _whitelistUserContoller.CreateWhitelistUser(_requestUserViewModelListTest1, _tenantId);
            var resultString = result.Result.Content.ReadAsStringAsync();
            var objectsResult = JObject.Parse(resultString.Result);
            var resultStatusCode = result.Result.StatusCode;
            Assert.AreEqual(HttpStatusCode.OK, resultStatusCode,
                                            "Expected status is not equal to actual");
            Assert.AreEqual(((int)ResponseCodes.WHITELISTEUSER_ADDED).ToString(), (string)objectsResult["Code"],
                                            "Expected response code is not matching with actual result");
            Assert.AreEqual("Whitelist Users successfully added", (string)objectsResult["Message"],
                                            "Expected Message is not matching with actual message");
        }

        [TestMethod]
        public void VerifyIfInvalidTenantIdExceptionRaisesInCreateWhitelistUserAPIThenCaughtInCatchBlock()
        {
            _requestUserViewModelListTest1[0].Email = "InvalidEmail";

            _whitelistUserApplicationMock.Setup
                (
                u => u.CreateWhitelistUser(_requestUserViewModelListTest1, _tenantId))
                .Throws(new InvalidTenantIdException()
                );

            var result = _whitelistUserContoller.CreateWhitelistUser(_requestUserViewModelListTest1, _tenantId);
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
        public void VerifyIfInvalidJsonExceptionRaisesInCreateWhitelistUserAPIThenCaughtInCatchBlock()
        {
            _requestUserViewModelListTest1[0] = null;

            _whitelistUserApplicationMock.Setup
                (
                u => u.CreateWhitelistUser(_requestUserViewModelListTest1, _tenantId))
                .Throws(new InvalidJsonException()
                );

            var result = _whitelistUserContoller.CreateWhitelistUser(_requestUserViewModelListTest1, _tenantId);
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

        [TestMethod]
        public void VerifyIfAnyUnhandledExceptionRaisesInCreateWhitelistUserAPIThenCaughtInCatchBlock()
        {
            _requestUserViewModelListTest1[0].AppId = 0;
            
            _whitelistUserApplicationMock.Setup
                (
                u => u.CreateWhitelistUser(_requestUserViewModelListTest1, _tenantId))
                .Throws(new Exception("Test Exception")
                );

            var result = _whitelistUserContoller.CreateWhitelistUser(_requestUserViewModelListTest1, _tenantId);
            var resultString = result.Result.Content.ReadAsStringAsync();
            var objectsResult = JObject.Parse(resultString.Result);
            var resultStatusCode = result.Result.StatusCode;
            Assert.AreEqual(HttpStatusCode.NotFound, resultStatusCode,
                                                "Expected status is not equal to actual");
            Assert.AreEqual(((int)ResponseCodes.INTERNAL_SEREVR_ERROR).ToString(), (string)objectsResult["Code"],
                                                "Expected response code is not matching with actual result");
            Assert.AreEqual("An unexpected error occurred. Please contact administrator", (string)objectsResult["Message"],
                                                "Expected Message is not matching with actual message");
        }

        //---------------------------------
    
        [TestMethod]
        public void VerifyGetWhitelistAPIReturnedUsersWhileSendingValidRequest()
        {
            var result = _whitelistUserContoller.GetWhitelistUsers(_tenantId);
            var resultString = result.Content.ReadAsStringAsync();
            var objectsResult = JObject.Parse(resultString.Result);
            var resultList = objectsResult["Data"]["Users"];
            Assert.AreEqual(((int)ResponseCodes.OK).ToString(), objectsResult["Code"].ToString(),
                                                   "Expected status code is not equal to actual value");
            Assert.AreEqual(_userViewModel.Users[0].UserId.ToString(), resultList[0]["UserId"].ToString(),
                                                    "Expected id is not equal to actual value");
            Assert.AreEqual(_userViewModel.Users[0].FirstName, resultList[0]["FirstName"].ToString(),
                                                    "Expected First Name is not equal to actual value");
            Assert.AreEqual(_userViewModel.Users[0].LastName, resultList[0]["LastName"].ToString(),
                                                    "Expected Second Name is not equal to actual value");
            Assert.AreEqual(_userViewModel.Users[0].EmailAddress, resultList[0]["EmailAddress"].ToString(),
                                                    "Expected Email is not equal to actual value");
            Assert.AreEqual(_userViewModel.Users[0].Prefix, resultList[0]["Prefix"].ToString(),
                                                    "Expected Prefix is not equal to actual value");

            Assert.AreEqual(_userViewModel.Users[1].UserId.ToString(), resultList[1]["UserId"].ToString(),
                                                    "Expected id is not equal to actual value");
            Assert.AreEqual(_userViewModel.Users[1].FirstName, resultList[1]["FirstName"].ToString(),
                                                    "Expected First Name is not equal to actual value");
            Assert.AreEqual(_userViewModel.Users[1].LastName, resultList[1]["LastName"].ToString(),
                                                    "Expected Last Name is not equal to actual value");
            Assert.AreEqual(_userViewModel.Users[1].EmailAddress, resultList[1]["EmailAddress"].ToString(),
                                                    "Expected EMail is not equal to actual value");
            Assert.AreEqual(_userViewModel.Users[1].Prefix, resultList[1]["Prefix"].ToString(),
                                                    "Expected Prefix is not equal to actual value");
        }
             

        [TestMethod]
        public void VerifyGetWhitelistUsersAPICaughtCatchCodeWhileExceptionThrows()
        {
            _tenantId = "Exception";
            _whitelistUserApplicationMock.Setup
                (
                u => u.GetWhitelistUsers(_tenantId))
                .Throws(new Exception("Test Exception")
                );

            var result = _whitelistUserContoller.GetWhitelistUsers(_tenantId);
            var resultString = result.Content.ReadAsStringAsync();
            var objectsResult = JObject.Parse(resultString.Result);
            var resultStatusCode = result.StatusCode;
            Assert.AreEqual(HttpStatusCode.NotFound, resultStatusCode,
                                                "Expected status is not equal to actual");
            Assert.AreEqual(((int)ResponseCodes.RESOURCE_NOTFOUND).ToString(), objectsResult["Code"].ToString(),
                                                "Expected response code is not matching with actual result");
            Assert.AreEqual("Resource not found with the given details", objectsResult["Message"].ToString(),
                                                "Expected Message is not matching with actual message");
        }

        [TestMethod]
        public void VerifyGetWhitelistUsersAPICaughtInvalidTenantIdExceptionWhilePassedInvalidTenantId()
        {
            _tenantId = "InvalidId";
            _whitelistUserApplicationMock.Setup
                (
                u => u.GetWhitelistUsers(_tenantId))
                .Throws(new InvalidTenantIdException()
                );

            var result = _whitelistUserContoller.GetWhitelistUsers(_tenantId);
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
        public void VerifyGetWhitelistUsersAPICaughtWhitelistUserNotAvaliableExceptionIfDataNotFound()
        {
            _tenantId = "";
            _whitelistUserApplicationMock.Setup
                (
                u => u.GetWhitelistUsers(_tenantId))
                .Throws(new WhitelistUserNotAvaliableException()
                );

            var result = _whitelistUserContoller.GetWhitelistUsers(_tenantId);
            var resultString = result.Content.ReadAsStringAsync();
            var objectsResult = JObject.Parse(resultString.Result);
            var resultStatusCode = result.StatusCode;
            Assert.AreEqual(HttpStatusCode.NotFound, resultStatusCode,
                                                "Expected status is not equal to actual");
            Assert.AreEqual(((int)ResponseCodes.WHITELISTUSER_NOT_AVAILABLE).ToString(), objectsResult["Code"].ToString(),
                                                "Expected response code is not matching with actual result");
            Assert.AreEqual("WhitelistUser not found with the given details", objectsResult["Message"].ToString(),
                                                "Expected Message is not matching with actual message");
        }

        // ----------------------------------------------

        [TestMethod]
        public void VerifyGetWhitelistAPIReturnedValidUserWhileSendingValidRequest()
        {
            var result = _whitelistUserContoller.GetWhitelistUser(_tenantId, _emailId);
            var resultString = result.Content.ReadAsStringAsync();
            var objectsResult = JObject.Parse(resultString.Result);
            var resultList = objectsResult["Data"];
            Assert.AreEqual(((int)ResponseCodes.OK).ToString(), objectsResult["Code"].ToString(),
                                                   "Expected status code is not equal to actual value");
            Assert.AreEqual(_userInfoTest1.UserId.ToString(), resultList["UserId"].ToString(),
                                                    "Expected id is not equal to actual value");
            Assert.AreEqual(_userInfoTest1.FirstName, resultList["FirstName"].ToString(),
                                                    "Expected First Name is not equal to actual value");
            Assert.AreEqual(_userInfoTest1.LastName, resultList["LastName"].ToString(),
                                                    "Expected Second Name is not equal to actual value");
            Assert.AreEqual(_userInfoTest1.EmailAddress, resultList["EmailAddress"].ToString(),
                                                    "Expected Email is not equal to actual value");
            Assert.AreEqual(_userInfoTest1.Prefix, resultList["Prefix"].ToString(),
                                                    "Expected Prefix is not equal to actual value");          
        }

        [TestMethod]
        public void VerifyGetWhitelistUserAPICaughtCatchCodeWhileExceptionThrows()
        {
            _tenantId = "Exception";
            _whitelistUserApplicationMock.Setup
                (
                u => u.GetWhitelistUser(_tenantId, _emailId))
                .Throws(new Exception("Test Exception")
                );

            var result = _whitelistUserContoller.GetWhitelistUser(_tenantId, _emailId);
            var resultString = result.Content.ReadAsStringAsync();
            var objectsResult = JObject.Parse(resultString.Result);
            var resultStatusCode = result.StatusCode;
            Assert.AreEqual(HttpStatusCode.NotFound, resultStatusCode,
                                                "Expected status is not equal to actual");
            Assert.AreEqual(((int)ResponseCodes.RESOURCE_NOTFOUND).ToString(), objectsResult["Code"].ToString(),
                                                "Expected response code is not matching with actual result");
            Assert.AreEqual("Resource not found with the given details", objectsResult["Message"].ToString(),
                                                "Expected Message is not matching with actual message");
        }

        [TestMethod]
        public void VerifyGetWhitelistUserAPICaughtInvalidTenantIdExceptionWhilePassedInvalidTenantId()
        {
            _tenantId = "InvalidId";
            _whitelistUserApplicationMock.Setup
                (
                u => u.GetWhitelistUser(_tenantId, _emailId))
                .Throws(new InvalidTenantIdException()
                );

            var result = _whitelistUserContoller.GetWhitelistUser(_tenantId, _emailId);
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
        public void VerifyGetWhitelistUserAPICaughtInvalidEmailExceptionWhilePassedInvalidEmail()
        {
            _tenantId = "InvalidId";
            _whitelistUserApplicationMock.Setup
                (
                u => u.GetWhitelistUser(_tenantId, _emailId))
                .Throws(new InvalidEmailException()
                );

            var result = _whitelistUserContoller.GetWhitelistUser(_tenantId, _emailId);
            var resultString = result.Content.ReadAsStringAsync();
            var objectsResult = JObject.Parse(resultString.Result);
            var resultStatusCode = result.StatusCode;
            Assert.AreEqual(HttpStatusCode.BadRequest, resultStatusCode,
                                                "Expected status is not equal to actual");
            Assert.AreEqual(((int)ResponseCodes.INVALID_EMAIL_ID).ToString(), objectsResult["Code"].ToString(),
                                                "Expected response code is not matching with actual result");
            Assert.AreEqual("Invalid email id", objectsResult["Message"].ToString(),
                                                "Expected Message is not matching with actual message");
        }

        [TestMethod]
        public void VerifyGetWhitelistUserAPICaughtWhitelistUserNotAvaliableExceptionIfDataNotFound()
        {
            _tenantId = "";
            _whitelistUserApplicationMock.Setup
                (
                u => u.GetWhitelistUser(_tenantId, _emailId))
                .Throws(new WhitelistUserNotAvaliableException()
                );

            var result = _whitelistUserContoller.GetWhitelistUser(_tenantId, _emailId);
            var resultString = result.Content.ReadAsStringAsync();
            var objectsResult = JObject.Parse(resultString.Result);
            var resultStatusCode = result.StatusCode;
            Assert.AreEqual(HttpStatusCode.BadRequest, resultStatusCode,
                                                "Expected status is not equal to actual");
            Assert.AreEqual(((int)ResponseCodes.WHITELISTUSER_NOT_AVAILABLE).ToString(), objectsResult["Code"].ToString(),
                                                "Expected response code is not matching with actual result");
            Assert.AreEqual("WhitelistUser not found with the given details", objectsResult["Message"].ToString(),
                                                "Expected Message is not matching with actual message");
        }

        //----------------------

        [TestMethod]
        public void VerifyDeprovisionUserAPIReturnSuccessMessageIfPassedWhiteListUsersList()
        {
            var result = _whitelistUserContoller.DeprovisionUser(_tenantId, _emailId, _appId);
            var resultString = result.Result.Content.ReadAsStringAsync();
            var resultStatusCode = result.Result.StatusCode;
            var objectsResult = JObject.Parse(resultString.Result);
            Assert.AreEqual(HttpStatusCode.OK, resultStatusCode,
                                                "Expected status is not equal to actual");
            
            Assert.AreEqual(((int)ResponseCodes.WHITELISTEUSER_DEPROVISIONED).ToString(), (string)objectsResult["Code"],
                                            "Expected response code is not matching with actual result");
            Assert.AreEqual("Whitelist user successfully updated", (string)objectsResult["Message"],
                                                "Expected Message is not matching with actual message");
        }
      
        [TestMethod]
        public void VerifyDeprovisionUserAPIReturnInvalidTenantIdExceptionMessageIfPassedInvalidTenantId()
        {
            _emailId = "InvalidEmail";
            _whitelistUserApplicationMock.Setup
             (
             u => u.DeprovisionUser(_tenantId, _emailId, _appId))
             .Throws(new InvalidTenantIdException()
             );
            var result = _whitelistUserContoller.DeprovisionUser(_tenantId, _emailId, _appId);
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
        public void VerifyDeprovisionUserAPIReturnWhitelistUserNotAvaliableExceptionIfResultNotFound()
        {
            _appId = 10;
            _whitelistUserApplicationMock.Setup(u => u.DeprovisionUser(_tenantId, _emailId, _appId)).Throws(new WhitelistUserNotAvaliableException());
            var result = _whitelistUserContoller.DeprovisionUser(_tenantId, _emailId, _appId);
            var resultString = result.Result.Content.ReadAsStringAsync();
            var resultStatusCode = result.Result.StatusCode;
            var objectsResult = JObject.Parse(resultString.Result);
            Assert.AreEqual(HttpStatusCode.BadRequest, resultStatusCode,
                                                "Expected status is not equal to actual");
            Assert.AreEqual(((int)ResponseCodes.WHITELISTUSER_NOT_AVAILABLE).ToString(), (string)objectsResult["Code"],
                                         "Expected response code is not matching with actual result");
            Assert.AreEqual("WhitelistUser not found with the given details", (string)objectsResult["Message"],
                                                "Expected Message is not matching with actual message");
        }

        [TestMethod]
        public void VerifyDeprovisionUserAPIReturnInvalidEmailExceptionIfPassedEmailIdInvalid()
        {
            _emailId = "Invalid@email.com";
            _whitelistUserApplicationMock.Setup(u => u.DeprovisionUser(_tenantId, _emailId, _appId)).Throws(new InvalidEmailException());
            var result = _whitelistUserContoller.DeprovisionUser(_tenantId, _emailId, _appId);
            var resultString = result.Result.Content.ReadAsStringAsync();
            var resultStatusCode = result.Result.StatusCode;
            var objectsResult = JObject.Parse(resultString.Result);
            Assert.AreEqual(HttpStatusCode.BadRequest, resultStatusCode,
                                                "Expected status is not equal to actual");
            Assert.AreEqual(((int)ResponseCodes.INVALID_EMAIL_ID).ToString(), (string)objectsResult["Code"],
                                         "Expected response code is not matching with actual result");
            Assert.AreEqual("Invalid email id", (string)objectsResult["Message"],
                                                "Expected Message is not matching with actual message");
        }

        [TestMethod]
        public void VerifyDeprovisionUserAPIReturnInvalidApplicationIdExceptionIfPassedAppIdInvalid()
        {
            _appId = 99;
            _whitelistUserApplicationMock.Setup(u => u.DeprovisionUser(_tenantId, _emailId, _appId)).Throws(new InvalidApplicationIdException());
            var result = _whitelistUserContoller.DeprovisionUser(_tenantId, _emailId, _appId);
            var resultString = result.Result.Content.ReadAsStringAsync();
            var resultStatusCode = result.Result.StatusCode;
            var objectsResult = JObject.Parse(resultString.Result);
            Assert.AreEqual(HttpStatusCode.BadRequest, resultStatusCode,
                                                "Expected status is not equal to actual");
            Assert.AreEqual(((int)ResponseCodes.INVALID_APPLICATION_ID).ToString(), (string)objectsResult["Code"],
                                         "Expected response code is not matching with actual result");
            Assert.AreEqual("Invalid application id for user", (string)objectsResult["Message"],
                                                "Expected Message is not matching with actual message");
        }

        [TestMethod]
        public void VerifyDeprovisionUserAPIReturnExceptionIfUnexpectedExceptionCaught()
        {
            _emailId = "Exception@email.com";
            _whitelistUserApplicationMock.Setup(u => u.DeprovisionUser(_tenantId, _emailId, _appId)).Throws(new Exception());
            var result = _whitelistUserContoller.DeprovisionUser(_tenantId, _emailId, _appId);
            var resultString = result.Result.Content.ReadAsStringAsync();
            var resultStatusCode = result.Result.StatusCode;
            var objectsResult = JObject.Parse(resultString.Result);
            Assert.AreEqual(HttpStatusCode.NotFound, resultStatusCode,
                                                "Expected status is not equal to actual");
            Assert.AreEqual(((int)ResponseCodes.INTERNAL_SEREVR_ERROR).ToString(), (string)objectsResult["Code"],
                                         "Expected response code is not matching with actual result");
            Assert.AreEqual("An unexpected error occurred. Please contact administrator", (string)objectsResult["Message"],
                                                "Expected Message is not matching with actual message");
        }
    }
}