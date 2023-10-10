using EvacuationProject.Models;
using EvacuationProject.BusinessLogic;

namespace EvacuationProjectTests
{
    [TestClass]
    public class PresenceIntegrationTests
    {
        DataService dataService = new();
        [TestInitialize]
        public void TestInitialize()
        {
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
            var myBuilding = new Building("TestBuildingName", 1);
            var myRoom = new Room(roomName, roomNumber, floor, myBuilding);
            Workstation myWorkstation = new(workstationName, roomNumber, myRoom);
            dataService.Save(myWorkstation, dataService.Workstations);
        }
        [TestMethod]
        public void User_CheckinShouldAddPresence()
        {
            //Arrange
            User myUser = dataService.Users[0];
            Workstation expectedWorkstation = dataService.Workstations[0];
            Presence expectedPresence = new(expectedWorkstation, DateTime.Now);
            string expectedTime = DateTime.Now.ToShortTimeString();
            string expectedDate = DateTime.Now.ToShortDateString();
            UserService userService = new(dataService);

            //Act
            userService.CheckIn(myUser, dataService.Workstations[0]);

            // Assert
            Assert.AreEqual(expectedWorkstation, myUser.Presence.Workstation);
            Assert.AreEqual(expectedTime, myUser.Presence.StartTime.ToShortTimeString());
            Assert.AreEqual(expectedDate, myUser.Presence.StartTime.ToShortDateString());
        }
        [TestMethod]
        public void User_CheckOutShouldRemovePresence()
        {
            //Arrange
            User myUser = dataService.Users[0];
            Workstation expectedWorkstation = dataService.Workstations[0];
            UserService userService = new(dataService);

            //Act
            userService.CheckIn(myUser, dataService.Workstations[0]);
            userService.CheckOut(myUser);

            // Assert
            Assert.AreEqual(null, myUser.Presence);
        }
    }
}