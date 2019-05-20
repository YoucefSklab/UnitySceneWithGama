using UnityEngine;
using System.Collections.Generic;
using System.Linq;

namespace ummisco.gama.unity.utils
{
    public class TriangulatorOrigin
    {
        public List<Vector2> m_points = new List<Vector2>();

        public List<Vector2> all_points = new List<Vector2>();

        public TriangulatorOrigin(Vector2[] points)
        {
            m_points = new List<Vector2>(points);
        }

        public void setPoints(Vector2[] points)
        {
            m_points = new List<Vector2>(points);
        }

        public void setAllPoints(Vector2[] points)
        {
            all_points = new List<Vector2>(points);
        }

        public int[] Triangulate()
        {
            List<int> indices = new List<int>();

            int n = m_points.Count;
            if (n < 3)
                return indices.ToArray();

            int[] V = new int[n];
            if (Area() > 0)
            {
                for (int v = 0; v < n; v++)
                    V[v] = v;
            }
            else
            {
                for (int v = 0; v < n; v++)
                    V[v] = (n - 1) - v;
            }

            int nv = n;
            int count = 2 * nv;
            for (int m = 0, v = nv - 1; nv > 2;)
            {
                if ((count--) <= 0)
                    return indices.ToArray();

                int u = v;
                if (nv <= u)
                    u = 0;
                v = u + 1;
                if (nv <= v)
                    v = 0;
                int w = v + 1;
                if (nv <= w)
                    w = 0;

                if (Snip(u, v, w, nv, V))
                {
                    int a, b, c, s, t;
                    a = V[u];
                    b = V[v];
                    c = V[w];
                    indices.Add(a);
                    indices.Add(b);
                    indices.Add(c);
                    m++;
                    for (s = v, t = v + 1; t < nv; s++, t++)
                        V[s] = V[t];
                    nv--;
                    count = 2 * nv;
                }
            }

            indices.Reverse();
            return indices.ToArray();
        }

        private float Area()
        {
            int n = m_points.Count;
            float A = 0.0f;
            for (int p = n - 1, q = 0; q < n; p = q++)
            {
                Vector2 pval = m_points[p];
                Vector2 qval = m_points[q];
                A += pval.x * qval.y - qval.x * pval.y;
            }
            return (A * 0.5f);
        }

        private bool Snip(int u, int v, int w, int n, int[] V)
        {
            int p;
            Vector2 A = m_points[V[u]];
            Vector2 B = m_points[V[v]];
            Vector2 C = m_points[V[w]];
            if (Mathf.Epsilon > (((B.x - A.x) * (C.y - A.y)) - ((B.y - A.y) * (C.x - A.x))))
                return false;
            for (p = 0; p < n; p++)
            {
                if ((p == u) || (p == v) || (p == w))
                    continue;
                Vector2 P = m_points[V[p]];
                if (InsideTriangle(A, B, C, P))
                    return false;
            }
            return true;
        }

        private bool InsideTriangle(Vector2 A, Vector2 B, Vector2 C, Vector2 P)
        {
            float ax, ay, bx, by, cx, cy, apx, apy, bpx, bpy, cpx, cpy;
            float cCROSSap, bCROSScp, aCROSSbp;

            ax = C.x - B.x; ay = C.y - B.y;
            bx = A.x - C.x; by = A.y - C.y;
            cx = B.x - A.x; cy = B.y - A.y;
            apx = P.x - A.x; apy = P.y - A.y;
            bpx = P.x - B.x; bpy = P.y - B.y;
            cpx = P.x - C.x; cpy = P.y - C.y;

            aCROSSbp = ax * bpy - ay * bpx;
            cCROSSap = cx * apy - cy * apx;
            bCROSScp = bx * cpy - by * cpx;

            return ((aCROSSbp >= 0.0f) && (bCROSScp >= 0.0f) && (cCROSSap >= 0.0f));
        }



