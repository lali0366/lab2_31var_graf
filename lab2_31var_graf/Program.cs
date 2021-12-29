namespace Graf
{
    class Program
    {
        static void Main(string[] args)
        {
            var graph = new List<List<List<int>>> { };

            Console.Write("Сколько вершин есть у графа? ");

            int N = int.Parse(Console.ReadLine());

            for (int i = 0; i < N; i++)
            {
                graph.Add(new List<List<int>> { });

                Console.Write($"Сколько смежных с {i} вершин? ");

                int M = int.Parse(Console.ReadLine());

                for (int j = 0; j < M; j++)
                {
                    Console.WriteLine("Введите номер смежной вершины ");

                    int temp1 = int.Parse(Console.ReadLine());

                    Console.WriteLine("Введите расстояние до вершины");

                    int temp2 = int.Parse(Console.ReadLine());

                    graph[i].Add(new List<int> { temp1, temp2 });
                }
            }

            //ВЫВОД СПИСКА ВСЕХ ВЕРШИН ПОСЛЕ ЗАПОЛНЕНИЯ В РУЧНУЮ И ИХ РАССТОЯНИЯ ДО ДРУГ ДРУГА
            List<int> se = new List<int> { };

            for (int i = 0; i < graph.Count(); i++)
            {
                se.Add(0);
            }
            for (int i = 0; i < graph.Count(); i++)
            {
                Console.Write($"Вершина {i}: ");

                foreach (List<int> templ in graph[i])
                {
                    Console.Write($"{templ[0]}({templ[1]}) ");
                }

                Console.WriteLine();
            }

            Console.WriteLine("---------------------------");

            for (int i = 0; i < graph.Count(); i++)
            {
                Console.Write($" {i}");
            }

            Console.WriteLine();

            //ВЫВОД МАТРИЦЫ 
            List<int> xok = new List<int> { };

            for (int i = 0; i < graph.Count(); i++)
            {
                Dictionary<int, int> temp = BreadthFirstSearch(graph, i);

                for (int j = 0; j < graph.Count(); j++)
                {

                    if (se[j] != -1)
                    {
                        if (temp[j] == -1)
                        {
                            se[j] = -1;
                        }

                        else
                        {
                            if (se[j] < temp[j])
                            {
                                se[j] = temp[j];
                            }
                        }
                    }
                }
            }

            Console.WriteLine("------------------------");

            //ВНЕШНИЙ РАДИУС РАСЧЁТЫ 
            foreach (int es in se)
            {
                if (es != -1)
                {
                    xok.Add(es);
                }
            }

            if (xok.Count() == 0)
            {
                Console.WriteLine("Невозможно найти радиус, так как у орграфа отсутствует центр");
            }

            else
            {
                List<int> center = new List<int> { };

                int rad = xok.Min();

                for (int i = 0; i < se.Count(); i++)
                {
                    if (se[i] == rad)
                        center.Add(i);
                }

                if (center.Count() == 1)
                {
                    Console.WriteLine($"Радиус орграфа равен {xok.Min()}, центр графа - вершина {center[0]}");

                    Console.WriteLine($"Внешний радиус графа равен {BreadthFirstSearch(graph, center[0]).Values.Max()}");
                }

                else
                {
                    int vnerad = 0;

                    Console.Write($"Радиус орграфа равен {xok.Min()}, центр графа - вершина ");

                    foreach (int elem in center)
                    {
                        Console.Write(elem + " ");

                        if (vnerad < BreadthFirstSearch(graph, elem).Values.Max())
                        {
                            vnerad = BreadthFirstSearch(graph, elem).Values.Max();
                        }
                    }

                    Console.WriteLine();
                    Console.WriteLine($"Внешний радиус графа равен {vnerad}");
                }
            }

            Console.ReadKey();
        }

        //ПОИСК В ШИРИНУ
        static Dictionary<int, int> BreadthFirstSearch(List<List<List<int>>> graph, int start)
        {
            var path = new Dictionary<int, int>();

            var S = new Dictionary<int, int>();

            for (int i = 0; i < graph.Count; i++)
            {
                path.Add(i, 0);
            }

            var queue = new Queue<int>();

            var visited = new List<bool>(new bool[graph.Count]);

            for (int i = 0; i < graph.Count(); i++)
            {
                S.Add(i, -1);
            }

            queue.Enqueue(start);

            visited[start] = true;

            int k = 0;

            S[start] = k;

            int oldv = -1;

            while (queue.Count > 0)
            {
                int d = queue.Dequeue();

                if ((d == start) || (S[d] != S[oldv]))
                {
                    k++;
                }

                foreach (List<int> templ in graph[d])
                {

                    int ver = templ[0];

                    if (path[ver] > (path[d] + templ[1]))
                    {
                        path[ver] = path[d] + templ[1];
                    }

                    if (!visited[ver])
                    {
                        path[ver] = path[d] + templ[1];

                        queue.Enqueue(ver);

                        visited[ver] = true;

                        S[ver] = k;
                    }

                    oldv = d;
                }
            }

            for (int i = 0; i < path.Count(); i++)
            {
                if ((path[i] == 0) && (i != start))
                {
                    path[i]--;
                }
            }

            Console.WriteLine(start + " " + String.Join(" ", path.Values));

            return path;
        }
    }
}