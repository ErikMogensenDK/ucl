using EvacuationProject.BusinessLogic;
using EvacuationProject.Models;

namespace EvacuationProjectTests.ModelsTests
{
    [TestClass]
    public class AdministratorTests
    {
        DataService dataService = new();
        AdministratorService administratorService; 
        [TestInitialize]
        public void TestInitialize()
        {
            administratorService = new(dataService);
            int userId = 123;
            string userName = "Test Name";
            AccessLevel level = AccessLevel.Employee;
            var myUser = new User(userId, userName, level);
            List<User> myUserList = new(){myUser};
            dataService.Save(myUser, dataService.Users);

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
        public void Administrator_CreateWorkstationCreatesWorkstation()
        {
            // Arrange
            string workstationName = "Test Workstation Name Two";
            string roomName = "TestRum 2";
            int roomNumber = 0;
            int floor = 0;
            string buildingName = "TestBuildingNameTwo";
            int buildingId = 1;
            var myBuilding = new Building(buildingName, buildingId);
            var myRoom = new Room(roomName, roomNumber, floor, myBuilding);
            Workstation myWorkstation = new(workstationName, roomNumber, myRoom);
            int adminId = 666;
            Administrator myAdmin = new(adminId, "AdministratorName", "AdministratorPassword");

            int expectedWorkstationCount = 2;
            string expectedWorkstationName = workstationName;
            int expectedWorkstationFloor = floor;
            string expectedWorkstationRoomName = roomName;
            string expectedWorkstationBuildingName = buildingName;

            // Act
            administratorService.Create(myWorkstation, dataService.Workstations);
            // Assert
            int actualWorkstationCount = dataService.Workstations.Count;
            string actualWorkstationName = dataService.Workstations[1].Name;
            int actualWorkstationFloor = dataService.Workstations[1].Room.Floor;
            string actualWorkstationRoomName = dataService.Workstations[1].Room.Name;
            string actualWorkstationBuildingName = dataService.Workstations[1].Room.Building.Name;

            Assert.AreEqual(expectedWorkstationCount, actualWorkstationCount);
            Assert.AreEqual(expectedWorkstationName, actualWorkstationName);
            Assert.AreEqual(expectedWorkstationFloor, actualWorkstationFloor);
            Assert.AreEqual(expectedWorkstationRoomName, actualWorkstationRoomName);
            Assert.AreEqual(expectedWorkstationBuildingName, actualWorkstationBuildingName);
        }

        [TestMethod]
        public void Administrator_CreatingWorkstationWithIdenticalIdShouldThrowException()
        {
            // Arrange
            string workstationName = "Test Workstation Name Two";
            string roomName = "TestRum 2";
            int roomNumber = 0;
            int floor = 0;
            string buildingName = "TestBuildingNameTwo";
            int buildingId = 1;
            var myBuilding = new Building(buildingName, buildingId);
            var myRoom = new Room(roomName, roomNumber, floor, myBuilding);
            Workstation myWorkstation = new(workstationName, roomNumber, myRoom);
            int adminId = 666;
            Administrator myAdmin = new(adminId, "AdministratorName", "AdministratorPassword");

            // Act
            administratorService.Create(myWorkstation, dataService.Workstations);

            // Assert
            Assert.ThrowsException<Exception>(() => administratorService.Create(myWorkstation, dataService.Workstations));
        }
        [TestMethod]
        public void Administrator_DeletingWorkstationShouldRemoveWorkstation()
        {
            // Arrange
            string workstationName = "Test Workstation Name Two";
            string roomName = "TestRum 2";
            int roomNumber = 0;
            int floor = 0;
            string buildingName = "TestBuildingNameTwo";
            int buildingId = 1;
            var myBuilding = new Building(buildingName, buildingId);
            var myRoom = new Room(roomName, roomNumber, floor, myBuilding);
            Workstation myWorkstation = new(workstationName, roomNumber, myRoom);
            int adminId = 666;
            Administrator myAdmin = new(adminId, "AdministratorName", "AdministratorPassword");

            // Act
            administratorService.Create(myWorkstation, dataService.Workstations);
            administratorService.Delete(myWorkstation, dataService.Workstations);
            // Assert
            Assert.IsFalse(dataService.AlreadyExists(myWorkstation, dataService.Workstations));
            Assert.AreEqual(1, dataService.Workstations.Count);
        }

        [TestMethod]
        public void Administrator_CreateUserShouldCreateEmployee()
        {
            //Arrange
            int userId = 321;
            string userName = "TestName1";
            AccessLevel access = AccessLevel.Employee;
            User myUser = new(userId, userName, access);

            int expectedUserId = userId;
            string expectedUsername = userName;
            AccessLevel expectedAccessLevel = access;

            //ACT
            int adminId = 666;
            Administrator myAdmin = new(adminId, "AdministratorName", "AdministratorPassword");
            administratorService.Create(myUser, dataService.Users);
            int? actualUserId = dataService.Users[1].Id;
            string actualUsername = dataService.Users[1].Name;
            AccessLevel actualAccessLevel = dataService.Users[1].AccessLevel;

            //Assert
            Assert.AreEqual(expectedUsername, actualUsername);
            Assert.AreEqual(expectedUserId, actualUserId);
            Assert.AreEqual(expectedAccessLevel, actualAccessLevel);
        }

        [TestMethod]
        public void Administrator_CreateUserShouldThrowExceptionIfDuplicateId()
        {
            //Arrange
            int userId = 321;
            string userName = "TestName1";
            AccessLevel access = AccessLevel.Employee;
            User myUser = new(userId, userName, access);

            //ACT
            int adminId = 666;
            Administrator myAdmin = new(adminId, "AdministratorName", "AdministratorPassword");
            administratorService.Create(myUser, dataService.Users);

            //Assert
            Assert.ThrowsException<Exception>(() => administratorService.Create(myUser, dataService.Users));
        }

        [TestMethod]
        public void Administrator_DeleteUserShouldDeleteUser()
        {
            //Arrange
            int userId = 321;
            string userName = "TestName1";
            AccessLevel access = AccessLevel.Employee;
            User myUser = new(userId, userName, access);

            //ACT
            int adminId = 666;
            Administrator myAdmin = new(adminId, "AdministratorName", "AdministratorPassword");
            administratorService.Create(myUser, dataService.Users);
            Assert.AreEqual(2, dataService.Users.Count);

            administratorService.Delete(myUser, dataService.Users);
            Assert.AreEqual(1, dataService.Users.Count);

            //Assert
            Assert.IsFalse(dataService.AlreadyExists(myUser, dataService.Users));
        }


        [TestMethod]
        public void Administrator_CreateRoomShouldCreateRoom()
        {
            //Arrange
            string expectedRoomName = "Test Room Name";
            int expectedRoomNumber = 5;
            int expectedFloor = 1;
            string expectedBuildingName = "testBuildingName";
            int expectedBuildingId = 1;
            var expectedBuilding = new Building(expectedBuildingName, expectedBuildingId);
            var myRoom = new Room(expectedRoomName, expectedRoomNumber, expectedFloor, expectedBuilding);

            //Act
            int adminId = 666;
            Administrator myAdmin = new(adminId, "AdministratorName", "AdministratorPassword");
            administratorService.Create(myRoom, dataService.Rooms);

            string actualRoomName = dataService.Rooms[1].Name;
            int? actualRoomNumber = dataService.Rooms[1].Id;
            int actualFloor = dataService.Rooms[1].Floor;
            string actualBuildingName = dataService.Rooms[1].Building.Name;
            int? actualBuildingId = dataService.Rooms[1].Building.Id;

            //Assert
            Assert.AreEqual(expectedRoomName, actualRoomName);
            Assert.AreEqual(expectedRoomNumber, actualRoomNumber);
            Assert.AreEqual(expectedFloor, actualFloor);
            Assert.AreEqual(expectedBuildingName, actualBuildingName);
            Assert.AreEqual(expectedBuildingId, actualBuildingId);
        }
        [TestMethod]
        public void Administrator_CreateRoomShouldThrowExceptionIfDuplicateRoomNumberOrName()
        {
            //Arrange
            string roomName = "Test Room Name";
            int roomNumber = 5;
            int floor = 1;
            string buildingName = "testBuildingName";
            int buildingId = 1;
            var building = new Building(buildingName, buildingId);
            var myRoom = new Room(roomName, roomNumber, floor, building);

            //Act
            int adminId = 666;
            Administrator myAdmin = new(adminId, "AdministratorName", "AdministratorPassword");
            administratorService.Create(myRoom, dataService.Rooms);

            //Assert
            Assert.ThrowsException<Exception>(() => administratorService.Create(myRoom, dataService.Rooms));
        }

        [TestMethod]
        public void Administrator_DeleteRoomShouldDeleteRoom()
        {
            //Arrange
            string roomName = "Test Room Name";
            int roomNumber = 5;
            int floor = 1;
            string buildingName = "testBuildingName";
            int buildingId = 1;
            var building = new Building(buildingName, buildingId);
            var myRoom = new Room(roomName, roomNumber, floor, building);

            //Act
            int adminId = 666;
            Administrator myAdmin = new(adminId, "AdministratorName", "AdministratorPassword");
            administratorService.Create(myRoom, dataService.Rooms);
            Assert.IsTrue(dataService.AlreadyExists(myRoom, dataService.Rooms));
            Assert.AreEqual(2, dataService.Rooms.Count);

            administratorService.Delete(myRoom, dataService.Rooms);
            Assert.AreEqual(1, dataService.Rooms.Count);

            //Assert
            Assert.IsFalse(dataService.AlreadyExists(myRoom, dataService.Rooms));
        }


        [TestMethod]
        public void Administrator_UpdateShouldUpdateInformationInRoom()
        {
            //Arrange
            string roomName = "Test Room Name";
            int id = 5;
            int floor = 1;
            string buildingName = "testBuildingName";
            int buildingId = 1;
            var building = new Building(buildingName, buildingId);
            var myRoom = new Room(roomName, id, floor, building);

            Assert.AreEqual(1, dataService.Rooms.Count);
            //Act
            int adminId = 666;
            Administrator myAdmin = new(adminId, "AdministratorName", "AdministratorPassword");
            administratorService.Create(myRoom, dataService.Rooms);
            Assert.AreEqual(2, dataService.Rooms.Count);
            Assert.AreEqual(roomName, dataService.Rooms[1].Name);
            
            string newRoomName = "New Room Name";
            int newFloor = 2;
            var newRoom = new Room(newRoomName, id, newFloor, building);
            administratorService.Update(newRoom, dataService.Rooms);

            //Assert
            Assert.AreEqual(2, dataService.Rooms.Count);
            Assert.AreEqual(newRoomName, dataService.Rooms[1].Name);
            Assert.AreEqual(newFloor, dataService.Rooms[1].Floor);
        }


        [TestMethod]
        public void Administrator_GetItemFromDatabaseShouldReturnUser()
        {
            //Arrange
            int userId = 111;
            string userName = "Test Name";
            AccessLevel level = AccessLevel.Employee;
            var myUser = new User(userId, userName, level);
            List<User> myUserList = new() { myUser };
            dataService.Save(myUser, dataService.Users);
            int adminId = 666;
            Administrator myAdmin = new(adminId, "AdministratorName", "AdministratorPassword");
            //Act
            User someUser = administratorService.GetItemFromDatabase(userId, dataService.Users);

            //Assert
            Assert.AreEqual(myUser.Name, someUser.Name);
            Assert.AreEqual(myUser.Id, someUser.Id);
            Assert.AreEqual(myUser.AccessLevel, someUser.AccessLevel);
        }

        [TestMethod]
        public void Administrator_GetItemFromDatabaseShouldThrowExceptionIfIdNotFound()
        {
            //Arrange
            int userId = 111;
            int idNotInDatabase = 42;
            string userName = "Test Name";
            AccessLevel level = AccessLevel.Employee;
            var myUser = new User(userId, userName, level);
            List<User> myUserList = new() { myUser };
            dataService.Save(myUser, dataService.Users);
            int adminId = 666;
            Administrator myAdmin = new(adminId, "AdministratorName", "AdministratorPassword");

            //Assert
            Assert.ThrowsException<Exception>(() => administratorService.GetItemFromDatabase(idNotInDatabase, dataService.Users));
        }
        [TestMethod]
        public void Administrator_CreateShouldSaveAdministrator()
        {
            // Arrange
            int id = 666;
            string username = "TestUserName";
            string password = "TestPassword";
            var myAdmin = new Administrator(id, username, password);
            // Act
            administratorService.Create(myAdmin, dataService.Administrators);
            string actualUsername = dataService.Administrators[0].Name;
            string actualPassword = dataService.Administrators[0].Password;
            string expectedUsername = username;
            string expectedPassword = password;
            // Assert
            Assert.AreEqual(expectedUsername, actualUsername);
            Assert.AreEqual(expectedPassword, actualPassword);

        }
    }
}