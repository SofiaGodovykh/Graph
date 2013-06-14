using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication2
{
    class Prim<TVertex, TEdge, TData, TWeight>
    {
        public List<MinEdge> Spanning;//минимальное остовное дерево
        public PriorityQueue QueueEdges;//очередь с весами ребер
        public GoGraph<TVertex, TEdge, TData, float> PrimGraph;

        public class MinEdge
        {
            public float Pr;
            public Vertex<TVertex> V;

            public MinEdge(float p, Vertex<TVertex> v)
            {
                Pr = p;
                V = v;
            }
        }
        public Prim(GoGraph<TVertex, TEdge, TData, float> g)
        {
            Spanning = new List<MinEdge>();
            QueueEdges = new PriorityQueue();
            PrimGraph = g;
        }

        public void MST_Prim(Vertex<TVertex> v1)
        {

            int comp;//для опознавания нужной вершины итератором
           
            for (int i = 0; i < PrimGraph.G.Vertexes.Count; i++)//заполнить очередь вершин, вес +inf, предок -1
            {
                if (i != PrimGraph.G.Vertexes.IndexOf(v1))
                {
                    var q = new PriorityQueue.QueueNode(float.PositiveInfinity, -1);
                    q.QueIndex = PrimGraph.G.Vertexes[i].Index;
                    QueueEdges.Enqueue(q);
                }
            }

            var t = new PriorityQueue.QueueNode(0, 0);//поместили в очередь вершину, от которой строим дерево
            t.QueIndex = v1.Index;
            QueueEdges.Enqueue(t);

            var v = QueueEdges.Dequeue(); //достали входящую
            
            while (QueueEdges.QueueLength != 0)
            {
                //Console.WriteLine(QueueEdges.PrioQueue[0].QueIndex);
                //for (int a = 0; a < QueueEdges.PrioQueue.Count; a++)
                //{
                //    Console.Write(QueueEdges.PrioQueue[a].QueIndex);
                //}
                //Console.WriteLine();
                int j;
                for (j = 0; j < PrimGraph.G.Vertexes.Count; j++)//определили ее положение в списке Vertexes
                {
                    if(PrimGraph.G.Vertexes[j].Index == v.QueIndex)
                        break;
                }

                var itout = Graph<TVertex, TEdge, TData, float>.IteratorOutEdge.MakeIt(PrimGraph.G.Vertexes[j], PrimGraph.G);//создали итератор исходящих вершин
                itout.Beg();

                for (int i = 0;; i++)
                {
                    if(itout.Input() == null)
                        break;

                    if (itout.Input().Vertex2.Index == v.QueIndex)
                    {
                        comp = itout.Input().Vertex1.Index;
                    }

                    else
                    {
                        comp = itout.Input().Vertex2.Index;
                    }

                    var c = QueueEdges.IfContains(comp);//если очередь содержит смежную вершину
                    if (c > 0 && itout.Input().Weight < QueueEdges.PrioQueue[c].PriorityWeight)
                    {
                        QueueEdges.PrioQueue.RemoveAt(c);//вытащили вершину из очереди
                        QueueEdges.QueueLength--;
                        //var h = new PriorityQueue.QueueNode(itout.Input().Weight, v.QueIndex);
                        var h = new PriorityQueue.QueueNode(itout.Input().Weight, j);
                        h.QueIndex = comp;
                        QueueEdges.Enqueue(h);
                    }
                    itout.Next();
                }

                v = QueueEdges.Dequeue();
                Console.WriteLine("вершина " + v.QueIndex.ToString() + " вес " + v.PriorityWeight + " от вершины " + v.Parent);
            }
        }
    }
}
