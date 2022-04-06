using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Engine : MonoBehaviour
{
    /// <summary>
    /// [0:2] - leftright lines, [3:5] - topdown lines, 6 - topright diagonal, 7 - topleft diagonal
    /// </summary>
    public Line[] Lines { get; private set; }
    public Point[] Points { get; private set; }


public void Awake()
    {
        AssignLinesAndPoints();
    }
    public struct Point
    {
        public int x;
        public int y;
        public Point(int X, int Y)
        {
            x = X;
            y = Y;
        }
        public override bool Equals(object obj)
        {
            Point p = (Point)obj;
            return x == p.x && y == p.y;
        }
    }
    public struct Line
    {
        public Point[] points;
        public Line(Point StartPoint, Point EndPoint)
        {
            points = new Point[3];
            points[0] = StartPoint;
            points[2] = EndPoint;
            points[1] = new Point((StartPoint.x + EndPoint.x) / 2, (StartPoint.y + EndPoint.y) / 2);
        }
    }
    private void AssignLinesAndPoints()
    {
        Lines = new Line[8];
        Lines[0] = new Line(new Point(0, 0), new Point(2, 0));
        Lines[1] = new Line(new Point(0, 1), new Point(2, 1));
        Lines[2] = new Line(new Point(0, 2), new Point(2, 2));

        Lines[3] = new Line(new Point(0, 0), new Point(0, 2));
        Lines[4] = new Line(new Point(1, 0), new Point(1, 2));
        Lines[5] = new Line(new Point(2, 0), new Point(2, 2));

        Lines[6] = new Line(new Point(0, 0), new Point(2, 2));
        Lines[7] = new Line(new Point(2, 0), new Point(0, 2));

        Points = new Point[9];
        int PointsCount = 0;
        for (int y = 0; y <= 2; y++)
        {
            for (int x = 0; x <= 2; x++)
            {
                Points[PointsCount] = new Point(x, y);
                PointsCount++;
            }
        }
    }
    public Point PointFromID(int id)
    {
        return Points[id];
    }
    public int GetPointID(Point point)
    {
        for (int i = 0; i < Points.Length; i++)
        {
            if (Points[i].Equals(point))
            {
                return i;
            }
        }
        Debug.Log("Not found");
        return 0;
    }
    
}
