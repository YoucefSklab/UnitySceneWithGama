using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Triangulate random points by first generating the convex hull of the points, then triangulate the convex hull
//and then add the other points and split the triangle the point is in
public static class TriangleSplittingAlgorithm
{
    public static List<Triangle> TriangulatePoints(List<Vertex> points)
    {
        //Generate the convex hull - will also remove the points from points list which are not on the hull
        List<Vertex> pointsOnConvexHull = HullAlgorithms.JarvisMarch(points);

        //Triangulate the convex hull
        List<Triangle> triangles = TriangulateHullAlgorithms.TriangulateConvexPolygon(pointsOnConvexHull);

        //Add the remaining points and split the triangles
        for (int i = 0; i < points.Count; i++)
        {
            Vertex currentPoint = points[i];
            
            //2d space
            Vector2 p = new Vector2(currentPoint.position.x, currentPoint.position.z);
        
            //Which triangle is this point in?
            for (int j = 0; j < triangles.Count; j++)
            {
                Triangle t = triangles[j];
            
                Vector2 p1 = new Vector2(t.v1.position.x, t.v1.position.z);
                Vector2 p2 = new Vector2(t.v2.position.x, t.v2.position.z);
                Vector2 p3 = new Vector2(t.v3.position.x, t.v3.position.z);

                if (Intersections.IsPointInTriangle(p1, p2, p3, p))
                {
                    //Create 3 new triangles
                    Triangle t1 = new Triangle(t.v1, t.v2, currentPoint);
                    Triangle t2 = new Triangle(t.v2, t.v3, currentPoint);
                    Triangle t3 = new Triangle(t.v3, t.v1, currentPoint);

                    //Remove the old triangle
                    triangles.Remove(t);

                    //Add the new triangles
                    triangles.Add(t1);
                    triangles.Add(t2);
                    triangles.Add(t3);

                    break;
                }
            }
        }

        return triangles;
    }
}
