using AutoMapper;
using DH.Curbside.Core.Application.Exceptions;
using DH.Curbside.Core.Application.WhitelistDomain;
using DH.Curbside.Core.Application.WhitelistDomain.ViewModels;
using DH.Curbside.Core.Persistence.RepositoryInterfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DH.Curbside.Core.Application.UnitTest.Applications
{
    [TestClass]
    public class DomainApplicationTest
    {

        private static WhitelistDomainApplication _whitelistDomainApplication;
        private static Mock<IDomainRepository> _domainRepositoryMock;

        private static Mock<IMapper> _mapperMock;


        private static Guid _tenantIdGUID = new Guid("90C641E8-E12F-406A-BB31-9557CE7D9F66");
        private static Guid _tenantIdInvalidGUID = new Guid("DD2F863A-D7D9-4DF6-B1F5-4F570185185D");
        private static string _tenantInvalidFormat = "InvalidFormattenantId";


        private static RequestDomainViewModel _requestDomainViewModelTest1 = new RequestDomainViewModel
        {
            DomainName = "DignityHealth",
            AppId = 1

        };
        private static RequestDomainViewModel _requestDomainViewModelTest2 = new RequestDomainViewModel
        {
            DomainName = "DignityHealth2",
            AppId = 2

        };

        private static Domain.Domain _domain1 = new Domain.Domain
        {
            DomainName = "DignityHealth",

        };
        private static Domain.Domain _domain2 = new Domain.Domain
        {
            DomainName = "DignityHealth1",

        };

        private static List<Domain.Domain> _domainViewModelListTest1 = new List<Domain.Domain>
        {
            _domain1,_domain2
        };

        private  static List<RequestDomainViewModel> _requestDomainViewModelListTest1 = new List<RequestDomainViewModel> { _requestDomainViewModelTest1, _requestDomainViewModelTest2 };

        [TestInitialize]
        public void TestInitialize()
        {
            _domainRepositoryMock = new Mock<IDomainRepository>();
            _mapperMock = new Mock<IMapper>();
            _mapperMock.Setup(u => u.Map<List<RequestDomainViewModel>, List<Domain.Domain>>(_requestDomainViewModelListTest1)).Returns(_domainViewModelListTest1);

            _whitelistDomainApplication = new WhitelistDomainApplication(_domainRepositoryMock.Object, _mapperMock.Object);

            _domainRepositoryMock.Setup(u => u.Add(_domain1)).Returns(Task.FromResult(1));


        }
        //POST Domains
        [TestMethod]
        public void VerifyAddDomainAPIThrowsInvalidTenantIdExceptionWhenInvalidTenantIdGiven()
        {
            var result = _whitelistDomainApplication.AddDomain(_requestDomainViewModelListTest1, _tenantInvalidFormat);
            Assert.AreEqual(typeof(InvalidTenantIdException), result.Exception.InnerException.GetType(),
                "Expected InvalidTenantIdException is not matching with actual");
        }

        [TestMethod]
        public void VerifyAddDomainAPIThrowsInvalidJsonExceptionWhenInvalidDataGiven()
        {
            _mapperMock.Setup(u => u.Map<List<RequestDomainViewModel>, List<Domain.Domain>>(_requestDomainViewModelListTest1)).Returns(_domainViewModelListTest1);
            var result = _whitelistDomainApplication.AddDomain(null, _tenantIdGUID.ToString());
            Assert.AreEqual(typeof(InvalidJsonException), result.Exception.InnerException.GetType(),
          "Expected InvalidJsonException is not matching with actual");

        }

    }
}
