using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication2
{
    class Prim<TVertex, TEdge, TData, TWeight>
    {
        public List<Edge<TEdge, TWeight, TVertex>> Spanning;//минимальное остовное дерево
        public PriorityQueue<TWeight> QueueEdges;//очередь с весами ребер
        public GoGraph<TVertex, TEdge, TData, TWeight> PrimGraph;

        public Prim(GoGraph<TVertex, TEdge, TData, TWeight> g)
        {
            Spanning = new List<Edge<TEdge, TWeight, TVertex>>();
            QueueEdges = new PriorityQueue<TWeight>();
            PrimGraph = g;
        }

        public void MST_Prim(Vertex<TVertex> v)
        {
            var itall = Graph<TVertex, TEdge, TData, TWeight>.IteratorAllEdges.MakeIt(PrimGraph.G);
            TWeight max;
            itall.Beg();
            if (itall.Input() != null)
                max = itall.Input().Weight;
            for (int i = 0; ; i++)
            {
                var a = (float)itall.Input().Weight;
            }
                
            for (int i = 0; i < PrimGraph.G.Vertexes.Count; i++)
            {
                QueueEdges.Enqueue(max, -1);
            }

            
        }
    }
}
