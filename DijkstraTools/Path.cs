using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace DijkstraTools
{
    /// <summary>
    /// The (calculated) Path (according to Dijkstra).
    /// Contains a list of Edges, which make up the path.
    /// </summary>
    public class Path<T>
    {
        /// <summary>
        /// List of Edges which make up the path.
        /// </summary>
        private readonly List<Edge<T>> _edgeList;

        /// <summary>
        /// States the current iteration of the Path.
        /// </summary>
        public int CurrentIterationOfPath { get; set; }

        /// <summary>
        /// returns the Count of the _edgeList.
        /// </summary>
        public int Count => _edgeList.Count;

        /// <summary>
        /// Initializes the Path
        /// </summary>
        /// <param name="edges">List of the edges that make up the Path.</param>
        public Path(List<Edge<T>> edges)
        {
            //TODO change this to let client be able to make up his own path.
            _edgeList = edges ?? throw new Exception("Can't create new path without edges.");
        }

        /// <summary>
        /// Gives the current Edge, based on CurrentIterationOfPath variable.
        /// </summary>
        /// <returns></returns>
        public Edge<T> GetCurrentEdge()
        {
            return GetEdge(CurrentIterationOfPath);
        }
        /// <summary>
        /// Get Edge based on the given iteration of Path
        /// </summary>
        /// <param name="iterationOfPath">Iteration of Path to get edge of</param>
        /// <returns>Edge based on given iteration of Path</returns>
        public Edge<T> GetEdge(int iterationOfPath)
        {
            if (iterationOfPath <= Count)
            {
                return _edgeList[iterationOfPath];
            }
            Debug.WriteLine("Given iterationOfPath is greater than the size of the Path.");
            return default(Edge<T>);
        }
        /// <summary>
        /// Gives a copy of the edgeList
        /// </summary>
        /// <returns></returns>
        public List<Edge<T>> GetCopyOfEdgeList()
        {
            return _edgeList.GetRange(0, Count); 
        }
    }
}