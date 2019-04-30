using System.Collections.Generic;

namespace TechnicalTest
{
    /// <summary>
    /// Class to represent a Node on a created Graph
    /// </summary>
    public class Node
    {
        /// <summary>
        /// The value of this current Node, in this example, a word in the dictionary
        /// </summary>
        public string Value { get; }

        /// <summary>
        /// All other nodes connected to this node by a vertex.
        /// </summary>
        public List<Node> ConnectedNodes { get; set; } = new List<Node>();

        /// <summary>
        /// The path to get from one node to another, updated when performing a graph search.
        /// </summary>
        public string Path { get; set; }

        public Node(string value)
        {
            Value = value;
        }
    }
}