        public Vector3[] get3dVertices(int elevation)
        {

            Vector3[] vertices = new Vector3[m_points.Count * 2];
            Vector2[] vertices2D = new Vector2[m_points.Count * 2];

            for (int i = 0; i < m_points.Count; i++)
            {
                vertices[i].x = m_points[i].x;
                vertices[i].y = m_points[i].y;
                vertices[i].z = -elevation; // front vertex
                vertices[i + m_points.Count].x = m_points[i].x;
                vertices[i + m_points.Count].y = m_points[i].y;
                vertices[i + m_points.Count].z = elevation;  // back vertex   

                // front vertex
                vertices2D[i].x = m_points[i].x;
                vertices2D[i].y = m_points[i].y;
                // back vertex  
                vertices2D[i + m_points.Count].x = m_points[i].x;
                vertices2D[i + m_points.Count].y = m_points[i].y;
            }

            return vertices;
        }


        public Vector2[] get2dVertices()
        {
            Vector2[] vertices2D = new Vector2[m_points.Count * 2];
            for (int i = 0; i < m_points.Count; i++)
            {
                // front vertex
                vertices2D[i].x = m_points[i].x;
                vertices2D[i].y = m_points[i].y;
                // back vertex  
                vertices2D[i + m_points.Count].x = m_points[i].x;
                vertices2D[i + m_points.Count].y = m_points[i].y;
            }
            return vertices2D;
        }


        public List<Vector3> get3dVerticesList(float elevation)
        {
            Vector3[] vertices = new Vector3[m_points.Count * 2];
            for (int i = 0; i < m_points.Count; i++)
            {
                vertices[i].x = m_points[i].x;
                vertices[i].y = m_points[i].y;
                vertices[i].z = - elevation; // front vertex
                vertices[i + m_points.Count].x = m_points[i].x;
                vertices[i + m_points.Count].y = m_points[i].y;
                vertices[i + m_points.Count].z =  0; //elevation;  // back vertex    
            }
            return vertices.OfType<Vector3>().ToList();
        }




        public int[] getTriangules()
        {
            // convert polygon to triangles

            int[] tris = Triangulate();
            Mesh m = new Mesh();

            int[] triangles = new int[tris.Length * 2 + m_points.Count * 6];
            int count_tris = 0;
            for (int i = 0; i < tris.Length; i += 3)
            {
                triangles[i] = tris[i];
                triangles[i + 1] = tris[i + 1];
                triangles[i + 2] = tris[i + 2];
            } // front vertices
            count_tris += tris.Length;
            for (int i = 0; i < tris.Length; i += 3)
            {
                triangles[count_tris + i] = tris[i + 2] + m_points.Count;
                triangles[count_tris + i + 1] = tris[i + 1] + m_points.Count;
                triangles[count_tris + i + 2] = tris[i] + m_points.Count;
            } // back vertices
            count_tris += tris.Length;
            for (int i = 0; i < m_points.Count; i++)
            {
                // triangles around the perimeter of the object
                int n = (i + 1) % m_points.Count;
                // triangles[count_tris] = i;
                // triangles[count_tris + 1] = i + m_points.Count;
                // triangles[count_tris + 2] = n;

                triangles[count_tris + 0] = n;
                triangles[count_tris + 1] = i + m_points.Count;
                triangles[count_tris + 2] = i;
                triangles[count_tris + 3] = n;
                triangles[count_tris + 4] = n + m_points.Count;
                triangles[count_tris + 5] = i + m_points.Count;
                count_tris += 6;
            }
            return triangles;
        }


        public List<int> getTriangulesList()
        {
            return get3DTriangulesFrom2D().OfType<int>().ToList();
        }


        //Is a triangle in 2d space oriented clockwise or counter-clockwise
        //https://math.stackexchange.com/questions/1324179/how-to-tell-if-3-connected-points-are-connected-clockwise-or-counter-clockwise
        //https://en.wikipedia.org/wiki/Curve_orientation
        public bool IsTriangleOrientedClockwise(Vector2 p1, Vector2 p2, Vector2 p3)
        {
            bool isClockWise = true;

            float determinant = p1.x * p2.y + p3.x * p1.y + p2.x * p3.y - p1.x * p3.y - p3.x * p2.y - p2.x * p1.y;

            if (determinant > 0f)
            {
                isClockWise = false;
            }

            return isClockWise;
        }



