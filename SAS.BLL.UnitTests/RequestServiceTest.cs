using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SAS.DAL.Entities;
using SAS.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAS.BLL.UnitTests
{
    [TestClass]
    public class RequestServiceTest
    {
        /*
        [TestMethod]
        public async Task Create_EmailNull_ShouldReturnFalseInOperationDetails()
        {
            //Arrange
            string userName = "User";
            var request = new Request() { Name = "Request", SkillRequirements = new List<SkillRequirement>() };
            var requests = new List<Request>();
            requests.Add(request);
            var mock = new Mock<IUnitOfWork>();
            mock.Setup(a => a.UserManager.FindByNameAsync(It.IsAny<string>()).Result()).Returns(new User() { Requests = requests });
        }
        */
    }
}
