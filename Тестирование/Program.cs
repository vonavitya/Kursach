using System;
using System.Globalization;
using System.IO;

namespace Тестирование
{
    class Program
    {
        //главное меню (для тестирования)
        static void Main(string[] args)
        {
            byte choice = 0;
            bool needClearScreen = true;

            while (choice != 3)
            {
                if (needClearScreen == true)
                    Console.Clear();
                else
                {
                    needClearScreen = true;
                    Console.WriteLine();
                }
                    

                //выбор действия
                choice = ChoiceOfAction();
                Console.Clear();

                switch (choice)
                {
                    case 1:   //создать файл с графом
                        MakingFile();
                        needClearScreen = false;
                        Console.WriteLine("\nВы найдёте файл в папке \"Документы\".");
                        break;
                        
                    case 2:   //тестирование жадного алгоритма с разными нумерациями
                        TestingGreadyAlgorithm(4, 12);
                        Console.WriteLine("\n\n=================================================================================================================================================");
                        Console.WriteLine("=================================================================================================================================================\n\n");
                        TestingGreadyAlgorithm(5, 10);
                        break;
                }
            }
        }


        /* выбор действия (тестирование или создание файла) (для тестирования)
         * выход: целое число от 1 до 5 */
        static byte ChoiceOfAction()
        {
            Console.WriteLine("1 - создать файл с графом;");
            Console.WriteLine("2 - тестирование жадного алгоритма с разными нумерациями;");
            Console.WriteLine("3 - выход.");
            
            return (byte)IntNumber("Выберите действие", "Вы должны ввести целое число от 1 до 3", 1, 3);
        }


        /* создает файл с графом для тестирования жадного алгоритма */
        static void MakingFile()
        {
            int nEdges;
            //создание графа в виде матрицы смежности
            int[,] graph = GraphSmeghn(out nEdges);
            Console.WriteLine();

            //ввод имени файла
            Console.Write("Введите название файла: ");
            string fName;

            //определение пути к папке "мои документы"
            string myDocuments = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\";

            do
            {
                fName = Console.ReadLine();

                //если такой файл уже существует в папке "мои документы"
                if (File.Exists(myDocuments + fName + ".txt"))
                {
                    Console.WriteLine("   Файл с таким именем уже существует в папке \"Документы\"!!");
                    Console.Write("   Введите другое название: ");
                }

                //если пользователь ничего не ввёл / ввёл пробелы
                if (StringIsEmpty(fName))
                {
                    Console.WriteLine("   Имя файла не может быть пустым или состоять из пробелов.");
                    Console.Write("   Введите корректное название файла: ");
                }

            } while (File.Exists(myDocuments + fName + ".txt") || StringIsEmpty(fName)); //пока пользователь не введёт нормальное название

            //проверка названия выполнена, поэтому чтобы в дальнейшем не приписывать везде формат, проще так
            fName += ".txt";

            //выбор в каком виде граф должен попасть в файл
            Console.WriteLine();
            byte choice = ChoiceOfGraphView();

            switch (choice)
            {
                case 1:   //создать файл с графом, заданным матрицей смежности
                    GraphSmeghnToFile(graph, fName);
                    break;

                case 2:   //создать файл с графом, заданным матрицей инциденций
                    GraphIncedentToFile(ConvertedGraphFromSmeghnToIncedent(graph, nEdges), fName);
                    break;

                case 3:   //создать файл с графом, заданным соответствием связанных вершин
                    GraphNeighbourToFile(ConvertedGraphFromSmeghnToNeighbours(graph, out bool okGraph), fName);
                    break;
            }

            //перемещение файла в папку "мои документы"
            File.Move(fName, myDocuments + fName);

        }

        
        /* проверка корректности строки для имени файла 
         * строка не должна быть пустой или состоять только из пробелов */
        private static bool StringIsEmpty(string s)
        {
            bool ok = true;

            //если строка пустая => плохо
            if (s == "")
                ok = false;
            //если строка состоит из пробелов => плохо
            else
                for (int tekChar = 0; tekChar < s.Length; tekChar++)
                    if (s[tekChar] != ' ')
                        ok = false;

            return ok;
        }


        /* выбор вида, в котором граф будет загружен в файл (для генерации тестов)
         * выход: целое число от 1 до 3 */
        static byte ChoiceOfGraphView()
        {
            Console.WriteLine("1 - матрица смежности;");
            Console.WriteLine("2 - матрица инцидентности;");
            Console.WriteLine("3 - списки соседних вершин.");
            
            return (byte)IntNumber("Выберите вид графа", "Вы должны ввести целое число от 1 до 3", 1, 3);
        }


        /* создание файла с матрицей смежности */
        static void GraphSmeghnToFile(int[,] graph, string fName)
        {
            //создание файла в папке с программой и открытие его в конце
            File.AppendAllText(fName, "матрица смежности");

            int nPoints = graph.GetLength(0); //количество вершин графа (строк в файле)

            for (int tekPoint = 0; tekPoint < nPoints; tekPoint++)
            {
                string neighbours = ""; //строка матрицы смежности

                //формирование строки в матрице смежности
                for (int tekNeigh = 0; tekNeigh < nPoints; tekNeigh++)
                    neighbours = neighbours + graph[tekPoint, tekNeigh] + " ";

                //запись i-ой строки матрицы смежности в файл
                File.AppendAllText(fName, "\n" + neighbours);
            }
        }


