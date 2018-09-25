using UnityEngine;
using System.Collections.Generic;
using ummisco.gama.unity.utils;
using System;
using System.Collections;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class CubeGenerator : MonoBehaviour
{


    private Rigidbody rb;
    public float inverseMoveTime;
    public float moveTime = 10000.1f;

    public MapBuilder mapBuilder; //= new MapBuilder();

    GameObject objectTest;


    Boolean isMove = false;

    void Start()
    {

        objectTest = CreateCube("testCube");

        rb = GetComponent<Rigidbody>();

        inverseMoveTime = 1f / moveTime;

        Debug.Log("The script Type is : " + gameObject.GetComponent("CubeGenerator").GetType());

        //mapBuilder = new MapBuilder();

        //---------------------------------------------------------------------------------------------
        Vector2[] vertices2D = new Vector2[] {
            new Vector2(0,0),
            new Vector2(0,50),
            new Vector2(50,50),
            new Vector2(50,100),
            new Vector2(0,100),
            new Vector2(0,150),
            new Vector2(150,150),
            new Vector2(150,100),
            new Vector2(100,100),
            new Vector2(100,50),
            new Vector2(150,50),
            new Vector2(150,0),
        };


        // Use the triangulator to get indices for creating triangles
        Triangulator tr = new Triangulator(vertices2D);
        int[] indices = tr.Triangulate();

        // Create the Vector3 vertices
        Vector3[] vertices = new Vector3[vertices2D.Length];
        for (int i = 0; i < vertices.Length; i++)
        {
            vertices[i] = new Vector3(vertices2D[i].x, vertices2D[i].y, 0);
        }

        Vector3[] vertices1 = {
            new Vector3 (0, 0, 0),
            new Vector3 (1, 0, 0),
            new Vector3 (1, 1, 0),
            new Vector3 (0, 1, 0),
            new Vector3 (0, 1, 1),
            new Vector3 (1, 1, 1),
            new Vector3 (1, 0, 1),
            new Vector3 (0, 0, 1),

            new Vector3 (0, 1, 1),
            new Vector3 (1, 1, 1),
            new Vector3 (1, 0, 1),
            new Vector3 (0, 0, 1),
        };

        vertices = vertices1;
        // Create the mesh
        Mesh msh = new Mesh();
        msh.vertices = vertices;
        msh.triangles = indices;
        msh.RecalculateNormals();
        msh.RecalculateBounds();

        GameObject gameObj = new GameObject("Cube2DCreator");

        // Set up game object with mesh;
        gameObj.AddComponent(typeof(MeshRenderer));
        MeshFilter filter = gameObj.AddComponent(typeof(MeshFilter)) as MeshFilter;
        filter.mesh = msh;

        //---------------------------------------------------------------------------------------------



        SetPointCloud();










    }






    private GameObject CreateCube(string name)
    {

        GameObject ob = new GameObject("Empty");

        ob.name = name;

        ob.AddComponent<MeshFilter>();
        ob.AddComponent<MeshRenderer>();
        ob.AddComponent<Rigidbody>();
        ob.AddComponent<BoxCollider>();

        Color objectColor = ConvertType.stringToColor("red");
        Renderer rend = ob.GetComponent<Renderer>();
        rend.material.color = objectColor;


        Vector3[] vertices = {
            new Vector3 (0, 0, 0),
            new Vector3 (1, 0, 0),
            new Vector3 (1, 1, 0),
            new Vector3 (0, 1, 0),
            new Vector3 (0, 1, 1),
            new Vector3 (1, 1, 1),
            new Vector3 (1, 0, 1),
            new Vector3 (0, 0, 1),
        };

        int[] newTriangles = { };
        int[] triangles = {
            0, 2, 1, //face front
			0, 3, 2,
            2, 3, 4, //face top
			2, 4, 5,
            1, 2, 5, //face right
			1, 5, 6,
            0, 7, 4, //face left
			0, 4, 3,
            5, 4, 7, //face back
			5, 7, 6,
            0, 6, 7, //face bottom
			0, 1, 6
        };

        Vector3 pos = new Vector3(2, 3.5f, 4);

        ob.transform.position = pos;

        Mesh mesh = ob.GetComponent<MeshFilter>().mesh;
        mesh.Clear();
        mesh.vertices = vertices;

        //mesh.triangles = triangles;
        mesh.triangles = newTriangles;
        //mesh.Optimize ();
        mesh.RecalculateNormals();


        //ob.GetComponent<MeshFilter>().mesh = mesh;


        return ob;

    }



    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        //	rb.AddForce (movement * 20);
        //	rb.MovePosition (movement);
        //	transform.position = new Vector3 (moveHorizontal, 0.0f, moveVertical+1);

        if (isMove)
        {
            //transform.position = new Vector3 (moveHorizontal+3, 0.0f, moveVertical+4);
            moveToPosition(moveHorizontal + 20, 0.0f, moveVertical + 18, 10);
            isMove = false;
        }

    }

    public void UpdatePosition(float moveHorizontal, float moveVertical)
    {
        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        //rb.AddForce (movement * speed);
        //rb.MovePosition (movement);
    }


    public void OnMoveEvent()
    {
        Debug.Log("OnMove called.");
    }





    public void moveToPosition(float xDir, float yDir, float zDir, int speed)
    {

        //Store start position to move from, based on objects current transform position.
        Vector3 start = gameObject.transform.position;

        // Calculate end position based on the direction parameters passed in when calling Move.
        Vector3 end = start + new Vector3(xDir, yDir, zDir);

        Vector3 movement = new Vector3(xDir, yDir, zDir);

        //rb.AddForce (movement * speed);

        StartCoroutine(SmoothMovement(end, speed));
    }

    //Co-routine for moving units from one space to next, takes a parameter end to specify where to move to.
    protected IEnumerator SmoothMovement(Vector3 end, int speed)
    {
        //Calculate the remaining distance to move based on the square magnitude of the difference between current position and end parameter. 
        //Square magnitude is used instead of magnitude because it's computationally cheaper.
        float sqrRemainingDistance = (transform.position - end).sqrMagnitude;



        inverseMoveTime = 100f;

        //While that distance is greater than a very small amount (Epsilon, almost zero):
        //while (sqrRemainingDistance > float.Epsilon) {
        while (sqrRemainingDistance > 0.1f)
        {
            //Find a new position proportionally closer to the end, based on the moveTime
            //Vector3 newPostion = Vector3.MoveTowards (rb.position, end, inverseMoveTime * Time.deltaTime);
            Vector3 newPostion = Vector3.MoveTowards(rb.position, end, speed * Time.deltaTime);


            //Call MovePosition on attached Rigidbody2D and move it to the calculated position.
            rb.MovePosition(newPostion);

            Debug.DrawLine(transform.position, newPostion, Color.yellow, 0.2f, true);

            //Recalculate the remaining distance after moving.
            sqrRemainingDistance = (transform.position - end).sqrMagnitude;

            //Return and loop until sqrRemainingDistance is close enough to zero to end the function
            yield return null;
        }

        rb.position = end;
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;



    }
    //-------------------------------------------




    public void SetPointCloud()//(IList<MyPoint> points)
    {

        Vector3[] points = {
            new Vector3 (0, 0, 0),
            new Vector3 (1, 0, 0),
            new Vector3 (1, 1, 0),
            new Vector3 (0, 1, 0),
            new Vector3 (0, 1, 1),
            new Vector3 (1, 1, 1),
            new Vector3 (1, 0, 1),
            new Vector3 (0, 0, 1),
        };

        int numMeshes = points.Length / 6;
        Mesh mesh;
        GameObject child;

        //   for (int i = transform.childCount; i < numMeshes; i++)
        //  {
        child = new GameObject("SecondTest"); //Instantiate(pointCloudPrefab);

        child.AddComponent<MeshFilter>();
        child.AddComponent<MeshRenderer>();
        child.AddComponent<Rigidbody>();
        child.AddComponent<BoxCollider>();

        child.transform.SetParent(transform, false);
        mesh = new Mesh();
        child.GetComponent<MeshFilter>().mesh = mesh;
        //    }

        List<Vector3> verts;
        List<Vector3> normals;
        int[] indices;
        int pointIndex = 0;
        for (int t = 0; t < transform.childCount; t++)
        {
            child = transform.GetChild(t).gameObject;
            if (pointIndex < points.Length)
            {
                child.SetActive(true);
                mesh = child.GetComponent<MeshFilter>().mesh;

                var size = Mathf.Min((points.Length - pointIndex) - 1, 65000);
                verts = new List<Vector3>(size);
                normals = new List<Vector3>(size);
                indices = new int[size];

                for (int i = 0; pointIndex < points.Length && i < 65000; i++, pointIndex++)
                {
                    var p = points[pointIndex];
                    verts.Add(new Vector3(p.x / 1000, p.y / 1000, p.y / 1000)); // Points are in mm, Vector3s are in meters.
                    normals.Add(new Vector3(p.x, p.y, p.z));
                    indices[i] = i;
                }
                mesh.SetVertices(verts);
                mesh.SetNormals(normals);
                mesh.SetIndices(indices, MeshTopology.Points, 0);
            }
            else
            {
                child.SetActive(false);
            }
        }
    }


    //---- ^^^^^^^-------
    public static List<Triangle> TriangulateConvexPolygon(List<Vertex> convexHullpoints)
    {
        List<Triangle> triangles = new List<Triangle>();

        for (int i = 2; i < convexHullpoints.Count; i++)
        {
            Vertex a = convexHullpoints[0];
            Vertex b = convexHullpoints[i - 1];
            Vertex c = convexHullpoints[i];

            triangles.Add(new Triangle(a, b, c));
        }

        return triangles;
    }

    //This assumes that we have a polygon and now we want to triangulate it
    //The points on the polygon should be ordered counter-clockwise
    //This alorithm is called ear clipping and it's O(n*n) Another common algorithm is dividing it into trapezoids and it's O(n log n)
    //One can maybe do it in O(n) time but no such version is known
    //Assumes we have at least 3 points
    public static List<Triangle> TriangulateConcavePolygon(List<Vector3> points)
    {
        //The list with triangles the method returns
        List<Triangle> triangles = new List<Triangle>();

        //If we just have three points, then we dont have to do all calculations
        if (points.Count == 3)
        {
            triangles.Add(new Triangle(points[0], points[1], points[2]));

            return triangles;
        }



        //Step 1. Store the vertices in a list and we also need to know the next and prev vertex
        List<Vertex> vertices = new List<Vertex>();

        for (int i = 0; i < points.Count; i++)
        {
            vertices.Add(new Vertex(points[i]));
        }

        //Find the next and previous vertex
        for (int i = 0; i < vertices.Count; i++)
        {
            int nextPos = MathUtility.ClampListIndex(i + 1, vertices.Count);

            int prevPos = MathUtility.ClampListIndex(i - 1, vertices.Count);

            vertices[i].prevVertex = vertices[prevPos];

            vertices[i].nextVertex = vertices[nextPos];
        }



        //Step 2. Find the reflex (concave) and convex vertices, and ear vertices
        for (int i = 0; i < vertices.Count; i++)
        {
            CheckIfReflexOrConvex(vertices[i]);
        }

        //Have to find the ears after we have found if the vertex is reflex or convex
        List<Vertex> earVertices = new List<Vertex>();

        for (int i = 0; i < vertices.Count; i++)
        {
            IsVertexEar(vertices[i], vertices, earVertices);
        }



        //Step 3. Triangulate!
        while (true)
        {
            //This means we have just one triangle left
            if (vertices.Count == 3)
            {
                //The final triangle
                triangles.Add(new Triangle(vertices[0], vertices[1], vertices[2]));

                break;
            }

            //Make a triangle of the first ear
            Vertex earVertex = earVertices[0];

            Vertex earVertexPrev = earVertex.prevVertex;
            Vertex earVertexNext = earVertex.nextVertex;

            Triangle newTriangle = new Triangle(earVertex, earVertexPrev, earVertexNext);

            triangles.Add(newTriangle);

            //Remove the vertex from the lists
            earVertices.Remove(earVertex);

            vertices.Remove(earVertex);

            //Update the previous vertex and next vertex
            earVertexPrev.nextVertex = earVertexNext;
            earVertexNext.prevVertex = earVertexPrev;

            //...see if we have found a new ear by investigating the two vertices that was part of the ear
            CheckIfReflexOrConvex(earVertexPrev);
            CheckIfReflexOrConvex(earVertexNext);

            earVertices.Remove(earVertexPrev);
            earVertices.Remove(earVertexNext);

            IsVertexEar(earVertexPrev, vertices, earVertices);
            IsVertexEar(earVertexNext, vertices, earVertices);
        }

        //Debug.Log(triangles.Count);

        return triangles;
    }



    //Check if a vertex if reflex or convex, and add to appropriate list
    private static void CheckIfReflexOrConvex(Vertex v)
    {
        v.isReflex = false;
        v.isConvex = false;

        //This is a reflex vertex if its triangle is oriented clockwise
        Vector2 a = v.prevVertex.GetPos2D_XZ();
        Vector2 b = v.GetPos2D_XZ();
        Vector2 c = v.nextVertex.GetPos2D_XZ();

        if (Geometry.IsTriangleOrientedClockwise(a, b, c))
        {
            v.isReflex = true;
        }
        else
        {
            v.isConvex = true;
        }
    }



    //Check if a vertex is an ear
    private static void IsVertexEar(Vertex v, List<Vertex> vertices, List<Vertex> earVertices)
    {
        //A reflex vertex cant be an ear!
        if (v.isReflex)
        {
            return;
        }

        //This triangle to check point in triangle
        Vector2 a = v.prevVertex.GetPos2D_XZ();
        Vector2 b = v.GetPos2D_XZ();
        Vector2 c = v.nextVertex.GetPos2D_XZ();

        bool hasPointInside = false;

        for (int i = 0; i < vertices.Count; i++)
        {
            //We only need to check if a reflex vertex is inside of the triangle
            if (vertices[i].isReflex)
            {
                Vector2 p = vertices[i].GetPos2D_XZ();

                //This means inside and not on the hull
                if (Intersections.IsPointInTriangle(a, b, c, p))
                {
                    hasPointInside = true;

                    break;
                }
            }
        }

        if (!hasPointInside)
        {
            earVertices.Add(v);
        }
    }


    //---- ^^^^^^^-------
}
