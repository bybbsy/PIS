using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PIS_2
{
    public partial class Form1 : Form
    {
        int axisYmin = 0;
        int axisYmax = 100;

        int axisXmin = 0;
        int axisXmax = 100;

        int dotWidth = 4;
        int dotHeight = 4;

        List<int> xPoints = new List<int>();
        List<int> yPoints = new List<int>();

        List<double> xDots = new List<double>() { 0, 1, 2 };
        List<double> yDots = new List<double>() { 0, 1, 3 };
        List<double> squareValues = new List<double>();

        Legendre legendre = new Legendre();

        public Form1()
        {
            InitializeComponent();
        }

        private void ppPaintHandler(object sender, PaintEventArgs e)
        {
            chart1.Series[0].Points.AddXY(0, 0);
            chart1.Series[0].Points.AddXY(1, 1);
            chart1.Series[0].Points.AddXY(2, 3);

            int iMain = Convert.ToInt32(Convert.ToDecimal(iNumeric.Value));

            Graphics g = e.Graphics;

            Pen blackPen = new Pen(Color.Black);
            Pen redPen = new Pen(Color.Red);

            Brush myBrush = new SolidBrush(Color.Red);

            double sq_sum = 0;
            //g.FillEllipse(myBrush, 10, 10, 100, 100);
            int prevIndex;

            if(xDots.Count == yDots.Count)
            {
                if(xDots.Count > 1)
                {
                    for (int i = 0; i < xDots.Count; i++)
                    {
                        prevIndex = i;

                        if(i - 1 >= 0)
                        {
                            prevIndex = i - 1;
                        }

                        g.DrawLine(blackPen, (float)xDots[prevIndex], (float)yDots[prevIndex], (float)xDots[i], (float)yDots[i]);
                        g.FillEllipse(myBrush, (float)xDots[prevIndex] - (this.dotWidth / 2), (float)yDots[prevIndex] - (this.dotHeight / 2), this.dotWidth, this.dotHeight);

                        double square = legendre.getSquare(iMain, xDots[prevIndex], yDots[prevIndex], xDots[i], yDots[i]);
                        sq_sum += square;

                        //if(i - 1 >= 0)
                        //{
                        //    //g.DrawLine(blackPen, (float)xDots[i - 1], (float)yDots[i - 1], (float)xDots[i], (float)yDots[i]);
                        //    //g.FillEllipse(myBrush, (float)xDots[i - 1] - (this.dotWidth / 2), (float)yDots[i - 1] - (this.dotHeight / 2), this.dotWidth, this.dotHeight);

                        //    //double square = legendre.getSquare(iMain, xDots[i - 1], yDots[i - 1], xDots[i], yDots[i]);
                        //    //sq_sum += square;

                        //} else
                        //{
                        //    //g.DrawLine(blackPen, (float)xDots[i], (float)yDots[i], (float)xDots[i], (float)yDots[i]);
                        //    //g.FillEllipse(myBrush, (float)xDots[i] - (this.dotWidth / 2), (float)yDots[i] - (this.dotHeight / 2), this.dotWidth, this.dotHeight);

                        //    //double square = legendre.getSquare(iMain, xDots[0], yDots[0], xDots[i], yDots[i]);
                        //    //sq_sum += square;
                        //}
                    }
                }
            }  
            squareValues.Add(sq_sum);
            labelMouseCoordinate.Text = String.Join("\n", squareValues);
        }

        private void ppDoubleClick(object sender, EventArgs e)
        {
            //labelMouseCoordinate.Text = $"X:{} Y:{}";
        }

        private void ppMouseDoubleClick(object sender, MouseEventArgs e)
        {
            //labelMouseCoordinate.Text = $"X:{e.X} Y:{e.Y}";
            double x = Math.Round(Convert.ToDouble(e.X) / 100, 3);
            double y = Math.Round(Convert.ToDouble(e.Y) / 100, 3);
            
            xDots.Add(x);
            yDots.Add(y);

            chart1.Series[0].Points.AddXY(x, y);
            dataGridView1.Rows.Clear();
            chart1.Series[1].Points.Clear();
            drawPanel.Refresh();
        }

        private void clearDataButton_Click(object sender, EventArgs e)
        {
            yDots.Clear();
            xDots.Clear();
            chart1.Series[0].Points.Clear();
            chart1.Series[1].Points.Clear();
            squareValues.Clear();
            labelMouseCoordinate.Text = "";
            dataGridView1.Rows.Clear();
            drawPanel.Refresh();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
