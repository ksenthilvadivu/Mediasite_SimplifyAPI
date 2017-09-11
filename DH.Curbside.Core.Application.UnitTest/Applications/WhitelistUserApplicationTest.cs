using AutoMapper;
using DH.Curbside.Core.Application.Exceptions;
using DH.Curbside.Core.Application.WhitelistUser;
using DH.Curbside.Core.Application.WhitelistUser.ViewModels;
using DH.Curbside.Core.Domain;
using DH.Curbside.Core.Enterprise.Email;
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
    public class WhitelistUserApplicationTest
    {
        private static WhitelistUserApplication _whitelistUserApplication;
        private static Mock<IWhitelistUserRepository> _whitelistUserRepositoryMock;
        private static Mock<IEmailService> _emailServiceMock;
        private static Mock<IMapper> _mapperMock;
        private static Guid _tenantIdGUID = new Guid("90C641E8-E12F-406A-BB31-9557CE7D9F66");
        private static Guid _tenantIdInvalidGUID = new Guid("DD2F863A-D7D9-4DF6-B1F5-4F570185185D");
        private static string _tenantInvalidFormat = "InvalidFormattenantId";

        private static DateTime _createdOnDateTest1 = DateTime.Now.AddDays(-3);
        private static DateTime _createdOnDateTest2 = DateTime.Now.AddDays(-1);
        private static DateTime _createdOnDateTest3 = DateTime.Now.AddDays(-2);
        private static DateTime _createdOnDateTest4 = DateTime.Now;
        private static string _emailValid = "valid@email.com";
        private static string _emailInvalid = "invalidemail";
        private static int _appIdValid = 1;
        private static int _appIdInvalid = 0;

        private static List<UserClientApplication> _userClientApplications = new List<UserClientApplication> { new UserClientApplication { ClientApplicationId = 1 } };

        private static User _userTest1 = new User
        {
            TenantId = _tenantIdGUID,
            UserId = 1,
            FirstName = "FNameTest1",
            LastName = "LNameTest1",
            EmailAddress = "testemail1@sample.com",
            CreatedBy = "CreatedByTest1",
            Prefix = "PrefixTest1",
            CreatedOn = _createdOnDateTest1,
            UserClientApplication = _userClientApplications
        };
        private static User _userTest2 = new User
        {
            TenantId = _tenantIdGUID,
            UserId = 2,
            FirstName = "FNameTest2",
            LastName = "LNameTest2",
            EmailAddress = "testemail2@sample.com",
            CreatedBy = "CreatedByTest2",
            Prefix = "PrefixTest2",
            CreatedOn = _createdOnDateTest2,
            UserClientApplication = _userClientApplications
        };

        private static User _userTest3 = new User
        {
            TenantId = _tenantIdGUID,
            UserId = 3,
            FirstName = "FNameTest3",
            LastName = "LNameTest3",
            EmailAddress = "testemail3@sample.com",
            CreatedBy = "CreatedByTest3",
            Prefix = "PrefixTest3",
            CreatedOn = _createdOnDateTest3,
            UserClientApplication = _userClientApplications
        };
        private static User _userTest4 = new User
        {
            TenantId = _tenantIdGUID,
            UserId = 4,
            FirstName = "FNameTest4",
            LastName = "LNameTest4",
            EmailAddress = "testemail4@sample.com",
            CreatedBy = "CreatedByTest4",
            Prefix = "PrefixTest4",
            CreatedOn = _createdOnDateTest4,
            UserClientApplication = _userClientApplications
        };

        private static UserInfo _userInfoTest1 = new UserInfo
        {
            ClientApplicationId = 1,
            FirstName = "FNameTest1",
            LastName = "LNameTest1",

            EmailAddress = "testemail1@sample.com",
            Prefix = "PrefixTest1",
            UserId = 1
        };
        private static UserInfo _userInfoTest2 = new UserInfo
        {
            ClientApplicationId = 1,
            FirstName = "FNameTest2",
            LastName = "LNameTest2",

            EmailAddress = "testemail2@sample.com",
            Prefix = "PrefixTest2",
            UserId = 2
        };
        private static UserInfo _userInfoTest3 = new UserInfo
        {
            ClientApplicationId = 1,
            FirstName = "FNameTest3",
            LastName = "LNameTest3",

            EmailAddress = "testemail3@sample.com",
            Prefix = "PrefixTest3",
            UserId = 3
        };
        private static UserInfo _userInfoTest4 = new UserInfo
        {
            ClientApplicationId = 1,
            FirstName = "FNameTest4",
            LastName = "LNameTest4",

            EmailAddress = "testemail4@sample.com",
            Prefix = "PrefixTest4",
            UserId = 4
        };

        private static List<UserInfo> _userInfoListTest = new List<UserInfo> { _userInfoTest1, _userInfoTest2, _userInfoTest3 };
        private static List<User> _usersList = new List<User> { _userTest1, _userTest2, _userTest3 };
        private static IQueryable<User> _usersQue = _usersList.AsQueryable();
        private static List<User> _usersListResult1 = _usersList.GetRange(0, 2);
        private static List<User> _usersListResult2 = new List<User> { _userTest3, _userTest4 };

        private static RequestUserViewModel requestUserViewModel1 = new RequestUserViewModel
        {
            AppId = 1,
            FirstName = "FNameTest1",
            LastName = "LNameTest1",
            Email = "testemail1@sample.com",
            Prefix = "PrefixTest1"
        };
        private static RequestUserViewModel requestUserViewModel2 = new RequestUserViewModel
        {
            AppId = 1,
            FirstName = "FNameTest2",
            LastName = "LNameTest2",
            Email = "testemail2@sample.com",
            Prefix = "PrefixTest2"
        };

        private static RequestUserViewModel requestUserViewModel3 = new RequestUserViewModel
        {
            AppId = 1,
            FirstName = "FNameTest3",
            LastName = "LNameTest3",
            Email = "testemail3@sample.com",
            Prefix = "PrefixTest3"
        };
        private static RequestUserViewModel requestUserViewModel4 = new RequestUserViewModel
        {
            AppId = 1,
            FirstName = "FNameTest4",
            LastName = "LNameTest4",
            Email = "testemail4@sample.com",
            Prefix = "PrefixTest4"
        };

        private static List<RequestUserViewModel> requestUserViewModelList1 = new List<RequestUserViewModel> { requestUserViewModel1, requestUserViewModel2 };
        private static List<RequestUserViewModel> requestUserViewModelList2 = new List<RequestUserViewModel> { requestUserViewModel3, requestUserViewModel4 };
        private static RequestInviteViewModel _requestInviteViewModelTest1 = new RequestInviteViewModel { AppId = 1, UserId = 1 };
        private static RequestInviteViewModel _requestInviteViewModelTest2 = new RequestInviteViewModel { AppId = 1, UserId = 2 };
        private static List<RequestInviteViewModel> _requestInviteViewModelList1 = new List<RequestInviteViewModel> { _requestInviteViewModelTest1, _requestInviteViewModelTest2 };


        [TestInitialize]
        public void TestInitialize()
        {
            _whitelistUserRepositoryMock = new Mock<IWhitelistUserRepository>();
            _emailServiceMock = new Mock<IEmailService>();
            _mapperMock = new Mock<IMapper>();
            _mapperMock.Setup(u => u.Map<List<User>, List<UserInfo>>(_usersList)).Returns(_userInfoListTest);
            _mapperMock.Setup(u => u.Map<User, UserInfo>(_userTest3)).Returns(_userInfoTest3);
            _mapperMock.Setup(u => u.Map<List<RequestUserViewModel>, List<User>>(requestUserViewModelList1)).Returns(_usersListResult1);
            _mapperMock.Setup(u => u.Map<List<RequestUserViewModel>, List<User>>(requestUserViewModelList2)).Returns(_usersListResult2);
            _whitelistUserApplication = new WhitelistUserApplication(_whitelistUserRepositoryMock.Object, _mapperMock.Object, _emailServiceMock.Object);
            _whitelistUserRepositoryMock.Setup(u => u.All()).Returns(_usersQue);
            _whitelistUserRepositoryMock.Setup(u => u.Add(_userTest4)).Returns(Task.FromResult(1));
            _whitelistUserRepositoryMock.Setup(u => u.Update(_userTest1)).Returns(Task.FromResult(true));
            _whitelistUserRepositoryMock.Setup(u => u.Add(_usersListResult1)).Returns(Task.FromResult(true));

        }

        [TestMethod]
        public void VerifyGetWhitelistUsersAPIThrowsInvalidTenantIdExceptionWhenInvalidTenantIdGiven()
        {
            try
            {
                _whitelistUserApplication.GetWhitelistUsers(_tenantInvalidFormat);
                Assert.Fail("Expected InvalidTenantIdException is not throwing.");
            }
            catch (InvalidTenantIdException)
            {
            }
        }

        [TestMethod]
        public void VerifyGetWhitelistUsersAPIThrowsWhitelistUserNotAvaliableExceptionWhenResultsNotFound()
        {
            try
            {
                _whitelistUserApplication.GetWhitelistUsers(_tenantIdInvalidGUID.ToString());
                Assert.Fail("Expected WhitelistUserNotAvaliableException is not throwing.");
            }
            catch (WhitelistUserNotAvaliableException)
            {
            }
        }

        [TestMethod]
        public void VerifyGetWhitelistUsersAPIReturnsDataWhenPassedValidRequest()
        {
            var userViewModel = _whitelistUserApplication.GetWhitelistUsers(_tenantIdGUID.ToString());

            Assert.AreEqual(3, _userInfoListTest.Count,
                "Expected result count is not equal to actual value");

            Assert.AreEqual(_userInfoListTest[0].ClientApplicationId, userViewModel.Users[0].ClientApplicationId,
                "Expected ClientApplicationId is not equal to actual value");
            Assert.AreEqual(_userInfoListTest[0].EmailAddress, userViewModel.Users[0].EmailAddress,
                "Expected EmailAddress is not equal to actual value");
            Assert.AreEqual(_userInfoListTest[0].FirstName, userViewModel.Users[0].FirstName,
                "Expected FirstName is not equal to actual value");
            Assert.AreEqual(_userInfoListTest[0].LastName, userViewModel.Users[0].LastName,
                "Expected LastName is not equal to actual value");
            Assert.AreEqual(_userInfoListTest[0].Prefix, userViewModel.Users[0].Prefix,
                "Expected Prefix is not equal to actual value");
            Assert.AreEqual(_userInfoListTest[0].UserId, userViewModel.Users[0].UserId,
                "Expected UserId is not equal to actual value");

            Assert.AreEqual(_userInfoListTest[1].ClientApplicationId, userViewModel.Users[1].ClientApplicationId,
                "Expected ClientApplicationId is not equal to actual value");
            Assert.AreEqual(_userInfoListTest[1].EmailAddress, userViewModel.Users[1].EmailAddress,
                "Expected EmailAddress is not equal to actual value");
            Assert.AreEqual(_userInfoListTest[1].FirstName, userViewModel.Users[1].FirstName,
                "Expected FirstName is not equal to actual value");
            Assert.AreEqual(_userInfoListTest[1].LastName, userViewModel.Users[1].LastName,
                "Expected LastName is not equal to actual value");
            Assert.AreEqual(_userInfoListTest[1].Prefix, userViewModel.Users[1].Prefix,
                "Expected Prefix is not equal to actual value");
            Assert.AreEqual(_userInfoListTest[1].UserId, userViewModel.Users[1].UserId,
                "Expected UserId is not equal to actual value");

            Assert.AreEqual(_userInfoListTest[2].ClientApplicationId, userViewModel.Users[2].ClientApplicationId,
                "Expected ClientApplicationId is not equal to actual value");
            Assert.AreEqual(_userInfoListTest[2].EmailAddress, userViewModel.Users[2].EmailAddress,
                "Expected EmailAddress is not equal to actual value");
            Assert.AreEqual(_userInfoListTest[2].FirstName, userViewModel.Users[2].FirstName,
                "Expected FirstName is not equal to actual value");
            Assert.AreEqual(_userInfoListTest[2].LastName, userViewModel.Users[2].LastName,
                "Expected LastName is not equal to actual value");
            Assert.AreEqual(_userInfoListTest[2].Prefix, userViewModel.Users[2].Prefix,
                "Expected Prefix is not equal to actual value");
            Assert.AreEqual(_userInfoListTest[2].UserId, userViewModel.Users[2].UserId,
                "Expected UserId is not equal to actual value");
        }

        // ------------------------------------------------------------------ GetWhitelistUser

        [TestMethod]
        public void VerifyGetWhitelistUserAPIThrowsInvalidTenantIdExceptionWhenInvalidTenantIdGiven()
        {
            try
            {
                _whitelistUserApplication.GetWhitelistUser(_tenantInvalidFormat, _emailValid);
                Assert.Fail("Expected InvalidTenantIdException is not throwing.");
            }
            catch (InvalidTenantIdException)
            {
            }
        }

        [TestMethod]
        public void VerifyGetWhitelistUserAPIThrowsWhitelistUserNotAvaliableExceptionWhenResultsNotFound()
        {
            try
            {
                _whitelistUserApplication.GetWhitelistUser(_tenantIdInvalidGUID.ToString(), _emailValid);
                Assert.Fail("Expected WhitelistUserNotAvaliableException is not throwing.");
            }
            catch (WhitelistUserNotAvaliableException)
            {
            }
        }

        [TestMethod]
        public void VerifyGetWhitelistUserAPIThrowsInvalidEmailExceptionWhenResultsNotFound()
        {
            try
            {
                _whitelistUserApplication.GetWhitelistUser(_tenantIdGUID.ToString(), _emailInvalid);
                Assert.Fail("Expected InvalidEmailException is not throwing.");
            }
            catch (InvalidEmailException)
            {
            }
        }

        [TestMethod]
        public void VerifyGetWhitelistUserAPIReturnsDataWhenPassedValidRequest()
        {
            var userInfo = _whitelistUserApplication.GetWhitelistUser(_tenantIdGUID.ToString(), _userInfoTest3.EmailAddress);

            Assert.IsNotNull(userInfo, "Expected result should not null");

            Assert.AreEqual(_userInfoTest3.ClientApplicationId, userInfo.ClientApplicationId,
                "Expected ClientApplicationId is not equal to actual value");
            Assert.AreEqual(_userInfoTest3.EmailAddress, userInfo.EmailAddress,
                "Expected EmailAddress is not equal to actual value");
            Assert.AreEqual(_userInfoTest3.FirstName, userInfo.FirstName,
                "Expected FirstName is not equal to actual value");
            Assert.AreEqual(_userInfoTest3.LastName, userInfo.LastName,
                "Expected LastName is not equal to actual value");
            Assert.AreEqual(_userInfoTest3.Prefix, userInfo.Prefix,
                "Expected Prefix is not equal to actual value");
            Assert.AreEqual(_userInfoTest3.UserId, userInfo.UserId,
                "Expected UserId is not equal to actual value");
        }

        // -------------------------------------------- DeprovisionUser

        [TestMethod]
        public void VerifyDeprovisionUserAPIThrowsInvalidTenantIdExceptionWhenInvalidTenantIdGiven()
        {
            var result = _whitelistUserApplication.DeprovisionUser(_tenantInvalidFormat, _emailValid, _appIdValid); ;
            Assert.AreEqual(typeof(InvalidTenantIdException), result.Exception.InnerException.GetType(),
                "Expected InvalidTenantIdException is not matching with actual");
        }

        [TestMethod]
        public void VerifyDeprovisionUserAPIThrowsWhitelistUserNotAvaliableExceptionWhenResultsNotFound()
        {
            var result = _whitelistUserApplication.DeprovisionUser(_tenantIdInvalidGUID.ToString(), _emailValid, _appIdValid);
            Assert.AreEqual(typeof(WhitelistUserNotAvaliableException), result.Exception.InnerException.GetType(),
                "Expected WhitelistUserNotAvaliableException is not matching with actual");
        }

        [TestMethod]
        public void VerifyDeprovisionUserAPIThrowsInvalidEmailExceptionWhenResultsNotFound()
        {
            var result = _whitelistUserApplication.DeprovisionUser(_tenantIdGUID.ToString(), _emailInvalid, _appIdValid);
            Assert.AreEqual(typeof(InvalidEmailException), result.Exception.InnerException.GetType(),
                "Expected InvalidEmailException is not matching with actual");
        }

        [TestMethod]
        public void VerifyDeprovisionUserAPIThrowsInvalidApplicationIdExceptionWhenResultsNotFound()
        {
            var result = _whitelistUserApplication.DeprovisionUser(_tenantIdGUID.ToString(), _userInfoTest3.EmailAddress, _appIdInvalid);
            Assert.AreEqual(typeof(InvalidApplicationIdException), result.Exception.InnerException.GetType(),
                "Expected InvalidApplicationIdException is not matching with actual");
        }

        [TestMethod]
        public void VerifyDeprovisionUserAPIReturnResultDataWhenPassedValidRequest()
        {
            var result = _whitelistUserApplication.DeprovisionUser(
                _tenantIdGUID.ToString(),
                _userTest1.EmailAddress,
                _userTest1.UserClientApplication.FirstOrDefault().ClientApplicationId.Value);

            Assert.AreEqual(true, result.Result, "Expected value is not equal to actual result");
        }

        // ------------------------------------------- CreateWhitelistUser

        [TestMethod]
        public void VerifyCreateWhitelistUserAPIThrowsInvalidTenantIdExceptionWhenInvalidTenantIdGiven()
        {
            var result = _whitelistUserApplication.CreateWhitelistUser(requestUserViewModelList1, _tenantInvalidFormat);
            Assert.AreEqual(typeof(InvalidTenantIdException), result.Exception.InnerException.GetType(),
                "Expected InvalidTenantIdException is not matching with actual");
        }

        [TestMethod]
        public void VerifyCreateWhitelistUserAPIThrowsInvalidJsonExceptionWhenInvalidDataGiven()
        {
            requestUserViewModelList1[0].Email = "InvalidEmail";
            _mapperMock.Setup(u => u.Map<List<RequestUserViewModel>, List<User>>(requestUserViewModelList1)).Returns((List<User>)null);
            var result = _whitelistUserApplication.CreateWhitelistUser(requestUserViewModelList1, _tenantIdGUID.ToString());
            Assert.AreEqual(typeof(InvalidJsonException), result.Exception.InnerException.GetType(),
                "Expected InvalidJsonException is not matching with actual");
        }

        [TestMethod]
        public void VerifyCreateWhitelistUserAPIReturnResultDataWhenPassedValidRequest()
        {
            var request = _usersListResult2.Where(u => u.EmailAddress == _usersListResult2[1].EmailAddress).ToList();
            _whitelistUserRepositoryMock.Setup(u => u.Add(request)).Returns(Task.FromResult(true));
            var result = _whitelistUserApplication.CreateWhitelistUser(requestUserViewModelList2, _tenantIdGUID.ToString());
            Assert.AreEqual(true, result.Result, "Expected value is not equal to actual result");
        }

        // ---------------------------------------------- InviteWhitelistUser

        [TestMethod]
        public void VerifyInviteWhitelistUserAPIThrowsInvalidTenantIdExceptionWhenInvalidTenantIdGiven()
        {
            try
            {
                _whitelistUserApplication.InviteWhitelistUser(_requestInviteViewModelList1, _tenantInvalidFormat);
                Assert.Fail("Expected InvalidTenantIdException is not throwing.");
            }
            catch (InvalidTenantIdException)
            {
            }
        }

        [TestMethod]
        public void VerifyInviteWhitelistUserAPIThrowsInvalidJsonExceptionWhenNullRequestPassed()
        {
            try
            {
                _whitelistUserApplication.InviteWhitelistUser(null, _tenantIdGUID.ToString());
                Assert.Fail("Expected InvalidJsonException is not throwing.");
            }
            catch (InvalidJsonException)
            {
            }
        }

        [TestMethod]
        public void VerifyInviteWhitelistUserAPIThrowsInValidDataExceptionWhenNullRequestPassed()
        {
            var tenantId = Guid.NewGuid();
            try
            {
                _whitelistUserApplication.InviteWhitelistUser(_requestInviteViewModelList1, tenantId.ToString());
                Assert.Fail("Expected InValidData is not throwing.");
            }
            catch (InValidData)
            {
            }
        }

        [TestMethod]
        public void VerifyInviteWhitelistUserAPISendEmailsForValidUsers()
        {
            _whitelistUserApplication.InviteWhitelistUser(_requestInviteViewModelList1, _tenantIdGUID.ToString());
            _emailServiceMock.Verify(x => x.SendEmailTemplate(EmailTemplateType.SubmitterRegistration, _usersList[0].EmailAddress), Times.Once(),
                    "Valid email invitation not done");
            _emailServiceMock.Verify(x => x.SendEmailTemplate(EmailTemplateType.SubmitterRegistration, _usersList[1].EmailAddress), Times.Once(),
                    "Valid email invitation not done");
        }
    }
}
