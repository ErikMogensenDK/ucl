using System.Runtime.InteropServices;
using DraftApp;
using DraftApp.Models;
namespace DraftAppTests;

[TestClass]
public class EmployeeServiceTests
{
    [TestMethod]
    public void WorkstationRegistrationAddsRegistration()
    {
        // Arrange
        string employeeId = "erik";
        string expectedWorkstation = "home";

        Employee testEmployee = new Employee(employeeId);
        List<Employee> employeeList = new List<Employee>();
        employeeList.Add(testEmployee);

        Workstation testWorkstation = new Workstation(expectedWorkstation);
        List<Workstation> workstationList = new List<Workstation>();
        workstationList.Add(testWorkstation);

        List<Checkin> checkinList = new List<Checkin>(); 

        DataService dataService = new(employeeList, workstationList, checkinList);
        EmployeeService employeeService = new(dataService);
        DateTime testStartTime = new();

        // Act
        employeeService.CheckinAtWorkstation(employeeId, expectedWorkstation, testStartTime);

        // Assert
        List<Checkin> Checkins = dataService.GetEmployeesCurrentlyCheckedIn();
        Assert.AreEqual(1, Checkins.Count);

        string actualWorkstation = Checkins[0].WorkstationId;

        Assert.AreEqual(expectedWorkstation, actualWorkstation);
    }
    [TestMethod]
    public void WorkstationRegistrationErrorsOnInvalidEmployee()
    {

        // Arrange
        string employeeId = "nonExistingEmployee";
        string testWorkstationId = "home";

        List<Employee> employeeList = new List<Employee>();

        Workstation testWorkstation = new Workstation(testWorkstationId);
        List<Workstation> workstationList = new List<Workstation>();
        workstationList.Add(testWorkstation);

        List<Checkin> checkinList = new List<Checkin>(); 

        DataService dataService = new(employeeList, workstationList, checkinList);
        EmployeeService employeeService = new(dataService);
        DateTime testStartTime = new();

        // Act
        // Assert
        Assert.ThrowsException<Exception>( () => employeeService.CheckinAtWorkstation(employeeId, testWorkstationId, testStartTime));
    }

    [TestMethod]
    public void WorkStationRegistrationAddsStartTime()
    {
        //Arrange
        DateTime ExpectedStartTime = new DateTime(2019,05,09,9,15,0);
        string employeeId = "erik";
        string expectedWorkstation = "home";

        Employee testEmployee = new Employee(employeeId);
        List<Employee> employeeList = new List<Employee>();
        employeeList.Add(testEmployee);

        Workstation testWorkstation = new Workstation(expectedWorkstation);
        List<Workstation> workstationList = new List<Workstation>();
        workstationList.Add(testWorkstation);

        List<Checkin> checkinList = new List<Checkin>(); 

        DataService dataService = new(employeeList, workstationList, checkinList);
        EmployeeService employeeService = new(dataService);

        // Act
        employeeService.CheckinAtWorkstation(employeeId, expectedWorkstation, ExpectedStartTime);
        List<Checkin> myCheckinList = dataService.GetEmployeesCurrentlyCheckedIn();
        DateTime ActualStartTime = myCheckinList[0].StartTime;

        //Assert
        Assert.AreEqual(ExpectedStartTime, ActualStartTime);
    }

