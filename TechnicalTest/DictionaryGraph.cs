using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechnicalTest
{
    /// <summary>
    /// Class for managing/searching a graph where the Nodes are words in a dictionary and the vertices represent that one node can create another by changing one character.       
    /// </summary>
    public class DictionaryGraph 
    {
        private List<string> _dictionary;
        private readonly int _wordLength;        
        private IGraphSearch _graphSearch;

        public List<Node> Graph { get; set; }

        /// <summary>
        /// Constructor, creates graph of a given dictionary.       
        /// </summary>
        /// <param name="dictionary">List of words, generated from input file</param>
        /// <param name="wordLength">Length of words in graph</param>
        /// <param name="graphSearch">Type of search used to get shortest path</param>
        public DictionaryGraph(List<string> dictionary, int wordLength, IGraphSearch graphSearch)
        {
            _dictionary = dictionary;
            _wordLength = wordLength;
            _graphSearch = graphSearch;
            Graph = CreateGraph();
        }

        /// <summary>
        /// Gets the shortest path between two given words in graph.
        /// Returns the result as a string of Node values, separated by commas.
        /// </summary>
        public string GetShortestPath(string startingWord, string endWord)
        {
            try
            {
                Node startingNode = Graph.FirstOrDefault(n => n.Value == startingWord);
                Node endNode = Graph.FirstOrDefault(n => n.Value == endWord);

                if (startingNode == null)
                {
                    return string.Format($"{startingWord} was not found in the dictionary");
                }
                else if (endNode == null)
                {
                    return string.Format($"{endWord} was not found in the dictionary");
                }
                else
                {
                    _graphSearch.Search(Graph, startingNode, endNode);
                    return _graphSearch.Result;
                }
            }
            catch (Exception ex)
            {
                return $"DictionaryGraph.ShortestPathSearch Error: {ex.InnerException}";
            }
        }

        /// <summary>
        /// Creates a List of Nodes to represent a graph to show connections between words in the graph where they differ by one character.
        /// </summary>
        private List<Node> CreateGraph()
        {
            List<Node> completedGraph = new List<Node>();
            completedGraph = _dictionary.Where(word => word.Length == _wordLength).Select(word => new Node(word)).ToList();

            foreach (Node node in completedGraph)
            {
                Parallel.ForEach(completedGraph, potentialConnection =>
                {
                    if (node.Value != potentialConnection.Value)
                    {
                        CheckNodesOneCharacterDifference(node, potentialConnection);
                    }
                });
            }

            return completedGraph;
        }

        private void CheckNodesOneCharacterDifference(Node currentNode, Node potentialConnection)
        {
            for (char letter = 'a'; letter <= 'z'; letter++)
            {
                for (int i = 0; i < _wordLength; i++)
                {
                    StringBuilder builder = new StringBuilder(currentNode.Value);
                    builder[i] = letter;
                    if (builder.ToString() == potentialConnection.Value)
                    {
                        currentNode.ConnectedNodes.Add(potentialConnection);
                    }
                }
            }
        }
    }
}
