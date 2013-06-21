

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text   ;
using TTWeight = System.Int32 ;
using TTVertex = System.Int32;
using TTData = System.Int32;
using TTEdge = System.Int32;



namespace ConsoleApplication2
{
    class Program
    {
        static void PrintMenu()
        {
            Console.WriteLine("Выберите пункт меню");
            Console.WriteLine("1 - создать пустой L - граф");
            Console.WriteLine("2 - создать граф из вершин");
            Console.WriteLine("3 - создать граф с вершинами и ребрами");
            Console.WriteLine("4 - добавить вершину");
            Console.WriteLine("5 - добавить ребро");
            Console.WriteLine("6 - удалить вершину");
            Console.WriteLine("7 - удалить ребаро");
            Console.WriteLine("8 - преобразовать граф");
            Console.WriteLine("9 - напечатать граф");
            Console.WriteLine("10 - удалить граф");
            Console.WriteLine("11 - создать итератор вершин");
            Console.WriteLine("12 - создать итератор всех ребер");
            Console.WriteLine("13 - создать итератор исходящих ребер");
            Console.WriteLine("14 - создать итератор входящих ребер");
            Console.WriteLine("15 - коэффициент насыщенности");
            Console.WriteLine("16 - количество ребер");
            Console.WriteLine("17 - количество вершин");
            Console.WriteLine("18 - форма представления");
            Console.WriteLine("19 - тип");
            Console.WriteLine("20 - Сортировка");
            Console.WriteLine("21 - Прим");
            Console.WriteLine("22 - чтение ребра");
            Console.WriteLine("23 - изменение ребра");
            Console.WriteLine("0 - выход");
        }

