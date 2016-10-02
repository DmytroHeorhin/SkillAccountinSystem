using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SAS.BLL.Services;
using SAS.DAL.Interfaces;
using Moq;
using SAS.BLL.DTO;
using System.Threading.Tasks;
using SAS.BLL.Infrastructure;

namespace SAS.BLL.UnitTests
{
    [TestClass]
    public class UserServiceTest
    {
        [TestMethod]
        public async Task Create_EmailNull_ShouldReturnFalseInOperationDetails()
        {
            //Arrange
            var mock = new Mock<IUnitOfWork>();
            UserService userService = new UserService(mock.Object);
            UserDTO userDTO = new UserDTO() { Email = null, Id = "id", Password = "123Qwerty", UserName = "user", Role = "user", SkillSetId = 1 };
            var expectedOpDet = new OperationDetails(false, "", "");

            //Act
            var actualOpDet = await userService.Create(userDTO);

            //Assert
            Assert.AreEqual(expectedOpDet.Succeeded, actualOpDet.Succeeded, "True OperationDetails.Succeded result occurred");
        }
    }
}
