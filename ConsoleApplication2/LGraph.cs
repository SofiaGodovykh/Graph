using System;
using System.Collections.Generic;

namespace ConsoleApplication2
{
    internal class LGraph<TVertex, TEdge, TData, TWeight> : Graph<TVertex, TEdge, TData, TWeight>
    {

        public LGraph()
        {
            Type = false;
            Adj = new List<AdjList<TVertex, TEdge, TData, TWeight>>();
        }

        public LGraph(int numberVertex, bool D, bool F)
            : base(D, F)
        {
            Type = false;
            Adj = new List<AdjList<TVertex, TEdge, TData, TWeight>>();
            Oriented = D;

            for (int i = 0; i < numberVertex; i++)
            {
                AddVertex();
            }
        }

        public LGraph(int numberVertex, int numberEdge, bool D, bool F)
            : base(D, F)
        {
            Type = false;
            Adj = new List<AdjList<TVertex, TEdge, TData, TWeight>>();
            Oriented = D;

            for (int i = 0; i < numberVertex; i++)
            {
                AddVertex();
            }
            do
            {
                Random r = new Random();
                if (AddEdge(Vertexes[r.Next(Vertexes.Count)], Vertexes[r.Next(Vertexes.Count)]) != null)
                    numberEdge--; //вставляем ребро со случайными вершинами
            } while (numberEdge != 0);
        }

        public override Vertex<TVertex> AddVertex() //добавление вершины
        {
            var v = new Vertex<TVertex>();
            v.Index = CurrentIndex;
            Vertexes.Add(v);
            var A = new AdjList<TVertex, TEdge, TData, TWeight>();
            A.AdjIndex = CurrentIndex;
            Adj.Add(A);
            CurrentIndex++;
            return v;
        }

        public override bool DeleteVertex(Vertex<TVertex> vertex) //TODO: delete all edges
        {
            int i;
            for (i = 0; i < Adj.Count; i++)
            {
                if (Adj[i].AdjIndex == vertex.Index)
                    break;
            }
            if (i == Adj.Count && Adj[i].AdjIndex != vertex.Index)
                return false; //не нашли вершину
            if (Oriented) //если ориентированный, удаляем список, проходим по всем спискам в поисках ребер
            {
                Adj.Remove(Adj[i]); //удалили вершину
                for (int j = 0; j < Adj.Count; j++) //ищем и удаляем ребра
                {
                    Adj[j].DeleteNode(vertex);
                }
                Vertexes.Remove(vertex);
                return true;
            }
            else //если неориентированный, удаляем обратные ребра, потом удаляем строчку
            {
                for (var q = Adj[i].Head; q != null; q = q.Next)
                {
                    if (q == null)
                        break;
                    DeleteEdge(q.AdjEdge.Vertex1, q.AdjEdge.Vertex2); //удаляем ребра
                }
                Adj.Remove(Adj[i]);
                Vertexes.Remove(vertex);
                return true;
            }
        }

        public override Edge<TEdge, TWeight, TVertex> AddEdge(Vertex<TVertex> v1, Vertex<TVertex> v2)
        {
            if (v1.Index == v2.Index)
                return null;
            if (Oriented)
            {
                for (int i = 0; i < Adj.Count; i++)
                {
                    if (v1.Index == Adj[i].AdjIndex)
                    {
                        var e = new Edge<TEdge, TWeight, TVertex>(v1, v2); //добавить проверку на добавление
                        if (Adj[i].AddNode(e))
                        {
                            EdgesCount++;
                            return e;
                        }
                        return null;
                    }
                }
                return null;
            }
            else
            {
                if (v1.Index > v2.Index)
                    //будем добавлять вершины в порядке возрастания индексов, чтобы не добавлять проверок в добавление
                {
                    var v = v1;
                    v1 = v2;
                    v2 = v;
                }
                bool f1 = false;
                bool f2 = false;
                var e = new Edge<TEdge, TWeight, TVertex>(v1, v2);

                for (int i = 0; i < Adj.Count; i++)
                {
                    if (v1.Index == Adj[i].AdjIndex)
                    {
                        f1 = Adj[i].AddNode(e);
                        break;
                    }
                }

                for (int i = 0; i < Adj.Count; i++)
                {
                    if (v2.Index == Adj[i].AdjIndex)
                    {
                        f2 = Adj[i].AddNode(e);
                        if (f1 && f2)
                        {
                            EdgesCount++;
                            return e;
                        }
                        else
                        {
                            return null;
                        }
                    }
                }
                return null;
            }
        }

