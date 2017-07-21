using System.Collections.Generic;

namespace WordCount.Interfaces
{
    public interface IFileReader
    {
        IEnumerable<char> GetCharacters();
    }
}