        /* создание файла с матрицей инцеденций */
        static void GraphIncedentToFile(int[,] graph, string fName)
        {
            //создание файла в папке с программой и открытие его в конце
            File.AppendAllText(fName, "матрица инцидентности");

            int nPoints = graph.GetLength(0); //количество вершин графа (строк в файле)
            int nEdges = graph.GetLength(1);  //количество ребер графа  (столбцов в файле)

            for (int tekPoint = 0; tekPoint < nPoints; tekPoint++)
            {
                string neighbours = ""; //строка матрицы инцедентности

                //формирование строки из матрицы инцедентности
                for (int tekEdge = 0; tekEdge < nEdges; tekEdge++)
                    neighbours = neighbours + graph[tekPoint, tekEdge] + " ";

                //запись i-ой строки матрицы инцедентности в файл
                File.AppendAllText(fName, "\n" + neighbours);
            }
        }


        /* создание файла с соответствием связанных вершин */
        static void GraphNeighbourToFile(int[][] graph, string fName)
        {
            //создание файла в папке с программой и открытие его в конце
            File.AppendAllText(fName, "списки соседних вершин");
            
            int nPoints = graph.Length; //количество вершин графа (строк в файле)

            for (int tekPoint = 0; tekPoint < nPoints; tekPoint++)
            {
                for (int tekNeigh = 0; tekNeigh < graph[tekPoint].Length; tekNeigh++)
                    graph[tekPoint][tekNeigh]++;

                string neighbours = ""; //строка из матрицы связанных вершин

                //заполнение строки матрицы связанных вершин
                for (int tekNeigh = 0; tekNeigh < graph[tekPoint].Length; tekNeigh++)
                    neighbours = neighbours + graph[tekPoint][tekNeigh] + " ";

                //запись строки связанных вершин в файл
                File.AppendAllText(fName, "\n" + neighbours);
            }
        }


        /* алгоритм проверки различных нумераций жадного алгоритма (для тестирования) */
        static void TestingGreadyAlgorithm(int p1, int p2 /*параметры отвечающие за значения параметров графа*/)
        {
            int s = 0,       //итог. сумма верных хром. чисел
                sRnd = 0,    //итог. сумма вычисленных хром. чисел, вычисленных при случайной нумерации
                sWidth = 0,  //итог. сумма вычисленных хром. чисел, вычисленных при нумерации обходом в ширину
                sDepth = 0,  //итог. сумма вычисленных хром. чисел, вычисленных при нумерации обходом в глубину
                sInc = 0,    //итог. сумма вычисленных хром. чисел, вычисленных при нумерациии порядке возрастания степеней вершин
                sDec = 0;    //итог. сумма вычисленных хром. чисел, вычисленных при нумерациии порядке убывания степеней вершин

            //изменения значения количества вершин графа
            for (int tekNPoints = p1; tekNPoints <= 100; tekNPoints += p2)
            {
                Console.WriteLine("Вершин: " + tekNPoints + "\n");

                //шаг с которым будет изменяться хром. число
                int gapChromNumber = tekNPoints / p1;
                
                //изменения значения хром. числа при заданном значении кчисла вершин
                for (int tekChromNumber = 2; tekChromNumber < tekNPoints; tekChromNumber+= gapChromNumber)
                {
                    //наиб. и наим. значения кол-ва рёбер при заданных числе вершин и хром. числе
                    int tekMaxNEdges = MaxNEdges(tekNPoints, tekChromNumber);
                    int tekMinNEdges = tekChromNumber * (tekChromNumber - 1) / 2;
                    //шаг с которым будет изменяться количество рёбер графа
                    int gapEdges = (tekMaxNEdges - tekMinNEdges) / 5;
                    if (gapEdges == 0)
                        gapEdges = 1;

                    //суммы вычисленных хром. чисел при заданных числе вершин и хром. числе
                    int sumRnd   = 0;
                    int sumWidth = 0;
                    int sumDepth = 0;
                    int sumInc   = 0;
                    int sumDec   = 0;
                    int tekS     = 0;  //сумма вернуъ хром. чисел при заданных числе вершин и хром. числе

                    //цикл изменения кол-ва рёбер графа при заданных числе рёбер и хром. числе
                    for (int tekNEdges = tekMinNEdges + 1; tekNEdges < tekMaxNEdges; tekNEdges+= gapEdges)
                    {
                        tekS += tekChromNumber;

                        //генерация графа
                        int[][] graph = ConvertedGraphToNeighbours(graphType: 1 /*смежность*/, out bool okGraph, graph12: GraphSmeghn(tekNPoints, tekChromNumber, tekNEdges));

                        //вычисление хром. числа с использованием каждой нумерации
                        sumWidth += ChromaticNumber(graph, out int[] pointColors, 1);
                        sumDepth += ChromaticNumber(graph, out pointColors, 2);
                        sumDec += ChromaticNumber(graph, out pointColors, 3);
                        sumInc += ChromaticNumber(graph, out pointColors, 4);
                        sumRnd += ChromaticNumber(graph, out pointColors, 5);

                    } //цикл изменения кол-ва рёбер графа при заданных числе рёбер и хром. числе

                    //добавление текущей верной суммы к итоговой
                    s += tekS;
                    //вывод результатов
                    Console.WriteLine("   Верная сумма хр. чисел: " + tekS);

                    sWidth += sumWidth;
                    Console.WriteLine("      При нумерации обходом в ширину                      - " + sumWidth);

                    sDepth += sumDepth;
                    Console.WriteLine("      При нумерации обходом в глубину                     - " + sumDepth);

                    sDec += sumDec;
                    Console.WriteLine("      При нумерации в порядке убывания степеней вершин    - " + sumDec);

                    sInc += sumInc;
                    Console.WriteLine("      При нумерации в порядке возрастания степеней вершин - " + sumInc);

                    sRnd += sumRnd;
                    Console.WriteLine("      При рандомной нумерации                             - " + sumRnd);

                    Console.WriteLine("      ---------------------------------------------------------");
                } //изменения значения хром. числа при заданном значении кчисла вершин

                Console.WriteLine("=====================================================================\n");
            } //изменения значения количества вершин графа

            Console.WriteLine("Итоговая сумма: " + s);

            Console.WriteLine("   При нумерации обходом в ширину                      - " + sWidth);
            Console.WriteLine("   При нумерации обходом в глубину                     - " + sDepth);
            Console.WriteLine("   При нумерации в порядке убывания степеней вершин    - " + sDec);
            Console.WriteLine("   При нумерации в порядке возрастания степеней вершин - " + sInc);
            Console.WriteLine("   При рандомной нумерации                             - " + sRnd);
            Console.WriteLine("   ----------------------------------------------------------");

            Console.Write("\nНажмите Enter для продолжения.");
            Console.ReadLine();
        }


