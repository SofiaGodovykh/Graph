//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;

//namespace ConsoleApplication2
//{
//     public class IteratorVertexes<TVertex, TEdge, TData, TWeight>:Graph<TVertex, TEdge, TData, TWeight>
//     {
//            public int CurrentCur;
			
//            public IteratorVertexes()
//            {
//            }

//            public Vertex<TVertex> Beg()
//            {
//                if (Vertexes.Count == 0)
//                    return null;
//                else
//                {
//                    CurrentCur = 0;
//                    return Vertexes[0];
//                }
//            }
			
//            public Vertex<TVertex> End()
//            {
//                if (Vertexes.Count == 0)
//                    return null;
//                else
//                {
//                    CurrentCur = Vertexes.Count - 1;
//                    return Vertexes[Vertexes.Count - 1];
//                }
//            }
			
//            public override void ++()
//            {
//                CurrentCur++;
//            }
			
//            public override Vertex<TVertex> *()
//            {
//                if(CurrentCur >= Vertexes.Count)
//                    return null;
//                return Vertexes[CurrentCur];
//            }
//        }
//}
