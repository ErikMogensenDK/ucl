using EvacuationProject.Models;
using EvacuationProject.BusinessLogic;

namespace EvacuationProjectTests
{
    [TestClass]

    public class DataServiceTests
    {
        private DataService dataService = new();
        [TestInitialize]
        public void TestInitialize()
        {
            // add test user
            int userId = 123;
            string userName = "Test Name";
            AccessLevel level = AccessLevel.Employee;
            var myUser = new User(userId, userName, level);
            dataService.Save(myUser, dataService.Users);

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
        }
        [TestMethod]
        public void DataService_SaveShouldSaveEmployee()
        {
            //Arrange
            int userId = 123;
            string userName = "Test Name";
            AccessLevel level = AccessLevel.Employee;
            var myUser = new User(userId, userName, level);
            // Act
            dataService.Save(myUser, dataService.Users);
            // Assert
            Assert.AreEqual(myUser, dataService.Users.Last());
        }
        [TestMethod]
        public void DataService_SeveralSavesShouldSaveSeveralEmployees()
        {
            //Arrange
            int userId = 123;
            string userName = "Test Name";
            AccessLevel level = AccessLevel.Employee;
            var myUser = new User(userId, userName, level);
            int expectedCount = 4;
            // Act
            dataService.Save(myUser, dataService.Users);
            dataService.Save(myUser, dataService.Users);
            dataService.Save(myUser, dataService.Users);
            // Assert
            Assert.AreEqual(expectedCount, dataService.Users.Count());
        }
        [TestMethod]
        public void DataService_SaveAdministratorShouldSaveAdministrator()
        {
            // Arrange
            string username = "TestUserName";
            string password = "TestPassword";
            int adminId = 666;
            var myAdmin = new Administrator(adminId, username, password);
            // Act
            dataService.Save(myAdmin, dataService.Administrators);
            string actualUsername = dataService.Administrators[0].Name;
            string actualPassword = dataService.Administrators[0].Password;
            string expectedUsername = username;
            string expectedPassword = password;
            // Assert
            Assert.AreEqual(expectedUsername, actualUsername);
            Assert.AreEqual(expectedPassword, actualPassword);
        }
        [TestMethod]
        public void Dataservice_GetUsersCurrentlyCheckedInShouldReturnListOfUsers()
        {
            //Arrange
            //deletes user in test-intialize
            dataService.Delete(dataService.Users[0], dataService.Users);
            int userId = 1;
            string userName = "Test Name";
            AccessLevel level = AccessLevel.Employee;

            UserService userService = new(dataService);

            // Act
            var myUser = new User(userId, userName, level);
            userService.CheckIn(myUser, dataService.Workstations[0]);

            userId = 2;
            myUser = new User(userId, userName, level);
            userService.CheckIn(myUser, dataService.Workstations[0]);

            userId = 3;
            myUser = new User(userId, userName, level);
            userService.CheckIn(myUser, dataService.Workstations[0]);

            List<User> myUsers = dataService.GetUsersCurrentlyCheckedIn();

            // Assert
            int idCounter = 1;
            Assert.AreEqual(3, myUsers.Count);
            foreach (User user in myUsers)
            {
                Assert.AreEqual(userName, user.Name);
                Assert.AreEqual(idCounter, user.Id);
                Assert.AreEqual(user.Presence.Workstation, dataService.Workstations[0]);
                idCounter ++;
            }
        }
        [TestMethod]
        public void Dataservice_GetUsersCurrentlyCheckedInShouldNotReturnUsersCheckedOut()
        {
            //Arrange
            //deletes user in test-intialize
            dataService.Delete(dataService.Users[0], dataService.Users);
            int userId = 1;
            string userName = "Test Name";
            AccessLevel level = AccessLevel.Employee;
            var myUser = new User(userId, userName, level);
            UserService userService = new(dataService);

            // Act
            userService.CheckIn(myUser, dataService.Workstations[0]);

            userId = 2;
            var myUserTwo = new User(userId, userName, level);
            userService.CheckIn(myUserTwo, dataService.Workstations[0]);

            userId = 3;
            var myUserThree = new User(userId, userName, level);
            userService.CheckIn(myUserThree, dataService.Workstations[0]);


            // Assert
            Assert.AreEqual(3, dataService.Users.Count);
            Assert.AreEqual(3, dataService.GetUsersCurrentlyCheckedIn().Count);

            userService.CheckOut(myUserTwo);

            Assert.AreEqual(3, dataService.Users.Count);
            Assert.AreEqual(2, dataService.GetUsersCurrentlyCheckedIn().Count);
            Assert.AreEqual(null, myUserTwo.Presence);

            userService.CheckOut(myUserThree);

            Assert.AreEqual(3, dataService.Users.Count);
            Assert.AreEqual(1, dataService.GetUsersCurrentlyCheckedIn().Count);
            Assert.AreEqual(null, myUserThree.Presence);
        }
    }
}