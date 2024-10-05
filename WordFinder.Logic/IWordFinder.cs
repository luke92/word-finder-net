using System;

namespace WordFinder.Logic
{
    public interface IWordFinder
    {
        public IEnumerable<string> Find(IEnumerable<string> wordstream);
    }
}