        public static void Menu(GoGraph<string, string, TTData, float> e)
        {
            for (; true;)
            {
                //var graph = new GoGraph<TTVertex, string, TTData, float>();
                int m = int.Parse(Console.ReadLine());

                switch (m)
                {
                    case 0:
                        return;
                    case 1:
                        e = new GoGraph<string, string, TTData, float>();
                        break;
                    case 2:
                        Console.WriteLine("Введите количество вершин, тип и форму");
                        int v = int.Parse(Console.ReadLine());
                        bool d2 = bool.Parse(Console.ReadLine());
                        bool f2 = bool.Parse(Console.ReadLine());
                        e = new GoGraph<string, string, TTData, float>(v, d2, f2);
                        break;
                    case 3:
                        Console.WriteLine("Введите количество вершин, ребер, тип и форму");
                        int v1 = int.Parse(Console.ReadLine());
                        
                        int e1 = int.Parse(Console.ReadLine());
                        bool d1 = bool.Parse(Console.ReadLine());
                        bool f1 = bool.Parse(Console.ReadLine());
                        e = new GoGraph<string, string, TTData, float>(v1, e1, d1, f1);

                        var it = Graph<string, string, TTData, float>.IteratorAllEdges.MakeIt(e.G);
                        it.Beg();

                        int y1 = 9;
                        Random r = new Random();
                        for (; it.Input() != null; it.Next())
                        {
                            it.Input().Weight = r.Next(y1);
                        }
                            break;

                    case 4:
                        {
                            var t = e.G.AddVertex();
                            Console.WriteLine("Добавлена вершина с индексом " + t.Index.ToString());
                            break;
                        }

                    case 5:
                        {
                            Console.WriteLine("Введите номера вершин");
                            int V1 = int.Parse(Console.ReadLine());
                            int V2 = int.Parse(Console.ReadLine());

                            if (V1 >= e.G.Vertexes.Count || V2 >= e.G.Vertexes.Count || V1 == V2)
                            {
                                Console.WriteLine("Неверный номер");
                                break;
                            }

                            else
                            {
                                var t = e.G.AddEdge(e.G.Vertexes[V1], e.G.Vertexes[V2]);
                                if(t == null)
                                { 
                                    Console.WriteLine("Между вершинами уже есть ребро");
                                    break;
                                }
                                Console.WriteLine("Добавлено ребро");
                                break;
                            }
                        }

                    case 6:
                        {
                            Console.WriteLine("Введите номер вершины");
                            int V1 = int.Parse(Console.ReadLine());
                            // var v = GRAPH.G.Vertexes[V1];
                            if (V1 >= e.G.Vertexes.Count)
                            {
                                Console.WriteLine("Неверная вершина");
                                break;
                            }

                            e.G.DeleteVertex(e.G.Vertexes[V1]);
                            break;
                        }

                    case 7:
                        {
                            Console.WriteLine("Введите номера вершин");
                            int V1 = int.Parse(Console.ReadLine());
                            int V2 = int.Parse(Console.ReadLine());

                            if (V1 >= e.G.Vertexes.Count || V2 >= e.G.Vertexes.Count || V1 == V2)
                            {
                                Console.WriteLine("Неверный номер");
                                break;
                            }

                            e.G.DeleteEdge(e.G.Vertexes[V1], e.G.Vertexes[V2]);
                            break;
                        }
                    case 8:
                        {
                            if (e.G == null)
                            {
                                Console.WriteLine("null");
                                break;
                            }
                            e.Reverse();
                            Console.WriteLine("Граф преобразован к типу " + e.G.Type.ToString());
                            break;
                        }
                    case 9:
                        {
                            if (e.G == null)
                            {
                                Console.WriteLine("null");
                                break;
                            }
                            e.Print();
                            break;
                        }
                    case 10:
                        {
                            e = null;
                            break;
                        }
                    case 11:
                        {
                            Console.WriteLine("Итератор вершин");
                            ItAllVer(e);
                            break;
                        }


                    case 12:
                        {
                            Console.WriteLine("Итератор всех ребер");
                            ItAllEdges(e);
                            break;
                        }
                    case 13:
                        {
                            Console.WriteLine("Итератор исходящих ребер");
                            ItOutEdge(e);
                            break;
                        }

                    case 14:
                        {
                            Console.WriteLine("Итератор входящих ребер");
                            ItInEdge(e);
                            break;
                        }
                    case 15:
                        {
                            Console.WriteLine(e.G.Saturation().ToString());
                            break;
                        }
                    case 16:
                        {
                            Console.WriteLine(e.G.EdgesCount.ToString());
                            break;
                        }
                    case 17:
                        {
                            Console.WriteLine(e.G.Vertexes.Count.ToString());
                            break;
                        }
                    case 18:
                        {
                            Console.WriteLine(e.G.Type.ToString());
                            break;
                        }
                    case 19:
                        {
                            Console.WriteLine(e.G.Oriented);
                            break;
                        }
                    case 20:
                        {
                            Console.WriteLine("Сортировка");
                            var Task1 = new DAGSort<string, string, TTData, float>(e);
                            Task1.EnterSort();
                            break;
                        }
                    case 21:
                        {
                            Console.WriteLine("Алгоритм Прима");
                            Console.WriteLine("Введите номер истока");
                            int y = int.Parse(Console.ReadLine());
                            if (y >= e.G.Vertexes.Count)
                            {
                                Console.WriteLine("Неверный номер");
                                break;
                            }

                            var Task2 = new Prim<string, string, TTData, float>(e, e.G.Vertexes[y]);
                            Task2.MST_Prim();
                            break;
                        }
                    case 22:
                        {
                            Console.WriteLine("Введите номера вершин");
                            int vv1 = int.Parse(Console.ReadLine());
                            int vv2 = int.Parse(Console.ReadLine());
                            if (vv1 >= e.G.Vertexes.Count || vv2 >= e.G.Vertexes.Count)
                            {
                                Console.WriteLine("Неверные вершины");
                                break;
                            }
                            var t = e.G.GetEdge(e.G.Vertexes[vv1], e.G.Vertexes[vv2]);
                            if (t == null)
                            {
                                Console.WriteLine("null");
                                break;
                            }
                            Console.WriteLine("Из вершины " + t.Vertex1.Index + " в вершину " + t.Vertex2.Index + " вес " + t.Weight);
                            break;
                        }
                    case 23:
                        {
                            Console.WriteLine("Введите номера вершин");
                            int vv1 = int.Parse(Console.ReadLine());
                            int vv2 = int.Parse(Console.ReadLine());

                            Console.WriteLine("Введите вес");
                            float ww1 = float.Parse(Console.ReadLine());
                            
                            if (vv1 >= e.G.Vertexes.Count || vv2 >= e.G.Vertexes.Count)
                            {
                                Console.WriteLine("Неверные вершины");
                                break;
                            }

                            if (e.G.GetEdge(e.G.Vertexes[vv1], e.G.Vertexes[vv2]) != null)
                            {
                                e.G.GetEdge(e.G.Vertexes[vv1], e.G.Vertexes[vv2]).Weight = ww1;
                                Console.WriteLine("Вес изменен");
                            }

                            else
                            {
                                Console.WriteLine("null");
                                break;
                            }
                          //  Console.WriteLine("Из вершины " + t.Vertex1.Index + " в вершину " + t.Vertex2.Index + " вес " + t.Weight + "данные " + t.Data);
                            break;
                        }
                }
            }
        }

