using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Xml.Serialization;
using System.IO;

namespace DrawPolygon
{
    public class Polygon
    {
        bool isCompleted;
        List<Point> points;

        public bool IsCompleted
        {
            get { return isCompleted; }
            set { isCompleted = value; }
        }

        public List<Point> Points
        {
            get { return points; }
            set { points = value; }
        }

        public Polygon()
        {
            IsCompleted = false;
            points = new List<Point>();
        }

        public void DrawLine(Graphics graph)
        {
            int pointCount = Points.Count;
            graph.DrawLine(Pens.Black, Points[pointCount - 1], Points[pointCount - 2]);
        }

        public void Complete(Graphics graph)
        {
            int pointCount = Points.Count;
            graph.DrawLine(Pens.Black, Points[pointCount - 1], Points[0]);
        }

        public bool CheckIsPointInside(Point point)
        {
            bool result = false;
            int j = Points.Count - 1;
            for (int i = 0; i < Points.Count; i++)
            {

                if ((((Points[i].Y <= point.Y) && (point.Y < Points[j].Y)) || ((Points[j].Y <= point.Y) && (point.Y < Points[i].Y))) &&
                 (point.X > (Points[j].X - Points[i].X) * (point.Y - Points[i].Y) / (Points[j].Y - Points[i].Y) + Points[i].X))
                { 
                    result = !result; 
                }

                j = i;
            }
            return result;
        }

        public void Save(string path)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Polygon));
            FileStream fs = new FileStream(path, FileMode.OpenOrCreate);
            serializer.Serialize(fs, this);
            fs.Close();
        }

        public static Polygon Load(string path)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Polygon));
            FileStream fs = new FileStream(path, FileMode.Open);
            return (Polygon)serializer.Deserialize(fs);           
        }

        public void DrawFull(Graphics graph)
        {
            graph.Clear(SystemColors.Control);
            int j = Points.Count - 1;
            for (int i = 0; i < Points.Count; i++)
            {
                graph.DrawLine(Pens.Black, Points[i], Points[j]);
                j = i;
            }
        }
    }
}