        public override bool DeleteEdge(Vertex<TVertex> vertex1, Vertex<TVertex> vertex2)
        {
            if (Oriented)
            {
                for (int i = 0; i < Adj.Count; i++)
                {
                    var v = Adj[i];
                    if (vertex1.Index == v.AdjIndex) //если нашли исходящую вершину
                    {
                        EdgesCount--;
                        return v.DeleteNode(vertex2);
                    }
                }
                return false;
            }

            else
            {
                bool flag1 = false;
                bool flag2 = false;

                for (int i = 0; i < Adj.Count; i++)
                {
                    var v = Adj[i];
                    if (vertex1.Index == v.AdjIndex) //если нашли исходящую вершину
                    {
                        flag1 = v.DeleteNode(vertex2);
                    }

                    if (vertex2.Index == v.AdjIndex) //если нашли исходящую вершину
                    {
                        flag1 = v.DeleteNode(vertex1);
                    }

                    if (vertex2.Index == v.AdjIndex) //если нашли исходящую вершину
                    {
                        flag2 = v.DeleteNode(vertex2);
                    }

                    if (vertex1.Index == v.AdjIndex) //если нашли исходящую вершину
                    {
                        flag2 = v.DeleteNode(vertex1);
                    }
                }

                if (flag1 && flag2)
                    EdgesCount--;
                return (flag1 && flag2);
            }
        }

        internal class LIteratorAllEdges : Graph<TVertex, TEdge, TData, TWeight>.IteratorAllEdges
        {
            public LIteratorAllEdges(Graph<TVertex, TEdge, TData, TWeight> g) :base(g)
            {
                //State = false;
                ItGraph = g;
                //I = 0;
                //J = 0;
            }

            public override bool Beg() //установка на начало
            {
                if (ItGraph.Vertexes.Count == 0)
                    return false;
                State = true;
                if (ItGraph.Oriented == false) //для неориентированного
                {
                    for (int k = 0; k < ItGraph.Vertexes.Count; k++)
                    {
                        int t = 0;
                        for (var q = ItGraph.Adj[k].Head; q != null; q = q.Next)
                        {
                            if (q == null)
                                break;
                            if (ItGraph.Adj[k].AdjIndex == q.AdjEdge.Vertex1.Index)
                            {
                                I = k;
                                J = t;
                                return true;
                            }
                            t++;
                        }
                    }
                    return false;
                }
                else//для ориентированного
                {
                    for (int k = 0; k < ItGraph.Vertexes.Count; k++)
                    {
                        int t = 0;
                        for (var q = ItGraph.Adj[k].Head; q != null; q = q.Next)
                        {
                            if (q == null)
                                break;
                            I = k;
                            J = t;
                            return true;
                        }
                        t++;
                    }
                    return false;
                }
            }

            public override bool End() //установка на конец
            {
                if (ItGraph.Vertexes.Count == 0)
                    return false;
                else
                {
                    State = true;
                    if (ItGraph.Oriented == false)//неориентированный
                    {
                        //for (int k = ItGraph.Vertexes.Count - 1; k > -1; k--)
                        //{
                        //    int t = ItGraph.Vertexes.Count - 1;
                        //    for (var q = ItGraph.Adj[k].Head; q != null; q = ItGraph.Adj[k].Prev(q))
                        //    {
                        //        if (q == null) //не нашли
                        //            break;
                        //        if (ItGraph.Adj[k].AdjIndex == q.AdjEdge.Vertex1.Index)
                        //        {
                        //            I = k;
                        //            J = t - 1;
                        //            return true;
                        //        }
                        //        t--;
                        //    }
                        //}
                        //return false;
                            for (int k = ItGraph.Vertexes.Count - 1; k > -1; k--)
                            {
                                int p = 0;
                                var q = ItGraph.Adj[k].Head;
                                for ( ; ; q = q.Next)
                                {
                                    if(ItGraph.Adj[k].Length == 0)
                                        break;
                                    if (q.Next == null)
                                    {
                                       break;
                                    }
                                    p++;
                                }

                                for (int i = 0; i < ItGraph.Adj[k].Length; i++)
                                {
                                    if(q == null)
                                        break;
                                    if (ItGraph.Adj[k].AdjIndex == q.AdjEdge.Vertex1.Index)
                                    {
                                        J = p;
                                        I = k;
                                        State = true;
                                       // Console.WriteLine(I.ToString() + " " + J.ToString());
                                        return true;
                                    }
                                    p--;
                                    q = ItGraph.Adj[k].Prev(q);
                                }
                            }
                        State = false;
                        return false;
                    }
                    else
                    {
                        for (int k = ItGraph.Vertexes.Count - 1; k > -1; k--)
                        {
                            int p = 0;
                            for (var q = ItGraph.Adj[k].Head; ; q = q.Next)
                            {
                                if(ItGraph.Adj[k].Length == 0)
                                    break;
                                if (q.Next == null)
                                {
                                    I = k;
                                    J = p;
                                    return true;
                                }
                                p++;
                            }
                        }
                        return false;
                    }
                }
            }

