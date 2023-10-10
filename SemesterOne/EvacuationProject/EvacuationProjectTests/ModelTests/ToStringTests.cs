using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EvacuationProject.BusinessLogic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EvacuationProject.Models;

namespace EvacuationProjectTests.ModelTests
{
    [TestClass]
    public class ToStringTests
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
            dataService.Save(myBuilding, dataService.Buildings);
            var myRoom = new Room(roomName, roomNumber, floor, myBuilding);
            dataService.Save(myRoom, dataService.Rooms);
            Workstation myWorkstation = new(workstationName, roomNumber, myRoom);
            dataService.Save(myWorkstation, dataService.Workstations);
        }
        [TestMethod]
        public void Administrator()
        {
            //Arrange
            Administrator myAdmin = new(123, "someName", "somePassword");
            string expectedText = "someName,123,somePassword";
            //Act
            string actualText = myAdmin.ToString();
            //Assert
            Assert.AreEqual(expectedText, actualText);
        }
        [TestMethod]
        public void Building()
        {
            //Arrange
            Building myBuilding = new("SomeName", 123);
            string expectedText = "SomeName,123";
            //Act
            string actualText = myBuilding.ToString();
            //Assert
            Assert.AreEqual(expectedText, actualText);
        }
        [TestMethod]
        public void Presence()
        {
            //Arrange
            DateTime myDateTime = DateTime.Now;
            Presence presence = new(dataService.Workstations[0], myDateTime);
            string expectedText = $"1,{myDateTime}";
            //Act
            string actualText = presence.ToString();
            //Assert
            Assert.AreEqual(expectedText, actualText);

        }
        [TestMethod]
        public void Room()
        {
            //Arrange
            DateTime myDateTime = DateTime.Now;
            Room room = new("RoomName", 123, 3, dataService.Buildings[0]);
            string expectedText = "RoomName,123,3,1";
            //Act
            string actualText = room.ToString();
            //Assert
            Assert.AreEqual(expectedText, actualText);
        }

        [TestMethod]
        public void Workstation()
        {
            //Arrange
            Workstation myWorkstation = new("workstationName 2", 5, dataService.Rooms[0]);
            string expectedText = "workstationName 2,5,1";
            //Act
            string actualText = myWorkstation.ToString();
            //Assert
            Assert.AreEqual(expectedText, actualText);
        }

        [TestMethod]
        public void User_WithPresence()
        {
            //Arrange
            User myUser = new(123, "SomeUser Name", AccessLevel.Employee);
            string expectedText = "SomeUser Name,123,Employee,null";
            //Act
            string actualText = myUser.ToString();
            //Assert
            Assert.AreEqual(expectedText, actualText);
        }
        [TestMethod]
        public void User_WithoutPresence()
        {
            //Arrange
            DateTime myDateTime = DateTime.Now;
            Presence presence = new(dataService.Workstations[0], myDateTime);
            User myUser = new(123, "SomeUser Name", AccessLevel.Employee, presence);
            string expectedText = $"SomeUser Name,123,Employee,1,{myDateTime}";
            //Act
            string actualText = myUser.ToString();
            //Assert
            Assert.AreEqual(expectedText, actualText);
        }

    }

}