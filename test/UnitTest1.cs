using NUnit.Framework;
using RudenkoEx2;

namespace test
{
    [TestFixture]
    public class UnitTest
    {
        private Graph graph;

        [SetUp]
        public void Setup()
        {
            graph = new Graph(4);

            string[] vertexNames = { "A", "B", "C", "D" };
            double[,] adjacencyMatrix = {
                { 0, 1, 2, 49,},
                { 1, 0, 0, 7,},
                { 2, 0, 0, 20,},
                { 49, 7, 20, 0,}
             };
            graph.InputGraph(vertexNames, adjacencyMatrix);

        }

        [Test]
        public void TestFindShortestPath_ValidPath()
        {
            double result = graph.FindShortestPath("A", "D");
            Assert.AreEqual(8, result);
        }
    }
}