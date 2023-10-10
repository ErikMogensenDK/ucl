using BankAccountAndSorting;
namespace BankAccountAndSortingTests;

[TestClass]
public class UnitTest1
{
    [TestMethod]
    public void ArraySortAscending1()
    {
        // #### ARRANGE ####
        IntArrayHelper arrayHelper = new IntArrayHelper();
        int[] thisIntArray = { 3, 2, 4, 1 };

        // #### ACT ####
        arrayHelper.SortAscending(thisIntArray);

        // #### ASSERT ####
        CollectionAssert.AreEqual(new int[] { 1, 2, 3, 4 }, thisIntArray);
    }

    [TestMethod]
    public void ArraySortAscending2()
    {
        // #### ARRANGE ####
        IntArrayHelper arrayHelper = new IntArrayHelper();
        int[] thisIntArray = { 3, 2, 4, 1, 17, 3 };

        // #### ACT ####
        arrayHelper.SortAscending(thisIntArray);

        // #### ASSERT ####
        CollectionAssert.AreEqual(new int[] { 1, 2, 3, 3, 4, 17 }, thisIntArray);
    }

    [TestMethod]
    public void ArraySortAscending3()
    {
        // #### ARRANGE ####
        IntArrayHelper arrayHelper = new IntArrayHelper();
        int[] thisIntArray = { 5, 2, 1, 5, 3, 4, 3 };

        // #### ACT ####
        arrayHelper.SortAscending(thisIntArray);

        // #### ASSERT ####
        CollectionAssert.AreEqual(new int[] { 1, 2, 3, 3, 4, 5, 5 }, thisIntArray);
    }

    [TestMethod]
    public void ArraySortAscending4()
    {
        // #### ARRANGE ####
        IntArrayHelper arrayHelper = new IntArrayHelper();
        int[] thisIntArray = { 1, 1, 3, 1, 2, 1, 3, 3, 3, 3, 1, 3, 4, 5, 4, 3, 7, 5, 4, 9, 1, 2 };

        // #### ACT ####
        arrayHelper.SortAscending(thisIntArray);

        // #### ASSERT ####
        CollectionAssert.AreEqual(new int[] { 1, 1, 1, 1, 1, 1, 2, 2, 3, 3, 3, 3, 3, 3, 3, 4, 4, 4, 5, 5, 7, 9 }, thisIntArray);
    }

    [TestMethod]
    public void ArraySortAscendingAndReverse1()
    {
        // #### ARRANGE ####
        IntArrayHelper arrayHelper = new IntArrayHelper();
        int[] thisIntArray = { 3, 2, 4, 1 };

        // #### ACT ####
        arrayHelper.SortAscendingAndReverse(thisIntArray);

        // #### ASSERT ####
        CollectionAssert.AreEqual(new int[] { 4, 3, 2, 1 }, thisIntArray);
    }

    [TestMethod]
    public void ArraySortAscendingAndReverse2()
    {
        // #### ARRANGE ####
        IntArrayHelper arrayHelper = new IntArrayHelper();
        int[] thisIntArray = { 3, 2, 4, 1, 17, 3 };

        // #### ACT ####
        arrayHelper.SortAscendingAndReverse(thisIntArray);

        // #### ASSERT ####
        CollectionAssert.AreEqual(new int[] { 17, 4, 3, 3, 2, 1 }, thisIntArray);
    }

    [TestMethod]
    public void ArraySortAscendingAndReverse3()
    {
        // #### ARRANGE ####
        IntArrayHelper arrayHelper = new IntArrayHelper();
        int[] thisIntArray = { 5, 2, 1, 5, 3, 4, 3 };

        // #### ACT ####
        arrayHelper.SortAscendingAndReverse(thisIntArray);

        // #### ASSERT ####
        CollectionAssert.AreEqual(new int[] { 5, 5, 4, 3, 3, 2, 1 }, thisIntArray);
    }

    [TestMethod]
    public void ArraySortAscendingAndReverse4()
    {
        // #### ARRANGE ####
        IntArrayHelper arrayHelper = new IntArrayHelper();
        int[] thisIntArray = { 11, 1, 3, 1, 2, 1, 3, 3, 3, 3, 1, 3, 4, 5, 4, 3, 7, 5, 4, 9, 1, 2 };

        // #### ACT ####
        arrayHelper.SortAscendingAndReverse(thisIntArray);

        // #### ASSERT ####
        CollectionAssert.AreEqual(new int[] { 11, 9, 7, 5, 5, 4, 4, 4, 3, 3, 3, 3, 3, 3, 3, 2, 2, 1, 1, 1, 1, 1 }, thisIntArray);
    }

}

