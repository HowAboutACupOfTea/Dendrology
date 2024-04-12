using BalancedSearchTreesMadeSimple.Lib;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BalancedSearchTreesMadeSimple.Test;

public class Tests
{
    [Test]
    public void Test_Search_Tree_Constructor()
    {
        //Arrange
        SearchTree<int> searchTree = new SearchTree<int>();

        //Act
        var elements = searchTree.Traverse(OrderEnum.inOrder);
        var list = elements.ToList();

        //Assert
        Assert.IsNotNull(searchTree._rootNode);
        Assert.IsNotNull(searchTree._rootNode.leftNode);
        Assert.IsNotNull(searchTree._rootNode.rightNode);
    }

    [TestCase(-9999)]
    [TestCase(-1)]
    [TestCase(0)]
    [TestCase(1)]
    [TestCase(9999)]
    public void Insert_Value_One_Int(int expectedValue)
    {
        //Arrange
        SearchTree<int> searchTree = new();

        //Act
        searchTree.Insert(expectedValue);
        int actualValue = searchTree._rootNode.Key;

        //Assert
        Assert.AreEqual(expectedValue, actualValue);
    }

    [TestCase("")]
    [TestCase(" ")]
    [TestCase("testString")]
    public void Insert_Value_One_String(string expectedValue)
    {
        //Arrange
        SearchTree<string> searchTree = new();

        //Act
        searchTree.Insert(expectedValue);
        string actualValue = searchTree._rootNode.Key;

        //Assert
        Assert.AreEqual(expectedValue, actualValue);
    }

    [Test]
    public void Insert_Value_Null()
    {
        //Arrange
        SearchTree<string> searchTree = new();
        string expectedValue = null;

        //Act
        void testDelegate() { searchTree.Insert(expectedValue); }

        //Assert
        Assert.Throws<ArgumentNullException>(testDelegate);
    }

    [Test]
    public void Insert_Value_Integers()
    {
        //Arrange
        SearchTree<int> actualTree = new();
        List<int> insertedValues = new() { 4, 3, -42, 9, 0, 0, 5, 4, 10, 4 };

        //Act
        foreach (var number in insertedValues)
        {
            actualTree.Insert(number);
        }

        // good for traverse test
        var list = actualTree.Traverse(OrderEnum.inOrder).ToList();

        //Assert
        Assert.AreEqual(5, actualTree._rootNode.Key);
        Assert.AreEqual(3, actualTree._rootNode.leftNode.Key);
        Assert.AreEqual(4, actualTree._rootNode.leftNode.rightNode.Key);
        Assert.AreEqual(9, actualTree._rootNode.rightNode.Key);
        Assert.AreEqual(10, actualTree._rootNode.rightNode.rightNode.Key);
    }

    [Test]
    public void Clear_With_Value_Type()
    {
        //Arrange
        SearchTree<int> actualTree = new();
        List<int> insertedValues = new() { 3, 9, 5, 4 };

        //Act
        foreach (var value in insertedValues)
        {
            actualTree.Insert(value);
        }

        actualTree.Clear();

        //Assert
        Assert.AreEqual(0, actualTree._rootNode.Key);
    }

    [Test]
    public void Clear_With_Reference_Type()
    {
        //Arrange
        SearchTree<string> actualTree = new();
        List<string> insertedValues = new() { "test string one", "test string two" };

        //Act
        foreach (var value in insertedValues)
        {
            actualTree.Insert(value);
        }

        actualTree.Clear();

        //Assert
        Assert.AreEqual(null, actualTree._rootNode.Key);
    }

    [TestCase(new int[] { 2, 4, 6, 8, 16 }, ExpectedResult = 5)]
    [TestCase(new int[] { -1, 0, 1 }, ExpectedResult = 3)]
    [TestCase(new int[] { 10 }, ExpectedResult = 1)]
    [TestCase(new int[] { }, ExpectedResult = 0)]
    public int Count(int[] insertedValues)
    {
        //Arrange
        SearchTree<int> actualTree = new();

        //Act
        foreach (var number in insertedValues)
        {
            actualTree.Insert(number);
        }

        int actualCount = actualTree.Count();

        //Assert
        return actualCount;
    }

    [TestCase(new int[] { 4, 4, 4, 4 }, 4, ExpectedResult = 4)]
    [TestCase(new int[] { 2, 2 }, 2, ExpectedResult = 2)]
    [TestCase(new int[] { 1 }, 1, ExpectedResult = 1)]
    [TestCase(new int[] { 0, 1, 2 }, 99, ExpectedResult = 0)]
    public int Count_Value(int[] insertedValues, int countedValue)
    {
        //Arrange
        SearchTree<int> actualTree = new();

        //Act
        foreach (var number in insertedValues)
        {
            actualTree.Insert(number);
        }

        int actualCount = actualTree.Count(countedValue);

        //Assert
        return actualCount;
    }

    [TestCase(new int[] { -5, 0, 5 }, ExpectedResult = 5)]
    [TestCase(new int[] { 1, 2, 2 }, ExpectedResult = 2)]
    public int Maximum(int[] insertedValues)
    {
        //Arrange
        SearchTree<int> actualTree = new();

        //Act
        foreach (var number in insertedValues)
        {
            actualTree.Insert(number);
        }

        int actualMaximum = actualTree.Maximum();

        //Assert
        return actualMaximum;
    }

    [Test]
    public void Maximum_Without_Inserted_Values()
    {
        //Arrange
        SearchTree<int> actualTree = new();

        //Act
        void testDelegate() { actualTree.Maximum(); }

        //Assert
        Assert.Throws<InvalidOperationException>(testDelegate);
    }

    [TestCase(new int[] { -5, 0, 5 }, ExpectedResult = -5)]
    [TestCase(new int[] { 1, 1, 2 }, ExpectedResult = 1)]
    public int Minimum(int[] insertedValues)
    {
        //Arrange
        SearchTree<int> actualTree = new();

        //Act
        foreach (var number in insertedValues)
        {
            actualTree.Insert(number);
        }

        int actualMaximum = actualTree.Minimum();

        //Assert
        return actualMaximum;
    }

    [Test]
    public void Minimum_Without_Inserted_Values()
    {
        //Arrange
        SearchTree<int> actualTree = new();

        //Act
        void testDelegate() { actualTree.Minimum(); }

        //Assert
        Assert.Throws<InvalidOperationException>(testDelegate);
    }

    [TestCase(new int[] { 0, 1, 2}, 2, ExpectedResult = true)]
    [TestCase(new int[] { 0, 1, 2}, 3, ExpectedResult = false)]
    public bool Contains(int[] insertedValues, int searchedValue)
    {
        //Arrange
        SearchTree<int> actualTree = new();

        //Act
        foreach (var number in insertedValues)
        {
            actualTree.Insert(number);
        }

        bool doesContain = actualTree.Contains(searchedValue);

        //Assert
        return doesContain;
    }

    [Test]
    public void Contains_Without_Inserted_Values()
    {
        //Arrange
        SearchTree<int> actualTree = new();

        //Act
        void testDelegate() { actualTree.Contains(1); }

        //Assert
        Assert.Throws<InvalidOperationException>(testDelegate);
    }
}
