using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication2
{
    class Edge<TEdge, TWeight, TVertex>
    {
        public Edge(Vertex<TVertex> vertex1, Vertex<TVertex> vertex2)
        {
            Vertex1 = vertex1;
            Vertex2 = vertex2;
        }

        public Edge(Vertex<TVertex> vertex1, Vertex<TVertex> vertex2, TWeight weight)
        {
            Vertex1 = vertex1;
            Vertex2 = vertex2;
            Weight = weight;
        }

        public Edge(Vertex<TVertex> vertex1, Vertex<TVertex> vertex2, TWeight weight, TEdge data)
        {
            Vertex1 = vertex1;
            Vertex2 = vertex2;
            Weight = weight;
            Data = data;
        }

        public Vertex<TVertex> Vertex1;

        public Vertex<TVertex> Vertex2;

        public TWeight Weight;

        public TEdge Data;

        public bool from(Vertex<TVertex> vertex)
        {
            if (Vertex1.Equals(vertex))
                return true;
            return false;
        }

        public Vertex<TVertex> other(Vertex<TVertex> vertex)
        {
            if (Vertex1.Equals(vertex))
                return Vertex2;
            if (Vertex2.Equals(vertex))
                return Vertex1;

            return null;
        }
    }
}
