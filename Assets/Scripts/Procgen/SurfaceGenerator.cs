using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurfaceGenerator : MonoBehaviour
{
    public Vector2 size = new Vector2(1000, 1000);
    public int plateauCount = 32;

    public Transform[] requiredPlateaus;

    public RangeAttribute plateauHeightRange = new RangeAttribute(10.0f, 40.0f);
    public float canyonDepth = -50.0f;

    private List<Vector3> plateauPoints;

    private struct Line
    {
        // ax + by = c
        float a, b, c;
        Vector2 p, q;

        public Vector2 P { get => p; }
        public Vector2 Q { get => q; }

        public Vector2 Midpoint
        {
            get { return new Vector2((p.x + q.x) * 0.5f, (p.y + q.y) * 0.5f);  }
        }

        public Line(Vector2 p, Vector2 q)
        {
            a = q.y - p.y;
            b = p.x - q.x;
            c = (a * p.x) + (b * p.y);

            this.p = p;
            this.q = q;
        }
        public Line GetPerpendicularBisector()
        {
            Vector2 midpoint = Midpoint;
            Line bisector = new Line(p, q);
            bisector.a = -b;
            bisector.b = a;
            bisector.c = -b * midpoint.x + a * midpoint.y;
            return bisector;
        }

        public static Vector2 Intersect(Line a, Line b)
        {
            float determinant = (a.a * b.b) - (b.a * a.b);
            if(determinant == 0)
            {
                return new Vector2(float.MaxValue, float.MaxValue);
            }
            else
            {
                float x = (b.b * a.c - a.b * b.c) / determinant;
                float y = (a.a * b.c - b.a * a.c) / determinant;
                return new Vector2(x, y);
            }
        }
    }

    private struct Triangle
    {
        private Vector2 a;
        private Vector2 b;
        private Vector2 c;

        public Vector2 A
        {
            get { return a; }
            set { circumcenterStale = true; a = value; }
        }

        public Vector2 B
        {
            get { return b; }
            set { circumcenterStale = true; b = value; }
        }

        public Vector2 C
        {
            get { return c; }
            set { circumcenterStale = true; c = value; }
        }

        public Vector2 Circumcenter
        {
            get { UpdateCircumcenter(); return circumcenter; }
        }

        public float CircumcircleRadius
        {
            get { UpdateCircumcenter(); return circumcircleRadius; }
        }

        private bool circumcenterStale;
        private Vector2 circumcenter;
        private float circumcircleRadius;

        public Triangle(Vector2 first, Vector2 second, Vector2 third)
        {
            a = first; b = second; c = third;
            circumcenterStale = true;
            circumcenter = Vector2.zero;
            circumcircleRadius = 0;
        }

        private void UpdateCircumcenter()
        {
            if(circumcenterStale)
            {
                Line ab = new Line(a, b);
                Line bc = new Line(b, c);
                Line m = ab.GetPerpendicularBisector();
                Line n = ab.GetPerpendicularBisector();
                circumcenter = Line.Intersect(m, n);
                circumcircleRadius = Vector2.Distance(a, circumcenter);
                circumcenterStale = false;
            }
        }

    }

    private void PopulatePlateauPoints()
    {
        plateauPoints = new List<Vector3>();

        foreach(Transform point in requiredPlateaus)
        {
            plateauPoints.Add(point.position);
        }

        while(plateauPoints.Count < plateauCount)
        {
            plateauPoints.Add(new Vector3(
                Random.Range(-0.5f * size.x, 0.5f * size.x),
                Random.Range(plateauHeightRange.min, plateauHeightRange.max),
                Random.Range(-0.5f * size.y, 0.5f * size.y))
            );
        }
    }

    private LinkedList<Triangle> BuildDelaunayTriangulation()
    {
        LinkedList<Triangle> trianglulation = new LinkedList<Triangle>();
        trianglulation.AddFirst(new Triangle(new Vector2(-0.5f * size.x, -0.5f * size.y),new Vector2(-0.5f * size.x, 0.5f * size.y),new Vector2(0.5f * size.x, -0.5f * size.y))        );

        foreach(Vector3 plateauPoint in plateauPoints)
        {
            AddPointToDelaunay(new Vector2(plateauPoint.x, plateauPoint.y), ref trianglulation);
        }

        return trianglulation;
    }

    private bool IsValidDelaunay(Triangle triangle, Vector2 point)
    {
        return Vector2.Distance(triangle.Circumcenter, point) > triangle.CircumcircleRadius;
    }

    private class ClockwiseComparer : IComparer<Vector2>
    {
        Vector2 center;

        public ClockwiseComparer(Vector2 aroundCenter)
        {
            center = aroundCenter;
        }

        public int Compare(Vector2 x, Vector2 y)
        {
            float xAngle = Vector2.Angle(Vector2.up, center - x);
            float yAngle = Vector2.Angle(Vector2.up, center - y);
            return Comparer<float>.Default.Compare(xAngle, yAngle);
        }
    }

    private void AddPointToDelaunay(Vector2 point, ref LinkedList<Triangle> trianglulation)
    {
        List<Triangle> toRemove = new List<Triangle>();
        List<Vector2> holdBoundry = new List<Vector2>();

        foreach (Triangle tri in trianglulation)
        {
            if(!IsValidDelaunay(tri, point))
            {
                toRemove.Add(tri);
                holdBoundry.Add(tri.A);
                holdBoundry.Add(tri.A);
                holdBoundry.Add(tri.C);
            }
        }

        // sort the boundary points so I can do a nice pie slice
        holdBoundry.Sort(new ClockwiseComparer(point));

        // Add some nice lil pie slice tris - there is a proof that all these are guaranteed to be valid delaunay I promise
        for(int i = 0; i < holdBoundry.Count; ++i)
        {
            int nextIndex = (i < holdBoundry.Count - 1) ? i + 1 : 0;
            trianglulation.AddLast(new Triangle(holdBoundry[i], holdBoundry[nextIndex], point));
        }
    }
}