        /* ввод параметров графа и его создание (для генерации тестов)
         * выход: граф, заданный матрицей смежности */
        static int[,] GraphSmeghn(out int nEdges /*кол-во ребер в графе*/)
        {
            int nPoints = IntNumber("Введите количество вершин графа (от 1 до 100)",
                                    "Нужно целое число от 1 до 100",
                                    1, 100);                                                                    //желаемое кол-во вершин графа
            int chromNumber = IntNumber("Введите хроматическое графа (от 1 до " + nPoints + ")",
                                        "Нужно целое число от 1 до " + nPoints,
                                        1, nPoints);                                                            //желаемое хроматич. число графа
            nEdges = IntNumber("Введите количество ребер будущего графа (от " + chromNumber * (chromNumber - 1) / 2 + " до " + MaxNEdges(nPoints, chromNumber) + ")",
                               "Нужно целое число от " + chromNumber * (chromNumber - 1) / 2 + " до " + MaxNEdges(nPoints, chromNumber),
                               chromNumber * (chromNumber - 1) / 2, MaxNEdges(nPoints, chromNumber));       //желаемое кол-во ребер граф

            return GraphSmeghn(nPoints, chromNumber, nEdges); //сгенерированный c учётом пожеланий граф
        }


        /* создание графа с заданными параметрам (для генерации тестов)
         * с нужными числом вершин, ребер, хроматич. числом
         * выход: граф в виде соответствия связанных вершин */
        static int[,] GraphSmeghn(int nPoints     /*кол-во вершин*/,
                                  int chromNumber /*хроматич. число*/,
                                  int nEdges      /*кол-во рёбер*/)
        {
            int[] notKlikaPoints; //массив под вершины НЕ ВОШЕДШИЕ в клику
            int[] klikaPoints;    //массив под вершины ВОШЕДШИЕ в клику
            //создание клики с размером chromaticNumber и еще nPoints-chromaticNumber обособленных вершин
            int[,] graph = KlikaIntoGraph(nPoints, chromNumber, out notKlikaPoints, out klikaPoints);

            //раскраска вершин графа так, чтобы количества вершин каждого цвета было наибольшим
            int[] pointColors = PointColoring(nPoints, chromNumber, notKlikaPoints, klikaPoints);

            //матрица возможных, еще не построенных ребер
            int[,] notUsedEdges = PossibleEdges(graph, pointColors);

            int tekNEdges = chromNumber * (chromNumber - 1) / 2;

            //соедиенение вершин до тех пор пока не будет достигнуто нужное кол-во ребер
            while (tekNEdges < nEdges)
            {
                //выбор рандомного ребра из массива возможных ребер
                Random rnd = new Random();
                int tekEdge = rnd.Next(0, notUsedEdges.GetLength(1));

                //добавление ребра в граф
                graph[notUsedEdges[0, tekEdge], notUsedEdges[1, tekEdge]] = 1;
                graph[notUsedEdges[1, tekEdge], notUsedEdges[0, tekEdge]] = 1;

                //удаление ребра из массива возможных ребер
                DeleteEdges(ref notUsedEdges, tekEdge, 1);

                //увеличение количества ребер в графе на 1 добавленное
                tekNEdges++;
            }

            return graph;
        }


