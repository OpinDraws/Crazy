using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ObjectSort;
using ArrayGeneric;
using ZedGraph;
using System.Diagnostics;
using System.Collections;
using System.IO;
namespace ProjectZomboni
{

    public partial class Form1 : Form
    {
        
        int choseArray;
        bool flag1 = false, flag2 = false, flag3 = false;
        int choseSort;
        List<List<int[]>> AllArray = new List<List<int[]>>();
        List<List<int[]>> AllArrayCopy = new List<List<int[]>>();

        public Form1()
        {
           
            InitializeComponent();
            GraphPane pane = zedGraphControl1.GraphPane;
            pane.Title.Text = "График скорости сортировки массивов в зависимости от количества элементов";
            pane.XAxis.Title.Text = "Количество элементов";
            pane.YAxis.Title.Text = "Время обработки, мкс";
        }

        private void checkedListBox1_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            for (int ix = 0; ix < checkedListBox1.Items.Count; ++ix)
                if (ix != e.Index) checkedListBox1.SetItemChecked(ix, false);
            choseArray = e.Index;
            flag1 = true;
        }

        private void checkedListBox2_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            for (int ix = 0; ix < checkedListBox2.Items.Count; ++ix)
                if (ix != e.Index) checkedListBox2.SetItemChecked(ix, false);
            choseSort = e.Index;
            flag2 = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (flag1 == true && flag2 == true && flag3 == true)
            {
                GraphPane pane = zedGraphControl1.GraphPane;
                Stopwatch sw = new Stopwatch();
                pane.CurveList.Clear();
                int pow = 0; int chose1 = 0;
                int chose2 = 0;
                ArraySort sort = new ArraySort();
                if (choseSort == 0)
                {
                    pow = 3;
                    chose1 = 0;
                    chose2 = 5;
                }
                if (choseSort == 1)
                {
                    pow = 4;
                    chose1 = 5;
                    chose2 = 8;
                }
                if (choseSort == 2)
                {
                    pow = 5;
                    chose1 = 8;
                    chose2 = 14;
                }
                long[] listTime = new long[14];
                for (int iz = chose1; iz < chose2;iz++)
                {
                    for (int i = 0; i < AllArray.Count; i++)
                    {
                        for (int j = 0; j < AllArray[i].Count; j++)
                        {
                            for (int k = 0; k < AllArray[i][j].Length; k++)
                            {
                                AllArrayCopy[i][j][k] = AllArray[i][j][k];
                            }

                        }
                    }
                    PointPairList list1 = new PointPairList();
                    for (int iy = 1; iy <= pow; iy++)
                    {

                        long timer = 0;
                        
                       
                       
                        for (int ix = 0; ix < 7; ix++)
                        {
                            sw.Restart();
                            sort.SortChose(iz, AllArrayCopy[iy - 1][ix]);
                            sw.Stop();
                            timer += sw.ElapsedMilliseconds*1000;
                        }
                        list1.Add((int)(Math.Pow(10, iy)), timer / 20);
                        listTime[iz] = timer;

                    }
                    switch (iz)
                    {
                        case 0: { LineItem myCurve1 = pane.AddCurve("Bubble Sort", list1, Color.Blue, SymbolType.None); label2.Text = $"Время максимальное: {listTime[0]}"; break; }
                        case 1: { LineItem myCurve1 = pane.AddCurve("Instertion Sort", list1, Color.Red, SymbolType.None); label4.Text = $"Время максимальное: {listTime[1]}"; break; }
                        case 2: { LineItem myCurve1 = pane.AddCurve("Selection Sort", list1, Color.Yellow, SymbolType.None); label5.Text = $"Время максимальное: {listTime[2]}"; break; }
                        case 3: { LineItem myCurve1 = pane.AddCurve("Shaker Sort", list1, Color.Chocolate, SymbolType.None); label6.Text = $"Время максимальное: {listTime[3]}"; break; }
                        case 4: { LineItem myCurve1 = pane.AddCurve("Dwarf Sort", list1, Color.BlueViolet, SymbolType.None); label7.Text = $"Время максимальное: {listTime[4]}"; label8.Text = $" "; break; }
                        case 5: { LineItem myCurve1 = pane.AddCurve("Bitonic Sort", list1, Color.Brown, SymbolType.None); label2.Text = $"Время максимальное: {listTime[5]}"; break; }
                        case 6: { LineItem myCurve1 = pane.AddCurve("Shell Sort", list1, Color.PaleGoldenrod, SymbolType.None); label4.Text = $"Время максимальное: {listTime[6]}"; break; }
                        case 7: { LineItem myCurve1 = pane.AddCurve("Tree Sort", list1, Color.DarkGreen, SymbolType.None); label5.Text = $"Время максимальное: {listTime[7]}"; label6.Text = $" "; label7.Text = $" "; label8.Text = $" "; break; }
                        case 8: { LineItem myCurve1 = pane.AddCurve("Comb Sort", list1, Color.RosyBrown, SymbolType.None); label2.Text = $"Время максимальное: {listTime[8]}"; break; }
                        case 9: { LineItem myCurve1 = pane.AddCurve("Heap Sort", list1, Color.MediumVioletRed, SymbolType.None); label4.Text = $"Время максимальное: {listTime[9]}"; break; }
                        case 10: { LineItem myCurve1 = pane.AddCurve("Quick Sort", list1, Color.Purple, SymbolType.None); label5.Text = $"Время максимальное: {listTime[10]}"; break; }
                        case 11: { LineItem myCurve1 = pane.AddCurve("Merge Sort", list1, Color.Black, SymbolType.None); label6.Text = $"Время максимальное: {listTime[11]}"; break; }
                        case 12: { LineItem myCurve1 = pane.AddCurve("Counting Sort", list1, Color.MediumTurquoise, SymbolType.None); label7.Text = $"Время максимальное: {listTime[12]}"; break; }
                        case 13: { LineItem myCurve1 = pane.AddCurve("Radix Sort", list1, Color.Aqua, SymbolType.None); label8.Text = $"Время максимальное: {listTime[13]}"; break; }
                    }

                    zedGraphControl1.AxisChange();
                    zedGraphControl1.Invalidate();
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void checkedListBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            
            StreamWriter sw = new StreamWriter("C:\\Users\\MiYa\\source\\repos\\ProjectZomboni\\Массивы.txt");
            int iz=0;
            switch (choseSort)
            {
                case 0:
                    iz = 0;
                    break;
                case 1:
                    iz = 5; break;
                case 2: iz = 8; break;
            }
            for (int i = 0; i < AllArrayCopy.Count; i++, iz++)
            {
                /*
                switch (iz)
                {
                    case 0: { sw.WriteLine("Bubble Sort"); break; }
                    case 1: { sw.WriteLine("Instertion Sort"); break; }
                    case 2: { sw.WriteLine("Selection Sort"); break; }
                    case 3: { sw.WriteLine("Shaker Sort"); break; }
                    case 4: { sw.WriteLine("Dwarf Sort"); break; }
                    case 5: { sw.WriteLine("Bitonic Sort"); break; }
                    case 6: { sw.WriteLine("Shell Sort"); break; }
                    case 7: { sw.WriteLine("Tree Sort"); break; }
                    case 8: { sw.WriteLine("Comb Sort"); break; }
                    case 9: { sw.WriteLine("Heap Sort"); break; }
                    case 10: { sw.WriteLine("Quick Sort"); break; }
                    case 11: { sw.WriteLine("Merge Sort"); break; }
                    case 12: { sw.WriteLine("Counting Sort"); break; }
                    case 13: { sw.WriteLine("Radix Sort"); break; }
                } */
                for (int j = 0; j < AllArrayCopy[i].Count; j++)
                {
                    for (int l = 0; l < AllArrayCopy[i][j].Length; l++)
                    {

                        sw.Write($"{AllArrayCopy[i][j][l]} ");

                    }
                }
                sw.WriteLine();
            }
            sw.Close();
            

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (flag1 == true && flag2 == true)
            {
                AllArray.Clear();
                AllArrayCopy.Clear();
                Arrays array = new Arrays();
                for (int iy = 1; iy <= choseSort + 3; iy++)
                {
                    List<int[]> help = new List<int[]>();
                    List<int[]> help2= new List<int[]>();
                    for (int ix = 0; ix < 7; ix++)
                    {
                        int[] helpArray = new int[(int)Math.Pow(10, iy)];
                        help.Add(array.Generaction(choseArray, (int)Math.Pow(10, iy)));
                        help2.Add(helpArray);
                    }
                    AllArray.Add(help);
                    AllArrayCopy.Add(help2);
                }
                flag3 = true;
            }
        }

        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
