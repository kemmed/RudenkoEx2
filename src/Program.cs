using System;
using System.IO;

namespace RudenkoEx2
{
    class Program
    {
        static void Main(string[] args)
        {
            string filePath = "";
            int size = 1;
            double[,] matrix = new double[size, size];


            while (true)
            {
                Console.WriteLine("В файле должна храниться матрица смежности графа с весами ребер.");
                Console.WriteLine("Разделитель для дробной части - запятая, разделитель для отдельных чисел в строке - tab");
                Console.Write("Укажите путь к файлу с матрицой смежности относительно папки src: ");
                filePath = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(filePath))
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Введите путь.");
                    Console.ReadKey();
                    Console.Clear();
                }
            } 
            try
            {
                string[] lines = File.ReadAllLines(@$"../../../{filePath}");
                size = lines.Length;
                matrix = new double[size, size];
                for (int i = 0; i < lines.Length; i++)
                {
                    string[] row = lines[i].Split('\t');
                    for (int j = 0; j < row.Length; j++)
                        matrix[i, j] = double.Parse(row[j]);
                }
            }
            catch
            {
                Console.WriteLine("Такой файл не найден или из него не возможно прочитать данные.");
                return;
            }
            Graph graph = new Graph(size);
                
                graph.InputGraph(graph.InputVertices(size), matrix);
                Console.Clear();
                graph.PrintAdjacencyMatrix();
                string startVertex;
                string endVertex;
                while (true)
                {
                    Console.WriteLine("Введите название вершины начала пути:");
                    startVertex = Console.ReadLine();
                    if (!string.IsNullOrWhiteSpace(startVertex) && Array.IndexOf(graph.vertices, startVertex) != -1)
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Введите корректное название начальной вершины.");
                        Console.ReadKey();
                        Console.WriteLine();
                    }
                }
                while (true)
                {
                    Console.WriteLine("Введите название вершины конца пути:");
                    endVertex = Console.ReadLine();
                    if (!string.IsNullOrWhiteSpace(endVertex) && Array.IndexOf(graph.vertices, endVertex) != -1)
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Введите корректное название конечной вершины.");
                        Console.ReadKey();
                        Console.WriteLine();
                    }
                }

                double result = graph.FindShortestPath(startVertex, endVertex);
                if (result == -1)
                {
                    Console.WriteLine("Одна из вершин не найдена.");
                }
                else if (result > 0)
                {
                    Console.WriteLine($"Кратчайшее расстояние от {startVertex} до {endVertex}: {result}");
                }
                else
                {
                    Console.WriteLine($"Нет пути от {startVertex} до {endVertex}");
                }            
        }
    }
}
