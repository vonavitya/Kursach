using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Поиск_хроматического_числа
{
    class Functionality
    {
        /* считывание графа и проверка его на корректность 
         * возвращает false и graph = null, если граф не корректно задан;
                      true  и graph[0][],   если подана пусатя матрица;
                      true  и graph[ ][],   если все нормально */
        public static bool CheckingInput(string[] input, out int[][] graph)
        {
            //если файл не пустой
            if (input.Length > 0)
            {
                byte graphType = 0;

                //обрезка пробелов в начале и конце строки
                input[0] = input[0].Trim();

                //проверка первой строчки на матрицу смежности
                if (input[0].StartsWith("матрица смежности") || input[0].StartsWith("Матрица смежности"))
                    graphType = 1;
                else

                //проверка первой строчки на матрицу инцидентности
                if (input[0].StartsWith("матрица инцидентности") || input[0].StartsWith("Матрица инцидентности"))
                    graphType = 2;
                else

                //проверка первой строчки на списки соседних вершин
                if (input[0].StartsWith("списки соседних вершин") || input[0].StartsWith("Списки соседних вершин"))
                    graphType = 3;

                //проверка графа на корректность
                bool okGraph = true;

                if (graphType != 0)
                    CorrectWork(input);

                switch (graphType)
                {
                    default:   //в случае если остался 0, значит в первой строке нет инфы о способе задания графа
                        graph = null;
                        return false;
                        break;

                    case 1:   //считывание матрицы смежности графа
                        int[,] graphSm = ReadGraphSmeghn(input, out okGraph);
                        //если матрица считалась и она не пустая
                        if (okGraph && graphSm.Length > 0)
                            graph = ConvertedGraphToNeighbours(1, out okGraph, graph12: graphSm);
                        //если матрица считалась, но пустая
                        else if ((okGraph && graphSm.Length == 0))
                            graph = new int[0][];
                        //если матрица не считалась
                        else
                            graph = null;
                        return okGraph;
                        break;

                    case 2:   //считывание матрицы инцидентности графа
                        int[,] graphInc = ReadGraphIncedent(input, out okGraph);
                        //если матрица считалась и она не пустая
                        if (okGraph && graphInc.Length > 0)
                            graph = ConvertedGraphToNeighbours(2, out okGraph, graph12: graphInc);
                        //если матрица считалась, но пустая
                        else if ((okGraph && graphInc.Length == 0))
                            graph = new int[0][];
                        //если матрица не считалась
                        else
                            graph = null;
                        return okGraph;
                        break;

                    case 3:   //считывание списков соседних вершин графа
                        graph = ReadGraphNeighbours(input, out okGraph);
                        //если матрица считалась и она не пустая
                        if (okGraph && graph.Length > 0)
                            graph = ConvertedGraphToNeighbours(3, out okGraph, graph3: graph);
                        //если матрица считалась, но пустая
                        else if ((okGraph && graph.Length == 0))
                            graph = new int[0][];
                        //если матрица не считалась
                        else
                            graph = null;
                        return okGraph;
                        break;
                }
            }
            //на случай если файл пустой
            else
            {
                graph = new int[0][];
                return false;
            }
        }


        /* удаление лишних разделителей в строке*/
        private static void CorrectWork(string[] s)
        {
            for (int tek = 1; tek < s.Length; tek++)
            {
                s[tek].Trim();

                while (s[tek].IndexOf("  ") != -1)
                    s[tek] = s[tek].Replace("  ", " ");

                while (s[tek] != "" && s[tek][s[tek].Length - 1] == ' ')
                    s[tek] = s[tek].Remove(s[tek].Length - 1, 1);
            }
        }


        /* считывание матрицы смежности и проверка её на корректность 
         * возвращает false и graph = null, если граф не корректно задан;
                      true  и graph[0, 0],   если подана пусатя матрица;
                      true  и graph[ ,  ],   если все нормально */
        private static int[,] ReadGraphSmeghn(string[] input, out bool okGraph)
        {
            //если введён не только способ задания графа
            if (input.Length > 1)
            {
                //создание массива для записи графа по количеству введённых строк
                int[,] graph = new int[input.Length - 1, input.Length - 1];

                //запись в массив всех слов, введённых в первой вершине
                string[] mas = input[1].Split(' ');
                int nPoints = 0;   //переменная для сравнения количества вершин в каждой строке
                //в случае если строка не оставлена пустой
                if (!(mas.Length == 1 && mas[0] == ""))
                    nPoints = mas.Length;
                //если же строка пустая
                else
                    //массив тоже делаем пустым
                    mas = new string[0];
                
                int nFilledStrings = 0;   //переменна для подсчета непустых строк

                //если количество вершин в первой строке больше количества строк (далее сравнение через nPoints)
                if (nPoints > graph.GetLength(0))
                {
                    //возникает ошибка т.к. матрица смежности д.б. симметрична
                    okGraph = false;
                    return null;
                }
                //если количество вершин в строке не больше количества строк
                else
                    //проход по всем строкам
                    for (int tekPoint = 1; tekPoint < input.Length; tekPoint++)
                    {
                        //считывание всех слов строки в массив
                        mas = input[tekPoint].Split(' ');
                        //удаление элемента, если строка была пустой
                        if (mas.Length == 1 && mas[0] == "")
                            mas = new string[0];

                        //если элементы в массиве есть
                        if (mas.Length > 0)
                        {
                            //увеличение количества непустых строк
                            nFilledStrings++;
                            //сравнение количества вершин с предыдущими строками + учет пустых строк
                            if (mas.Length != nPoints && nPoints > 0)
                            {
                                okGraph = false;
                                return null;
                            }
                            //если количество вершин в строке совпадает с предыдущими непустыми строками
                            else
                            {
                                //если все предыдущие строки были пустыми
                                if (nPoints == 0)
                                {
                                    //задание количества вершин в строке
                                    nPoints = mas.Length;
                                    //если количество вершин в первой строке больше количества строк
                                    if (nPoints > graph.GetLength(0))
                                    {
                                        okGraph = false;
                                        return null;
                                    }
                                }

                                //проход по элементам строки и запись их в массив
                                for (int tekNeighbour = 0; tekNeighbour < nPoints; tekNeighbour++)
                                    //если граф заполняется корректно
                                    if (mas[tekNeighbour] == "0" || mas[tekNeighbour] == "1")
                                        graph[nFilledStrings - 1, tekNeighbour] = Convert.ToInt32(mas[tekNeighbour]);
                                    //если граф заполняется символами / буквами / ...
                                    else
                                    {
                                        //ошибка и вылет
                                        okGraph = false;
                                        return null;
                                    }
                            }
                        }
                    }

                //если количество непустых строк совпадает с количеством вершин в строке
                if (nFilledStrings == nPoints)
                {
                    okGraph = true;
                    //массив для графа без оставшихся пустых незаполненных элементов
                    int[,] newGraph = new int[nPoints, nPoints];

                    //копирование заполненной части старого массива с графом в новый
                    for (int tekPoint = 0; tekPoint < nPoints; tekPoint++)
                        for (int tekNeighbour = 0; tekNeighbour < nPoints; tekNeighbour++)
                            newGraph[tekPoint, tekNeighbour] = graph[tekPoint, tekNeighbour];

                    return newGraph;
                }
                else
                {
                    okGraph = false;
                    return null;
                }
            }
            else
            {
                okGraph = true;
                return new int[0, 0];
            }
        }


        /* считывание матрицы инцидентности и проверка её на корректность 
         * возвращает false и graph = null, если граф не корректно задан;
                      true  и graph[0, 0],   если подана пусатя матрица;
                      true  и graph[ ,  ],   если все нормально */
        private static int[,] ReadGraphIncedent(string[] input, out bool okGraph)
        {
            //если введён не только способ задания графа
            if (input.Length > 1)
            {
                //создание массива для записи графа по количеству введённых строк
                int[,] graph = new int[input.Length - 1, (input.Length - 1) * (input.Length - 2) / 2];

                //запись в массив всех слов, введённых в первой вершине
                string[] mas = input[1].Split(' ');
                int nEdges = 0;   //переменная для сравнения количества рёбер в каждой строке
                //в случае если строка не оставлена пустой
                if (!(mas.Length == 1 && mas[0] == ""))
                    nEdges = mas.Length;
                //если же строка пустая
                else
                    //массив тоже делаем пустым
                    mas = new string[0];

                int nPoints = 0;   //переменна для подсчета непустых строк

                //если количество рёбер в первой строке больше количества возможных (далее сравнение через nEdges)
                if (nEdges > graph.GetLength(1))
                {
                    //возникает ошибка т.к. матрица смежности д.б. симметрична
                    okGraph = false;
                    return null;
                }
                //если количество вершин в строке не больше количества строк
                else
                    //проход по всем строкам
                    for (int tekPoint = 1; tekPoint < input.Length; tekPoint++)
                    {
                        //считывание всех слов строки в массив
                        mas = input[tekPoint].Split(' ');
                        //удаление элемента, если строка была пустой
                        if (mas.Length == 1 && mas[0] == "")
                            mas = new string[0];

                        //если элементы в массиве есть
                        if (mas.Length > 0)
                        {
                            //увеличение количества непустых строк
                            nPoints++;
                            //сравнение количества рёбер с предыдущими строками + учет пустых строк
                            if (mas.Length != nEdges && nEdges > 0)
                            {
                                okGraph = false;
                                return null;
                            }
                            //если количество рёбер в строке совпадает с предыдущими непустыми строками
                            else
                            {
                                //если все предыдущие строки были пустыми
                                if (nEdges == 0)
                                {
                                    //задание количества рёбер в строке
                                    nEdges = mas.Length;
                                    //если количество рёбер в первой строке больше количества возможных
                                    if (nEdges > graph.GetLength(1))
                                    {
                                        okGraph = false;
                                        return null;
                                    }
                                }

                                //проход по элементам строки и запись их в массив
                                for (int tekEdge = 0; tekEdge < nEdges; tekEdge++)
                                    //если граф заполняется корректно
                                    if (mas[tekEdge] == "0" || mas[tekEdge] == "1")
                                        graph[nPoints - 1, tekEdge] = Convert.ToInt32(mas[tekEdge]);
                                    //если граф заполняется символами / буквами / ...
                                    else
                                    {
                                        //ошибка и вылет
                                        okGraph = false;
                                        return null;
                                    }
                            }
                        }
                    }

                okGraph = true;
                //массив для графа без оставшихся пустых незаполненных элементов
                int[,] newGraph = new int[nPoints, nEdges];

                //копирование заполненной части старого массива с графом в новый
                for (int tekPoint = 0; tekPoint < nPoints; tekPoint++)
                    for (int tekEdge = 0; tekEdge < nEdges; tekEdge++)
                        newGraph[tekPoint, tekEdge] = graph[tekPoint, tekEdge];

                return newGraph;
            }
            else
            {
                okGraph = true;
                return new int[0, 0];
            }
        }


        /* считывание списков соседних вершин и проверка их на корректность 
         * возвращает false и graph = null, если граф не корректно задан;
                      true  и graph[0][],   если подана пусатя матрица;
                      true  и graph[ ][],   если все нормально */
        private static int[][] ReadGraphNeighbours(string[] input, out bool okGraph)
        {
            okGraph = true;

            //если введён не только способ задания графа
            if (input.Length > 1)
            {
                //создание массива для записи графа по количеству введённых строк
                int[][] graph = new int[input.Length][];

                int nPoints = 0;   //переменна для подсчета непустых строк

                //проход по всем строкам
                for (int tekPoint = 1; tekPoint < input.Length; tekPoint++)
                {
                    //считывание всех слов строки в массив
                    string[] mas = input[tekPoint].Split(' ');
                    //удаление пустого элемента, если строка была пустой
                    if (mas.Length == 1 && mas[0] == "")
                        mas = new string[0];

                    //если элементы в массиве есть
                    if (mas.Length > 0)
                    {
                        //увеличение количества непустых строк
                        nPoints++;
                        //сравнение количества соседей вершины с макс количеством вершин
                        if (mas.Length > graph.Length)
                        {
                            okGraph = false;
                            return null;
                        }
                        //если количество вершин в строке меньше макс кол-ва вершин
                        else
                        {
                            graph[nPoints - 1] = new int[mas.Length];
                            int neighbour;
                            //проход по элементам строки и запись их в массив
                            for (int tekNeighbour = 0; tekNeighbour < mas.Length; tekNeighbour++)
                                //если граф заполняется корректно
                                if (Int32.TryParse(mas[tekNeighbour], out neighbour) && neighbour <= graph.Length && neighbour > 0)
                                    graph[nPoints - 1][tekNeighbour] = neighbour - 1;
                                //если граф заполняется символами / буквами / ...
                                else
                                {
                                    //ошибка и вылет
                                    okGraph = false;
                                    return null;
                                }
                        }
                    }
                    else
                    {
                        graph[nPoints] = new int[0];
                        nPoints++;
                    }
                }

                okGraph = true;

                Array.Resize(ref graph, nPoints);

                //проверка чтобы в качестве соседей не было несуществующих вершин
                for (int tekPoint = 0; tekPoint < nPoints; tekPoint++)
                    for (int tekNeighbour = 0; tekNeighbour < graph[tekPoint].Length; tekNeighbour++)
                        if (graph[tekPoint][tekNeighbour] > nPoints - 1)
                        {
                            okGraph = false;
                            return null;
                        }

                return graph;
            }
            else
            {
                okGraph = true;
                return new int[0][];
            }
        }


        /* проверка графа на корректность и преобразование к спискам соседних вершин */
        private static int[][] ConvertedGraphToNeighbours(byte        graphType,      /*способ задание графа (1 - смежность, 2 - инцидентность, 3 - списки соседей*/
                                                          out bool    okGraph,        /*проверка корректности поданного графа*/
                                                              int[][] graph3 = null,  /*в случае пустой ссылки будет чекать сл способ задания*/
                                                              int[,]  graph12 = null  /*в случае если граф задан смежностью/инцидентностью*/)
        {
            //в случае если переданы пустые графы, возникает ошибка
            if (graph12 == null && graph3 == null)
            {
                okGraph = false;
                return null;
            }

            //если графы не пустые, идёт преобразование, если необходимо, и проверка на корректность 
            else
                switch (graphType)
                {
                    default: //если граф задан матрицей смежности
                        return ConvertedGraphFromSmeghnToNeighbours(graph12, out okGraph);
                        break;

                    case 2: //если граф задан матрицей инцидентности
                        return ConvertedGraphFromIncedentToNeighbours(graph12, out okGraph);
                        break;

                    case 3: //если граф задан списками соседних вершин
                        okGraph = OkNeighboursGraph(graph3);
                        return graph3;
                        break;
                }
        }


        /* переделывание матрицы смежности графа к соответствию связанных вершин
         * выход: граф[номер вершины][соседи вершины] */
        private static int[][] ConvertedGraphFromSmeghnToNeighbours(    int[,] graph,   /*граф, который нужно переделать*/
                                                                    out bool   okGraph  /*перменная для проверки корректности графа*/)
        {
            //создание матрицы соответствия вершин для графа, заданного матрицей соответствия вершин
            int[][] pointNeighbours = new int[graph.GetLength(0)][];

            okGraph = true;

            //цикл прохода по каждой вершине
            for (int tekPoint = 0; tekPoint < graph.GetLength(0); tekPoint++)
            {
                //поскольку заранее низвестно сколько соседей у вершины. создаем массив вершин, где
                //кол-во элементов = кол-во вершин графа - сама вершина
                int[] futureNeighbours = new int[graph.GetLength(1) - 1];
                int tekFutureNeighbour = 0; //для подсчета кол-ва соседей вершины

                if (graph[tekPoint, tekPoint] != 0)
                {
                    okGraph = false;
                    return null;
                }

                //заполняем массив соседей вершины и считаем их
                for (int tekNeighbour = 0; tekNeighbour < graph.GetLength(1); tekNeighbour++)
                    if (tekPoint != tekNeighbour && graph[tekPoint, tekNeighbour] != 0)
                    {
                        //если матрица смежности заполнена некорректно
                        if (graph[tekPoint, tekNeighbour] != graph[tekNeighbour, tekPoint])
                        {
                            okGraph = false;
                            return null;
                        }

                        futureNeighbours[tekFutureNeighbour] = tekNeighbour;
                        tekFutureNeighbour++;
                    }

                //теперь кол-во соседей известно, переписываем все в чистовой массив соответствия связанных вершин
                pointNeighbours[tekPoint] = new int[tekFutureNeighbour];
                for (int tekNeighbour = 0; tekNeighbour < pointNeighbours[tekPoint].Length; tekNeighbour++)
                    pointNeighbours[tekPoint][tekNeighbour] = futureNeighbours[tekNeighbour];
            }

            return pointNeighbours;
        }


        /* переделывание матрицы инциденций графа к соответствию связанных вершин (для жадного алгоритма)
         * выход: граф[номер вершины][соседи вершины] */
        private static int[][] ConvertedGraphFromIncedentToNeighbours(int[,] graph,  /*граф, который нужно переделать*/
                                                                      out bool okGraph /*перменная для проверки корректности графа*/)
        {
            //создание матрицы смежности для промежуточного перехода
            int[,] graphSmeghn = new int[graph.GetLength(0), graph.GetLength(0)];

            okGraph = true;

            //проход по каждому ребру
            for (int tekEdge = 0; tekEdge < graph.GetLength(1); tekEdge++)
            {
                int point1 = 0,  //вершина 1, инцедентная к текущему ребру
                    point2 = 0;  //вершина 2, инцедентная к текущему ребру

                bool one = false,  //найдена первая вершина ребра
                     both = false;  //найдены обе вершины ребра

                //поиск вершин, инцедентных текущему ребру
                for (int tekPoint = 0; tekPoint < graph.GetLength(0); tekPoint++)
                    //если вершина и ребро инцедентны
                    if (graph[tekPoint, tekEdge] == 1)
                        //если не найдена ни 1 вершина, инцедентная текущему ребру
                        if (one == false)
                        {
                            point1 = tekPoint;
                            one = true;
                        }
                        //если найдена 1 вершина, инцедентная текущему ребру
                        else
                        if (both == false)
                        {
                            point2 = tekPoint;
                            both = true;
                        }
                        else
                        {
                            okGraph = false;
                            return null;
                        }

                //если найдены обе вершины инцедентные ребру
                if (both == true)
                {
                    graphSmeghn[point1, point2] = 1;
                    graphSmeghn[point2, point1] = 1;
                }
                else
                //граф не корректен
                {
                    okGraph = false;
                    return null;
                }
            }

            return ConvertedGraphFromSmeghnToNeighbours(graphSmeghn, out bool okGraphSmeghn);
        }


        /* проверка списков соседних вершин на корректность 
         * вывод: корректный (true) / нет (false) */
        private static bool OkNeighboursGraph(int[][] graph /*граф который нужно проверить*/)
        {
            //инициализация матрицы смежности (проверять матрицу смежности на корректность много проще
            int[,] graphSm = new int[graph.Length, graph.Length];

            //переход к матрице смежности
            for (int tekPoint = 0; tekPoint < graph.Length; tekPoint++)
                for (int tekNeighbour = 0; tekNeighbour < graph[tekPoint].Length; tekNeighbour++)
                    //симметричная ячейка не заполняется специально т.к. она заполнится при проходе по соседям др. вершины 
                    graphSm[tekPoint, graph[tekPoint][tekNeighbour]] = 1;

            //проверка матрицы смежности на корректность
            for (int tekPoint = 0; tekPoint < graphSm.GetLength(0) - 1; tekPoint++)
                for (int tekNeighbour = tekPoint + 1; tekNeighbour < graphSm.GetLength(0); tekNeighbour++)
                    //если при переходе граф получился ориентированный, списки соседних вершин не корректные
                    if (graphSm[tekPoint, tekNeighbour] != graphSm[tekNeighbour, tekPoint])
                        return false;

            return true;
        }


        /* поиск хроматического числа графа (жадным алгоритмом)
         * выход: хроматич. число, массив цвет вершины[номер вершины] */
        public static int ChromaticNumber(int[][] graph,             /*граф для поиска*/
                                           out int[] pointColors      /*массив с цветами вершин*/)
        {
            int[] pointNums;  //исходный номер вершины[присвоенный номер]
            pointNums = DecreasingPointDegreeNumeration(graph);

            pointColors = new int[graph.Length];  //цвет вершины[исходный номер вершины]

            int alreadyColored = 0, //кол-во уже покрашенных вершин
                color = 0;          //цвет, используемый для раскраски на данном проходе по вершинам

            //цикл раскраски вершин
            while (alreadyColored < graph.Length)   //пока не будут раскрашены все вершины
            {
                //переход на следующий цвет
                color += 1;

                //цикл раскраски вершин в текущий цвет
                for (int tekPoint = 0 /*присвоенный номер*/; tekPoint < graph.Length && alreadyColored < graph.Length; tekPoint++)
                {
                    //если вершина не покрашена и у неё нет соседей текущего цвета, то покраска этой вершины
                    if (pointColors[pointNums[tekPoint]] == 0 && NoSuchNeighbours(graph[pointNums[tekPoint]], pointColors, color))
                    {
                        alreadyColored += 1;
                        pointColors[pointNums[tekPoint]] = color;
                    }
                }
            }

            return color;
        }


        /* нумерация вершин графа в порядке убывания их степеней (для жадного алгоритма)
         * выход: исходный номер выершины[присвоенный] */
        private static int[] DecreasingPointDegreeNumeration(int[][] graph /*граф для нумерации вершин*/)
        {
            int[,] pointNumsAndDegrees = new int[2, graph.Length];   //исходный[присвоенный номер]

            //заполнение массива номеров
            for (int tekPoint = 0; tekPoint < graph.Length; tekPoint++)
            {
                pointNumsAndDegrees[0, tekPoint] = tekPoint;
                pointNumsAndDegrees[1, tekPoint] = graph[tekPoint].Length;

                int tekPointNum = tekPoint;  //номер столбца в массиве номеров и степеней 
                //пока (элемент не первый и степень элемента больше степени предыдущего)
                while (tekPointNum - 1 >= 0 && pointNumsAndDegrees[1, tekPointNum] > pointNumsAndDegrees[1, tekPointNum - 1])
                {
                    //обмен с предыдущим
                    int tmp = pointNumsAndDegrees[0, tekPointNum];
                    pointNumsAndDegrees[0, tekPointNum] = pointNumsAndDegrees[0, tekPointNum - 1];
                    pointNumsAndDegrees[0, tekPointNum - 1] = tmp;

                    tmp = pointNumsAndDegrees[1, tekPointNum];
                    pointNumsAndDegrees[1, tekPointNum] = pointNumsAndDegrees[1, tekPointNum - 1];
                    pointNumsAndDegrees[1, tekPointNum - 1] = tmp;

                    //уменьшение порядкого номера элемента в следствие обмена с предыдущим
                    tekPointNum--;
                }
            }

            int[] pointNums = new int[graph.Length];  //исходный номер[присвоенный номер]

            //избавляемся от степеней вершин
            for (int tekPointNum = 0; tekPointNum < graph.Length; tekPointNum++)
                pointNums[tekPointNum] = pointNumsAndDegrees[0, tekPointNum];

            return pointNums;
        }


        /* проверка на наличие у вершины графа соседей того же цвета (для жадного алгоритма)
         * выход: если нету - true, есть - false */
        private static bool NoSuchNeighbours(int[] pointNeighbours /*все соседи вершины*/,
                                             int[] pointColors     /*цвет[номер вершины]*/,
                                             int color           /*цвет для проверки*/)
        {
            bool no = true; //да = нет таких соседей; нет - есть такие соседи

            for (int tekNeighbour = 0; tekNeighbour < pointNeighbours.Length; tekNeighbour++)
            {
                //если этот сосед такого цвета
                if (pointColors[pointNeighbours[tekNeighbour]] == color)
                {
                    no = false;
                    break;
                }
            }

            return no;
        }


    }
}
