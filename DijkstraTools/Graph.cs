using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace DijkstraTools
{
    /// <summary>
    /// The Graph contains a list with Vertices and a list with Edges, and provides methods to add and remove vertices and edges including their weight.
    /// </summary>
    public class Graph<T>
    {
        /// <summary>
        /// All the vertices added to the graph
        /// </summary>
        private readonly List<Vertex<T>> _vertices;

        /// <summary>
        /// All the edges of the Graph.
        /// </summary>
        private readonly List<Edge<T>> _edges;

        /// <summary>
        /// Initializes all properties.
        /// </summary>
        public Graph(List<Vertex<T>> vertices = null)
        {
            _vertices = vertices ?? new List<Vertex<T>>();
            _edges = new List<Edge<T>>();
        }

        /// <summary>
        /// Adds a Vertex to the Vertices list.
        /// </summary>
        /// <param name="vertex">The Vertex you want to add</param>
        /// <returns>True if succeeded, false otherwise</returns>
        public bool AddVertex(Vertex<T> vertex)
        {
            if (!_vertices.Contains(vertex))
            {
                Debug.WriteLine("Vertex already known to the Graph.");
            }
            // Try to add the Vertex to the Vertices list. If it succeeds, return true.
            try
            {
                _vertices.Add(vertex);
                return true;
            }
            // If it did not succeed, debug the exception and return false.
            catch (Exception e)
            {
                Debug.WriteLine(e);
                return false;
            }
        }

        /// <summary>
        /// Removes the specified Vertex from the Graph.
        /// Also removes all the connected Edges from and towards it.
        /// </summary>
        /// <param name="vertex">The Rertex too remove from the Graph</param>
        /// <returns>True if succeeded, false otherwise</returns>
        public bool RemoveVertex(Vertex<T> vertex)
        {
            // First check if vertex is already in Graph. If not, return false;
            if (!_vertices.Contains(vertex))
            {
                Debug.WriteLine("The Vertex was not existent in the Graph. Not removing.");
                return false;
            }

            // Remove all Edges containing the given Vertex as starting and ending Vertex
            List<Edge<T>> edgesToRemove = _edges.FindAll(x => x.VertexFrom.Equals(vertex) || x.VertexTo.Equals(vertex));
            foreach (Edge<T> edge in edgesToRemove)
            {
                RemoveEdge(edge);
            }
            
            // Try to Remove the Vertex from the Vertices List. If it succeeds, return true
            try
            {
                _vertices.Remove(vertex);
                return true;
            }
            // If it did not succeed, Debug the exception and return false.
            catch (Exception e)
            {
                Debug.WriteLine(e);
                return false;
            }
        }

        /// <summary>
        /// Adds an Edge to the Graph.
        /// All the Vertices should be existent in the Graph already.
        /// Also, the Edge (non-directed) or Edges (directed) may not exist yet.
        /// </summary>
        /// <param name="vertexStart">The Vertex to start the Edge from</param>
        /// <param name="vertexEnd">The Vertex to end the Edge with</param>
        /// <param name="directed">True if the Edge works in both ways, false otherwise.</param>
        /// <param name="weight">the weight of the Edge</param>
        /// <returns>The newly created Edge </returns>
        public bool AddEdge(Vertex<T> vertexStart, Vertex<T> vertexEnd, bool directed, int weight = 1)
        {
            // If either one of the Vertices does not yet exist in the Vertices list, debug the problem and don't add.
            if (!(ContainsVertex(vertexStart) || ContainsVertex(vertexEnd)))
            {
                Debug.WriteLine(
                    "Either one or both of the Vertices provided haven't been added as Vertex to the Graph. Please do so first.");
                return false;
            }

            Edge<T> edgeFromStartToEnd = new Edge<T>(vertexStart, vertexEnd, weight);
            // Try to add the edge from vertexStart towards vertexEnd with the given weight.
            if (!AddEdge(edgeFromStartToEnd))
            {
                Debug.WriteLine("Coudn't add edge from Start towards the End.");
                return false;
            }

            // if the edge is not directed, we're done, so return true.
            if (!directed)
            {
                return true;
            }

            // Create emtpy Edge for reference.
            // Try to add the edge from vertexEnd towards vertexEnd with the given weight. 
            
            Edge<T> edgeFromEndToStart = new Edge<T>(vertexEnd, vertexStart, weight);
            if (!AddEdge(edgeFromEndToStart))
            {
                RemoveEdge(edgeFromStartToEnd);   
            }
            return true;
        }

        /// <summary>
        /// Adds an edge to the Graph.
        /// </summary>
        /// <param name="edge">Edge to Add.</param>
        /// <returns></returns>
        public bool AddEdge(Edge<T> edge)
        {
            try
            {
                _edges.Add(edge);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                return false;
            }

            return true;
        }

        /// <summary>
        /// Removes the given edge from the Graph.
        /// </summary>
        /// <returns></returns>
        public bool RemoveEdge(Edge<T> edgeToRemove)
        {
            try
            {
                _edges.Remove(edgeToRemove);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                return false;
            }

            return true;
        }

        /// <summary>
        /// Tells if given vertex exists in the Graph
        /// </summary>
        /// <param name="vertex">The Vertex to check if existent in Graph</param>
        /// <returns>True if existent, false if not.</returns>
        public bool ContainsVertex(Vertex<T> vertex)
        {
            return _vertices.Contains(vertex);
        }

        /// <summary>
        /// Tells if there is an edge between the given startVertex and the endVertex with the optionally given weight.
        /// </summary>
        /// <param name="startVertex">The starting Vertex of the Edge to check.</param>
        /// <param name="endVertex">The ending Vertex of the Edge to check.</param>
        /// <param name="weight">the Weight of the edge to check on.</param>
        /// <returns>True if edge exists from within the Graph, false if not.</returns>
        public bool ContainsEdge(Vertex<T> startVertex, Vertex<T> endVertex, int? weight = null)
        {
            if (_edges.Exists(x =>
                x.VertexFrom.Equals(startVertex) && x.VertexTo.Equals(endVertex) && x.Weight == weight))
            {
                return true;
            }

            Debug.WriteLine("The Edge is not known to this Graph.");
            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="edge"></param>
        /// <returns></returns>
        public bool ContainsEdge(Edge<T> edge)
        {
            if (_edges.Contains(edge))
            {
                return true;
            }

            Debug.WriteLine("The Edge is not known to this Graph.");
            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="vertex"></param>
        /// <returns></returns>
        public bool ContainsEdgeFromVertex(Vertex<T> vertex)
        {
            return !vertex.Equals(default(Vertex<T>)) && _edges.Exists(x => x.VertexFrom.Equals(vertex));
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="vertex"></param>
        /// <returns></returns>
        public bool ContainsEdgeToVertex(Vertex<T> vertex)
        {
            return !vertex.Equals(default(Vertex<T>)) && _edges.Exists(x => x.VertexTo.Equals(vertex));
        }

        /// <summary>
        /// Gives a list of all the Vertices known to the Graph.
        /// </summary>
        /// <returns>list of all the vertices known to the Graph.</returns>
        public List<Vertex<T>> GetCopyOfAllVertices()
        {
            List<Vertex<T>> copyOfVerticesList = _vertices.GetRange(0, _vertices.Count);
            return copyOfVerticesList;
        }

        /// <summary>
        /// Gives a list of all the Edges known to the Graph.
        /// </summary>
        /// <returns></returns>
        public List<Edge<T>> GetCopyOfAllEdges()
        {
            List<Edge<T>> copyOfEdgesList = _edges.GetRange(0, _edges.Count);
            return copyOfEdgesList;
        }
    }
}