        /* создание внутри графа (nPoints вершин) клики (nKlika вершин) (для генерации тестов)
         * клику образуют рандомные nKlika вершин графа
         * выход: граф, заданный матрицей смежности с кликой внутри */
        static int[,] KlikaIntoGraph(    int   nPoints        /*количество вершин графа*/,
                                         int   nKlika         /*количество вершин клики*/,
                                     out int[] notKlikaPoints /*массив под вершины НЕ ВОШЕДШИЕ в клику*/,
                                     out int[] klikaPoints    /*массив под вершины ВОШЕДШИЕ в клику*/)
        {
            int[,] graph = new int[nPoints, nPoints];   //матрица смежности графа

            //создание массива в котором хранятся номера всех вершин
            notKlikaPoints = new int[nPoints];
            for (int tekPoint = 0; tekPoint < nPoints; tekPoint++)
                notKlikaPoints[tekPoint] = tekPoint;

            //создание массива тех вершин, из которых будет состять клика
            klikaPoints = new int[nKlika];  
            for(int tekKlikaPoint = 0; tekKlikaPoint < nKlika; tekKlikaPoint++)
            {
                Random rnd = new Random();

                //рандомный выбор вершины для клики из массива с номерами еще не использованных для клики вершин
                int tekPoint = rnd.Next(0, notKlikaPoints.Length - tekKlikaPoint);
                klikaPoints[tekKlikaPoint] = notKlikaPoints[tekPoint];

                //удаление из массива всех вершин вершины, использованной для клики
                for (int i = tekPoint; i < nPoints - tekKlikaPoint - 1; i++)
                    notKlikaPoints[i] = notKlikaPoints[i + 1];
            }

            //окончательно убираем вершины клики из массива вершин вне клики
            Array.Resize(ref notKlikaPoints, nPoints - nKlika);
            
            //клика образуется за счет вершин, указанных в klikaPoints
            for (int tekKlikaPoint = 0; tekKlikaPoint < nKlika - 1; tekKlikaPoint++)
                for (int tekKlikaNeighbour = tekKlikaPoint + 1; tekKlikaNeighbour < nKlika; tekKlikaNeighbour++)
                {
                    graph[klikaPoints[tekKlikaPoint], klikaPoints[tekKlikaNeighbour]] = 1;
                    graph[klikaPoints[tekKlikaNeighbour], klikaPoints[tekKlikaPoint]] = 1;
                }

            return graph;
        }


        /* рандомное окрашивание вершин графа (для генерации тестов)
         * окрашивание так, чтобы вершин каждого цвета было примерно одинаково
           для того чтобы была возможность потом добавить наиб. кол-во ребер
         * выход: массив цвет вершины[номер вершины] */
        static int[] PointColoring(int   nPoints        /*количество вершин графа*/,
                                   int   chromNumber    /*хроматическое число*/,
                                   int[] notKlikaPoints /*массив под вершины НЕ ВОШЕДШИЕ в клику*/,
                                   int[] klikaPoints    /*массив под вершины ВОШЕДШИЕ в клику*/)
        {
            int[] pointColors = new int[nPoints];   //цвет вершины[номер вершины]

            int tekColor = 0;   //цвет, в который будет краситься вершина в цикле покраски
            for (int tekKlikaPoint = 0; tekKlikaPoint < chromNumber; tekKlikaPoint++)
            {
                //переход на другой цвет с каждой итерацией для вариативности количества ребер
                tekColor++;

                //присвоение вершине цвета
                pointColors[klikaPoints[tekKlikaPoint]] = tekColor;
            }

            tekColor = 0;   //цвет, в который будет краситься вершина в цикле покраски
            for (int tekPoint = 0; tekPoint < nPoints - chromNumber; tekPoint++)
            {
                Random rnd = new Random();

                //чтобы цветов было chromNumber нужно следить за изменением цвета
                tekColor++;
                if (tekColor > chromNumber)
                    tekColor = 1;

                //рандомный выбор вершины для клики из массива с номерами еще не использованных для клики вершин
                int tekNotKlikaPoint = rnd.Next(0, notKlikaPoints.Length - tekPoint);
                //присвоение выбранной вершине цвета
                pointColors[notKlikaPoints[tekNotKlikaPoint]] = tekColor;

                //удаление из массива всех вершин вне клики вершины, уже покрашенной
                for (int i = tekNotKlikaPoint; i < notKlikaPoints.Length - tekPoint - 1; i++)
                    notKlikaPoints[i] = notKlikaPoints[i + 1];
            }

            return pointColors;
        }


