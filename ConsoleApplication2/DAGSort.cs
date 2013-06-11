using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication2
{
    class DAGSort<TVertex, TEdge, TData, TWeight>
    {
        public List<int[]> QueueSource;
        public GoGraph<TVertex, TEdge, TData, TWeight> DAG;
        public List<int> SortedVertexes; 

        public DAGSort(GoGraph<TVertex, TEdge, TData, TWeight> g)
        {
            DAG = g;
            QueueSource = new List<int[]>();
            SortedVertexes = new List<int>();

            for (int i = 0; i < DAG.G.Vertexes.Count; i++)
            {
                var it = Graph<TVertex, TEdge, TData, TWeight>.IteratorInputEdge.MakeIt(DAG.G.Vertexes[i], DAG.G);//создали итератор входящих вершин
                it.Beg();
                int inputdegree = 0;
                for (int j = 0;; j++)
                {
                    if (it.Input() == null)
                    {
                        break;
                    }
                    inputdegree++;
                    it.Next();
                }
                QueueSource.Add(new int [] {i, inputdegree});
                Console.WriteLine(QueueSource[i][0].ToString() + QueueSource[i][1].ToString());
            }
        }

        public void DAGSorting()
        {
            int i;
            for (i = 0; i < QueueSource.Count; i++)
            {
                if(QueueSource[i][1] <= 0)
                {
                    SortedVertexes.Add(QueueSource[i][0]); //добавили в исток
                    break;
                }

                if(i >= QueueSource.Count)
                    return;
            }

            var itout = Graph<TVertex, TEdge, TData, TWeight>.IteratorOutEdge.MakeIt(DAG.G.Vertexes[QueueSource[i][0]], DAG.G);//создали итератор исходящих вершин
            itout.Beg();

            for (int j = 0;; j++)
            {
                if (itout.Input() == null)
                    break;

                var s1 = DAG.G.Vertexes.IndexOf(itout.Input().Vertex2);//индекс в очереди 
                for (int k = 0; k < QueueSource.Count; k++)
                {
                    if (QueueSource[k][0] == s1)
                    {
                        QueueSource[k][1]--;
                        break;
                    }
                }

                itout.Next();
            }

            //if (i >= QueueSource.Count)
            //    i = QueueSource.Count - 1;
            QueueSource.RemoveAt(i);
        }

        public void EnterSort()
        {
            do DAGSorting(); while (QueueSource.Count != 0);

            for (int i = 0; i < SortedVertexes.Count; i++)
            {
                Console.WriteLine(DAG.G.Vertexes[SortedVertexes[i]].Index.ToString());
            }
        }
    }
}
