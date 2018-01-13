namespace DijkstraTools
{
    public struct Vertex<T>
    {
        public T Value { get; private set; }
        public Vertex(T item)
        {
            Value = item;
        }
    }
}