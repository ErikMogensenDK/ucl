using JumpTest;
namespace JumpTestTests;

[TestClass]
public class UnitTest1
{
    [TestMethod]
    public void TestMethod1()
    {
        Assert.AreEqual("12,12", JumpHelper.CalculateMeetingPoint(0, 3, 4, 2));
    }
    [TestMethod]
    public void TestMethod2()
    {
        Assert.AreEqual("NO", JumpHelper.CalculateMeetingPoint(0, 2, 5, 3));
    }
    [TestMethod]
    public void TestMethod3()
    {
        Assert.AreEqual("182,182", JumpHelper.CalculateMeetingPoint(14, 4, 98, 2));
    }
    [TestMethod]
    public void TestMethod4()
    {
        Assert.AreEqual("NO", JumpHelper.CalculateMeetingPoint(21, 6, 47, 3));
    }
    [TestMethod]
    public void TestMethod5()
    {
        Assert.AreEqual("198,198", JumpHelper.CalculateMeetingPoint(42, 3, 94, 2));
    }
    [TestMethod]
    public void TestMethod6()
    {
        Assert.AreEqual("NO", JumpHelper.CalculateMeetingPoint(6644, 5868, 8349, 3477));
    }

}