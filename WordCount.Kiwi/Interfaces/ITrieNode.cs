using System.Collections.Generic;

namespace WordCount.Interfaces
{
    public interface ITrieNode
    {
        TrieNode AddCharacter(char keyCharacter);
        void PopulateWordCountDictionary(ref Dictionary<string, int> wordCountDictionary);
        string ToString();
    }
}
