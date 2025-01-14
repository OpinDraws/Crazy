using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using ZedGraph;
using System.Diagnostics;

namespace Task17
{
    
    public partial class Form1 : Form
    {
        string answer;
        int choseArray;
        int choseMove;
        bool checkArray;
        MyHashMap<int, int> listLi= new MyHashMap<int, int>();
        MyTreeMap<int, int> listAr = new MyTreeMap<int, int>();
        public Form1()
        {
            InitializeComponent();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            choseArray = listBox1.SelectedIndex;
        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            choseMove = listBox2.SelectedIndex; 
        }

        private void button1_Click(object sender, EventArgs e)
        {
            LineItem my;
            GraphPane pane = zedGraphControl1.GraphPane;
            PointPairList list1 = new PointPairList();
            PointPairList list2 = new PointPairList();
            pane.CurveList.Clear();
            pane.XAxis.Scale.Min = 0;
           
            
            for (int j=0;j<choseArray+5; j++)
            {
                long countime=0;
                for (int l = 0; l < 1; l++) { 
                    Stopwatch clock = new Stopwatch();
                    switch (choseMove)
                    {
                        case 0:
                            {

                                clock.Restart();
                                for (int i = 0; i < Math.Pow(10, j); i++)
                                {

                                    listAr.Put(i, i);
                                }
                                break;
                            }
                        case 1:
                            {
                                clock.Restart();
                                for (int i = 0; i < Math.Pow(10, j); i++)
                                {
                                    listAr.Get(i);
                                }
                                break;
                            }

                        case 2:
                            {
                                clock.Restart();
                                if (checkArray == true)
                                {
                                    for (int i = 0; i < Math.Pow(10, j); i++)
                                    {
                                        listAr.Remove(i);
                                    }
                                }
                                break;

                            }
                       
                    }
                    clock.Stop();
                    countime += clock.ElapsedMilliseconds*100000;
                    
                }

                list1.Add(j, countime/2000);
            }
            for (int j = 0; j < choseArray + 5; j++)
            {
                long countime = 0;
                for (int l = 0; l < 10; l++)
                {
                    Stopwatch clock = new Stopwatch();
                    switch (choseMove)
                    {
                        case 0:
                            {

                                clock.Restart();
                                for (int i = 0; i < Math.Pow(10, j); i++)
                                {

                                    listLi.Put(i, i);
                                }
                                break;
                            }
                        case 1:
                            {
                                clock.Restart();
                                for (int i = 0; i < Math.Pow(10, j); i++)
                                {
                                    listLi.Get(i);
                                }
                                break;
                            }

                        case 2:
                            {
                                if (checkArray == true)
                                {
                                    clock.Restart();
                                    for (int i = 0; i < Math.Pow(10, j); i++)
                                    {
                                        listLi.RemoveKey(i);
                                    }
                                }
                                break;
                            }
                    }
                    clock.Stop();
                    countime += clock.ElapsedMilliseconds*100000;
                    
                }
                list2.Add(j, countime / 20);

            }
            LineItem myCurve = pane.AddCurve("Реальный", list1, Color.Black, SymbolType.None);
            LineItem myCurve2 = pane.AddCurve("Ссылочный", list2, Color.Red, SymbolType.None);
            zedGraphControl1.AxisChange();
            zedGraphControl1.Invalidate();
        }
    }

}
