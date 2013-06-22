using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication2
{
    abstract class Graph<TVertex, TEdge, TData, TWeight>
    {
        public bool Type;
        public bool Oriented;
        public bool Weighted;
        public List<Vertex<TVertex>> Vertexes;
        public int CurrentIndex;
        public int EdgesCount;//количество ребер
        public List<AdjList<TVertex, TEdge, TData, TWeight>> Adj; //вектор смежности
        public Edge<TEdge, TWeight, TVertex>[,] Matrix;

        internal abstract class IteratorAllEdges
        {
            protected int I;
            protected int J;
            protected bool State; //0 - не установлен, 1 - установлен
            protected Graph<TVertex, TEdge, TData, TWeight> ItGraph;

            public IteratorAllEdges(Graph<TVertex, TEdge, TData, TWeight> g)
            {
                State = false;
                // ItGraph = g;
                I = 0;
                J = 0;
            }

            public static IteratorAllEdges MakeIt(Graph<TVertex, TEdge, TData, TWeight> g)
            {
                if (g.Type == false)
                {
                    var q = new LGraph<TVertex, TEdge, TData, TWeight>.LIteratorAllEdges(g);
                    return q;
                }

                else
                {
                    var q = new MGraph<TVertex, TEdge, TData, TWeight>.MIteratorAllEdges(g);
                    return q;
                }
            }

            public abstract bool Beg();
            public abstract bool End();
            public abstract void Next();
            public abstract Edge<TEdge, TWeight, TVertex> Input();
        }

        internal abstract class IteratorOutEdge
        {
            protected Vertex<TVertex> ItVertex;
            protected bool State;
            protected int J;
            protected Graph<TVertex, TEdge, TData, TWeight> ItGraph;

            public IteratorOutEdge(Vertex<TVertex> v, Graph<TVertex, TEdge, TData, TWeight> g)
            {
                //ItVertex = v;
                State = false;
                J = 0;
                //ItGraph = g;
            }

            public static IteratorOutEdge MakeIt(Vertex<TVertex> v, Graph<TVertex, TEdge, TData, TWeight> g)
            {
                if (g.Type == false)
                {
                    //IteratorOutEdge q;
                    var q = new LGraph<TVertex, TEdge, TData, TWeight>.LIteratorOutEdge(v, g);
                    return q;
                }

                else
                {
                    var q = new MGraph<TVertex, TEdge, TData, TWeight>.MIteratorOutEdge(v, g);
                    return q;
                }
            }

            public abstract bool Beg();
            public abstract bool End();
            public abstract void Next();
            public abstract Edge<TEdge, TWeight, TVertex> Input();
        }

        internal abstract class IteratorInputEdge
        {
            protected Vertex<TVertex> ItVertex;
            protected bool State;
            protected int J;
            protected int I;
            protected Graph<TVertex, TEdge, TData, TWeight> ItGraph;

            public IteratorInputEdge(Vertex<TVertex> v, Graph<TVertex, TEdge, TData, TWeight> g)
            {
                State = false;
                //ItVertex = v;
                //ItGraph = g;
                I = 0;
                J = 0;
            }

            public static IteratorInputEdge MakeIt(Vertex<TVertex> v, Graph<TVertex, TEdge, TData, TWeight> g)
            {
                if (g.Type == false)
                {
                    //IteratorOutEdge q;
                    var q = new LGraph<TVertex, TEdge, TData, TWeight>.LIteratorInputEdge(v, g);
                    return q;
                }

                else
                {
                    var q = new MGraph<TVertex, TEdge, TData, TWeight>.MIteratorInputEdge(v, g);
                    return q;
                }
            }

            public abstract bool Beg();
            public abstract bool End();
            public abstract void Next();
            public abstract Edge<TEdge, TWeight, TVertex> Input();
        }

        public class IteratorVertexes<TVertex, TEdge, TData, TWeight>
        {
            private bool VS;
			private int CurrentCur;
            private Graph<TVertex, TEdge, TData, TWeight> iGG;
			
            public IteratorVertexes(Graph<TVertex, TEdge, TData, TWeight> g)
            {
                VS = false;
                iGG = g;
                CurrentCur = 0;
            }

            public Vertex<TVertex> Beg()
            {
                if (iGG.Vertexes.Count == 0)
                {
                    VS = false;
                    return null;
                }

                else
                {
                    VS = true;
                    CurrentCur = 0;
                    return iGG.Vertexes[0];
                }
            }
			
			public Vertex<TVertex> End()
            {
			    if (iGG.Vertexes.Count == 0)
			    {
			        VS = false;
			        return null;
			    }

			    else
			    {
			        VS = true;
			        CurrentCur = iGG.Vertexes.Count - 1;
			        return iGG.Vertexes[iGG.Vertexes.Count - 1];
			    }
            }
			
            public void Next()//++
            {
                if(VS)
                CurrentCur++;
                if (CurrentCur >= iGG.Vertexes.Count)
                {
                    VS = false;
                }
            }

             public Vertex<TVertex> Input()//*
             {
                 if (VS == false)
                     return null;
                 else
                 {
                     return iGG.Vertexes[CurrentCur];
                 }
             }
        }

        public Graph()
        {
            CurrentIndex = 0;
            Vertexes = new List<Vertex<TVertex>>();
            Matrix = new Edge<TEdge, TWeight, TVertex>[0,0];
            Oriented = false;
            Weighted = false;
        }

        public Graph(bool D, bool F)
        {
            Type = F;
            EdgesCount = 0;
            CurrentIndex = 0;
            Vertexes = new List<Vertex<TVertex>>();
            Matrix = new Edge<TEdge, TWeight, TVertex>[0, 0];
            Oriented = D;
        }

        static public Graph<TVertex, TEdge, TData, TWeight> MakeGraph(int V, bool D, bool F)
        {
            if (!F)
            {
                return new LGraph<TVertex, TEdge, TData, TWeight>(V, D, F);
            }
            else
            {
                return new MGraph<TVertex, TEdge, TData, TWeight>(V, D, F);
            }
        }

        static public void Clear(Graph<TVertex, TEdge, TData, TWeight> graph)
        {
            graph = null;
        }

        public Vertex<TVertex> GetVertex(int i)
        {
            if (i >= Vertexes.Count)
                return null;
            return Vertexes[i];
        }

        static public Graph<TVertex, TEdge, TData, TWeight> MakeGraph(int V, int E, bool D, bool F)
        {
            if (!F)
            {
                return new LGraph<TVertex, TEdge, TData, TWeight>(V, E, D, F);
            }
            else
            {
                return new MGraph<TVertex, TEdge, TData, TWeight>(V, E, D, F);
            }
        }

        public bool Directed()
        {
            return Oriented;
        }

        public float Saturation()
        {
            if (Oriented)
            {
                return (2*EdgesCount/Vertexes.Count);
            }

            return (EdgesCount / Vertexes.Count);
        }

        public int V()
        {
            return Vertexes.Count;
        }

        public int E()
        {
            return EdgesCount;
        }

        public abstract Edge<TEdge, TWeight, TVertex> AddEdge(Vertex<TVertex> v1, Vertex<TVertex> v2);
        public abstract bool DeleteVertex(Vertex<TVertex> vertex);
        public abstract Vertex<TVertex> AddVertex();
        public abstract bool DeleteEdge(Vertex<TVertex> vertex1, Vertex<TVertex> vertex2);
      //  public abstract Edge<TEdge, TWeight, TVertex> GetEdge(Vertex<TVertex> v1, Vertex<TVertex> v2);
    }
}
