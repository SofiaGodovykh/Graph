using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication2
{
    class PriorityQueue
    {
        public List<QueueNode> PrioQueue;

        public class QueueNode
        {
            public float PriorityWeight;
            public int Parent;
            public int QueIndex;
            public QueueNode(float p, int n)
            {
                PriorityWeight = p;
                Parent = n;
            }
        }

        public int QueueLength;

        public PriorityQueue()
        {
            PrioQueue = new List<QueueNode>();
            QueueLength = 0;
            var e = new QueueNode(-100, -100);
            e.QueIndex = -100;
            PrioQueue.Add(e);
        }

        public void Enqueue(QueueNode q)
        {
            QueueLength++;
            PrioQueue.Add(q);
            int i = QueueLength;
            QueueNode temp;
            while (i > 1 && PrioQueue[i].PriorityWeight < PrioQueue[(i/2)].PriorityWeight)
            {
                temp = PrioQueue[i];
                PrioQueue[i] = PrioQueue[i/2];
                PrioQueue[i/2] = temp;
                i = i/2;
            }
            return;
        }

        public QueueNode Dequeue()
        {
            if (QueueLength == 0)
                return null;
            var x = PrioQueue[1];
            PrioQueue[1] = PrioQueue[PrioQueue.Count - 1];
            QueueLength--;
            int i = 1;
            int j;
            QueueNode temp;
            while (i <= QueueLength/2)
            {
                if (2*i < QueueLength && PrioQueue[2*i + 1].PriorityWeight < PrioQueue[2*i].PriorityWeight)
                    j = 2*i + 1;
                j = 2*i;

                if (PrioQueue[i].PriorityWeight > PrioQueue[j].PriorityWeight)
                {
                    temp = PrioQueue[i];
                    PrioQueue[i] = PrioQueue[j];
                    PrioQueue[j] = temp;
                    i = j;
                }

                else
                {
                    i = QueueLength;
                }
            }
            PrioQueue.RemoveAt(PrioQueue.Count - 1);
            //QueueLength--;
            return x;
        }

        public int IfContains(int ind)
        {
            for (int i = 1; i < PrioQueue.Count; i++)
            {
                if (PrioQueue[i].QueIndex == ind)
                    return i;
            }
            return -1;
        }

        //public void Pull(int ind)
        //{
        //    PrioQueue.RemoveAt(ind);
        //}
    }
}
