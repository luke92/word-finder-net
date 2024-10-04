namespace WordFinder.Tests;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using WordFinder.Logic;

[TestClass]
public class SimpleUnitTest
{
    [TestMethod]
    public void TestGetMessage()
    {
        // Act: Call the method from the Logic project
        var result = FakeClass.GetMessage();

        // Assert: Verify that the method returns the expected result
        Assert.AreEqual("Hello from the Business Logic!", result);
    }
}