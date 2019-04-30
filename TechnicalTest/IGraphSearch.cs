using System.Collections.Generic;

namespace TechnicalTest
{
    /// <summary>
    /// Interface to represent a method of searching a graph (List of Nodes).
    /// Currently only implemented by BreadthFirstSearch, however could easily be implemented by similar graph search algorithms
    /// e.g. Depth First Search or Dijkstra's Algorithm (If searching a weighted graph)
    /// </summary>
    public interface IGraphSearch
    {
        string Result { get; set; }

        void Search(List<Node> graph, Node startingNode, Node endNode);
    }
}
