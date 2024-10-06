using System;
using WordFinderApp.Logic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace WordFinderApp.Tests
{
    [TestClass]
    public class WordFinderTests
    {
        private IWordFinder _service;

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException),"Matrix cannot be null")]
        public void TestMatrixNull()
        {
            IEnumerable<string> matrix = null;
            var _service = new WordFinder(matrix);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException),"First word cannot be empty")]
        public void TestMatrixEmptyWords()
        {
            IEnumerable<string> matrix = new List<string> { "", "banana", "cherry" };
            var _service = new WordFinder(matrix);
        }
    }
}