        public int[] checkClockWise(int[] tr, Vector3[] vert)
        {


            for (int i = 0; i < tr.Length; i += 3)
            {

                Vector2 p1 = new Vector2(vert[tr[i + 0]].x, vert[tr[i + 0]].y);
                Vector2 p2 = new Vector2(vert[tr[i + 1]].x, vert[tr[i + 1]].y);
                Vector2 p3 = new Vector2(vert[tr[i + 2]].x, vert[tr[i + 2]].y);
                if (IsTriangleOrientedClockwise(p1, p2, p3))
                {
                    // Debug.Log("Yes it is ClockWise ->- {" + tr[i + 0] + "},{" + tr[i + 1] + "},{" + tr[i + 2] + "}");
                }
                else
                {
                    int n = tr[i + 1];
                    tr[i + 1] = tr[i + 2];
                    tr[i + 2] = n;
                    p1 = new Vector2(vert[tr[i + 0]].x, vert[tr[i + 0]].y);
                    p2 = new Vector2(vert[tr[i + 1]].x, vert[tr[i + 1]].y);
                    p3 = new Vector2(vert[tr[i + 2]].x, vert[tr[i + 2]].y);
                }
            }
            return tr;
        }




        public int[] get3DTriangulesFrom2D()
        {
            // convert the initial polygon to triangles
            int[] tris = Triangulate();

            Mesh m = new Mesh();

            // Array to contain all the triangules of the mesh
            int[] triangles = new int[tris.Length * 2 + m_points.Count * 6];


            // Compute triangules of front vertices
            int count_tris = 0;
            for (int i = 0; i < tris.Length; i += 3)
            {
                triangles[i + 0] = tris[i + 0];
                triangles[i + 1] = tris[i + 1];
                triangles[i + 2] = tris[i + 2];

                //----
                // triangles[i + 0] = tris[i + 2];
                // triangles[i + 1] = tris[i + 1];
                // triangles[i + 2] = tris[i + 0];
                if (1 == 1)
                    if (!IsTriangleOrientedClockwise(m_points[triangles[i]], m_points[triangles[i + 1]], m_points[triangles[i + 2]]))
                    {
                        triangles[i + 0] = tris[i + 0];
                        triangles[i + 1] = tris[i + 2];
                        triangles[i + 2] = tris[i + 1];

                        if (!IsTriangleOrientedClockwise(m_points[triangles[i]], m_points[triangles[i + 1]], m_points[triangles[i + 2]]))
                        {
                            triangles[i + 0] = tris[i + 1];
                            triangles[i + 1] = tris[i + 2];
                            triangles[i + 2] = tris[i + 0];
                            if (!IsTriangleOrientedClockwise(m_points[triangles[i]], m_points[triangles[i + 1]], m_points[triangles[i + 2]]))
                            {
                                triangles[i + 0] = tris[i + 1];
                                triangles[i + 1] = tris[i + 0];
                                triangles[i + 2] = tris[i + 2];
                                if (!IsTriangleOrientedClockwise(m_points[triangles[i]], m_points[triangles[i + 1]], m_points[triangles[i + 2]]))
                                {
                                    triangles[i + 0] = tris[i + 2];
                                    triangles[i + 1] = tris[i + 1];
                                    triangles[i + 2] = tris[i + 0];
                                    if (!IsTriangleOrientedClockwise(m_points[triangles[i]], m_points[triangles[i + 1]], m_points[triangles[i + 2]]))
                                    {
                                        triangles[i + 0] = tris[i + 2];
                                        triangles[i + 1] = tris[i + 0];
                                        triangles[i + 2] = tris[i + 1];
                                    }
                                }
                            }
                        }
                    }

            }

            // Compute triangules of back vertices
            count_tris += tris.Length;
            for (int i = 0; i < tris.Length; i += 3)
            {
                //triangles[count_tris + i + 0] = tris[i + 2] + m_points.Count;
                //triangles[count_tris + i + 1] = tris[i + 1] + m_points.Count;
                //triangles[count_tris + i + 2] = tris[i + 0] + m_points.Count;

                //-----
                triangles[count_tris + i + 0] = tris[i + 0] + m_points.Count;
                triangles[count_tris + i + 1] = tris[i + 1] + m_points.Count;
                triangles[count_tris + i + 2] = tris[i + 2] + m_points.Count;

                if (1 == 1)
                    if (IsTriangleOrientedClockwise(all_points[triangles[count_tris + i + 0]], all_points[triangles[count_tris + i + 1]], all_points[triangles[count_tris + i + 2]]))
                    {
                        //Debug.Log("Back Triangle not  Clockwise > ");
                        triangles[count_tris + i + 0] = tris[i + 0] + m_points.Count;
                        triangles[count_tris + i + 1] = tris[i + 2] + m_points.Count;
                        triangles[count_tris + i + 2] = tris[i + 1] + m_points.Count;
                        if (IsTriangleOrientedClockwise(all_points[triangles[count_tris + i + 0]], all_points[triangles[count_tris + i + 1]], all_points[triangles[count_tris + i + 2]]))
                        {
                            //    Debug.Log("Back Triangle again not  Clockwise > ");
                            triangles[count_tris + i + 0] = tris[i + 1] + m_points.Count;
                            triangles[count_tris + i + 1] = tris[i + 2] + m_points.Count;
                            triangles[count_tris + i + 2] = tris[i + 0] + m_points.Count;
                            if (IsTriangleOrientedClockwise(all_points[triangles[count_tris + i + 0]], all_points[triangles[count_tris + i + 1]], all_points[triangles[count_tris + i + 2]]))
                            {
                                //    Debug.Log("Back Triangle again not  Clockwise > ");
                                triangles[count_tris + i + 0] = tris[i + 1] + m_points.Count;
                                triangles[count_tris + i + 1] = tris[i + 0] + m_points.Count;
                                triangles[count_tris + i + 2] = tris[i + 2] + m_points.Count;
                                if (IsTriangleOrientedClockwise(all_points[triangles[count_tris + i + 0]], all_points[triangles[count_tris + i + 1]], all_points[triangles[count_tris + i + 2]]))
                                {
                                    //    Debug.Log("Back Triangle again not  Clockwise > ");
                                    triangles[count_tris + i + 0] = tris[i + 2] + m_points.Count;
                                    triangles[count_tris + i + 1] = tris[i + 1] + m_points.Count;
                                    triangles[count_tris + i + 2] = tris[i + 0] + m_points.Count;
                                    if (IsTriangleOrientedClockwise(all_points[triangles[count_tris + i + 0]], all_points[triangles[count_tris + i + 1]], all_points[triangles[count_tris + i + 2]]))
                                    {
                                        //    Debug.Log("Back Triangle again not  Clockwise > ");
                                        triangles[count_tris + i + 0] = tris[i + 2] + m_points.Count;
                                        triangles[count_tris + i + 1] = tris[i + 0] + m_points.Count;
                                        triangles[count_tris + i + 2] = tris[i + 1] + m_points.Count;
                                    }
                                }
                            }
                        }
                    }



            }


            count_tris += tris.Length;
            for (int i = 0; i < m_points.Count; i++)
            {
                // triangles around the perimeter of the object
                int n = (i + 1) % m_points.Count;
                triangles[count_tris + 0] = i;
                triangles[count_tris + 1] = i + m_points.Count;
                triangles[count_tris + 2] = n;

                if (1 == 1)
                    if (IsTriangleOrientedClockwise(all_points[triangles[count_tris + 0]], all_points[triangles[count_tris + 1]], all_points[triangles[count_tris + 2]]))
                    {
                        triangles[count_tris + 0] = n;
                        triangles[count_tris + 1] = i + m_points.Count;
                        triangles[count_tris + 2] = i;
                        if (IsTriangleOrientedClockwise(all_points[triangles[count_tris + 0]], all_points[triangles[count_tris + 1]], all_points[triangles[count_tris + 2]]))
                        {
                            triangles[count_tris + 0] = n;
                            triangles[count_tris + 2] = i + m_points.Count;
                            triangles[count_tris + 1] = i;
                            if (IsTriangleOrientedClockwise(all_points[triangles[count_tris + 0]], all_points[triangles[count_tris + 1]], all_points[triangles[count_tris + 2]]))
                            {
                                triangles[count_tris + 1] = n;
                                triangles[count_tris + 2] = i + m_points.Count;
                                triangles[count_tris + 0] = i;
                                if (IsTriangleOrientedClockwise(all_points[triangles[count_tris + 0]], all_points[triangles[count_tris + 1]], all_points[triangles[count_tris + 2]]))
                                {
                                    triangles[count_tris + 1] = n;
                                    triangles[count_tris + 0] = i + m_points.Count;
                                    triangles[count_tris + 2] = i;
                                    if (IsTriangleOrientedClockwise(all_points[triangles[count_tris + 0]], all_points[triangles[count_tris + 1]], all_points[triangles[count_tris + 2]]))
                                    {
                                        triangles[count_tris + 2] = n;
                                        triangles[count_tris + 1] = i + m_points.Count;
                                        triangles[count_tris + 0] = i;
                                        if (IsTriangleOrientedClockwise(all_points[triangles[count_tris + 0]], all_points[triangles[count_tris + 1]], all_points[triangles[count_tris + 2]]))
                                        {
                                            triangles[count_tris + 2] = n;
                                            triangles[count_tris + 0] = i + m_points.Count;
                                            triangles[count_tris + 1] = i;
                                        }
                                    }
                                }
                            }
                        }
                    }

                /* 
                //---
                triangles[count_tris + 0] = n;
                triangles[count_tris + 1] = i + m_points.Count;
                triangles[count_tris + 2] = i;
                */
                triangles[count_tris + 3] = n;
                triangles[count_tris + 4] = n + m_points.Count;
                triangles[count_tris + 5] = i + m_points.Count;

                if (1 == 1)
                    if (!IsTriangleOrientedClockwise(all_points[triangles[count_tris + 3]], all_points[triangles[count_tris + 4]], all_points[triangles[count_tris + 5]]))
                    {
                        triangles[count_tris + 3] = i + m_points.Count;
                        triangles[count_tris + 4] = n + m_points.Count;
                        triangles[count_tris + 5] = n;
                        if (!IsTriangleOrientedClockwise(all_points[triangles[count_tris + 3]], all_points[triangles[count_tris + 4]], all_points[triangles[count_tris + 5]]))
                        {
                            triangles[count_tris + 3] = i + m_points.Count;
                            triangles[count_tris + 5] = n + m_points.Count;
                            triangles[count_tris + 4] = n;
                            if (!IsTriangleOrientedClockwise(all_points[triangles[count_tris + 3]], all_points[triangles[count_tris + 4]], all_points[triangles[count_tris + 5]]))
                            {
                                triangles[count_tris + 4] = i + m_points.Count;
                                triangles[count_tris + 3] = n + m_points.Count;
                                triangles[count_tris + 5] = n;
                                if (!IsTriangleOrientedClockwise(all_points[triangles[count_tris + 3]], all_points[triangles[count_tris + 4]], all_points[triangles[count_tris + 5]]))
                                {
                                    triangles[count_tris + 4] = i + m_points.Count;
                                    triangles[count_tris + 5] = n + m_points.Count;
                                    triangles[count_tris + 3] = n;
                                    if (!IsTriangleOrientedClockwise(all_points[triangles[count_tris + 3]], all_points[triangles[count_tris + 4]], all_points[triangles[count_tris + 5]]))
                                    {
                                        triangles[count_tris + 5] = i + m_points.Count;
                                        triangles[count_tris + 3] = n + m_points.Count;
                                        triangles[count_tris + 4] = n;
                                        if (!IsTriangleOrientedClockwise(all_points[triangles[count_tris + 3]], all_points[triangles[count_tris + 4]], all_points[triangles[count_tris + 5]]))
                                        {
                                            triangles[count_tris + 5] = i + m_points.Count;
                                            triangles[count_tris + 4] = n + m_points.Count;
                                            triangles[count_tris + 3] = n;
                                        }
                                    }
                                }
                            }
                        }
                    }


                count_tris += 6;
            }
            return triangles;
        }