        /* составление двхмерного массива возможных ребер (для генерации тестов)
         * ребра не д.б. построены в графе ранее
           рёбра не должны соединять вершины одного цвета
         * выход: в 1 и 2 строках указаны вершины, которые соединет ребро с номером столбца */
        static int[,] PossibleEdges(int[,] graph       /*граф для помтроения рёбер*/,
                                    int[]  pointColors /*цвета вершин графа*/)
        {
            //массив из максимально возможного в graph ребер количества элементов
            int[,] notUsedEdges = new int[2, graph.GetLength(0) * (graph.GetLength(0) - 1) / 2];

            int tekEdge = 0; //текущее кол-во возможных ребер

            //проход по всем ВЕРШИНАМ
            for (int tekPoint = 0; tekPoint < graph.GetLength(0) - 1; tekPoint++)
                //проход по всем СОСЕДЯМ ВЕРШИНЫ (кроме тех которые уже рассматривались в качестве ВЕРШИНЫ)
                for (int tekNeighbour = tekPoint + 1; tekNeighbour < graph.GetLength(1); tekNeighbour++)
                    //если (вершина и её сосед не соединены) и (цвета вершины и её соседа не совпадают)
                    if (graph[tekPoint, tekNeighbour] == 0 && pointColors[tekPoint] != pointColors[tekNeighbour])
                    {
                        //создание ребра в массиве ребер
                        notUsedEdges[0, tekEdge] = tekPoint;
                        notUsedEdges[1, tekEdge] = tekNeighbour;

                        //увеличение кол-ва ребер, после создания ребра т.к. нумерация в массиве с 0
                        tekEdge++;
                    }

            //удаление незаполненных мест в массиве вершин
            DeleteEdges(ref notUsedEdges, tekEdge, notUsedEdges.GetLength(1) - tekEdge);

            return notUsedEdges;
        }


        /* удаление ребер из двумерного массива ребер (для генерации тестов)
         * удаление deleteHowMany ребер
         * удаление начиная с deleteFrom места */
        static void DeleteEdges(ref int[,] edges         /*откуда*/,
                                    int    deleteFrom    /*с какого места*/,
                                    int    deleteHowMany /*сколько*/)
        {
            //массив под ребра с уже удаленными лишними ребрами
            int[,] newEdges = new int[2, edges.GetLength(1) - deleteHowMany];

            //заполнение нового массива с ребрами
            for (int tekNewEdge = 0, tekEdge = 0; tekNewEdge < newEdges.GetLength(1); tekNewEdge++, tekEdge++)
            {
                //если достигли в старом массиве с ребрами места, откуда нужно удалять, то пропускаем столько вершин, сколько
                //нужно удалить, и добавляем в новый массив ребер сл. ребра
                if (tekEdge == deleteFrom)
                    tekEdge = tekEdge + deleteHowMany;

                newEdges[0, tekNewEdge] = edges[0, tekEdge];
                newEdges[1, tekNewEdge] = edges[1, tekEdge];
            }

            edges = newEdges;
        }


        /* поиск наиб. кол-ва рёбер при заданных кол-ве вершин и хроматич. числе (для генерации тестов)
         * нумерация вершин в данном случае по кругу (1 2 3; 1 2 3; 1 2 ...)
           т.к. чем больше разных цветов, тем больше ребер
         * выход: число - наиб. кол-во рёбер*/
        static int MaxNEdges(int nPoints     /*количество вершин*/,
                             int chromNumber /*хроматич. число*/)
        {
            int nColors = nPoints % chromNumber,                        //кол-во цветов, вершин кот-х на 1 БОЛЬШЕ
                nPointsOfColor = nPoints / chromNumber + 1,             //кол-во вершин 1 из цветов, вершин кот-х на 1 БОЛЬШЕ
                nEdgesOfPoint = nPoints - nPointsOfColor,               //кол-во вершин, с кот-ми можно соединить вершины тех цветов, вершин кот-х на 1 БОЛЬШЕ
                nEdges = nColors * nPointsOfColor * nEdgesOfPoint / 2;  //кол-во ребер тех вершин, цветов кот-х на 1 БОЛЬШЕ

            nColors = chromNumber - nColors;                                //кол-во цветов, вершин кот-х на 1 МЕНЬШЕ
            nPointsOfColor = nPointsOfColor - 1;                            //кол-во вершин 1 из цветов, вершин кот-х на 1 МЕНЬШЕ
            nEdgesOfPoint = nPoints - nPointsOfColor;                       //кол-во ребер тех вершин, цветов кот-х на 1 МЕНЬШЕ
            nEdges = nEdges + nColors * nPointsOfColor * nEdgesOfPoint / 2; //общее наибольшее кол-во ребер

            return nEdges;
        }