    [TestMethod]
    public void WorkStationSignoutAddsEndTimeToCheckin()
    {
        //Arrange
        DateTime expectedEndTime = new DateTime(2019,05,09,9,15,0);
        DateTime expectedEndTime2 = new DateTime(2020,05,09,9,15,0);
        DateTime startTime = new();
        string employeeId = "erik";
        string expectedWorkstation = "home";

        Employee testEmployee = new Employee(employeeId);
        List<Employee> employeeList = new List<Employee>();
        employeeList.Add(testEmployee);

        Workstation testWorkstation = new Workstation(expectedWorkstation);
        List<Workstation> workstationList = new List<Workstation>();
        workstationList.Add(testWorkstation);

        List<Checkin> checkinList = new List<Checkin>(); 

        DataService dataService = new(employeeList, workstationList, checkinList);
        EmployeeService employeeService = new(dataService);

        // Act
        employeeService.CheckinAtWorkstation(employeeId, expectedWorkstation, startTime);
        List<Checkin> myCheckinList = dataService.GetCheckins(employeeId);
        myCheckinList[0].SetEndTime(expectedEndTime);
        DateTime? ActualEndTime = myCheckinList[0].GetEndTime();

        // Adding second checkin
        employeeService.CheckinAtWorkstation(employeeId, expectedWorkstation, startTime);
        myCheckinList = dataService.GetCheckins(employeeId);
        myCheckinList[myCheckinList.Count-1].SetEndTime(expectedEndTime2);
        DateTime? ActualEndTime2 = myCheckinList[1].GetEndTime();

        //Assert
        Assert.AreEqual(expectedEndTime, ActualEndTime);
        Assert.AreEqual(expectedEndTime2, ActualEndTime2);
    }
    [TestMethod]
    public void GetAllSignedInEmployeesReturnsAll()
    {
        //Arrange
        //Create list of 3 checkins
        List<Employee> employeeList = new();
        string testEmployeeId1 = "Test1";
        string testEmployeeId2 = "Test2";
        string testEmployeeId3 = "Test3";

        Employee testEmployee1 = new(testEmployeeId1);
        Employee testEmployee2 = new(testEmployeeId2);
        Employee testEmployee3 = new(testEmployeeId3);

        employeeList.Add(testEmployee1);
        employeeList.Add(testEmployee2);
        employeeList.Add(testEmployee3);

        List<Workstation> workstationList = new();
        string testWorkstationId1 = "TestWorkstation1";
        string testWorkstationId2 = "TestWorkstation2";

        Workstation workstation1 = new(testWorkstationId1);
        Workstation workstation2 = new(testWorkstationId2);
        workstationList.Add(workstation1);
        workstationList.Add(workstation2);

        DateTime testStartTime = new();

        List<Checkin> emptyCheckinList = new();
        
        DataService dataService = new(employeeList, workstationList, emptyCheckinList);
        EmployeeService employeeService = new(dataService);

        List<Checkin> expectedListOfCheckins = new();
        Checkin testCheckin1 = new(testEmployeeId1, testWorkstationId1, testStartTime);
        Checkin testCheckin2 = new(testEmployeeId2, testWorkstationId2, testStartTime);
        Checkin testCheckin3 = new(testEmployeeId3, testWorkstationId2, testStartTime);
        expectedListOfCheckins.Add(testCheckin1);
        expectedListOfCheckins.Add(testCheckin2);
        expectedListOfCheckins.Add(testCheckin3);

        //Act
        employeeService.CheckinAtWorkstation(testEmployeeId1, testWorkstationId1, testStartTime);
        employeeService.CheckinAtWorkstation(testEmployeeId2, testWorkstationId2, testStartTime);
        employeeService.CheckinAtWorkstation(testEmployeeId3, testWorkstationId2, testStartTime);

        List<Checkin> actualListOfCheckins = dataService.GetEmployeesCurrentlyCheckedIn();

        //Assert
        for (int i = 0; i < expectedListOfCheckins.Count; i++)
        {
            Assert.AreEqual(expectedListOfCheckins[i].EmployeeId, actualListOfCheckins[i].EmployeeId);
            Assert.AreEqual(expectedListOfCheckins[i].WorkstationId, actualListOfCheckins[i].WorkstationId);
            Assert.AreEqual(expectedListOfCheckins[i].StartTime, actualListOfCheckins[i].StartTime);
        }
    }
    [TestMethod]
    public void GetAllSignedInEmployeesReturnsOnlyThoseWithoutCheckout()
    {
        //Arrange
        //Create list of 3 checkins
        List<Employee> employeeList = new();
        string testEmployeeId1 = "Test1";
        string testEmployeeId2 = "Test2";
        string testEmployeeId3 = "Test3";

        Employee testEmployee1 = new(testEmployeeId1);
        Employee testEmployee2 = new(testEmployeeId2);
        Employee testEmployee3 = new(testEmployeeId3);

        employeeList.Add(testEmployee1);
        employeeList.Add(testEmployee2);
        employeeList.Add(testEmployee3);

        List<Workstation> workstationList = new();
        string testWorkstationId1 = "TestWorkstation1";
        string testWorkstationId2 = "TestWorkstation2";

        Workstation workstation1 = new(testWorkstationId1);
        Workstation workstation2 = new(testWorkstationId2);
        workstationList.Add(workstation1);
        workstationList.Add(workstation2);

        DateTime testStartTime = new();

        List<Checkin> emptyCheckinList = new();
        
        DataService dataService = new(employeeList, workstationList, emptyCheckinList);
        EmployeeService employeeService = new(dataService);

        List<Checkin> expectedListOfCheckins = new();
        Checkin testCheckin1 = new(testEmployeeId1, testWorkstationId1, testStartTime);
        Checkin testCheckin2 = new(testEmployeeId2, testWorkstationId2, testStartTime);

        expectedListOfCheckins.Add(testCheckin1);
        expectedListOfCheckins.Add(testCheckin2);


        //Act
        employeeService.CheckinAtWorkstation(testEmployeeId1, testWorkstationId1, testStartTime);
        employeeService.CheckinAtWorkstation(testEmployeeId2, testWorkstationId2, testStartTime);
        employeeService.CheckinAtWorkstation(testEmployeeId3, testWorkstationId2, testStartTime);

        employeeService.Checkout(testEmployeeId3);

        List<Checkin> actualListOfCheckins = dataService.GetEmployeesCurrentlyCheckedIn();

        //Assert
        for (int i = 0; i < expectedListOfCheckins.Count; i++)
        {
            Assert.AreEqual(expectedListOfCheckins[i].EmployeeId, actualListOfCheckins[i].EmployeeId);
            Assert.AreEqual(expectedListOfCheckins[i].WorkstationId, actualListOfCheckins[i].WorkstationId);
            Assert.AreEqual(expectedListOfCheckins[i].StartTime, actualListOfCheckins[i].StartTime);
        }
    }


