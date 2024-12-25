using System;
using System.Collections.Generic;
using System.Text;

namespace RudenkoEx2
{
        public class Graph
        {
            public double[,] adjacencyMatrix;
            public string[] vertices;
            public int vertexCount;

            public Graph(int size)
            {
                vertexCount = size;
                adjacencyMatrix = new double[size, size];
                vertices = new string[size];
                for (int i = 0; i < size; i++)
                    for (int j = 0; j < size; j++)
                        adjacencyMatrix[i, j] = int.MaxValue;
                for (int i = 0; i < size; i++)
                    adjacencyMatrix[i, i] = 0;
            }
            //public void InputGraph()
            //{
            //    Console.WriteLine("Введите названия вершин:");

            //    for (int i = 0; i < vertexCount; i++)
            //    {
            //        string vertexName;
            //        while (true)
            //        {
            //            Console.Write($"Вершина {i + 1}: ");
            //            vertexName = Console.ReadLine();
            //        if (!string.IsNullOrWhiteSpace(vertexName))
            //        {
            //            vertices[i] = vertexName;
            //            break;
            //        }
            //        else
            //            Console.WriteLine("Введите корректное название.");
            //        }
            //    }

            //    Console.WriteLine("Введите количество рёбер:");
            //    double edgeCount;
            //    while (!double.TryParse(Console.ReadLine(), out edgeCount) || edgeCount <= 0)
            //    {
            //        Console.WriteLine("Введите корректное положительное число для количества рёбер.");
            //    }

            //    for (int i = 0; i < edgeCount; i++)
            //    {
            //        Console.WriteLine($"Ребро {i + 1}:");

            //        string startVertex;
            //        string endVertex;
            //        double weight;
            //        while (true)
            //        {
            //            Console.Write("Введите начальную вершину: ");
            //            startVertex = Console.ReadLine();
            //            if (Array.IndexOf(vertices, startVertex) != -1)
            //                break;
            //            else
            //                Console.WriteLine("Начальная вершина не найдена, введите существующую вершину.");
            //        }
            //        while (true)
            //        {
            //            Console.Write("Введите конечную вершину: ");
            //            endVertex = Console.ReadLine();
            //            if (Array.IndexOf(vertices, endVertex) != -1)
            //                break;
            //            else
            //                Console.WriteLine("Конечная вершина не найдена, введите существующую вершину.");
            //        }
            //        while (true)
            //        {
            //            Console.Write("Введите вес ребра: ");
            //            if (double.TryParse(Console.ReadLine(), out weight) && weight > 0)
            //                break;
            //            else
            //                Console.WriteLine("Вес ребра должен быть положительным числом, введите корректное значение.");
            //        }

            //        int startIndex = Array.IndexOf(vertices, startVertex);
            //        int endIndex = Array.IndexOf(vertices, endVertex);

            //        if (startIndex != -1 && endIndex != -1)
            //        {
            //        //adjacencyMatrix[startIndex, endIndex] = weight;
            //    }
            //    }
            //}
            public void InputGraph(string[] vertexNames, double[,] adjacencyMatrix)
            {
                vertices = vertexNames;
                this.adjacencyMatrix = adjacencyMatrix;
            }
            public string[] InputVertices(int size)
            {
                Console.WriteLine("Введите названия вершин для графа:");
                string[] vertexNames = new string[size];
                for (int i = 0; i < size; i++)
                {
                    string vertexName;
                    while (true)
                    {
                        Console.Write($"Вершина {i + 1}: ");
                        vertexName = Console.ReadLine();
                        if (!string.IsNullOrWhiteSpace(vertexName))
                        {
                            vertexNames[i] = vertexName;
                            break;
                        }
                        else
                            Console.WriteLine("Введите корректное название.");
                    }
                }
                return vertexNames;
            }
            public void PrintAdjacencyMatrix()
            {
                Console.WriteLine("Ваша матрица смежности:");
                for (int i = 0; i < vertexCount; i++)
                {
                    for (int j = 0; j < vertexCount; j++)
                        Console.Write($"{adjacencyMatrix[i,j]}  ");
                    Console.WriteLine();
                }
            }
        public double FindShortestPath(string startVertex, string endVertex)
            {
                int startIndex = Array.IndexOf(vertices, startVertex);
                int endIndex = Array.IndexOf(vertices, endVertex);

                if (startIndex == -1 || endIndex == -1)
                    return -1;

                double[] distances = new double[vertexCount];
                var previousVertices = new int[vertexCount];
                bool[] visited = new bool[vertexCount];

                for (int i = 0; i < vertexCount; i++)
                {
                    distances[i] = double.MaxValue;
                    previousVertices[i] = -1;
                    visited[i] = false;
                }

                distances[startIndex] = 0;

                for (int i = 0; i < vertexCount - 1; i++)
                {
                    int minDistanceIndex = GetMinDistanceIndex(distances, visited);
                    visited[minDistanceIndex] = true;

                    for (int j = 0; j < vertexCount; j++)
                    {
                        if (!visited[j] && adjacencyMatrix[minDistanceIndex, j] != double.MaxValue &&
                            distances[minDistanceIndex] + adjacencyMatrix[minDistanceIndex, j] < distances[j])
                        {
                            distances[j] = distances[minDistanceIndex] + adjacencyMatrix[minDistanceIndex, j];
                            previousVertices[j] = minDistanceIndex;
                        }
                    }
                }
                PrintPath(previousVertices, startIndex, endIndex);
                if (distances[endIndex] == double.MaxValue)
                    return 0;
                else
                    return distances[endIndex];
            }

        //private static double[,] Floyd(double[,] a)
        //{
        //    double[,] d = new double[n, n];
        //    d = (double[,])a.Clone();
        //    for (int i = 1; i <= n; i++)
        //        for (int j = 0; j <= n - 1; j++)
        //            for (int k = 0; k <= n - 1; k++)
        //                if (d[j, k] > d[j, i - 1] + d[i - 1, k])
        //                    d[j, k] = d[j, i - 1] + d[i - 1, k];
        //    return d;
        //}

        public int GetMinDistanceIndex(double[] distances, bool[] visited)
            {
                double minDistance = double.MaxValue;
                int minIndex = -1;

                for (int i = 0; i < vertexCount; i++)
                {
                    if (!visited[i] && distances[i] <= minDistance)
                    {
                        minDistance = distances[i];
                        minIndex = i;
                    }
                }

                return minIndex;
            }
        public void PrintPath(int[] previousVertices, int startIndex, int endIndex)
        {
            if (previousVertices[endIndex] == -1)
            {
                //Console.WriteLine("Нет пути.");
                return;
            }
            var path = new Stack<string>();
            for (int at = endIndex; at != -1; at = previousVertices[at])
            {
                path.Push(vertices[at]);
            }
            Console.WriteLine("Путь: " + string.Join(" -> ", path));
        }
    }
}