        /*переделывание матрицы смежности графа к матрице инцедентностиий (для генерации тестов)
         * выход: 1/0[вершина, ребро] */
        static int[,] ConvertedGraphFromSmeghnToIncedent(int[,] graph, /*граф, который нужно переделать*/
                                                         int    nEdges /*кол-во ребер в графе*/)
        {
            //граф для матрицы инцеденцй
            int[,] graphIncedent = new int[graph.GetLength(0), nEdges];

            //найденное кол-во ребер
            int tekNEdges = 0;

            //преобразование
            for (int tekPoint = 0; tekPoint < graph.GetLength(0) - 1; tekPoint++)
                //чтобы не проходить по каждому ребру по 2 раза, второй цикл не с 0
                for (int tekNeighbour = tekPoint + 1; tekNeighbour < graph.GetLength(1); tekNeighbour++)
                    //если вершины связаны
                    if (graph[tekPoint, tekNeighbour] == 1)
                    {
                        graphIncedent[tekPoint, tekNEdges] = 1;
                        graphIncedent[tekNeighbour, tekNEdges] = 1;
                        tekNEdges++;
                    }

            return graphIncedent;
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

                //заполняем массив соседей вершины и считаем их
                for (int tekNeighbour = 0; tekNeighbour < graph.GetLength(1); tekNeighbour++)
                    if (tekPoint != tekNeighbour && graph[tekPoint, tekNeighbour] != 0)
                    {
                        //если матрица смежности заполнена некорректно
                        if (graph[tekPoint, tekNeighbour] != graph[tekNeighbour, tekPoint])
                            okGraph = false;

                        futureNeighbours[tekFutureNeighbour] = tekNeighbour;
                        tekFutureNeighbour++;
                    }

                //теперь кол-во соседей известно, переписываем все в чистовой массив соответствия связанных вершин
                pointNeighbours[tekPoint] = new int[tekFutureNeighbour];
                for (int tekNeighbour = 0; tekNeighbour < pointNeighbours[tekPoint].Length; tekNeighbour++)
                    pointNeighbours[tekPoint][tekNeighbour] = futureNeighbours[tekNeighbour];
            }

            int[] pointColors;
            int gh = ChromaticNumber(pointNeighbours, out pointColors);

            return pointNeighbours;
        }


        /* переделывание матрицы инциденций графа к соответствию связанных вершин (для жадного алгоритма)
         * выход: граф[номер вершины][соседи вершины] */
        private static int[][] ConvertedGraphFromIncedentToNeighbours(    int[,] graph,  /*граф, который нужно переделать*/
                                                                      out bool   okGraph /*перменная для проверки корректности графа*/)
        {
            //создание матрицы смежности для промежуточного перехода
            int[,] graphSmeghn = new int[graph.GetLength(0), graph.GetLength(0)];

            okGraph = true;

            //проход по каждому ребру
            for (int tekEdge = 0; tekEdge < graph.GetLength(1); tekEdge++)
            {
                int point1 = 0,  //вершина 1, инцедентная к текущему ребру
                    point2 = 0;  //вершина 1, инцедентная к текущему ребру

                bool one  = false,  //найдена первая вершина ребра
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
                            okGraph = false;


                //если найдены обе вершины инцедентные ребру
                if (both == true)
                {
                    graphSmeghn[point1, point2] = 1;
                    graphSmeghn[point2, point1] = 1;
                }
                else
                    //граф не корректен
                    okGraph = false;
            }

            return ConvertedGraphFromSmeghnToNeighbours(graphSmeghn, out bool okGraphSmeghn);
        }