    // make test of whether new checkin automatically checks out previous ones
    [TestMethod]
    public void SignInShouldSignOutFirst()
    {
        //Arrange
        List<Employee> employeeList = new();
        string testEmployeeId1 = "Test1";
        string testEmployeeId2 = "Test2";
        string testEmployeeId3 = "Test3";

        Employee testEmployee1 = new(testEmployeeId1);
        Employee testEmployee2 = new(testEmployeeId2);
        Employee testEmployee3 = new(testEmployeeId3);

        employeeList.Add(testEmployee1);
        employeeList.Add(testEmployee2);
        employeeList.Add(testEmployee3);

        List<Workstation> workstationList = new();
        string testWorkstationId1 = "TestWorkstation1";
        string testWorkstationId2 = "TestWorkstation2";

        Workstation workstation1 = new(testWorkstationId1);
        Workstation workstation2 = new(testWorkstationId2);
        workstationList.Add(workstation1);
        workstationList.Add(workstation2);

        DateTime testStartTime = new();

        List<Checkin> emptyCheckinList = new();
        
        DataService dataService = new(employeeList, workstationList, emptyCheckinList);
        EmployeeService employeeService = new(dataService);

        List<Checkin> expectedListOfCheckins = new();
        Checkin testCheckin1 = new(testEmployeeId1, testWorkstationId1, testStartTime);
        Checkin testCheckin2 = new(testEmployeeId2, testWorkstationId2, testStartTime);
        Checkin testCheckin3 = new(testEmployeeId3, testWorkstationId1, testStartTime);

        //Checkin testCheckin3 = new(testEmployeeId3, testWorkstationId2, testStartTime);

        expectedListOfCheckins.Add(testCheckin1);
        expectedListOfCheckins.Add(testCheckin2);
        expectedListOfCheckins.Add(testCheckin3);


        //Act
        employeeService.CheckinAtWorkstation(testEmployeeId1, testWorkstationId1, testStartTime);
        employeeService.CheckinAtWorkstation(testEmployeeId1, testWorkstationId2, testStartTime);
        employeeService.CheckinAtWorkstation(testEmployeeId1, testWorkstationId1, testStartTime);
        employeeService.CheckinAtWorkstation(testEmployeeId2, testWorkstationId1, testStartTime);
        employeeService.CheckinAtWorkstation(testEmployeeId2, testWorkstationId2, testStartTime);
        employeeService.CheckinAtWorkstation(testEmployeeId3, testWorkstationId1, testStartTime);
        employeeService.CheckinAtWorkstation(testEmployeeId3, testWorkstationId1, testStartTime);

        List<Checkin> actualListOfCheckins = dataService.GetEmployeesCurrentlyCheckedIn();

        //Assert
        for (int i = 0; i < expectedListOfCheckins.Count; i++)
        {
            Console.WriteLine("I was: " + i);
            Assert.AreEqual(expectedListOfCheckins[i].EmployeeId, actualListOfCheckins[i].EmployeeId);
            Assert.AreEqual(expectedListOfCheckins[i].WorkstationId, actualListOfCheckins[i].WorkstationId);
            Assert.AreEqual(expectedListOfCheckins[i].StartTime, actualListOfCheckins[i].StartTime);
        }
    }

}