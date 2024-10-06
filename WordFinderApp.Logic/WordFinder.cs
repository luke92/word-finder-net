using System;
using System.ComponentModel;

namespace WordFinderApp.Logic
{
    public class WordFinder : IWordFinder
    {
        private int _columns = 0;
        public WordFinder(IEnumerable<string> matrix)
        {
            if (matrix == null) {
                throw new ArgumentNullException("Matrix cannot be null");
            }
            
            foreach (string row in matrix)
            {
                if (_columns == 0)
                {
                    _columns = row.Length;
                    break;
                }
            }

            if (_columns == 0) {
                throw new ArgumentException("First word cannot be empty");
            }
        }

        public IEnumerable<string> Find(IEnumerable<string> wordstream)
        {
            throw new NotImplementedException();
        }
    }
}
