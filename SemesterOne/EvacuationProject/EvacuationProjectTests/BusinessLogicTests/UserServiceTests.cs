using EvacuationProject.BusinessLogic;
using EvacuationProject.Models;

namespace EvacuationProjectTests.BusinessLogicTests
{
    [TestClass]
    public class UserServiceTests
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
        public void UserService_CheckinShouldCheckinEmployeeAndReplaceOldEmployee()
        {
            //Arrange
            UserService userService = new(dataService);
            User myUser = dataService.Users[0];
            Workstation myWorkstation = dataService.Workstations[0];
            //Act
            userService.CheckIn(myUser, myWorkstation);
            List<User> ListOfUsersCheckedIn = dataService.GetUsersCurrentlyCheckedIn();
            User expectedCheckedinUser = myUser; 
            User actualCheckedinUser = ListOfUsersCheckedIn[0];
            //Assert
            Assert.AreEqual(expectedCheckedinUser.Name, actualCheckedinUser.Name);
            Assert.AreEqual(expectedCheckedinUser.Id, actualCheckedinUser.Id);
            Assert.IsTrue(actualCheckedinUser.Presence != null);
        }
        [TestMethod]
        public void UserService_CheckoutShouldCheckoutEmployee()
        {
            //Arrange
            UserService userService = new(dataService);
            User myUser = dataService.Users[0];
            Workstation myWorkstation = dataService.Workstations[0];
            //Act
            userService.CheckIn(myUser, myWorkstation);
            userService.CheckOut(myUser);
            List<User> ListOfUsersCheckedIn = dataService.GetUsersCurrentlyCheckedIn();

            int expectedLengthofList = 0;
            int actualLengthofList = dataService.GetUsersCurrentlyCheckedIn().Count;

            //Assert
            Assert.AreEqual(expectedLengthofList, actualLengthofList);
        }
        [TestMethod]
        public void UserService_CheckinShouldNotAddExtraEmployees()
        {
            //Arrange
            UserService userService = new(dataService);
            User myUser = dataService.Users[0];
            Workstation myWorkstation = dataService.Workstations[0];
            //Act
            int lengtOfListBeforeCheckin = dataService.GetUsersCurrentlyCheckedIn().Count;
            userService.CheckIn(myUser, myWorkstation);
            List<User> ListOfUsersCheckedIn = dataService.GetUsersCurrentlyCheckedIn();
            int lengtOfListAfterCheckin = dataService.GetUsersCurrentlyCheckedIn().Count;
            //Assert
            Assert.AreEqual(lengtOfListBeforeCheckin, lengtOfListBeforeCheckin);
        }
    }
}