        public static void ItAllEdges(GoGraph<string, string, TTData, float> e)
        {
            var it = Graph<string, string, TTData, float>.IteratorAllEdges.MakeIt(e.G); //создали итератор
            Console.WriteLine("1 - установить на начало");
            Console.WriteLine("2 - установить на конец");
            Console.WriteLine("3 - следующий");
            Console.WriteLine("4 - чтение");
            Console.WriteLine("5 - запись");
            Console.WriteLine("6 - выход");

            for (; true;)
            {
                int k = int.Parse(Console.ReadLine());
                switch (k)
                {
                    case 1:
                        {
                            it.Beg();
                            break;
                        }
                    case 2:
                        {
                            it.End();
                            break;
                        }
                    case 3:
                        {
                            it.Next();
                            break;
                        }
                    case 4:
                        {
                            var a = it.Input();
                            if(a != null)
                            { 
                                Console.WriteLine("Исходящая " + a.Vertex1.Index.ToString() + " входящая " +
                                              a.Vertex2.Index.ToString() + " с весом " + a.Weight);
                            }

                            else
                            {
                                Console.WriteLine("null");
                            }
                            break;
                        }
                    case 5:
                        {
                            if (it.Input() == null)
                            {
                                Console.WriteLine("null");
                                break;
                            }

                            Console.WriteLine("Введите вес");
                            float k1 = float.Parse(Console.ReadLine());
                            Console.WriteLine("Введите данные");
                            string k2 = Console.ReadLine();
                            it.Input().Data = k2;
                            it.Input().Weight = k1;
                            Console.WriteLine("Данные изменены");
                            break;
                        }
                    case 6:
                        {
                            return;
                            break;
                        }
                    default:
                        break;
                }
            }
        }

        public static void ItInEdge(GoGraph<string, string, TTData, float> e)
        {
            Console.WriteLine("Введите номер вершины");
            int k5 = int.Parse(Console.ReadLine());
            var it = Graph<string, string, TTData, float>.IteratorInputEdge.MakeIt(e.G.Vertexes[k5], e.G); //создали итератор
            Console.WriteLine("1 - установить на начало");
            Console.WriteLine("2 - установить на конец");
            Console.WriteLine("3 - следующий");
            Console.WriteLine("4 - чтение");
            Console.WriteLine("5 - запись");
            Console.WriteLine("6 - выход");

            for (; true; )
            {
                int k = int.Parse(Console.ReadLine());
                switch (k)
                {
                    case 1:
                        {
                            it.Beg();
                            break;
                        }
                    case 2:
                        {
                            it.End();
                            break;
                        }
                    case 3:
                        {
                            it.Next();
                            break;
                        }
                    case 4:
                        {
                            var a = it.Input();
                            Console.WriteLine("Исходящая " + a.Vertex1.Index.ToString() + " входящая " +
                                              a.Vertex2.Index.ToString() + " с весом " + a.Weight.ToString() +
                                              " и данными " + a.Data.ToString());
                            break;
                        }
                    case 5:
                        {
                            if (it.Input() == null)
                            {
                                Console.WriteLine("null");
                                break;
                            }

                            Console.WriteLine("Введите вес");
                            float k1 = float.Parse(Console.ReadLine());
                            Console.WriteLine("Введите данные");
                            string k2 = Console.ReadLine();
                            it.Input().Data = k2;
                            it.Input().Weight = k1;
                            Console.WriteLine("Данные изменены");
                            break;
                        }
                    case 6:
                        {
                            return;
                            break;
                        }
                    default:
                        break;
                }
            }
        }

