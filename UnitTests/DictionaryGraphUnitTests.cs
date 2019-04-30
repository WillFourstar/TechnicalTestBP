using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using TechnicalTest;

namespace Test
{
    [TestFixture]
    public class UnitTests
    {
        private readonly List<string> _simpleDictionary = new List<string> { "dog", "hog", "hot", "joy" };

        [Test]
        public void No_Valid_Path_Found_BFS()
        {
            DictionaryGraph dictionaryGraph = new DictionaryGraph(_simpleDictionary, 3, new BreadthFirstSearch());
            string searchResult = dictionaryGraph.GetShortestPath("dog", "joy");
            Assert.AreEqual(searchResult, "No Path Found");
        }

        [Test]
        public void Valid_Path_Found_BFS()
        {
            DictionaryGraph dictionaryGraph = new DictionaryGraph(_simpleDictionary, 3, new BreadthFirstSearch());
            string searchResult = dictionaryGraph.GetShortestPath("dog", "hot");
            Assert.AreEqual(searchResult, "dog,hog,hot");
        }

        [Test]
        public void Word_Not_In_Dictionary_BFS()
        {
            DictionaryGraph dictionaryGraph = new DictionaryGraph(_simpleDictionary, 3, new BreadthFirstSearch());
            string searchResult = dictionaryGraph.GetShortestPath("dog", "cat");
            Assert.AreEqual(searchResult, "cat was not found in the dictionary");
        }

        [Test]
        public void Valid_Graph_Created()
        {
            DictionaryGraph dictionaryGraph = new DictionaryGraph(_simpleDictionary, 3, new BreadthFirstSearch());
            Node dogNode = dictionaryGraph.Graph.FirstOrDefault(n => n.Value == "dog");
            Node hogNode = dictionaryGraph.Graph.FirstOrDefault(n => n.Value == "hog");
            Node hotNode = dictionaryGraph.Graph.FirstOrDefault(n => n.Value == "hot");
            Node joyNode = dictionaryGraph.Graph.FirstOrDefault(n => n.Value == "joy");

            Assert.True(dogNode.ConnectedNodes.Count == 1);
            Assert.True(hogNode.ConnectedNodes.Count == 2);
            Assert.True(hotNode.ConnectedNodes.Count == 1);
            Assert.True(joyNode.ConnectedNodes.Count == 0);
        }
    }
}