using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication2
{
    class PriorityQueue<TWeight>
    {
        public List<QueueNode<TWeight>> PrioQueue;

        public class QueueNode<TWeight>
        {
            public TWeight Pr;
            public int Number;

            public QueueNode(TWeight p, int n)
            {
                Pr = p;
                Number = n;
            }
        }

        public int QueueLength;

        public PriorityQueue()
        {
            PrioQueue = new List<QueueNode<TWeight>>();
            QueueLength = 0;
            PrioQueue.Add(new QueueNode<TWeight> (default(TWeight), -1));
        }

        public void Enqueue(TWeight prio, int number)
        {
            QueueLength++;
            PrioQueue.Add(new QueueNode<TWeight> (prio, number));
            int i = QueueLength;
            QueueNode<TWeight> temp;
            while (i > 1 && PrioQueue[i].Pr < PrioQueue[(i/2)].Pr)
            {
                temp = PrioQueue[i];
                PrioQueue[i] = PrioQueue[i/2];
                PrioQueue[i/2] = temp;
                i = i/2;
            }
            return;
        }

        public QueueNode<TWeight> Dequeue()
        {
            if (QueueLength == 0)
                return null;
            var x = PrioQueue[1];
            PrioQueue[0] = PrioQueue[PrioQueue.Count - 1];
            int i = 1;
            int j;
            QueueNode<TWeight> temp;
            while (i < PrioQueue.Count/2)
            {
                if (2*i < QueueLength && PrioQueue[2*i + 1].Pr < PrioQueue[2*i].Pr)
                    j = 2*i + 1;
                j = 2*i;

                if (PrioQueue[i].Pr > PrioQueue[j].Pr)
                {
                    temp = PrioQueue[i];
                    PrioQueue[i] = PrioQueue[j];
                    PrioQueue[j] = temp;
                    i = j;
                }

                else
                {
                    i = PrioQueue.Count - 1;
                }
            }
            PrioQueue.RemoveAt(1);
            return x;
        }
    }
}
