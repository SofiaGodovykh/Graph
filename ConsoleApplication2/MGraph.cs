using System;

namespace ConsoleApplication2
{
    internal class MGraph<TVertex, TEdge, TData, TWeight> : Graph<TVertex, TEdge, TData, TWeight>
    {

        public MGraph(int numberVertex, bool D, bool F)
            : base(D, F)
        {
            Type = true;
            Matrix = new Edge<TEdge, TWeight, TVertex>[numberVertex,numberVertex];
            Oriented = D;

            for (int i = 0; i < numberVertex; i++)
            {
                AddVertex();
            }
        }

        public MGraph(int numberVertex, int numberEdge, bool D, bool F)
            : base(D, F)
        {
            Type = true;
            Oriented = D;
            Matrix = new Edge<TEdge, TWeight, TVertex>[numberVertex,numberVertex];

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
            var M = Matrix;
            Matrix = new Edge<TEdge, TWeight, TVertex>[Vertexes.Count + 1,Vertexes.Count + 1];

            for (int i = 0; i < Vertexes.Count; i++)
            {
                {
                    for (int j = 0; j < Vertexes.Count; j++)
                    {
                        if (j == Vertexes.Count || i == Vertexes.Count)
                        {
                            Matrix[i, j] = null;
                        }

                        Matrix[i, j] = M[i, j];
                    }
                }
            }
            CurrentIndex++;
            Vertexes.Add(v);
            return v;
        }

        public override bool DeleteVertex(Vertex<TVertex> vertex)
        {
            var M = Matrix;
            Matrix = new Edge<TEdge, TWeight, TVertex>[Vertexes.Count - 1,Vertexes.Count - 1];
            int im = 0, jm = 0;
            for (int i = 0; i < Vertexes.Count; i++, im++)
            {
                if (i == Vertexes.IndexOf(vertex)) //если дошли до строчки, которой не будет
                    i++;
                jm = 0;
                for (int j = 0; j < Vertexes.Count; j++, jm++)
                {
                    if (j == Vertexes.IndexOf(vertex)) //если дошли до столбца, которого не будет
                        j++;
                    Matrix[im, jm] = M[i, j];
                }
            }
            //bool fj = false;
            //bool fi = false;
            //if (i == Vertexes.IndexOf(vertex))
            //    fi = true;
            //for (int j = 0; j < Vertexes.Count; j++)
            //{
            //    if (j == Vertexes.IndexOf(vertex))
            //        fj = true;
            //    if((!fj)&&(j != Vertexes.Count - 1)&&(i != Vertexes.Count - 1))
            //        Matrix[i, j] = M[i, j];
            //    else
            //    {
            //        if((j != Vertexes.Count - 1)&&(i != Vertexes.Count - 1))
            //        Matrix[i, j] = M[i + 1, j + 1];   
            //    }
            //}
            return Vertexes.Remove(vertex);
        }

