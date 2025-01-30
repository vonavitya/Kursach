using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Поиск_хроматического_числа
{
    public partial class fFindChromaticNumber : Form
    {
        private string fileName;
        private int[][] graph;

        //загрузка формы
        public fFindChromaticNumber()
        {
            InitializeComponent();
        }


        /* возвращение исходных параметров виимости/доступности всем элементам */
        private void BackToStart()
        {
            //скрытие панели с кнопками
            P_Actions.Visible = false;

            //скрытие панели для ввода графа
            P_Input.Visible = false;
            //блокировка кнопки, завершающем ввод графа
            B_Input.Enabled = false;

            //скрытие панели, сообщающей результат ввода/загрузки графа
            L_InputResult.Visible = false;
            //скрытие кнопки подсчёта хроматического числа
            B_Calculate.Visible = false;

            //скрытие панели с выводом посчитанного хром. числа
            P_Answer.Visible = false;

            //скрытие панели в тексбоксом
            P_InputOutput.Visible = false;
            //возврат заголовка
            L_InputOutput.Text = "Информация о графе";
            //запрет на ввод текста в текстбокс и его очистка
            TB_InputOutput.ReadOnly = true;
            TB_InputOutput.Clear();

            //обнуление графа на случай повторного использования приложения/ошибки
            graph = null;
        }
        
        
        /* загрузка графа из файла */
        private void B_Download_Click(object sender, EventArgs e)
        {
            //возвращение исходных параметрова видимости / доступности всем элементам
            BackToStart();

            //открытие окна выбора файла
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                //показ панели с кнопками
                P_Actions.Visible = true;

                //для взаимодействия запоминаем путь и имя файла
                fileName = openFileDialog1.FileName;

                //проверка корректности файла
                TB_InputOutput.Text = File.ReadAllText(fileName);
                bool okGraph = Functionality.CheckingInput(TB_InputOutput.Lines, out graph);
                //если файл содержит корректный граф
                if (okGraph)
                {
                    //сообщение об успешной загрузке
                    L_InputResult.Text = "Граф успешно загружен!";
                    L_InputResult.ForeColor = Color.Black;
                    L_InputResult.Visible = true;
                    
                    //в случае если граф задан списками соседних вершин или матрицей инцидентности и нет ребер матрицы будут пустые
                    if (graph.Length == 0)
                    {
                        L_Result.Text = "1";
                        P_Answer.Visible = true;

                        L_InputOutput.Text = "Вершина - цвет";
                        TB_InputOutput.Text = "Все вершины красятся в один цвет.";
                        P_InputOutput.Visible = true;
                    }
                    else
                    {
                        //показ кнопки "вычислить хром. число"
                        B_Calculate.Visible = true;
                        B_Calculate.Enabled = true;
                    }
                        
                }
                //если в файле нет графа или он задан некорректно
                else
                {
                    //сообщение о некорректных данных
                    L_InputResult.Text = "Некорректные данные!\nПовторите попытку!";
                    L_InputResult.ForeColor = Color.Red;
                    L_InputResult.Visible = true;
                }
            }
        }


        /* ввод информации о графе через окно взаимодействия */
        private void B_Discribe_Click(object sender, EventArgs e)
        {
            //возвращение исходных параметрова видимости / доступности всем элементам
            BackToStart();
            
            //показать панель с кнопками
            P_Actions.Visible = true;
            //показать панель для ввода
            P_Input.Visible = true;

            //показываем панель с информацией о графе
            P_InputOutput.Visible = true;
            //даем возможность ввода в текстбокс (после ввода отключаем)
            TB_InputOutput.ReadOnly = false;
        }


        //проверка текстбокса на непустоту при режиме ввода (идёт ввод графа)
        private void TB_InputOutput_TextChanged(object sender, EventArgs e)
        {
            //если поле пустое и включен режим редактирования (идет ввод графа)
            if (TB_InputOutput.Text != "" && TB_InputOutput.ReadOnly == false)
                B_Input.Enabled = true;
            else
            //если включён режим редактирования (идёт ввод графа)
            if (TB_InputOutput.ReadOnly == false)
                B_Input.Enabled = false;
        }


        /* при вводе инфы о графе нажатие кнопки "Готово" после ввода */
        private void B_Input_Click(object sender, EventArgs e)
        {
            //блокировка кнопки "Готово"
            B_Input.Enabled = false;

            //проверка корректности графа
            bool okGraph = Functionality.CheckingInput(TB_InputOutput.Lines, out graph);
            //если граф корректный
            if (okGraph)
            {
                //запрет ввода в текстбокс
                TB_InputOutput.ReadOnly = true;

                //сообщение об успешной загрузке графа
                L_InputResult.Text = "Граф успешно загружен!";
                L_InputResult.ForeColor = Color.Black;
                L_InputResult.Visible = true;


                if (graph.Length == 0)
                {
                    L_Result.Text = "1";
                    P_Answer.Visible = true;

                    L_InputOutput.Text = "Вершина - цвет";
                    TB_InputOutput.Text = "Все вершины красятся в один цвет.";
                    P_InputOutput.Visible = true;
                }
                else
                {
                    //показ кнопки "вычислить хром. число"
                    B_Calculate.Visible = true;
                    B_Calculate.Enabled = true;
                }
            }
            //если граф некорректный
            else
            {
                //сообщение о некорректности данных
                L_InputResult.Text = "Некорректные данные!\nПовторите попытку!";
                L_InputResult.ForeColor = Color.Red;
                L_InputResult.Visible = true;
            }
        }


        //подсчёт результатов
        private void B_Calculate_Click(object sender, EventArgs e)
        {
            //блокировка кнопки "посчитать хром. число"
            B_Calculate.Enabled = false;
            
            //получение результатов и их вывод
            int[] pointColors;   //массив со способом раскраски
            L_Result.Text = Convert.ToString(Functionality.ChromaticNumber(graph, out pointColors));
            P_Answer.Visible = true;

            //изменение текста в текстбоксе на способ раскраски
            L_InputOutput.Text = "Вершина - цвет";
            string[] output = new string[graph.Length];
            for (int tekPoint = 1; tekPoint <= pointColors.Length; tekPoint++)
                output[tekPoint - 1] = tekPoint + " - " + pointColors[tekPoint - 1] + "\n";
            TB_InputOutput.Lines = output;
            //показ текстбокса
            P_InputOutput.Visible = true;
        }

        
        //показ окна со справкой
        private void B_Manual_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Требования к информации о графе распространяются и на графы, загруженные из файла, и на графы, введённые в приложении.\n" +
                            "   1. В первой строчке должна быть размещена информация\n" +
                            "       о способе задания графа, а именно:\n" +
                            "         - \"матрица смежности\",\n" +
                            "         - \"матрица инцидентности\",\n" +
                            "         - \"списки соседних вершин\".\n" +
                            "       Если фраза будет написана неправльно, приложение выдаст\n" +
                            "       ошибку. В следующих строчках должен быть задан граф.\n" +
                            "   2. Граф не может быть ориентированным.\n" +
                            "   3. В случае задания графа матрицей инцидентности, каждому\n" +
                            "       ребру должна быть инцидентны ровно две вершины. В случае\n" +
                            "       задания списками соседних вершин, для каждой пары смежных\n" +
                            "       вершин необходимо укзать соседа как в списке соседей первой\n" +
                            "       вершины, так и в списке соседей второй.\n" +
                            "   4. В графе должно быть не меньше одной вершины. Пустые\n" +
                            "       матрица инцидентности, списки соседних вершин или матрица\n" +
                            "       смежности воспринимаются как графы без рёбер.\n" +
                            "   5. При задании графа использовать систему 0/1, разделенные\n" +
                            "       пробелом (в случае матрицы смежности / инцидентности).\n" +
                            "       В случае списков соседних вершин нумерация вершин\n" +
                            "       начинается с 1.");
        }



    }
}
