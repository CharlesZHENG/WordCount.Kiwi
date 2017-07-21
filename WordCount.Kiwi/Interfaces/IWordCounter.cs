using System.Collections.Generic;

namespace WordCount.Interfaces
{
    public interface IWordCounter
    {
        void StartCount();
        Dictionary<string, int> GetWordCountDictionary();
    }
}