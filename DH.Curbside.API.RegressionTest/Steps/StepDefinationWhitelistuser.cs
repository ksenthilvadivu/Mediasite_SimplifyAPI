using System.Collections.Generic;
using TechTalk.SpecFlow;
using CurbsideTestProject.API.WhitelistUser;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Configuration;
using CurbsideTestProject.DataAccess;


namespace CurbsideTestProject
{
    [Binding]
    public sealed class StepDefinationuser
    {
        UserCaller WhitelistUser;
        List<UserModel> WhitelistUserInformation = null;
        DBConnection DBConnect = new DBConnection();

        /// <summary>
        /// Get Users
        /// </summary>

        [Given("I access the User URL")]
        public void GivenIAccessTheURL()
        {
            string getWhiteList = ConfigurationManager.AppSettings["URL"];
            WhitelistUser = new UserCaller(getWhiteList);
        }

        [When("I get all the users from service")]
        public void WhenIGetAllTheWhiteListUsersFromService()
        {
            WhitelistUserInformation = WhitelistUser.GetWhiteListUsers();
        }

        [Then("I should see users count as")]
        public void ThenWhiteListUserCountShouldbe()
        {
            Assert.IsTrue(WhitelistUserInformation[0].Code == 2000);
            Assert.IsTrue(WhitelistUserInformation[0].Message == "Success");
            Assert.IsTrue(WhitelistUserInformation[0].Data.Users.Count == DBConnect.GetUserCount());            
        }
    }

}
