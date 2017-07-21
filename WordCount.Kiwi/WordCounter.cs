using System;
using System.Collections.Generic;
using WordCount.Interfaces;

namespace WordCount
{
    public class WordCounter : IWordCounter
    {
        private TrieNode startingNode;
        private readonly TrieNode rootNode;
        private readonly IFileReader fileReader;

        public WordCounter(IFileReader fileReader, TrieNode rootTrieNode)
        {
            this.startingNode = rootTrieNode;
            this.rootNode = rootTrieNode;
            this.fileReader = fileReader;
        }

        public void StartCount()
        {
            foreach (var character in this.fileReader.GetCharacters())
            {
                if (Char.IsLetter(character))
                {
                    this.startingNode = this.startingNode.AddCharacter(character);
                }
                else
                {
                    WordFinished();
                }
            }
            WordFinished();

            Console.WriteLine("Counting finished");
        }

        public Dictionary<string, int> GetWordCountDictionary()
        {
            var wordCountDictionary = new Dictionary<string, int>();
            this.rootNode.PopulateWordCountDictionary(ref wordCountDictionary);
            return  wordCountDictionary;
        }

        private void WordFinished()
        {
            this.startingNode.WordCount++;
            this.startingNode = this.rootNode;
        }
    }
}