            public override void Next()
            {
                if (State == false)
                    return;

                if (ItGraph.Oriented == false)
                {
                    bool f = false;
                    int r = 0;
                    for (var q = ItGraph.Adj[I].Head; q != null; q = q.Next) //если в этом же списке смежности
                    {
                        if (q == null)
                            break;
                        if (f == true && ItGraph.Adj[I].AdjIndex == q.AdjEdge.Vertex1.Index)
                        {
                            J = r;
                            return;
                        }
                        if (r == J)
                            f = true;
                        r++;
                    }

                    for (int p = I + 1; p < ItGraph.Vertexes.Count; p++)
                    {
                        if (p == ItGraph.Vertexes.Count)
                        {
                            State = false;
                            return;
                        }

                        var v = ItGraph.Adj[p].Head;
                        int y = 0;
                        for (int j = 0; j < ItGraph.Adj[p].Length; j++) //идем по списку смежности
                        {
                            if (ItGraph.Adj[p].Length == 0)
                                break;
                            if(v == null)
                                break;
                            if (ItGraph.Adj[p].AdjIndex == v.AdjEdge.Vertex1.Index)
                            {
                                I = p;
                                J = y;
                                return;
                            }
                            y++;
                            v = v.Next;
                        }
                    }
                    State = false;
                    return;
                }

                else //если ориентированный
                {
                    for (var q = ItGraph.Adj[I].Head; q != null; q = q.Next) //если в этом же списке смежности
                    {
                        if (q == null)
                            break;
                        if (q.AdjEdge.Vertex1.Equals(Input().Vertex1) && q.AdjEdge.Vertex2.Equals(Input().Vertex2)) //если q совпадает с текущим положением итератора
                        {
                            if (q.Next != null)
                            {
                                J++;
                                return;
                            }
                            else
                                break;
                        }
                    }

                    for (int i = I + 1; i < ItGraph.Vertexes.Count; i++)
                    {
                        if (i == ItGraph.Vertexes.Count)
                        {
                            State = false;
                            return;
                        }

                        if (ItGraph.Adj[i].Head != null)
                        {
                            I = i;
                            J = 0;
                            return;
                        }
                    }
                    State = false;
                    return;
                }
            }

            public override Edge<TEdge, TWeight, TVertex> Input()
            {
                if (State == false)
                    return null;
                var q = ItGraph.Adj[I].Head;
                if (J == 0)
                    return ItGraph.Adj[I].Head.AdjEdge;
                for (int k = 0; k < ItGraph.Adj[I].Length; k++)
                {
                    if (k == J)
                        return q.AdjEdge;
                    q = q.Next;
                }
                return null;
            }
        }

        ////internal class LIteratorAllEdges : Graph<TVertex, TEdge, TData, TWeight>.IteratorAllEdges
        internal class LIteratorOutEdge : Graph<TVertex, TEdge, TData, TWeight>.IteratorOutEdge
        {
            public LIteratorOutEdge(Vertex<TVertex> v, Graph<TVertex, TEdge, TData, TWeight> g):base(v, g)
            {
                ItVertex = v;
                ItGraph = g;
            }

            public override bool Beg()
            {
                if (ItGraph.Adj[ItVertex.Index].Length == 0)
                {
                    State = false;
                    return false;
                }

                State = true;
                J = 0;
                return true;
            }

            public override bool End()
            {
                if (ItGraph.Adj[ItVertex.Index].Length == 0)
                {
                    State = false;
                    return false;
                }

                State = true;
                J = ItGraph.Adj[ItVertex.Index].Length - 1;
                return true;
            }

            public override void Next()
            {
                if (State == false)
                    return;
                if (J == ItGraph.Adj[ItVertex.Index].Length - 1)
                {
                    State = false;
                    return;
                }

                J++;
                return;
            }

           public override Edge<TEdge, TWeight, TVertex> Input()
            {
                if (State == false)
                    return null;

                var q = ItGraph.Adj[ItVertex.Index].Head;
                for (int i = 0;; i++)
                {
                    if (i == J)
                    {
                        return q.AdjEdge;
                    }
                    q = q.Next;
                }
                return null;
            }
        }

        //internal class LIteratorAllEdges : Graph<TVertex, TEdge, TData, TWeight>.IteratorAllEdges
        internal class LIteratorInputEdge : Graph<TVertex, TEdge, TData, TWeight>.IteratorInputEdge
        {
             public LIteratorInputEdge(Vertex<TVertex> v, Graph<TVertex, TEdge, TData, TWeight> g):base(v, g)
             {
                 ItGraph = g;
                 ItVertex = v;
             }


