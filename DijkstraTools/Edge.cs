namespace DijkstraTools
{
	/// <summary>
	/// Struct for a Dijkstra Path Part
	/// </summary>
	/// <typeparam name="T">The Type of the Vertex</typeparam>
    public struct Edge<T>
    {
		/// <summary>
		/// The Vertex where the PathPart starts
		/// </summary>
	    public Vertex<T> VertexFrom { get; private set; }
		/// <summary>
		/// The Vertex where the PathPart ends
		/// </summary>
	    public Vertex<T> VertexTo { get; private set; }
		/// <summary>
		/// The Weight of the PathPart
		/// </summary>
	    public int Weight { get; private set; }
		/// <summary>
		/// Constructor setting the properties
		/// </summary>
		/// <param name="vertexFrom">The Vertex where the PathPart starts</param>
		/// <param name="vertexTo">The Vertex where the PathPart ends</param>
		/// <param name="weight">The Weight of the PathPart</param>
		public Edge(Vertex<T> vertexFrom, Vertex<T> vertexTo, int weight)
	    {
		    VertexFrom = vertexFrom;
		    VertexTo = vertexTo;
		    Weight = weight;
	    }
    }
}
