using EvacuationProject.Models;
using EvacuationProject.BusinessLogic;
using EvacuationProject.DataHandling;

namespace EvacuationProjectTests
{
    [TestClass]
    public class DatabaseReaderTests
    {
        DataService dataService = new();
        DataService newDataService= new();

        [TestInitialize]
        public void TestInitialize()
        {
            int userId = 123;
            string userName = "Test Name";
            AccessLevel level = AccessLevel.Employee;
            var myUser = new User(userId, userName, level);
            List<User> myUserList = new() { myUser };
            dataService.Save(myUser, dataService.Users);
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

            Administrator myAdmin = new(123, "Test Person", "LongPassword");
            dataService.Save(myAdmin, dataService.Administrators);
        }
        [TestMethod]
        public void ReadAdministrator_CorrectlyAddsAdministratorToDataService()
        {
            //Arrange
            Administrator expectedAdmin = dataService.Administrators[0];
            TextDataHandler handler = new(newDataService);
            //Act
            Administrator actualAdmin = handler.ReadAdministrator("Administrator Id:123,Name:Test Person,Password:LongPassword");
            //Assert
            Assert.AreEqual(expectedAdmin.Name, actualAdmin.Name);
            Assert.AreEqual(expectedAdmin.Id, actualAdmin.Id);
            Assert.AreEqual(expectedAdmin.Password, actualAdmin.Password);
        }

