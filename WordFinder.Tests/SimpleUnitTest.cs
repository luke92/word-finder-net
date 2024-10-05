namespace WordFinder.Tests;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using WordFinder.Logic;

[TestClass]
public class SimpleUnitTest
{
    [TestMethod]
    public void TestGetMessage()
    {
        var result = FakeClass.GetMessage();
        Assert.AreEqual("Hello from the Business Logic!", result);
    }
}