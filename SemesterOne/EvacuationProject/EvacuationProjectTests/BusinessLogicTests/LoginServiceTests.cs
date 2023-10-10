using EvacuationProject.BusinessLogic;
using EvacuationProject.Models;

namespace EvacuationProjectTests.BusinessLogicTests
{
    [TestClass]
    public class LoginServiceTests
    {
        private DataService dataService = new();
        private LoginService loginService;

        [TestInitialize]
        public void TestInitialize()
        {
            // add test user
            int userId = 123;
            string userName = "Test Name";
            AccessLevel level = AccessLevel.Employee;
            var myUser = new User(userId, userName, level);
            dataService.Save(myUser, dataService.Users);

            //add test admin
            string adminUsername = "TestAdminName";
            string password = "TestAdminPassword";
            int adminId = 666;
            var myAdmin = new Administrator(adminId, adminUsername, password);
            dataService.Save(myAdmin, dataService.Administrators);

            // add test workstation
            string workstationName = "Test Workstation Name";
            string roomName = "TestRum 1";
            int roomNumber = 1;
            int floor = 0;
            var myBuilding = new Building("TestBuildingName", 0);
            var myRoom = new Room(roomName, roomNumber, floor, myBuilding);
            dataService.Save(myRoom, dataService.Rooms);
            Workstation myWorkstation = new(workstationName, roomNumber, myRoom);
            dataService.Save(myWorkstation, dataService.Workstations);

            loginService = new(dataService);
        }
        [TestMethod]
        public void LoginService_IsValidUserIdShouldReturnTrue()
        {
            // Arrange
            int userId = 123;

            // Act
            bool actualBool = loginService.IsValidUserId(userId);

            // Assert
            Assert.IsTrue(actualBool);

        }
        [TestMethod]
        public void LoginService_IsValidUserIdShouldReturnFalse()
        {
            // Arrange
            int userId = 111;

            // Act
            bool actualBool = loginService.IsValidUserId(userId);

            // Assert
            Assert.IsFalse(actualBool);
        }
        [TestMethod]
        public void LoginService_IsValidAdministratorShouldReturnTrue()
        {
            // Arrange
            int userId = 666;

            // Act
            bool expectedTrue = loginService.IsValidAdministrator(userId);
            bool expectedFalse = loginService.IsValidAdministrator(1234);

            // Assert
            Assert.IsTrue(expectedTrue);
            Assert.IsFalse(expectedFalse);
        }
        [TestMethod]
        public void LoginService_ValidAdminPasswordShouldReturntrue()
        {
            //Arrange
            int userId = 666;
            string password = "TestAdminPassword";

            //Act
            bool isValidPassword= loginService.IsValidAdminPassword(userId, password);

            //Assert
            Assert.IsTrue(isValidPassword);
        }
        [TestMethod]
        public void LoginService_InvalidAdminPasswordShouldThrowException()
        {
            //Arrange
            int userId = 666;
            string password = "IncorrectPassword";

            //Assert
            Assert.ThrowsException<Exception>(() => loginService.IsValidAdminPassword(userId, password));
        }
        [TestMethod]
        public void LoginUser_ShouldReturnUser()
        {
            //Arrange
            int userId = 123;
            //Act
            User myUser = loginService.LoginUser(userId);
            //Assert
            Assert.AreEqual(userId, myUser.Id);
            Assert.AreEqual("Test Name", myUser.Name);
            Assert.AreEqual(AccessLevel.Employee, myUser.AccessLevel);
        }
    }
}