            public override bool Beg()
            {
                if (ItGraph.EdgesCount == 0)
                {
                    State = false;
                    return false;
                }

                if (ItGraph.Oriented == false)
                {
                    if (ItGraph.Adj[ItVertex.Index].Length == 0)
                    {
                        State = false;
                        return false;
                    }

                    else
                    {
                        State = true;
                        I = ItVertex.Index;
                        J = 0;
                        return true;
                    }
                }

                else
                {
                    for (int i = 0; i < ItGraph.Vertexes.Count; i++)
                    {
                        var q = ItGraph.Adj[i].Head;
                        for (int j = 0; j < ItGraph.Vertexes.Count; j++)
                        {
                            if(q == null)
                                break;
                            if (q.AdjEdge.Vertex2.Equals(ItVertex))//нашли (v1, v2), где v1 - начало, v2 - конец
                            {
                                State = true;
                                I = i;
                                J = j;
                                return true;
                            }
                        }
                    }
                    return false;
                }
            }

            public override bool End()
            {
                if (ItGraph.EdgesCount == 0)
                {
                    State = false;
                    return false;
                }

                if (ItGraph.Oriented == false)
                {
                    if (ItGraph.Adj[ItVertex.Index].Length == 0)
                    {
                        State = false;
                        return false;
                    }

                    else
                    {
                        State = true;
                        I = ItVertex.Index;
                        J = ItGraph.Adj[ItVertex.Index].Length - 1;
                        return true;
                    }
                }

                else
                {
                    for (int i = ItGraph.Vertexes.Count - 1; i > - 1; i--)
                    {
                        var q = ItGraph.Adj[i].Head;
                            if(ItGraph.Adj[i].Head == null)//если голова пустая
                                break;
                        if (ItGraph.Adj[i].Length == 1) //если один элемент 
                        {
                            if (ItGraph.Adj[i].Head.AdjEdge.Vertex2.Equals(ItVertex)) //если в первом
                            {
                                State = true;
                                I = i;
                                J = 0;
                                return true;
                            }

                            else
                            {
                                State = false;
                                return false;
                            }
                        }

                        else //если длинный список, ищем последний
                        {
                            for (int f = 0;; f++)
                            {
                                if (q.Next != null)
                                    q = q.Next;
                                else
                                    break;
                            }

                            for (int k = ItGraph.Vertexes.Count - 1; k > -1; k--)
                            {
                                if (q == null)
                                    break;

                                if (q.AdjEdge.Vertex2.Equals(ItVertex))
                                {
                                    I = i;
                                    J = k;
                                    State = true;
                                    return true;
                                }

                                else
                                    q = ItGraph.Adj[i].Prev(q);
                            }
                        }
                    }
                    State = false;
                    return false;
                }
            }

            public override void Next()
            {
                if(State == false)
                    return;
                else
                {
                    if (ItGraph.Oriented == false) //если неориентированный
                    {
                        if (J == ItGraph.Adj[I].Length - 1)
                        {
                            State = false;
                            return;
                        }

                        else
                        {
                            J++;
                            return;
                        }
                    }

                    else
                    {
                        var q = ItGraph.Adj[I].Head;
                        bool f = false;
                        for (int k = J; k < ItGraph.Adj[I].Length; k++)//если в этом же списке
                        {
                            if (f == false && q.AdjEdge.Equals(Input()))//был найден элемент
                            {
                                f = true;
                            }

                            q = q.Next;
                            if (q == null)
                                break;
                            if (f)
                            {
                                if (q.AdjEdge.Vertex2.Equals(ItVertex))
                                {
                                    J = k;
                                    return;
                                }
                            }
                        }

                        for (int i = I + 1  ; i < ItGraph.Vertexes.Count; i++)
                        {
                            if (i == ItGraph.Vertexes.Count)
                            {
                                State = false;
                                return;
                            }

                            var p = ItGraph.Adj[i].Head;
                            for (int j = 0; j < ItGraph.Adj[i].Length; j++)
                            {
                                if(p == null)
                                    break;
                                if (p.AdjEdge.Vertex2.Equals(ItVertex))
                                {
                                    I = i;
                                    J = j;
                                    return;
                                }
                                p = p.Next;
                            }
                        }
                        State = false;
                        return;
                    }
                }
            }

            public override Edge<TEdge, TWeight, TVertex> Input()
            {
                if (State == false)
                    return null;

                var q = ItGraph.Adj[I].Head;
                for (int i = 0; i < ItGraph.Adj[I].Length; i++)
                {
                    if (i == J)
                    {
                        return q.AdjEdge;
                    }
                    q = q.Next;
                }
                return null;
            }
        }
    }
}