        /* проверка графа на корректность и преобразование к спискам соседних вершин */
        private static int[][] ConvertedGraphToNeighbours(    byte    graphType,      /*способ задание графа (1 - смежность, 2 - инцидентность, 3 - списки соседей*/
                                                          out bool    okGraph,        /*проверка корректности поданного графа*/
                                                              int[][] graph3  = null, /*в случае пустой ссылки будет чекать сл способ задания*/
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


        /* проверка списков соседних вершин на корректность 
         * вывод: корректный (true) / нет (false) */
        private static bool OkNeighboursGraph (int[][] graph /*граф который нужно проверить*/)
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
        private static int ChromaticNumber(    int[][] graph,             /*граф для поиска*/
                                           out int[]   pointColors,       /*массив с цветами вершин*/
                                               int     numerationType = 5 /*каким способом нумеровать вершины*/)
        {
            int[] pointNums;  //исходный номер вершины[присвоенный номер]

            //нумерация вершин в зависимости от того, как нумеровать
            switch (numerationType)
            {
                default:  //рандомная нумерация
                    pointNums = RandomNumeration(graph.Length);
                    break;

                case 4:  //в порядке возрастная степеней вершин
                    pointNums = IncreasingPointDegreeNumeration(graph);
                    break;

                case 3:  //в порядке убывания степеней вершин
                    pointNums = DecreasingPointDegreeNumeration(graph);
                    break;

                case 1:  //обход в ширину
                    pointNums = WidthNumeration(graph);
                    break;

                case 2:  //обход в глубину
                    pointNums = DepthNumeration(graph);
                    break;
            }

            pointColors = new int[graph.Length];              //цвет вершины[исходный номер вершины]

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


        /* нумерация обходом в ширину
         * выход: исходный номер[присвоенный номер] */
        static int[] WidthNumeration(int[][] graph /*граф для нумерации вершин*/)
        {
            int[] pointNums = new int[graph.Length];  //исходный номер[присвоенный номер]
            
            bool[] alreadyNumed = new bool[graph.Length];  //массив для проверки, пронумерована ли та или иная вершина
            //изначально нет пронумерованных вершин
            for (int tekPoint = 0; tekPoint < graph.Length; tekPoint++)
                alreadyNumed[tekPoint] = false;

            int tekPointNum = 0,                    //место, на котором стоит последняя вершина в массиве нумерации
                tekNAlreadyNumed = 0;               //кол-во развернутых вершина
            pointNums[tekPointNum] = 0;             //самой первой вершиной в нумерации будет нулевая
            alreadyNumed[tekPointNum] = true;       //запоминаем, что нулевая вершина уже пронумерована

            //пока какой-нибудь вершине не будет присвоен последний номер
            while (tekPointNum < graph.Length - 1)
            {
                //запись в очередь всех соседей раскрертывающейся вершины (она под номером tekNalreadyNumed в массиве pointNums)
                for (int tekNeighbour = 0; tekNeighbour < graph[pointNums[tekNAlreadyNumed]].Length; tekNeighbour++)
                    //если этот сосед еще не пронумерован
                    if (alreadyNumed[graph[pointNums[tekNAlreadyNumed]][tekNeighbour]] == false)
                    {
                        tekPointNum++;
                        pointNums[tekPointNum] = graph[pointNums[tekNAlreadyNumed]][tekNeighbour];
                        alreadyNumed[graph[pointNums[tekNAlreadyNumed]][tekNeighbour]] = true;
                    }

                //т.к. всех соседей этой вершиныуже поместили в очередь, она стала развёрнутой
                tekNAlreadyNumed++;

                //в случае если очередь закончилась, а вершины пронумерованы не все
                if (tekPointNum < graph.Length - 1 && pointNums[tekNAlreadyNumed] == 0)
                {
                    //поиск первой непронумерованной вершины
                    int tekPoint;
                    for (tekPoint = 1; alreadyNumed[tekPoint] == true; tekPoint++);

                    //нумерация этой вершины
                    pointNums[tekNAlreadyNumed] = tekPoint;
                    tekPointNum++;
                    alreadyNumed[tekPoint] = true;
                }

            }

            return pointNums;
        }


        /* нумерация обходом глубину
         * выход: исходный номер[присвоенный номер] */
        static int[] DepthNumeration(int[][] graph /*граф для нумерации вершин*/)
        {
            int  [] pointNums = new int[graph.Length];     //исходный номер[присвоенный номер]
            int     tekPointNum;                           //номер последней вершины в pointNums
            int  [] way = new int[graph.Length];           //запоминаем путь по которому спускались
            int     tekWay;                                //номер последней вершины в way
            bool [] alreadyNumed = new bool[graph.Length]; //проверка пронумерована ли вершина

            //стартуем обход с нулевой вершины
            tekPointNum = 0;
            pointNums[tekPointNum] = 0;
            tekWay = 0;
            way[tekWay] = 0;
            alreadyNumed[0] = true;

            //все остальные вершины пока не пронумерованы
            for (int tekPoint = 1; tekPoint < graph.Length; tekPoint++)
                alreadyNumed[tekPoint] = false;

            while (pointNums[graph.Length - 1] == 0)
            {
                int tekNeighbour;
                for (tekNeighbour = 0; tekNeighbour < graph[way[tekWay]].Length && alreadyNumed[graph[way[tekWay]][tekNeighbour]] == true; tekNeighbour++);

                //если у вершины нет соседей или (все соседи уже пронумерованы)
                if (graph[way[tekWay]].Length == 0 || (tekNeighbour == graph[way[tekWay]].Length && alreadyNumed[graph[way[tekWay]][tekNeighbour - 1]] == true))
                {
                    //возращаемся на верхнюю вершины в пути
                    way[tekWay] = 0;
                    tekWay--;
                }
                //если же у вершины есть непронумерованные соседи
                else
                {
                    //добавляем 1ого соседа в путь
                    tekWay++;
                    way[tekWay] = graph[way[tekWay - 1]][tekNeighbour];

                    //говорим, что он пронумерован
                    alreadyNumed[graph[way[tekWay - 1]][tekNeighbour]] = true;

                    //присваиваем ему номер
                    tekPointNum++;
                    pointNums[tekPointNum] = graph[way[tekWay - 1]][tekNeighbour];
                }

                //в случае если пришли в начальную вершины пути, дальше идти некуда, а пронумерованы еще не все вершины
                if (tekWay < 0 && pointNums[graph.Length - 1] == 0)
                {
                    //ищем непронумерованную вершину
                    int tekPoint;
                    for (tekPoint = 1; alreadyNumed[tekPoint] == true; tekPoint++);
                    
                    //делаем ее стартовой в новом пути
                    tekWay = 0;
                    way[tekWay] = tekPoint;

                    //присваиваем ей номер
                    tekPointNum++;
                    pointNums[tekPointNum] = tekPoint;

                    //говорим, что она уже пронумерована
                    alreadyNumed[tekPoint] = true;
                }
            }

            return pointNums;
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
                while(tekPointNum - 1 >= 0 && pointNumsAndDegrees[1, tekPointNum] > pointNumsAndDegrees[1, tekPointNum - 1])
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


        /* нумерация вершин графа в порядке возрастания их степеней (для жадного алгоритма)
         * выход: исходный номер выершины[присвоенный] */
        private static int[] IncreasingPointDegreeNumeration(int[][] graph /*граф для нумерации вершин*/)
        {
            int[,] pointNumsAndDegrees = new int[2, graph.Length];   //исходный[присвоенный номер]ъ

            //заполнение массива номеров
            for (int tekPoint = 0; tekPoint < graph.Length; tekPoint++)
            {
                pointNumsAndDegrees[0, tekPoint] = tekPoint;
                pointNumsAndDegrees[1, tekPoint] = graph[tekPoint].Length;

                int tekPointNum = tekPoint;  //номер столбца в массиве номеров и степеней 
                //пока (элемент не первый и степень элемента меньше степени предыдущего)
                while (tekPointNum - 1 >= 0 && pointNumsAndDegrees[1, tekPointNum] < pointNumsAndDegrees[1, tekPointNum - 1])
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


        /* рандомная нумерация вершин графа (для жадного алгоритма)
         * выход: исходный номер выершины[присвоенный] */
        private static int[] RandomNumeration(int nPoints /*количество вершин для нумерации*/)
        {
            int[] pointNums = new int[nPoints];   //исходный[присвоенный номер]

            int[] pointMas = new int[nPoints];    //номер элемента[номер вершины графа]
            for (int i = 0; i < nPoints; i++)
                pointMas[i] = i;

            Random rnd = new Random();

            //цикл нумерации вершин
            for (int tekPoint = nPoints; tekPoint > 0; tekPoint--)
            {
                //выбор вершины из оставшихся point вершин в массиве pointMas
                int tekiInPointMas = rnd.Next(0, tekPoint);

                //присвоение вершине номера point
                pointNums[tekPoint - 1] = pointMas[tekiInPointMas];

                //удаление из массива вершин той, которой уже присвоили номер
                for (int i = tekiInPointMas; i < tekPoint - 1; i++)
                    pointMas[i] = pointMas[i + 1];
            }

            return pointNums;
        }


        /* проверка на наличие у вершины графа соседей того же цвета (для жадного алгоритма)
         * выход: если нету - true, есть - false */
        private static bool NoSuchNeighbours(int[] pointNeighbours /*все соседи вершины*/,
                                             int[] pointColors     /*цвет[номер вершины]*/,
                                             int   color           /*цвет для проверки*/)
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


        /* печать графа (для тестирования)
         * выход: массив печатается как как массив одномерных массивов */
        static void PrintArray(int[][] mas                 /*массив для печати*/,
                               string description = "Граф" /*выводимая перед массивом строка*/)
        {
            //если описания нет, чтобы не пропускалась строчка
            if (description != "")
                Console.WriteLine(description + ": ");

            //обработка пустого массива
            if (mas.Length == 0)
                Console.WriteLine("граф пустой");

            //если массив не пустой, то вывод всех элементов
            else
            {
                Console.WriteLine("   Вершина: соседи");
                //цикл вывода рваного массива как массива одномерных массивов
                for (int i = 0; i < mas.Length; i++)
                    //печать одномерного массива
                    PrintArray(mas[i], Convert.ToString(i + 1));
            }
        }


        /* печать соседей вершины (для тестирования)
         * выход: сообщает об отсутствии соседей или выводит всех соседей */
        static void PrintArray(int[]  mas              /*соседи для печати*/,
                               string description = "" /*выводимая перед массивом строка*/)
        {
            //если описания нет, чтобы не пропускалась строчка
            if (description != "")
                Console.Write("   " + description + ": ");

            //обратботка пустого массива
            if (mas.Length == 0)
                Console.WriteLine("нет соседей");

            //если массив не пустой, то вывод всех элементов
            else
            {
                foreach (int x in mas)
                    Console.Write(Convert.ToString(x) + " ");
                Console.WriteLine();
            }
        }


        /* ввод целого числа (для тестирования)
         * из отрезка от left до right */
        static int IntNumber(string action  = "Введите целое число",
                             string mistake = "Нужно целое число",
                             int    left    = -2147483648,
                             int    right   =  2147483647 /*границы чисел типа int*/)
        {
            Console.Write(action + ": ");

            int z;        //целое число
            string input; //строка для ввовда

            do //пока юзер не введёт нужное число
            {
                input = Console.ReadLine();

                //если введено не число, или оно выходит за границы
                if (!int.TryParse(input, out z) || z < left || z > right)
                    Console.Write("   " + mistake + ": ");

            } while (!int.TryParse(input, out z) || z < left || z > right);

            return z;
        }




        static void PrintMas (int[,] mas)
        {
            for (int i = 0; i < mas.GetLength(0); i++)
            {
                for (int j = 0; j < mas.GetLength(1); j++)
                    Console.Write(mas[i, j] + " ");
                Console.WriteLine();
            }
                
        }


    }
}