        public static void ItOutEdge(GoGraph<string, string, TTData, float> e)
        {
            Console.WriteLine("Введите номер вершины");
            int k5 = int.Parse(Console.ReadLine());
            var it = Graph<string, string, TTData, float>.IteratorOutEdge.MakeIt(e.G.Vertexes[k5], e.G); //создали итератор
            Console.WriteLine("1 - установить на начало");
            Console.WriteLine("2 - установить на конец");
            Console.WriteLine("3 - следующий");
            Console.WriteLine("4 - чтение");
            Console.WriteLine("5 - запись");
            Console.WriteLine("6 - выход");

            for (; true; )
            {
                int k = int.Parse(Console.ReadLine());
                switch (k)
                {
                    case 1:
                        {
                            it.Beg();
                            break;
                        }
                    case 2:
                        {
                            it.End();
                            break;
                        }
                    case 3:
                        {
                            it.Next();
                            break;
                        }
                    case 4:
                        {
                            var a = it.Input();
                            Console.WriteLine("Исходящая " + a.Vertex1.Index.ToString() + " входящая " +
                                              a.Vertex2.Index.ToString() + " с весом " + a.Weight.ToString() +
                                              " и данными " + a.Data.ToString());
                            break;
                        }
                    case 5:
                        {
                            if (it.Input() == null)
                            {
                                Console.WriteLine("null");
                                break;
                            }

                            Console.WriteLine("Введите вес");
                            float k1 = float.Parse(Console.ReadLine());
                            Console.WriteLine("Введите данные");
                            string k2 = Console.ReadLine();
                            it.Input().Data = k2;
                            it.Input().Weight = k1;
                            Console.WriteLine("Данные изменены");
                            break;
                        }
                    case 6:
                        {
                            return;
                            break;
                        }
                    default:
                        break;
                }
            }
        }

        public static void ItAllVer(GoGraph<string, string, TTData, float> e)
        {
            var it = new Graph<string, TTEdge, TTData, TTWeight>.IteratorVertexes
                                <string, string, TTData, float>(e.G);
            Console.WriteLine("1 - установить на начало");
            Console.WriteLine("2 - установить на конец");
            Console.WriteLine("3 - следующий");
            Console.WriteLine("4 - чтение");
            Console.WriteLine("5 - запись");
            Console.WriteLine("6 - выход");

            for (; true; )
            {
                int k = int.Parse(Console.ReadLine());
                switch (k)
                {
                    case 1:
                        {
                            it.Beg();
                            break;
                        }
                    case 2:
                        {
                            it.End();
                            break;
                        }
                    case 3:
                        {
                            it.Next();
                            break;
                        }
                    case 4:
                        {
                            var a = it.Input();
                            if (a == null)
                            {
                                Console.WriteLine("null");
                                break;
                            }
                            Console.WriteLine("Индекс " + a.Index.ToString() + " с именем " + a.Name.ToString() + " с данными " + a.Data.ToString());
                            break;
                        }
                    case 5:
                        {
                            if (it.Input() == null)
                            {
                                Console.WriteLine("null");
                                break;
                            }

                            Console.WriteLine("Введите данные");
                            string k2 = Console.ReadLine();
                            Console.WriteLine("Введите имя");
                            string k3 = Console.ReadLine();
                            it.Input().Name = k3;
                            it.Input().Data = k2;
                            Console.WriteLine("Данные изменены");
                            break;
                        }
                    case 6:
                        {
                            return;
                            break;
                        }
                    default:
                        break;
                }
            }
        }

        private static void Main(string[] args)
        {
            GoGraph<string, string, TTData, float> MainGraph = new GoGraph<string, string, TTData, float>();
            PrintMenu();
            Menu(MainGraph);
        }

       // Console.Read();
        }
    }


