using System.ComponentModel.Design;
using StaticApp.Models;

namespace StaticAppTests;

[TestClass]
public class UnitTest1
{
    [TestMethod]
    public void EmployeeCheckIn_EmployeeShouldBeCheckedIn()
    {
        //Arrange
        int testEmployeeId = 1234;
        Employee myEmployee = new(testEmployeeId, "Myname");
        CheckIn myCheckin = new(myEmployee.UserId, workstation);
        List<CheckIn> expectedCheckIns = new() {};
        //Act

        //Assert
        Assert.AreEqual(dataService.GetCheckins, expectedCheckins)
    }
}