        [TestMethod]
        public void ReadAdministrator_CorrectlyAddsAdministratorFromFile()
        {
            //Arrange
            string pathToTxtFile = "Admin.txt";
            StreamWriter myWriter = new(pathToTxtFile);
            myWriter.WriteLine("Administrator Id:123,Name:Test Person,Password:LongPassword");
            myWriter.Close();
            Administrator expectedAdmin = dataService.Administrators[0];
            TextDataHandler handler = new(newDataService);
            //Act
            handler.ReadItemsIntoList("Admin.txt", newDataService.Administrators, handler.ReadAdministrator);
            Administrator actualAdmin = newDataService.Administrators[0];

            //Assert
            Assert.AreEqual(expectedAdmin.Name, actualAdmin.Name);
            Assert.AreEqual(expectedAdmin.Id, actualAdmin.Id);
            Assert.AreEqual(expectedAdmin.Password, actualAdmin.Password);
        }
        [TestMethod]
        public void WriteListToTextFile_CorrectlyWritesAdministratorToFile()
        {
            //Arrange
            string pathToTxtFile = "Admin.txt";
            List<Administrator> myAdmins = dataService.Administrators;
            TextDataHandler handler = new(dataService);
            string expectedTextString = dataService.Administrators[0].ToString();

            //Act
            handler.WriteListToTextFile(pathToTxtFile, myAdmins);
            StreamReader myReader = new(pathToTxtFile);
            string actualTextString = myReader.ReadLine();
            myReader.Close();

            //Assert
            Assert.AreEqual(expectedTextString, actualTextString);
        }
        [TestMethod]
        public void WriteListToTextFile_CorrectlyWritestwoAdministratorToFile()
        {
            //Arrange
            string pathToTxtFile = "Admin.txt";
            Administrator myAdmin = new(1111, "some new Username", "someRandomPassword");
            dataService.Save(myAdmin, dataService.Administrators);
            List<Administrator> myAdmins = dataService.Administrators;
            TextDataHandler handler = new(dataService);
            string expectedTextString1 = dataService.Administrators[0].ToString();
            string expectedTextString2 = dataService.Administrators[1].ToString();

            //Act
            handler.WriteListToTextFile(pathToTxtFile, myAdmins);
            StreamReader myReader = new(pathToTxtFile);
            string actualTextString1 = myReader.ReadLine();
            string actualTextString2 = myReader.ReadLine();
            myReader.Close();

            //Assert
            Assert.AreEqual(expectedTextString1, actualTextString1);
            Assert.AreEqual(expectedTextString2, actualTextString2);
        }
        [TestMethod]
        public void WriteListToTextFile_CorrectlyWritesWorkstationToFile()
        {
            //Arrange
            string pathToTxtFile = "Workstations.txt";
            TextDataHandler handler = new(dataService);
            string expectedTextString = dataService.Workstations[0].ToString();

            //Act
            handler.WriteListToTextFile(pathToTxtFile, dataService.Workstations);
            StreamReader myReader = new(pathToTxtFile);
            string actualTextString = myReader.ReadLine();
            myReader.Close();

            //Assert
            Assert.AreEqual(expectedTextString, actualTextString);
        }
        [TestMethod]
        public void WriteDatabaseToTextFilesCorrectlyWritesEntireDatabaseToFiles()
        {
            //Arrange
            TextDataHandler handler = new(dataService);
            handler.WriteDatabase();
            TextDataHandler newHandler = new(newDataService);
            //Act
            newHandler.ReadDatabase();
            //Assert
            Assert.AreEqual(dataService.Administrators[0].Name, newDataService.Administrators[0].Name);
            Assert.AreEqual(dataService.Administrators[0].Id, newDataService.Administrators[0].Id);
            Assert.AreEqual(dataService.Workstations[0].Name, newDataService.Workstations[0].Name);
            Assert.AreEqual(dataService.Workstations[0].Id, newDataService.Workstations[0].Id);
            Assert.AreEqual(dataService.Users[0].Id, newDataService.Users[0].Id);
            Assert.AreEqual(dataService.Users[0].Name, newDataService.Users[0].Name);
            Assert.AreEqual(dataService.Buildings[0].ToString(), newDataService.Buildings[0].ToString());
        }
        [TestMethod]
        public void WriteListToTextFile_CorrectlyWritesEachTypeOfListToFile()
        {
            //Arrange
            TextDataHandler handler = new(dataService);
            TextDataHandler newHandler = new(newDataService);
            //Act
            handler.WriteListToTextFile("AdministratorTestSave.txt", dataService.Administrators);
            handler.WriteListToTextFile("UserTestSave.txt", dataService.Users);
            handler.WriteListToTextFile("BuildingTestSave.txt", dataService.Buildings);
            handler.WriteListToTextFile("RoomTestSave.txt", dataService.Rooms);
            handler.WriteListToTextFile("WorkstationTestSave.txt", dataService.Workstations);
            newHandler.ReadItemsIntoList("AdministratorTestSave.txt", newDataService.Administrators, newHandler.ReadAdministrator);
            newHandler.ReadItemsIntoList("UserTestSave.txt", newDataService.Users, newHandler.ReadUser);
            newHandler.ReadItemsIntoList("BuildingTestSave.txt", newDataService.Buildings, newHandler.ReadBuilding);
            newHandler.ReadItemsIntoList("RoomTestSave.txt", newDataService.Rooms, newHandler.ReadRoom);
            newHandler.ReadItemsIntoList("WorkstationTestSave.txt", newDataService.Workstations, newHandler.ReadWorkstation);
            //Assert
            Assert.AreEqual(dataService.Administrators[0].Name, newDataService.Administrators[0].Name);
            Assert.AreEqual(dataService.Administrators[0].Password, newDataService.Administrators[0].Password);
            Assert.AreEqual(dataService.Administrators[0].Id, newDataService.Administrators[0].Id);
            Assert.AreEqual(dataService.Administrators[0].ToString(), newDataService.Administrators[0].ToString());

            Assert.AreEqual(dataService.Buildings[0].ToString(), newDataService.Buildings[0].ToString());
            Assert.AreEqual(dataService.Users[0].ToString(), newDataService.Users[0].ToString());
            Assert.AreEqual(dataService.Rooms[0].ToString(), newDataService.Rooms[0].ToString());
            Assert.AreEqual(dataService.Workstations[0].ToString(), newDataService.Workstations[0].ToString());
        }

    }
}