        public int[] get3DTriangulesFrom2DOld()
        {
            // convert the initial polygon to triangles
            int[] tris = Triangulate();

            Mesh m = new Mesh();

            // Array to contain all the triangules of the mesh
            int[] triangles = new int[tris.Length * 2 + m_points.Count * 6];


            // Compute triangules of front vertices
            int count_tris = 0;
            for (int i = 0; i < tris.Length; i += 3)
            {
                triangles[i + 0] = tris[i + 0];
                triangles[i + 1] = tris[i + 1];
                triangles[i + 2] = tris[i + 2];

                //----
                // triangles[i + 0] = tris[i + 2];
                // triangles[i + 1] = tris[i + 1];
                // triangles[i + 2] = tris[i + 0];
                if (1 == 1)
                    if (!IsTriangleOrientedClockwise(m_points[triangles[i]], m_points[triangles[i + 1]], m_points[triangles[i + 2]]))
                    {
                        triangles[i + 0] = tris[i + 0];
                        triangles[i + 1] = tris[i + 2];
                        triangles[i + 2] = tris[i + 1];
                        if (!IsTriangleOrientedClockwise(m_points[triangles[i]], m_points[triangles[i + 1]], m_points[triangles[i + 2]]))
                        {
                            triangles[i + 0] = tris[i + 1];
                            triangles[i + 1] = tris[i + 2];
                            triangles[i + 2] = tris[i + 0];
                        }
                    }

            }

            // Compute triangules of back vertices
            count_tris += tris.Length;
            for (int i = 0; i < tris.Length; i += 3)
            {
                //triangles[count_tris + i + 0] = tris[i + 2] + m_points.Count;
                //triangles[count_tris + i + 1] = tris[i + 1] + m_points.Count;
                //triangles[count_tris + i + 2] = tris[i + 0] + m_points.Count;

                //-----
                triangles[count_tris + i + 0] = tris[i + 0] + m_points.Count;
                triangles[count_tris + i + 1] = tris[i + 1] + m_points.Count;
                triangles[count_tris + i + 2] = tris[i + 2] + m_points.Count;

                if (1 == 1)
                    if (IsTriangleOrientedClockwise(all_points[triangles[count_tris + i + 0]], all_points[triangles[count_tris + i + 1]], all_points[triangles[count_tris + i + 2]]))
                    {
                        //Debug.Log("Back Triangle not  Clockwise > ");
                        triangles[count_tris + i + 0] = tris[i + 0] + m_points.Count;
                        triangles[count_tris + i + 1] = tris[i + 2] + m_points.Count;
                        triangles[count_tris + i + 2] = tris[i + 1] + m_points.Count;
                        if (IsTriangleOrientedClockwise(all_points[triangles[count_tris + i + 0]], all_points[triangles[count_tris + i + 1]], all_points[triangles[count_tris + i + 2]]))
                        {
                            //    Debug.Log("Back Triangle again not  Clockwise > ");
                            triangles[count_tris + i + 0] = tris[i + 0] + m_points.Count;
                            triangles[count_tris + i + 1] = tris[i + 2] + m_points.Count;
                            triangles[count_tris + i + 2] = tris[i + 1] + m_points.Count;
                        }
                    }



            }


            count_tris += tris.Length;
            for (int i = 0; i < m_points.Count; i++)
            {
                // triangles around the perimeter of the object
                int n = (i + 1) % m_points.Count;
                triangles[count_tris + 0] = i;
                triangles[count_tris + 1] = i + m_points.Count;
                triangles[count_tris + 2] = n;

                if (1 == 1)
                    if (IsTriangleOrientedClockwise(all_points[triangles[count_tris + 0]], all_points[triangles[count_tris + 1]], all_points[triangles[count_tris + 2]]))
                    {
                        triangles[count_tris + 0] = n;
                        triangles[count_tris + 1] = i + m_points.Count;
                        triangles[count_tris + 2] = i;
                    }

                /* 
                //---
                triangles[count_tris + 0] = n;
                triangles[count_tris + 1] = i + m_points.Count;
                triangles[count_tris + 2] = i;
                */
                triangles[count_tris + 3] = n;
                triangles[count_tris + 4] = n + m_points.Count;
                triangles[count_tris + 5] = i + m_points.Count;

                if (1 == 1)
                    if (!IsTriangleOrientedClockwise(all_points[triangles[count_tris + 3]], all_points[triangles[count_tris + 4]], all_points[triangles[count_tris + 5]]))
                    {
                        triangles[count_tris + 3] = i + m_points.Count;
                        triangles[count_tris + 4] = n + m_points.Count;
                        triangles[count_tris + 5] = n;
                    }


                count_tris += 6;
            }
            return triangles;
        }












    }




}