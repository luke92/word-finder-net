using System;

namespace WordFinderApp.Logic
{
    public interface IWordFinder
    {
        public IEnumerable<string> Find(IEnumerable<string> wordstream);
    }
}
