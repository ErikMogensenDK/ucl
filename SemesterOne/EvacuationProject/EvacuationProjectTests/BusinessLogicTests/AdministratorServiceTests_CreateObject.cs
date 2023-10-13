using EvacuationProject.BusinessLogic;
using EvacuationProject.Models;

namespace EvacuationProjectTests.BusinessLogicTests
{
    [TestClass]
    public class AdministratorServiceTests_CreateObject
    {
        DataService dataService = new();
        AdministratorService administratorService;

        [TestInitialize]
        public void Init()
        {
            int userId = 123;
            string userName = "Test Name";
            AccessLevel level = AccessLevel.Employee;
            var myUser = new User(userId, userName, level);
            dataService.Save(myUser, dataService.Users);

            string workstationName = "Test Workstation Name";
            string roomName = "TestRum 1";
            int roomNumber = 1;
            int floor = 0;
            var myBuilding = new Building("TestBuildingName", 0);
            dataService.Save(myBuilding, dataService.Buildings);
            var myRoom = new Room(roomName, roomNumber, floor, myBuilding);
            dataService.Save(myRoom, dataService.Rooms);
            Workstation myWorkstation = new(workstationName, roomNumber, myRoom);
            dataService.Save(myWorkstation, dataService.Workstations);
            administratorService = new(dataService);
        }

        [TestMethod]
        public void CreateObject_CreationOfUserShouldAddUser()
        {
            //Arrange
            User user = new(1234, "TestName");
            string objectString = user.ToString();

            //Act
            administratorService.CreateObject(objectString, user, dataService.Users);

            //Assert
            Assert.AreEqual(dataService.Users[1].Name, user.Name);
            Assert.AreEqual(dataService.Users[1].Id, user.Id);
        }
        [TestMethod]
        public void CreateObject_CreationOfWorkstationShouldAddWorkstation()
        {
            //Arrange
            Workstation workstation = new("MyWorkstation number 1", 5, dataService.Rooms[0]);
            string objectString = workstation.ToString();

            //Act
            administratorService.CreateObject(objectString, workstation, dataService.Workstations);

            //Assert
            Assert.AreEqual(dataService.Workstations[1].Name, workstation.Name);
            Assert.AreEqual(dataService.Workstations[1].Id, workstation.Id);
        }

        [TestMethod]
        public void CreateObject_CreationOfBuildingShouldAddBuilding()
        {
            //Arrange
            Building building = new("Name of test building");
            string objectString = building.ToString();

            //Act
            administratorService.CreateObject(objectString, building, dataService.Buildings);

            //Assert
            Assert.AreEqual(dataService.Buildings[1].Name, building.Name);
        }
        [TestMethod]
        public void CreateObject_CreationOfAdministratorShouldAddAdministrator()
        {
            //Arrange
            Administrator administrator = new(91988,"Name of test administrator", "PasswordForTesting123!");
            string objectString = administrator.ToString();

            //Act
            administratorService.CreateObject(objectString, administrator, dataService.Administrators);

            //Assert
            Assert.AreEqual(dataService.Administrators[0].Name, administrator.Name);
            Assert.AreEqual(dataService.Administrators[0].Id, administrator.Id);
            Assert.AreEqual(dataService.Administrators[0].Password, administrator.Password);
        }
        [TestMethod]
        public void CreateObject_CreationOfRoomShouldAddRoom()
        {
            //Arrange
            Room room = new("Name of my room", 123, 2, dataService.Buildings[0]);
            string objectString = room.ToString();

            //Act
            administratorService.CreateObject(objectString, room, dataService.Rooms);

            //Assert
            Assert.AreEqual(dataService.Rooms[1].Name, room.Name);
            Assert.AreEqual(dataService.Rooms[1].Id, room.Id);
            Assert.AreEqual(dataService.Rooms[1].Floor, room.Floor);
            Assert.AreEqual(dataService.Rooms[1].Building.Name, room.Building.Name);
        }
    }
}