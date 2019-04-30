using System.Collections.Generic;

namespace TechnicalTest
{
    public class BreadthFirstSearch : IGraphSearch
    {
        ///<summary>
        /// The final path between the startNode and the endNode
        /// </summary>
        public string Result { get; set; } = "No Path Found";

        /// <summary>
        /// Performs a Breath First Search - Queues the startingNode updates the Path property and enqueues each nodes connected to that node,
        /// the starting node is then dequeued and the we repeat the process with the remaining nodes in the queue until the endNode value is found.
        /// </summary>
        public void Search(List<Node> graph, Node startingNode, Node endNode)
        {
            HashSet<string> visitedNodes = new HashSet<string>();
            startingNode.Path = startingNode.Value;
            Queue<Node> queuedNodes = new Queue<Node>();
            queuedNodes.Enqueue(startingNode);
            visitedNodes.Add(startingNode.Value);

            while (queuedNodes.Count > 0)
            {
                Node node = queuedNodes.Dequeue();

                if (node.Value == endNode.Value)
                {
                    Result = node.Path;
                    queuedNodes.Clear();
                    continue;
                }

                foreach (Node connectedNode in node.ConnectedNodes)
                {
                    if (!visitedNodes.Contains(connectedNode.Value))
                    {
                        connectedNode.Path = node.Path + "," + connectedNode.Value;
                        queuedNodes.Enqueue(connectedNode);
                        visitedNodes.Add(connectedNode.Value);
                    }
                }
            }
        }
    }
}
