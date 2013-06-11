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
            Console.WriteLine("0 - выход");
        }

        public static void Menu(GoGraph<TTVertex, TTEdge, TTData, TTWeight> GRAPH)
        {
            int m = int.Parse(Console.ReadLine());
           // GoGraph<TTVertex, TTEdge, TTData, TTWeight> graph = new GoGraph<TTVertex, TTEdge, TTData, TTWeight>();
            switch (m)
            {
                /*case 1:
                    graph = new GoGraph<TTVertex, TTEdge, TTData, TTWeight>();
                    //graph = graph1;
                    break;
                case 2:
                    Console.WriteLine("Введите количество вершин, тип и форму");
                    int v = int.Parse(Console.ReadLine());
                    bool d = bool.Parse(Console.ReadLine());
                    bool f = bool.Parse(Console.ReadLine());
                    graph = new GoGraph<TTVertex, TTEdge, TTData, TTWeight>(v, d, f);
                    break;
                case 3:
                    Console.WriteLine("Введите количество вершин, ребер, тип и форму");
                    int v1 = int.Parse(Console.ReadLine());
                    int e1 = int.Parse(Console.ReadLine());
                    bool d1 = bool.Parse(Console.ReadLine());
                    bool f1 = bool.Parse(Console.ReadLine());
                    graph = new GoGraph<TTVertex, TTEdge, TTData, TTWeight>(v1, e1, d1, f1);

                    break; */
/*
                case 4:
                    {
                        GRAPH.AddVertex();
                        break;
                    }
                case 5:
                    {
                        Console.WriteLine("Введите индексы вершин");
                        int V1 = int.Parse(Console.ReadLine());
                        int V2 = int.Parse(Console.ReadLine());
                        GRAPH.AddEdge(GRAPH.G.Vertexes[V1], GRAPH.G.Vertexes[V2]);
                        break;
                    }
                case 6:
                    {
                        Console.WriteLine("Введите индекс вершины");
                        int V1 = int.Parse(Console.ReadLine());
                       // var v = GRAPH.G.Vertexes[V1];
                        GRAPH.DeleteVertex(GRAPH.G.Vertexes[V1]);
                        break;
                    }
                case 7:
                    {
                        Console.WriteLine("Введите индексы вершин");
                        int V1 = int.Parse(Console.ReadLine());
                        int V2 = int.Parse(Console.ReadLine());
                        GRAPH.DeleteEdge(GRAPH.G.Vertexes[V1], GRAPH.G.Vertexes[V2]);
                        break;
                    }
                case 8:
                    {
                        Console.WriteLine(GRAPH.G.Type.ToString());
                        bool f = GRAPH.G.Type;
                        GRAPH.Reverse();
                        if (GRAPH.G.Type == true)
                            GRAPH.G.Type = false;
                        else
                        {
                            GRAPH.G.Type = true;
                        }
                        Console.WriteLine(GRAPH.G.Type.ToString());
                        break;
                    }
                case 9:
                    {
                        GRAPH.Print();
                        break;
                    }
                case 10:
                    {
                        GRAPH = null;
                        break;
                    }
                case 11:
                    {
                        Console.WriteLine("Итератор вершин");
                        var it = new Graph<TTVertex, TTEdge, TTData, TTWeight>.IteratorVertexes<TTVertex, TTEdge, TTData, TTWeight>(GRAPH.G);
                        Console.WriteLine("1 - установка на начало");
                        Console.WriteLine("2 - установка на конец");
                        Console.WriteLine("3 - следующий");
                        Console.WriteLine("4 - текущий");

                        for (int y = 0; y < 50; y++)
                        {
                            int d = int.Parse(Console.ReadLine());

                            switch (d)
                            {
                                case 1:
                                    it.Beg();
                                    break;
                                case 2:
                                    it.End();
                                    break;
                                case 3:
                                    it.Next();
                                    break;
                                case 4:
                                    if (it.Input() != null)
                                        Console.WriteLine(it.Input().Index.ToString());
                                    else
                                    {
                                        Console.WriteLine("null");
                                    }
                                    break;
                            }
                        }
                    }
                    break;

                case 12:
                    {
                        Console.WriteLine("Итератор исходящих ребер");
                        Console.WriteLine("Введите индекс вершины");
                        int ind = int.Parse(Console.ReadLine());
                        var it = Graph<TTVertex, TTEdge, TTData, TTWeight>.IteratorInputEdge.MakeIt(GRAPH.G.Vertexes[ind], GRAPH.G);
                        Console.WriteLine("1 - установка на начало");
                        Console.WriteLine("2 - установка на конец");
                        Console.WriteLine("3 - следующий");
                        Console.WriteLine("4 - текущий");

                        for (int y = 0; y < 50; y++)
                        {
                            int d = int.Parse(Console.ReadLine());

                            switch (d)
                            {
                                case 1:
                                    it.Beg();
                                    break;
                                case 2:
                                    it.End();
                                    break;
                                case 3:
                                    it.Next();
                                    break;
                                case 4:
                                    if (it.Input() != null)
                                        Console.WriteLine(it.Input().Vertex1.Index.ToString() + it.Input().Vertex2.Index.ToString());
                                    else
                                    {
                                        Console.WriteLine("null");
                                    }
                                    break;
                            }
                        }
                    }
                    break;
            }
        }

        static void Main(string[] args)
        {
            GoGraph<TTVertex, TTEdge, TTData, TTWeight> graph = new GoGraph<TTVertex, TTEdge, TTData, TTWeight>();
            Console.WriteLine("1 - пустой L, 2 - вершины, 3 - ребра");
            int n = int.Parse(Console.ReadLine());
            switch (n)
            {
                case 1:
                    graph = new GoGraph<TTVertex, TTEdge, TTData, TTWeight>();
                    break;
                case 2:
                    Console.WriteLine("Введите количество вершин, тип и форму");
                    int v = int.Parse(Console.ReadLine());
                    bool d = bool.Parse(Console.ReadLine());
                    bool f = bool.Parse(Console.ReadLine());
                    graph = new GoGraph<TTVertex, TTEdge, TTData, TTWeight>(v, d, f);
                    graph.G.Type = f;
                    graph.G.Oriented = d;
                    break;
                case 3:
                    Console.WriteLine("Введите количество вершин, ребер, тип и форму");
                    int v1 = int.Parse(Console.ReadLine());
                    int e1 = int.Parse(Console.ReadLine());
                    bool d1 = bool.Parse(Console.ReadLine());
                    bool f1 = bool.Parse(Console.ReadLine());
                    graph = new GoGraph<TTVertex, TTEdge, TTData, TTWeight>(v1, e1, d1, f1);
                    graph.G.Type = f1;
                    graph.G.Oriented = d1;
                    break;
                default:
                    break;
            }

            PrintMenu();
            for (int i = 0; i < 100; i++)
            {
                Menu(graph);
            }
            
            
            Console.Read();
        }
    }
}
*/

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
             var graph = new GoGraph<TTVertex, TTEdge, TTData, TTWeight>();
             for (int i = 0; i < 10; i++)
             {
                 graph.AddVertex();
             }

             graph.G.Oriented = true;
             graph.AddEdge(graph.G.Vertexes[0], graph.G.Vertexes[1]);
             graph.AddEdge(graph.G.Vertexes[0], graph.G.Vertexes[2]);
             graph.AddEdge(graph.G.Vertexes[1], graph.G.Vertexes[8]);
             graph.AddEdge(graph.G.Vertexes[1], graph.G.Vertexes[3]);
             graph.AddEdge(graph.G.Vertexes[2], graph.G.Vertexes[4]);
             graph.AddEdge(graph.G.Vertexes[3], graph.G.Vertexes[5]);
             graph.AddEdge(graph.G.Vertexes[3], graph.G.Vertexes[7]);
             graph.AddEdge(graph.G.Vertexes[4], graph.G.Vertexes[7]);
             graph.AddEdge(graph.G.Vertexes[5], graph.G.Vertexes[2]);
             graph.AddEdge(graph.G.Vertexes[6], graph.G.Vertexes[4]);
             graph.AddEdge(graph.G.Vertexes[6], graph.G.Vertexes[9]);
             graph.AddEdge(graph.G.Vertexes[9], graph.G.Vertexes[3]);
             graph.AddEdge(graph.G.Vertexes[9], graph.G.Vertexes[8]);

             //graph.AddEdge(graph.G.Vertexes[0], graph.G.Vertexes[3]);
             //graph.AddEdge(graph.G.Vertexes[0], graph.G.Vertexes[1]);
             //graph.AddEdge(graph.G.Vertexes[1], graph.G.Vertexes[2]);
             graph.Print();

             var dag = new DAGSort<TTVertex, TTEdge, TTData, TTWeight>(graph);
             dag.EnterSort();

             Console.ReadLine();
         }
        
    }
}