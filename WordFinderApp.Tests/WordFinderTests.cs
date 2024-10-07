using System;
using WordFinderApp.Logic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace WordFinderApp.Tests
{
    [TestClass]
    public class WordFinderTests
    {
        private IWordFinder? _service;
        private readonly string _longString = "rdyayfvpcybanlryqpvlpmdjfscgwedehcststiirskbszhfufqdnmgmkieowwvga";
        private void InitializeService(IEnumerable<string>? matrix)
        {
            _service = new WordFinder(matrix);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException), "Matrix cannot be null")]
        public void TestMatrixNull()
        {
            InitializeService(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException), "Row cannot be null")]
        public void TestMatrixFirstRowNullWord()
        {
            InitializeService(new List<string> { null });
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "First row cannot be empty")]
        public void TestMatrixFirstRowEmptyWord()
        {
            InitializeService(new List<string> { "" });
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "The matrix cannot have more than 64 columns")]
        public void TestMatrixFirstRowWithMoreColumnsThanAllowed()
        {
            InitializeService(new List<string> { _longString });
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException), "The matrix cannot have more than 64 rows")]
        public void TestMatrixWithMoreRowsThanAllowed()
        {
            var list = new List<string>();
            foreach (var character in _longString)
            {
                list.Add(character.ToString());
            }
            InitializeService(list);
            _service?.Find(new List<string>{ "ab" });
        }

        [TestMethod]
        public void TestFindWithNullWordStream()
        {
            InitializeService(new List<string> { "apples", "banana", "cherry" });
            var foundWords = _service?.Find(null);
            Assert.AreEqual(0, foundWords?.Count());
        }

        [TestMethod]
        public void TestFindWithEmptyWordsInWordStream()
        {
            InitializeService(new List<string> { "apples", "banana", "cherry" });
            var foundWords = _service?.Find(new List<string> { "", null });
            Assert.AreEqual(0, foundWords?.Count());
        }

        [TestMethod]
        public void TestFindWithTinyExample()
        {
            InitializeService(new List<string>
            {
                "abcdc",
                "fgwio",
                "chill",
                "pqnsd",
                "uvdxy"
            });
            var foundWords = _service?.Find(new List<string>
            {
                "cold",
                "wind",
                "snow",
                "chill"
            });

            IEnumerable<string> expected = new[]
            {
                "chill",
                "wind",
                "cold"
            };
            Assert.IsTrue(foundWords?.SequenceEqual(expected));
        }

        [TestMethod]
        public void TestFindWithTwoWordStreams()
        {
            InitializeService(new List<string>
            {
                "abcdc",
                "fgwio",
                "chill",
                "pqnsd",
                "uvdxy"
            });
            var foundWords = _service?.Find(new List<string>
            {
                "cold",
                "wind",
            });

            IEnumerable<string> expected = new[]
            {
                "wind",
                "cold"
            };
            Assert.IsTrue(foundWords?.SequenceEqual(expected));

            var foundWords2 = _service?.Find(new List<string>
            {
                "snow",
                "chill"
            });
            IEnumerable<string> expected2 = new[]
            {
                "chill",
            };
            Assert.IsTrue(foundWords2?.SequenceEqual(expected2));
        }

        [TestMethod]
        public void TestFindWithWordRepeatedAnd10MostRepeatedWords()
        {
            InitializeService(new List<string>
            {
                "xsmegpveifuiksumptkq",
                "bponnsjaypearbyqvaer",
                "kslhckezqnujzqsxsamy",
                "axlwcmgteuaopywevdwe",
                "xribgorlvapcvcmtiabk",
                "jeonofafimejidgjapji",
                "fbgigvpelpaachfwpeiw",
                "hrnhubenenccxweepaui",
                "orangemfmdhcehsnlcrj",
                "bmcbruqtovhhagnpeach",
                "arhbkkfznsmaqkyjlsgc",
                "nmezxuffiowmymvfkrru",
                "alrzoemjpgypiquewbnn",
                "nrrhzdmelonezbbuvjud",
                "ajyisiwmrproilpkirhh"
            });
            var foundWords = _service?.Find(new List<string>
            {
                "apple",
                "orange",
                "banana",
                "grape",
                "peach",
                "cherry",
                "melon",
                "kiwi",
                "pear",
                "lemon",
                "jpg",
                "ajyisiwmrproilpkirhhh"
            });

            IEnumerable<string> expected = new[]
            {
                "peach",
                "pear",
                "orange",
                "jpg",
                "melon",
                "banana",
                "cherry",
                "grape",
                "lemon",
                "apple",
            };

            Assert.IsTrue(foundWords?.Count() == 10);
            Assert.IsTrue(foundWords.SequenceEqual(expected));
        }
    }
}
