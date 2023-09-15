using System.Diagnostics;
using Calculator;

namespace CalculatorTests;

[TestClass]
public class UnitTest1
{
    Calculator myCalc = new();
    [TestMethod]
    public void Add_SimpleIntsShouldAdd()
    {
        // Given
        int num1 = 5;
        int num2 = 10;
        int expected = 15;
    
        // When
        int actual = myCalc.Add(num1, num2);
    
        // Then
        Assert.AreEqual(expected, actual);
        
    }
    [TestMethod]
    public void Subtract_SimpleIntsShouldSubtract()
    {
        // Given
        int num1 = 20;
        int num2 = 10;
        int expected = 10;
    
        // When
        int actual = myCalc.Subtract(num1, num2);
    
        // Then
        Assert.AreEqual(expected, actual);
    }
    [TestMethod]
    public void Mulitply_SimpleIntsShouldMultiply()
    {
        // Given
        int num1 = 20;
        int num2 = 10;
        int expected = 200;
    
        // When
        int actual = myCalc.Multiply(num1, num2);
    
        // Then
        Assert.AreEqual(expected, actual);
    }
    [TestMethod]
    public void Divide_SimpleIntsShouldDivide()
    {
        // Given
        int num1 = 20;
        int num2 = 10;
        double expected = 2;
    
        // When
        double actual = myCalc.Divide(num1, num2);
    
        // Then
        Assert.AreEqual(expected, actual);
    }
    [TestMethod]
    public void Divide_CorrectlyCastsToDouble()
    {
        // Given
        int num1 = 15;
        int num2 = 10;
        double expected = 1.5;
    
        // When
        double actual = myCalc.Divide(num1, num2);
    
        // Then
        Assert.AreEqual(expected, actual);
    }
}