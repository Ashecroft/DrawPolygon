using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml.Serialization;
using System.IO;

namespace DrawPolygon
{
    public partial class Form1 : Form
    {
        Graphics graph;
        Polygon myPolygon = null;
        public Form1()
        {
            InitializeComponent();

            graph = pnlCanvas.CreateGraphics();

            tslPointState.Text = "";
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            if (myPolygon != null && myPolygon.IsCompleted == false)
            {
                myPolygon.Points.Add(new Point(e.X, e.Y));
                if (myPolygon.Points.Count > 1)
                {
                    myPolygon.DrawLine(graph);
                }
            }
            if (myPolygon != null && myPolygon.IsCompleted == true)
            {
                if (myPolygon.CheckIsPointInside(new Point(e.X, e.Y)))
                    tslPointState.Text = "Точка внутри многоугольника";
                else
                    tslPointState.Text = "Точка снаружи многоугольника";

            }
        }

        private void panel1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (myPolygon != null)
            {
                myPolygon.Points.Add(new Point(e.X, e.Y));
                if (myPolygon.Points.Count > 1)
                {
                    myPolygon.DrawLine(graph);
                    myPolygon.Complete(graph);
                    myPolygon.IsCompleted = true;
                }
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            if (myPolygon != null)
            {
                graph.Clear(SystemColors.Control);
            }
            myPolygon = new Polygon();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                try
                {
                    myPolygon.Save(saveFileDialog1.FileName);
                    tslPointState.Text = "Полигон сохранен";

                }
                catch 
                {
                    tslPointState.Text = "Ошибка при сохранении!!!";
                }
                
            }
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                try
                {
                    myPolygon = Polygon.Load(openFileDialog1.FileName);
                    myPolygon.DrawFull(graph);
                    tslPointState.Text = "Полигон загружен";
                }
                catch
                {
                    tslPointState.Text = "Ошибка при загрузке!!!";
                }

            }
        }
    }
}
