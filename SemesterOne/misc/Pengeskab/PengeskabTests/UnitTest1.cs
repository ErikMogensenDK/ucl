namespace PengeskabTests;
using Pengeskab;

[TestClass]
public class UnitTest1
{
    StateHelper myStateHelper; 
    [TestInitialize]
    public void Init()
    {
        myStateHelper = new();
    }
    [TestMethod]
    public void StateHelper_CloseShouldCloseOpenSafe()
    {
        //Arrange
        State myState = State.Open;
        State expectedState = State.Closed;
        //Act
        State actualState = myStateHelper.Close(myState);
        //Assert
        Assert.AreEqual(expectedState, actualState);
    }
    [TestMethod]
    public void StateHelper_OpenShouldNotOpenLockedSafe()
    {
        //Arrange
        State myState = State.Open;
        State expectedState = State.Locked;
        //Act
        State actualState = myStateHelper.Open(myState);
        //Assert
        Assert.AreNotEqual(expectedState, actualState);
    }
}