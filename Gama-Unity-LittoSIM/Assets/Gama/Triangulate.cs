using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ummisco.gama.unity.utils;
using UnityEditor;
using UnityEngine;

public class Triangulate : MonoBehaviour
{

    public Material myNewMaterial;
    public Triangulator triangulator;

    Vector2[] vertices2D;

    // Use this for initialization
    void Start()
    {

        // Create Vector2 vertices
        vertices2D = new Vector2[] { new Vector2(0, 0), new Vector2(10, 0), new Vector2(10, 10), new Vector2(0, 10), };
        Vector2[] vertices2D2 = new Vector2[] { new Vector2(0, 0), new Vector2(2, 2), new Vector2(4, 0), new Vector2(4, 8), new Vector2(2, 6), new Vector2(0, 8) };
        Vector2[] vertices2D3 = new Vector2[] { new Vector2(1, 0), new Vector2(2, 1), new Vector2(3, 4), new Vector2(5, 0), new Vector2(4, 5), new Vector2(5, 4), new Vector2(4, 6), new Vector2(5, 8), new Vector2(2, 5), new Vector2(1, 3), new Vector2(0, 5), new Vector2(0, 2) };
        Vector2[] vertices2D4 = new Vector2[] { new Vector2(340.4437f, 739.1506f), new Vector2(350.0437f, 688.6506f), new Vector2(369.9437f, 695.1506f), new Vector2(366.7437f, 712.6506f), new Vector2(374.9437f, 713.9506f), new Vector2(383.5437f, 714.5506f), new Vector2(387.2437f, 714.7506f), new Vector2(387.7437f, 712.6506f), new Vector2(383.7437f, 712.3506f), new Vector2(371.8438f, 708.3506f), new Vector2(373.8438f, 695.3506f), new Vector2(398.5437f, 702.7506f), new Vector2(398.4437f, 705.2506f), new Vector2(406.8438f, 707.4506f), new Vector2(407.3438f, 715.0506f), new Vector2(403.0437f, 754.9506f), new Vector2(390.0437f, 754.8506f), new Vector2(389.8438f, 756.5506f), new Vector2(365.3438f, 752.5506f), new Vector2(363.7437f, 743.0506f), new Vector2(361.1437f, 742.7506f), new Vector2(361.8438f, 745.0506f), new Vector2(339.1437f, 747.0506f) };
        Vector2[] vertices2D5 = new Vector2[] { new Vector2(340, 739), new Vector2(350, 688), new Vector2(369, 695), new Vector2(366, 712), new Vector2(374, 713), new Vector2(383, 714), new Vector2(387, 714), new Vector2(387, 712), new Vector2(383, 712), new Vector2(371, 708), new Vector2(373, 695), new Vector2(398, 702), new Vector2(398, 705), new Vector2(406, 707), new Vector2(407, 715), new Vector2(403, 754), new Vector2(390, 754), new Vector2(389, 756), new Vector2(365, 752), new Vector2(363, 743), new Vector2(361, 742), new Vector2(361, 745), new Vector2(339, 747) };
        Vector2[] vertices2D83 = new Vector2[] { new Vector2(364, 839), new Vector2(351, 843), new Vector2(333, 784), new Vector2(333, 776), new Vector2(337, 753), new Vector2(350, 756), new Vector2(346, 773), new Vector2(346, 779), new Vector2(348, 785)};
        Vector2[] vertices2D27 = new Vector2[] { new Vector2(264, 870), new Vector2(260, 865), new Vector2(257, 857), new Vector2(263, 854), new Vector2(264, 858), new Vector2(268, 856), new Vector2(271, 854), new Vector2(272, 850), new Vector2(269, 850), new Vector2(272, 837), new Vector2(261, 826), new Vector2(248, 813), new Vector2(257, 804), new Vector2(268, 814), new Vector2(270, 813), new Vector2(274, 817), new Vector2(272, 819), new Vector2(280, 827), new Vector2(284, 819), new Vector2(289, 805), new Vector2(280, 808), new Vector2(279, 803), new Vector2(304, 797), new Vector2(305, 803), new Vector2(307, 804), new Vector2(309, 813), new Vector2(307, 817), new Vector2(311, 818), new Vector2(318, 845), new Vector2(314, 848), new Vector2(290, 840), new Vector2(288, 848), new Vector2(295, 857), new Vector2(313, 862), new Vector2(323, 861), new Vector2(327, 876), new Vector2(329, 877), new Vector2(334, 890), new Vector2(328, 893), new Vector2(323, 897), new Vector2(310, 892), new Vector2(292, 886), new Vector2(292, 883), new Vector2(281, 880), new Vector2(281, 878), new Vector2(269, 874)};
        Vector2[] vertices2D156 = new Vector2[] { new Vector2(264, 689), new Vector2(317, 699), new Vector2(315, 713), new Vector2(297, 709), new Vector2(295, 719), new Vector2(261, 713), new Vector2(261, 703) };
        Vector2[] vertices2D162 = new Vector2[] { new Vector2(543, 711), new Vector2(542, 699), new Vector2(563, 698), new Vector2(564, 710) };
         Vector2[] verticesLineString = new Vector2[] { new Vector2(342, 586), new Vector2(345, 581), new Vector2(348, 579), new Vector2(351, 579), new Vector2(355, 579), new Vector2(357, 579), new Vector2(360, 579), new Vector2(362, 579), new Vector2(365, 579), new Vector2(370, 579), new Vector2(375, 579), new Vector2(380, 579) };
        Vector2[] verticesWorldShape = new Vector2[] { new Vector2(0.0f, 984f), new Vector2(855f, 984f), new Vector2(855f, 0.0f), new Vector2(0.0f, 0.0f) };
        Vector3[] verticesWorldShape3 = new Vector3[] {  new Vector3(0.0f, 0.0f, 0.0f), new Vector3(0.0f, 984f, 0.0f), new Vector3(855f, 984f, 0.0f), new Vector3(855f, 0.0f, 0.0f) };

        //verticesWorldShape3 = new Vector3[] {new Vector3(0.0f,0.0f,984f), new Vector3(855f,0.0f,984f),new Vector3(855f,0.0f,0.0f),new Vector3(0.0f,0.0f,0.0f)};

        triangulator = new Triangulator(vertices2D);
        GameObject poly;
        MeshFilter filter;
        /* 
        vertices2D = vertices2D5;
        triangulator.setPoints(vertices2D);
        poly = new GameObject("Poly_1");
        poly.AddComponent(typeof(MeshRenderer));
        filter = poly.AddComponent(typeof(MeshFilter)) as MeshFilter;
        poly.GetComponent<MeshFilter>().mesh.Clear();
        poly.GetComponent<MeshFilter>().mesh = CreateMesh(10);
        poly.GetComponent<Renderer>().material = myNewMaterial;

        vertices2D = vertices2D83;
        triangulator.setPoints(vertices2D);
        poly = new GameObject("Poly_83");
        poly.AddComponent(typeof(MeshRenderer));
        filter = poly.AddComponent(typeof(MeshFilter)) as MeshFilter;
        poly.GetComponent<MeshFilter>().mesh.Clear();
        poly.GetComponent<MeshFilter>().mesh = CreateMesh(10);
        poly.GetComponent<Renderer>().material = myNewMaterial;

        vertices2D = vertices2D27;
        triangulator.setPoints(vertices2D);
        poly = new GameObject("Poly_27");
        poly.AddComponent(typeof(MeshRenderer));
        filter = poly.AddComponent(typeof(MeshFilter)) as MeshFilter;
        poly.GetComponent<MeshFilter>().mesh.Clear();
        poly.GetComponent<MeshFilter>().mesh = CreateMesh(10);
        poly.GetComponent<Renderer>().material = myNewMaterial;
        



        vertices2D = vertices2D156;
        triangulator.setPoints(vertices2D);

        poly = new GameObject("Poly_156");
        poly.AddComponent(typeof(MeshRenderer));
        filter = poly.AddComponent(typeof(MeshFilter)) as MeshFilter;
        poly.GetComponent<MeshFilter>().mesh.Clear();
        poly.GetComponent<MeshFilter>().mesh = CreateMesh(10);
        poly.GetComponent<Renderer>().material = myNewMaterial;
        */
        
        /* 
        vertices2D = verticesWorldShape;
        triangulator.setPoints(vertices2D);
        poly = new GameObject("worldShape");
        poly.AddComponent(typeof(MeshRenderer));
        filter = poly.AddComponent(typeof(MeshFilter)) as MeshFilter;
        poly.GetComponent<MeshFilter>().mesh.Clear();
        poly.GetComponent<MeshFilter>().mesh = CreateMesh(0);
        poly.GetComponent<Renderer>().material = myNewMaterial;

        Mesh m = new Mesh();
        m.Clear();
        m.vertices = verticesWorldShape3;
        //m.triangles = new int[] { 0, 1, 2, 3, 0, 2 };
        m.triangles = new int[] { };
        Unwrapping.GenerateSecondaryUVSet(m);

        m.RecalculateNormals();
        m.RecalculateBounds();
        MeshUtility.Optimize(m);

        poly.GetComponent<MeshFilter>().mesh = m;
        poly.transform.position = new Vector3(0, 0, 2);

        Color col = new Color();

        var bigint =  -4260135;
        var r = (bigint >> 16) & 255;
        var g = (bigint >> 8) & 255;
        var b = bigint & 255;

        col.r = r;
        col.b = b;
        col.g = g;

     //   if (ColorUtility.TryParseHtmlString(htmlValue, out col))
     //       property.colorValue = newCol;

     //   col.gamma = -4260135;

        Renderer rend = poly.GetComponent<Renderer>();
        rend.material.color = col;
        
        */







        /*

        vertices2D = vertices2D162;
        triangulator.setPoints(vertices2D);
        poly = new GameObject("Poly_162");
        poly.AddComponent(typeof(MeshRenderer));
        filter = poly.AddComponent(typeof(MeshFilter)) as MeshFilter;
        poly.GetComponent<MeshFilter>().mesh.Clear();
        poly.GetComponent<MeshFilter>().mesh = CreateMesh(10);
        poly.GetComponent<Renderer>().material = myNewMaterial;
        poly.GetComponent<Renderer>().material = (Material)Resources.Load("UVGrid", typeof(Material));

        Material mat = Utils.getMaterialByName("UVGrid");
        if (mat != null)
        {
            poly.GetComponent<Renderer>().material = mat;
        }

         */

        /* 
        vertices2D = verticesLineString;
        triangulator.setPoints(vertices2D);
        poly = new GameObject("LineString_109");
        poly.AddComponent(typeof(MeshRenderer));
        filter = poly.AddComponent(typeof(MeshFilter)) as MeshFilter;
        poly.GetComponent<MeshFilter>().mesh.Clear();
        poly.GetComponent<MeshFilter>().mesh = CreateMesh(0);
        poly.GetComponent<Renderer>().material = myNewMaterial;
        */

        List<Vector3> verticies = new List<Vector3>();
        for (int i = 0; i < vertices2D.Length; i++)
        {
            verticies.Add(vertices2D[i]);
        }



        //-----------------------------------------------------------------------------
        //-----------------------------------------------------------------------------

        List<Vector3> points = verticies;

        int startWidth = 10;
        /* 
        gameObject.AddComponent<LineRenderer>();
        line = (LineRenderer)gameObject.GetComponent(typeof(LineRenderer));
        line.SetPositions(verticesLineString);
        GameObject caret = null;
       
        line.
        */

        /* 
        Vector3 left, right; // A position to the left of the current line
        GameObject caret = new GameObject("Lines");
        Debug.Log("- Before - Total line position is : " + points.Count);
        // For all but the last point
        for (var i = 0; i < points.Count - 1; i++)
        {
            Debug.Log("- Before - Total line position is : " + i);
            caret.transform.position = points[i];
            caret.transform.LookAt(points[i + 1]);
            right = caret.transform.position + transform.right * startWidth / 2;
            left = caret.transform.position - transform.right * startWidth / 2;
            points.Add(left);
            points.Add(right);
        }

        Debug.Log("- After - Total line position is : " + points.Count);

        // Last point looks backwards and reverses
        caret.transform.position = points[(points.Count - 1)];
        caret.transform.LookAt(points[(points.Count - 2)]);
        right = caret.transform.position + transform.right * startWidth / 2;
        left = caret.transform.position - transform.right * startWidth / 2;
        points.Add(left);
        points.Add(right);


       
       // triangulator.setPoints(vertices2D);
       // poly = new GameObject("LineString_109");
        caret.AddComponent(typeof(MeshRenderer));
        filter = caret.AddComponent(typeof(MeshFilter)) as MeshFilter;
        caret.GetComponent<MeshFilter>().mesh.Clear();
        caret.GetComponent<MeshFilter>().mesh = DrawLineMesh(points);

        */

        //caret.GetComponent<Renderer>().material = myNewMateria
        //Destroy(caret);



        //-----------------------------------------------------------------------------
        //-----------------------------------------------------------------------------
    }