        public override Edge<TEdge, TWeight, TVertex> AddEdge(Vertex<TVertex> v1, Vertex<TVertex> v2)
        {
            if (v1.Equals(v2))
                return null;
            if (Oriented)
            {
                if (Matrix[Vertexes.IndexOf(v1), Vertexes.IndexOf(v2)] == null)
                {
                    var e = new Edge<TEdge, TWeight, TVertex>(v1, v2);
                    Matrix[Vertexes.IndexOf(v1), Vertexes.IndexOf(v2)] = e;
                    EdgesCount++;
                    return e;
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
                if (Matrix[Vertexes.IndexOf(v1), Vertexes.IndexOf(v2)] == null)
                {
                    var e = new Edge<TEdge, TWeight, TVertex>(v1, v2);
                    Matrix[Vertexes.IndexOf(v1), Vertexes.IndexOf(v2)] = e;
                    Matrix[Vertexes.IndexOf(v2), Vertexes.IndexOf(v1)] = e;
                    EdgesCount++;
                    return e;
                }
                return null;
            }
        }

        public override bool DeleteEdge(Vertex<TVertex> v1, Vertex<TVertex> v2)
        {
            if (Oriented)
            {
                if (Matrix[Vertexes.IndexOf(v1), Vertexes.IndexOf(v2)] != null)
                {
                    Matrix[Vertexes.IndexOf(v1), Vertexes.IndexOf(v2)] = null;
                    EdgesCount--;
                    return true;
                }
                return false;
            }
            else
            {
                if (Matrix[Vertexes.IndexOf(v1), Vertexes.IndexOf(v2)] != null)
                {
                    Matrix[Vertexes.IndexOf(v1), Vertexes.IndexOf(v2)] = null;
                    Matrix[Vertexes.IndexOf(v2), Vertexes.IndexOf(v1)] = null;
                    EdgesCount--;
                    return true;
                }
                return false;
            }
        }

        internal class MIteratorAllEdges : Graph<TVertex, TEdge, TData, TWeight>.IteratorAllEdges
        {
            public MIteratorAllEdges(Graph<TVertex, TEdge, TData, TWeight> g)
                : base(g)
            {
                //State = false;
                ItGraph = g;
                //I = 0;
                //J = 0;
            }

            public override bool Beg() //установка на начало
            {
                if (ItGraph.EdgesCount == 0)
                    return false;
                if (ItGraph.Oriented == false) //для неориентированного
                {
                    for (int i = 0; i < ItGraph.Vertexes.Count; i++)
                    {
                        for (int j = 0; j < ItGraph.Vertexes.Count; j++)
                        {
                            if (ItGraph.Matrix[i, j] != null)
                            {
                                if (ItGraph.Matrix[i, j].Vertex1.Index < ItGraph.Matrix[i, j].Vertex2.Index)
                                {
                                    State = true;
                                    I = i;
                                    J = j;
                                    return true;
                                }
                            }
                        }
                    }
                    return false;
                }

                else //для ориентированного
                {
                    for (int i = 0; i < ItGraph.Vertexes.Count; i++)
                    {
                        for (int j = 0; j < ItGraph.Vertexes.Count; j++)
                        {
                            if (ItGraph.Matrix[i, j] != null)
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
                    return false;
                if (ItGraph.Oriented == false) //для неориентированного
                {
                    for (int i = ItGraph.Vertexes.Count - 1; i > -1; i++)
                    {
                        for (int j = ItGraph.Vertexes.Count - 1; j > -1; j++)
                        {
                            if (ItGraph.Matrix[i, j] != null)
                            {
                                if (ItGraph.Matrix[i, j].Vertex1.Index < ItGraph.Matrix[i, j].Vertex2.Index)
                                {
                                    State = true;
                                    I = i;
                                    J = j;
                                    return true;
                                }
                            }
                        }
                    }
                    return false;
                }

                else //для ориентированного
                {
                    for (int i = ItGraph.Vertexes.Count - 1; i > -1; i++)
                    {
                        for (int j = ItGraph.Vertexes.Count - 1; j > -1; j++)
                        {
                            if (ItGraph.Matrix[i, j] != null)
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

            public override void Next()
            {
                if (State == false)
                    return;
                if (ItGraph.Oriented == false) //для неориентированного
                {
                    for (int p = J + 1; p < ItGraph.Vertexes.Count; p++)
                    {
                        if (ItGraph.Matrix[I, p] != null)
                        {
                            if (ItGraph.Matrix[I, p].Vertex1.Index < ItGraph.Matrix[I, p].Vertex2.Index)
                            {
                                J = p;
                                return;
                            }
                        }
                    }

                    for (int i = I + 1; i < ItGraph.Vertexes.Count; i++)
                    {
                        if (i == ItGraph.Vertexes.Count)
                        {
                            State = false;
                            return;
                        }

                        for (int j = 0; j < ItGraph.Vertexes.Count; j++)
                        {
                            if (ItGraph.Matrix[i, j] != null)
                            {
                                if (ItGraph.Matrix[i, j].Vertex1.Index < ItGraph.Matrix[i, j].Vertex2.Index)
                                {
                                    I = i;
                                    J = j;
                                    return;
                                }
                            }
                        }
                    }
                    return;
                }
            }

            public override Edge<TEdge, TWeight, TVertex> Input()
            {
                if (State == false)
                    return null;
                return ItGraph.Matrix[I, J];
            }
        }

        internal class MIteratorOutEdge : IteratorOutEdge
        {
            public MIteratorOutEdge(Vertex<TVertex> v, Graph<TVertex, TEdge, TData, TWeight> g)
                : base(v, g)
            {
                ItVertex = v;
                ItGraph = g;
            }

            public override bool Beg()
            {
                if (ItGraph.EdgesCount == 0)
                {
                    State = false;
                    return false;
                }

                for (int i = 0; i < ItGraph.Vertexes.Count; i++)
                {
                    if (ItGraph.Matrix[ItGraph.Vertexes.IndexOf(ItVertex), i] != null)
                    {
                        J = i;
                        State = true;
                        return true;
                    }
                }
                State = false;
                return false;
            }

            public override bool End()
            {
                if (ItGraph.EdgesCount == 0)
                {
                    State = false;
                    return false;
                }

                for (int i = ItGraph.Vertexes.Count - 1; i > -1; i--)
                {
                    if (ItGraph.Matrix[ItGraph.Vertexes.IndexOf(ItVertex), i] != null)
                    {
                        J = i;
                        State = true;
                        return true;
                    }
                }
                State = false;
                return false;
            }

            public override void Next()
            {
                if (State == false)
                    return;
                for (int i = J + 1; i < ItGraph.Vertexes.Count; i++)
                {
                    if (i == ItGraph.Vertexes.Count - 1)
                    {
                        State = false;
                        return;
                    }

                    else
                    {
                        if (ItGraph.Matrix[ItGraph.Vertexes.IndexOf(ItVertex), i] != null)
                        {
                            J = i;
                            return;
                        }
                    }
                }
                State = false;
                return;
            }

            public override Edge<TEdge, TWeight, TVertex> Input()
            {
                if (State == false)
                    return null;
                return ItGraph.Matrix[ItGraph.Vertexes.IndexOf(ItVertex), J];
            }
        }

        internal class MIteratorInputEdge : IteratorInputEdge
        {
            public MIteratorInputEdge(Vertex<TVertex> v, Graph<TVertex, TEdge, TData, TWeight> g)
                : base(v, g)
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

                for (int i = 0; i < ItGraph.Vertexes.Count; i++)
                {
                    if (ItGraph.Matrix[i, ItGraph.Vertexes.IndexOf(ItVertex)] != null)
                    {
                        State = true;
                        J = i;
                        return true;
                    }
                }
                State = false;
                return false;
            }

            public override bool End()
            {
                if (ItGraph.EdgesCount == 0)
                {
                    State = false;
                    return false;
                }

                for (int i = ItGraph.Vertexes.Count - 1; i > -1; i--)
                {
                    if (ItGraph.Matrix[i, ItGraph.Vertexes.IndexOf(ItVertex)] != null)
                    {
                        State = true;
                        J = i;
                        return true;
                    }
                }
                State = false;
                return false;
            }

            public override void Next()
            {
                for (int j = J + 1; j < ItGraph.Vertexes.Count; j++)
                {
                    if (j == ItGraph.Vertexes.Count)
                    {
                        State = false;
                    }

                    if (ItGraph.Matrix[j, ItGraph.Vertexes.IndexOf(ItVertex)] != null)
                    {
                        J = j;
                        return;
                    }
                }
                State = false;
                return;
            }

            public override Edge<TEdge, TWeight, TVertex> Input()
            {
                if (State == false)
                    return null;
                return ItGraph.Matrix[J, ItGraph.Vertexes.IndexOf(ItVertex)];
            }
        }
    }
}