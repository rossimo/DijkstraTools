using System;
using System.Collections.Generic;
using DijkstraTools;

namespace DebugConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            
            Vertex<string> vertexOne = new Vertex<string>("test1");
            Vertex<string> vertexTwo = new Vertex<string>("test2");
            Vertex<string> vertexThree = new Vertex<string>("test3");
            Vertex<string> vertexFour = new Vertex<string>("test4");
            Vertex<string> vertexFive = new Vertex<string>("test5");
            List<Vertex<string>> verticesOfGraph = new List<Vertex<string>>
            {
                vertexOne,
                vertexTwo,
                vertexThree,
                vertexFour,
                vertexFive
            };
            Graph<string> graph = new Graph<string>(verticesOfGraph);
            graph.AddEdge(vertexOne, vertexTwo, false, 1);
            graph.AddEdge(vertexTwo, vertexThree, true, 2);
            graph.AddEdge(vertexThree, vertexFour, false, 3);
            graph.AddEdge(vertexTwo, vertexFive, false, 4);
            graph.AddEdge(vertexFive, vertexFour, true, 5);
            graph.AddEdge(vertexFour, vertexOne, false, 6);
            PathFinder<string> pathFinder = new PathFinder<string>(graph);
            Path<string> path1 = pathFinder.CalculateDijkstraPath(vertexOne, vertexFour);
            Path<string> path2 = pathFinder.CalculateDijkstraPath(vertexTwo, vertexOne);
            Path<string> path3 = pathFinder.CalculateDijkstraPath(vertexThree, vertexFive);
            Path<string> path4 = pathFinder.CalculateDijkstraPath(vertexFive, vertexTwo);
            Path<string> path5 = pathFinder.CalculateDijkstraPath(vertexFour, vertexThree);
            Path<string> path6 = pathFinder.CalculateDijkstraPath(vertexTwo, vertexThree);
        }
    }
}