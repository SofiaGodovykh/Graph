using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication2
{
    class GoGraph<TVertex, TEdge, TData, TWeight>
    {
        public Graph<TVertex, TEdge, TData, TWeight> G;
       // public bool Oriented;
       // public bool Type;

        //internal abstract class IteratorAllEdges
        //{
        //    public int I;
        //    public int J;
        //    public bool State; //0 - не установлен, 1 - установлен
        //    public Graph<TVertex, TEdge, TData, TWeight> ItGraph;

        //    public IteratorAllEdges(Graph<TVertex, TEdge, TData, TWeight> g)
        //    {
        //        State = false;
        //        // ItGraph = g;
        //        I = 0;
        //        J = 0;
        //    }

        //    public IteratorAllEdges MakeIt(Graph<TVertex, TEdge, TData, TWeight> g)
        //    {
        //        if (g.Type == false)
        //        {
        //            var q = new LGraph<TVertex, TEdge, TData, TWeight>.LIteratorAllEdges(g);
        //            return q;
        //        }

        //        else
        //        {
        //            var q = new MGraph<TVertex, TEdge, TData, TWeight>.MIteratorAllEdges(g);
        //            return q;
        //        }
        //    }

        //    public abstract bool Beg();
        //    public abstract bool End();
        //    public abstract void Next();
        //    public abstract Edge<TEdge, TWeight, TVertex> Input();
        //}

        public GoGraph()
        {
            G = new LGraph<TVertex, TEdge, TData, TWeight>();
        }

        public GoGraph(int V, bool D, bool F)
        {
            G = Graph<TVertex, TEdge, TData, TWeight>.MakeGraph(V, D, F);
        }

        public GoGraph(int V, int E, bool D, bool F)
        {
            G = Graph<TVertex, TEdge, TData, TWeight>.MakeGraph(V, E, D, F);
        }

        public void Reverse()
        {
            var g = G;
            if (G.Type)//если M, то в L
            {
                G = new LGraph<TVertex, TEdge, TData, TWeight>();
                G.Oriented = g.Oriented;
                for (int i = 0; i < g.Vertexes.Count; i++)//пополняем вектор смежности
                {
                    var A = new AdjList<TVertex, TEdge, TData, TWeight>();
                    A.AdjIndex = g.Vertexes[i].Index;
                    G.Adj.Add(A);
                }
                
                for (int i = 0; i < g.Vertexes.Count; i++)
                {
                    for (int j = 0; j < g.Vertexes.Count; j++)
                    {
                        if (g.Matrix[i, j] != null)
                        {
                            G.Adj[i].AddNode(g.Matrix[i, j]);
                        }
                    }
                }
               // G.Type = false;
                g = null;
            }
            else//из L в M
            {
                G.Matrix = new Edge<TEdge, TWeight, TVertex>[G.Vertexes.Count,G.Vertexes.Count];
                for (int i = 0; i < G.Vertexes.Count; i++)
                {
                    for (var q = G.Adj[i].Head; q != null; q = q.Next)
                    {
                        G.Matrix[q.AdjEdge.Vertex1.Index, q.AdjEdge.Vertex2.Index] = q.AdjEdge;
                        if(!G.Oriented)
                            G.Matrix[q.AdjEdge.Vertex2.Index, q.AdjEdge.Vertex1.Index] = q.AdjEdge;
                    }
                }
                G.Adj = null;
               // G.Type = true;
            }
        }
        
        public Edge<TEdge, TWeight, TVertex> AddEdge(Vertex<TVertex> v1, Vertex<TVertex> v2)
        {
            return G.AddEdge(v1, v2);
        }

        public bool DeleteVertex(Vertex<TVertex> vertex)
        {
            return G.DeleteVertex(vertex);
        }

        public Vertex<TVertex> AddVertex()
        {
            return G.AddVertex();
        }

        public bool DeleteEdge(Vertex<TVertex> vertex1, Vertex<TVertex> vertex2)
        {
            return G.DeleteEdge(vertex1, vertex2);
        }

        public void Print()
        {
            string str = "";
            if (G.Type)
            {
                for (int i = 0; i < G.Vertexes.Count; i++)
                {
                    for (int j = 0; j < G.Vertexes.Count; j++)
                    {
                        if(G.Matrix[i, j] != null)
                            str = str + " " + G.Matrix[i, j].Vertex1.Index.ToString() + " " + G.Matrix[i, j].Vertex2.Index.ToString();
                        str = str + "   ";
                    }
                    Console.WriteLine(str);
                    str = "";
                }
            }
            else
            {
                for (int i = 0; i < G.Adj.Count; i++)
                {
                    Console.WriteLine(G.Adj[i].AdjIndex.ToString());
                    var q = G.Adj[i].Head;
                    if (q != null && q.AdjEdge!=null)
                    {
                        for (q = G.Adj[i].Head; (q != null); q = q.Next)
                        {
                            str = str + " " + q.AdjEdge.Vertex1.Index.ToString() + " " +
                                  q.AdjEdge.Vertex2.Index.ToString();
                        }
                        Console.WriteLine(str);
                    }
                    str = "";
                }
            }
        }
    }
}