/*
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text   ;
using TTWeight = System.Int32 ;
using TTVertex = System.Int32;
using TTData = System.Int32;
using TTEdge = System.Int32;



namespace ConsoleApplication2
{
    class Program
    {
         static void Main(string[] args)
         {
             var graph = new GoGraph<TTVertex, TTEdge, TTData, float>();
             graph.G.Oriented = false;
             graph.G.Weighted = true;

             for (int i = 0; i < 9; i++)
             {
                 graph.AddVertex();
             }

             graph.AddEdge(graph.G.Vertexes[0], graph.G.Vertexes[1]).Weight = 4;
             graph.AddEdge(graph.G.Vertexes[0], graph.G.Vertexes[8]).Weight = 8;
             graph.AddEdge(graph.G.Vertexes[1], graph.G.Vertexes[8]).Weight = 11;
             graph.AddEdge(graph.G.Vertexes[1], graph.G.Vertexes[2]).Weight = 8;
             graph.AddEdge(graph.G.Vertexes[2], graph.G.Vertexes[7]).Weight = 2;
             graph.AddEdge(graph.G.Vertexes[2], graph.G.Vertexes[5]).Weight = 4;
             graph.AddEdge(graph.G.Vertexes[2], graph.G.Vertexes[3]).Weight = 7;
             graph.AddEdge(graph.G.Vertexes[3], graph.G.Vertexes[5]).Weight = 14;
             graph.AddEdge(graph.G.Vertexes[3], graph.G.Vertexes[4]).Weight = 9;
             graph.AddEdge(graph.G.Vertexes[4], graph.G.Vertexes[5]).Weight = 10;
             graph.AddEdge(graph.G.Vertexes[5], graph.G.Vertexes[6]).Weight = 2;
             graph.AddEdge(graph.G.Vertexes[6], graph.G.Vertexes[7]).Weight = 6;
             graph.AddEdge(graph.G.Vertexes[6], graph.G.Vertexes[8]).Weight = 1;
             graph.AddEdge(graph.G.Vertexes[7], graph.G.Vertexes[8]).Weight = 7;

             var pr = new Prim<TTVertex, TTEdge, TTData, float>(graph);

             pr.MST_Prim(pr.PrimGraph.G.Vertexes[0]);

             for (int i = 0; i < pr.Spanning.Count; i++)
             {
                 Console.WriteLine(pr.Spanning[i].Pr.ToString() + " " + pr.Spanning[i].V.Index.ToString());
             }
                 //var graph = new GoGraph<TTVertex, TTEdge, TTData, TTWeight>();
                 //for (int i = 0; i < 10; i++)
                 //{
                 //    graph.AddVertex();
                 //}

                 //graph.G.Oriented = true;
                 //graph.AddEdge(graph.G.Vertexes[0], graph.G.Vertexes[1]);
                 //graph.AddEdge(graph.G.Vertexes[0], graph.G.Vertexes[2]);
                 //graph.AddEdge(graph.G.Vertexes[1], graph.G.Vertexes[8]);
                 //graph.AddEdge(graph.G.Vertexes[1], graph.G.Vertexes[3]);
                 //graph.AddEdge(graph.G.Vertexes[2], graph.G.Vertexes[4]);
                 //graph.AddEdge(graph.G.Vertexes[3], graph.G.Vertexes[5]);
                 //graph.AddEdge(graph.G.Vertexes[3], graph.G.Vertexes[7]);
                 //graph.AddEdge(graph.G.Vertexes[4], graph.G.Vertexes[7]);
                 //graph.AddEdge(graph.G.Vertexes[5], graph.G.Vertexes[2]);
                 //graph.AddEdge(graph.G.Vertexes[6], graph.G.Vertexes[4]);
                 //graph.AddEdge(graph.G.Vertexes[6], graph.G.Vertexes[9]);
                 //graph.AddEdge(graph.G.Vertexes[9], graph.G.Vertexes[3]);
                 //graph.AddEdge(graph.G.Vertexes[9], graph.G.Vertexes[8]);

                 ////graph.AddEdge(graph.G.Vertexes[0], graph.G.Vertexes[3]);
                 ////graph.AddEdge(graph.G.Vertexes[0], graph.G.Vertexes[1]);
                 ////graph.AddEdge(graph.G.Vertexes[1], graph.G.Vertexes[2]);
                 //graph.Print();

                 //var dag = new DAGSort<TTVertex, TTEdge, TTData, TTWeight>(graph);
                 //dag.EnterSort();

                 //PriorityQueue Q = new PriorityQueue();
                 //Q.Enqueue(10, 0);
                 //Q.Enqueue(18,0);
                 //Q.Enqueue(8, 0);
                 //Q.Enqueue(5, 0);
                 //Q.Enqueue(9, 0);
                 //Q.Enqueue(10, 0);
                 //Q.Enqueue(6, 0);
                 //Q.Enqueue(9, 0);
                 //Q.Enqueue(3, 0);
                 //Q.Enqueue(10, 0);
                 //Q.Enqueue(19, 0);

                 //for (int i = 0; i < Q.PrioQueue.Count; i++)
                 //{
                 //    Console.WriteLine(Q.PrioQueue[i][0]);
                 //}

                 //Console.WriteLine();

                 //while (Q.PrioQueue.Count != 1)
                 //{
                 //    Console.WriteLine(Q.Dequeue()[0]);
                 //}
                 Console.ReadLine();
         }
        
    }
} */