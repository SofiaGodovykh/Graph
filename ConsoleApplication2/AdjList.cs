using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication2
{
    internal class AdjList<TVertex, TEdge, TData, TWeight>
    {
        public int AdjIndex;

        public int Length;

        public Node Head;

        public class Node
        {
            public Node Next;
            public Edge<TEdge, TWeight, TVertex> AdjEdge;

            public Node()
            {
                AdjEdge = null;
                Next = null;
            }

            public Node(Edge<TEdge, TWeight, TVertex> edge)
            {
                AdjEdge = edge;
                Next = null;
            }
        }

        public AdjList()
        {
            Length = 0;
            Head = null;
        }

        public bool AddNode(Edge<TEdge, TWeight, TVertex> edge)
        {
            if (Length == 0)
            {
               // Head.Next = new Node(edge);
                Head = new Node(edge);
                //Head.AdjEdge = edge;
                Length++;
                return true;
            }

            Node q = Head;
            if ((q.AdjEdge.Vertex1.Index == edge.Vertex1.Index) && q.AdjEdge.Vertex2.Index == edge.Vertex2.Index)
                return false;
            for (int i = 0; i < Length - 1; i++)
            {
                if ((q.AdjEdge.Vertex1.Index == edge.Vertex1.Index) && q.AdjEdge.Vertex2.Index == edge.Vertex2.Index)
                    return false;
                q = q.Next;
            }
            if ((q.AdjEdge.Vertex1.Index == edge.Vertex1.Index) && q.AdjEdge.Vertex2.Index == edge.Vertex2.Index)
            {
                return false;
            }
            else
            {
                q.Next = new Node(edge);
                Length++;
                return true;
            }
        }

        public bool DeleteNode(Vertex<TVertex> vertex1, Vertex<TVertex> vertex2)
        {
            if (Length == 0)
                return false;

            if ((Length == 1) && ((((Head.AdjEdge.Vertex2.Index == vertex2.Index) && (Head.AdjEdge.Vertex1.Index == vertex1.Index)) || ((Head.AdjEdge.Vertex2.Index == vertex1.Index && Head.AdjEdge.Vertex1.Index == vertex2.Index)))))
            {
                Length--;
                Head = null;
                return true;
            }

            Node q = Head;
            if (((Head.AdjEdge.Vertex2.Index == vertex2.Index) && (Head.AdjEdge.Vertex1.Index == vertex1.Index)) || ((Head.AdjEdge.Vertex2.Index == vertex1.Index && Head.AdjEdge.Vertex1.Index == vertex2.Index)))
            {
                if (q.Next == null)
                {
                    q = null;
                    Length--;
                    return true;
                }
                Head = q.Next;
                Length--;
                return true;
            }
            for (int i = 0; i < Length - 1; i++)
            {
                if (q == null)//не нашли
                    return false;

                if ((((q.Next.AdjEdge.Vertex2.Index == vertex2.Index) && (q.Next.AdjEdge.Vertex1.Index == vertex1.Index)) || ((q.Next.AdjEdge.Vertex2.Index == vertex1.Index && q.Next.AdjEdge.Vertex1.Index == vertex2.Index))) && (q.Next.Next == null))//если стоит в конце
                {
                    q.Next = null;
                    Length--;
                    return true;
                }

                if ((q.Next.AdjEdge.Vertex2.Index == vertex2.Index) && (q.Next.AdjEdge.Vertex1.Index == vertex1.Index) || (q.Next.AdjEdge.Vertex2.Index == vertex1.Index && q.Next.AdjEdge.Vertex1.Index == vertex2.Index))//если не в конце
                {
                    q.Next = q.Next.Next;
                    Length--;
                    return true;
                }

                q = q.Next;
            }
            return false;
        }

        public Node Prev(Node q)
        {
            if (Length == 1)
                return null;
            Node p = Head;
            for (int i = 0; i < Length; i++)
            {
                if (p == null)
                    return null;
                if (p.Next == null)
                    return null;
                if (p.Next.AdjEdge.Vertex1.Index == q.AdjEdge.Vertex1.Index && p.Next.AdjEdge.Vertex2.Index == q.AdjEdge.Vertex2.Index)
                    return p;
                p = p.Next;
            }
            return null;
        }
    }
}
