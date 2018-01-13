using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace DijkstraTools
{
	/// <summary>
	/// Class to find a path within the DijkstraGraph, using the Dijkstra Algorhitm.
	/// </summary>
	/// <typeparam name="T">The type of the DijkstraGraph</typeparam>
	public class PathFinder<T>
	{
		private readonly Graph<T> _graph;

		/// <summary>
		/// Initializes the PathFinder
		/// </summary>
		/// <param name="graph">The DijkstraGraph to use to find the Path wihtin.</param>
		public PathFinder(Graph<T> graph)
		{
			_graph = graph;
		}

		/// <summary>
		/// Calculates the DijkstraPath.
		/// </summary>
		/// <param name="vertexFrom">The vertex to start the DijkstraPath with.</param>
		/// <param name="vertexTo">The Vertex to end the DijkstraPath with.</param>
		/// <returns></returns>
		public Path<T> CalculateDijkstraPath(Vertex<T> vertexFrom, Vertex<T> vertexTo)
		{
			// Check if either one or both of the vertices is/are not existent in the Graph.
			// If so, return null.
			if (!(_graph.ContainsVertex(vertexFrom) && _graph.ContainsVertex(vertexTo)))
			{
				Debug.WriteLine(
					"Either one or both of the Vertices is/are not existent in the Graph. Please alter the graph before trying to find a path.");
				return null;
			}

			// Start trying to find a Path.
			// First create lists for calculation.
			// Distances for the distances relative to the vertexFrom.
			Dictionary<Vertex<T>, int> distances = new Dictionary<Vertex<T>, int>();
			// Create list of Vertices where we have been.
			Dictionary<Vertex<T>, Vertex<T>> visitedVertices = new Dictionary<Vertex<T>, Vertex<T>>();
			// List of all unvisited Vertices (they might be visited to see if it is useful for the path).
			List<Vertex<T>> unvisitedVertices = _graph.GetCopyOfAllVertices();
			// Set each Vertex' distance to int.MaxValue, but set the distance of the vertexFrom to 0.
			foreach (Vertex<T> vertex in _graph.GetCopyOfAllVertices())
			{
				if (vertex.Equals(vertexFrom))
				{
					distances[vertex] = 0;
				}
				else
				{
					distances[vertex] = int.MaxValue;
				}
			}

			Vertex<T> workingVertex;
			while (unvisitedVertices.Count > 0)
			{
				// sort the dictionary to get the key with the lowest value first.
				workingVertex = unvisitedVertices.OrderBy(x => distances[x]).First();
				// Set the Vertex with the lowest distance as the first Vertex to check.
				// Remove the workingVertex from the unvisited Vertices.
				unvisitedVertices.Remove(workingVertex);
				if (!_graph.ContainsEdgeFromVertex(workingVertex))
				{
					distances.Remove(workingVertex);
				}
				foreach (Edge<T> edge in _graph.GetCopyOfAllEdges().Where(x=> x.VertexFrom.Equals(workingVertex)))
				{
					int newCalculatedDistanceForVertex = distances[workingVertex] + edge.Weight;
					if (newCalculatedDistanceForVertex < distances[edge.VertexTo])
					{
						distances[edge.VertexTo] = newCalculatedDistanceForVertex;
						visitedVertices[edge.VertexTo] = workingVertex;
					}
				}
			}
			// Create list for the path.
			List<Edge<T>> shortestPath = new List<Edge<T>>();
			workingVertex = vertexTo;
			while (visitedVertices.ContainsKey(workingVertex))
			{
				int weight = distances[workingVertex] - distances[visitedVertices[workingVertex]];
				Edge<T> edge = new Edge<T>(visitedVertices[workingVertex], workingVertex, weight);
				shortestPath.Insert(0, edge);
				
				workingVertex = visitedVertices[workingVertex];
				
			}
			Path<T> calculatedPath = new Path<T>(shortestPath);
			return calculatedPath;
		}
	}

	//1  function Dijkstra(Graph, source):
	//2
	//3      create vertex set Q
	//4
	//5      for each vertex v in Graph:             // Initialization
	//6          dist[v] ← INFINITY                  // Unknown distance from source to v
	//7          prev[v] ← UNDEFINED                 // Previous node in optimal path from source
	//8          add v to Q                          // All nodes initially in Q (unvisited nodes)
	//9
	//10      dist[source] ← 0                        // Distance from source to source
	//11
	//12      while Q is not empty:
	//13          u ← vertex in Q with min dist[u]    // Node with the least distance
	//14                                                      // will be selected first
	//15          remove u from Q
	//16
	//17          for each neighbor v of u:           // where v is still in Q.
	//18              alt ← dist[u] + length(u, v)
	//19              if alt < dist[v]:               // A shorter path to v has been found
	//20                  dist[v] ← alt
	//21                  prev[v] ← u
	//22

	//1  S ← empty sequence
	//2  u ← target
	//3  while prev[u] is defined:                  // Construct the shortest path with a stack S
	//4      insert u at the beginning of S         // Push the vertex onto the stack
	//5      u ← prev[u]                            // Traverse from target to source
	//6  insert u at the beginning of S             // Push the source onto the stack

}