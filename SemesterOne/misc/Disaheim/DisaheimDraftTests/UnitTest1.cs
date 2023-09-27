namespace DisaheimDraftTests;
using DisaheimDraft.Models;
using DisaheimDraft;

[TestClass]
public class UnitTest1
{
    [TestMethod]
    public void GetAllSalesWithinTimePeriod()
    {
        //Arrange
        DataService _dataService = new();
        Product myProduct1 = new(100, "Ulvetid", "Bog");
        Product myProduct2 = new(200, "Lækker halskæde altså", "Smykke");
        List<Product> ListOfProducts = new() {myProduct1, myProduct2};

        Transaction mySale1 = new(100, DateTime.Today, myProduct1, true);
        Transaction mySale2 = new(250, DateTime.Today, myProduct2, true);
        Transaction mySale3 = new(1000, DateTime.Today, myProduct2, true);
        List <Transaction> expectedSales = new(){mySale1, mySale2, mySale3};

        //Act
        List <Transaction> actualSales = _dataService.GetAllSalesWithinTimePeriod();
        
        //Assert
        for (int i = 0; i < expectedSales.Count; i++)
        {
            Assert.AreEqual(actualSales[i].Product.Name, expectedSales[i].Product.Name);
        }
    }
    //GetAllPurchasesWithinTimePeriod
    //GetProductInfo
    //GetProductsCurrentlyInStock
}