    private Mesh DrawLineMesh(List<Vector3> points)
    {
        Mesh m = new Mesh();
        m.Clear();
        Vector3[] verticies = new Vector3[points.Count];

        for (int i = 0; i < verticies.Length; i++)
        {
            verticies[i] = points[i];
        }

        int[] triangles = new int[((points.Count / 2) - 1) * 6];
        //Works on linear patterns tn = bn+c
        int position = 6;
        for (int i = 0; i < (triangles.Length / 6); i++)
        {
            Debug.Log("Triangles are: " + i);
            triangles[i * position] = 2 * i;
            triangles[i * position + 3] = 2 * i;

            triangles[i * position + 1] = 2 * i + 3;
            triangles[i * position + 4] = (2 * i + 3) - 1;

            triangles[i * position + 2] = 2 * i + 1;
            triangles[i * position + 5] = (2 * i + 1) + 2;
        }

        m.vertices = verticies;
        m.triangles = triangles;
       
        // For Android Build
        Unwrapping.GenerateSecondaryUVSet(m);
        
        m.RecalculateNormals();
        m.RecalculateBounds();
        
        // For Android Build
        MeshUtility.Optimize(m);
        Debug.Log("Triangles are: " + triangles.ToString());
        return m;
    }


    // Update is called once per frame
    void Update()
    {

    }





    public Mesh CreateMesh(int elevation)
    {

        Mesh m = new Mesh();

        m.Clear();
        m.vertices = triangulator.get3dVertices(elevation);
        triangulator.setAllPoints(triangulator.get2dVertices());
        m.triangles = triangulator.get3DTriangulesFrom2D();

        // For Android Build
        Unwrapping.GenerateSecondaryUVSet(m);

        m.RecalculateNormals();
        m.RecalculateBounds();
        // For Android Build
        MeshUtility.Optimize(m);

        //Debug.Log("After -> " + m.uv.Length);

        return m;
    }

}
