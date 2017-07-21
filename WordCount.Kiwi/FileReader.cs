using System.Collections.Generic;
using System.IO;
using WordCount.Interfaces;

namespace WordCount
{
    public class FileReader : IFileReader
    {
        private readonly string fileName;

        public FileReader(string fileName)
        {
            this.fileName = fileName;
        }

        public IEnumerable<char> GetCharacters()
        {
           using (var streamReader = File.OpenText(this.fileName))
            {
                while (!streamReader.EndOfStream)
                {
                    yield return (char) streamReader.Read();
                }
            }
        }
    }
}
