using System;

namespace WordFinder.Logic
{
    public class WordFinder : IWordFinder
    {
        private readonly IEnumerable<string> _matrix;
        public WordFinder(IEnumerable<string> matrix) {
            _matrix = matrix;
        }

        public IEnumerable<string> Find(IEnumerable<string> wordstream)
        {
            throw new NotImplementedException();
        }
    }
}
