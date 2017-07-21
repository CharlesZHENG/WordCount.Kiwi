using System.Collections.Generic;
using WordCount.Interfaces;

namespace WordCount
{
    public class TrieNode : ITrieNode
    {
        private readonly char character;
        public int WordCount;
        private readonly TrieNode parentNode;
        private readonly Dictionary<char, TrieNode> childrenNodes;

        public TrieNode(TrieNode parentNode, char character)
        {
            this.character = character;
            this.WordCount = 0;
            this.parentNode = parentNode;
            this.childrenNodes = new Dictionary<char, TrieNode>();
        }

        public TrieNode AddCharacter(char keyCharacter)
        {
            TrieNode characterNode;

            if (childrenNodes.ContainsKey(keyCharacter))
            {
                characterNode = childrenNodes[keyCharacter];
            }
            else
            {
                var newTrieNode = new TrieNode(this, keyCharacter);

                childrenNodes.Add(keyCharacter, newTrieNode);
                characterNode = newTrieNode;
            }
            return characterNode;
        }

        public void PopulateWordCountDictionary(ref Dictionary<string, int> wordCountDictionary)
        {
            foreach (var childNode in this.childrenNodes.Values)
            {
                if (childNode.WordCount != 0)
                {
                    wordCountDictionary.Add(childNode.ToString(), childNode.WordCount);
                }
                childNode.PopulateWordCountDictionary(ref wordCountDictionary);
            }
        }

        public override string ToString()
        {
            if (parentNode == null) return "";
            return parentNode.ToString() + character;
        }
    }
}
