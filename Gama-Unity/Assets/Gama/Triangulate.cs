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
        Vector2[] vertices2D4 = new Vector2[] { new Vector2(340.4437f, 739.1506f), new Vector2(350.0437f, 688.6506f), new Vector2(369.9437f, 695.1506f), new Vector2(366.7437f, 712.6506f), new Vector2(374.9437f, 713.9506f), new Vector2(383.5437f, 714.5506f), new Vector2(387.2437f, 714.7506f), new Vector2(387.7437f, 712.6506f), new Vector2(383.7437f, 712.3506f), new Vector2(371.8438f, 708.3506f), new Vector2(373.8438f, 695.3506f), new Vector2(398.5437f, 702.7506f), new Vector2(398.4437f, 705.2506f), new Vector2(406.8438f, 707.4506f), new Vector2(407.3438f, 715.0506f), new Vector2(403.0437f, 754.9506f), new Vector2(390.0437f, 754.8506f), new Vector2(389.8438f, 756.5506f), new Vector2(365.3438f, 752.5506f), new Vector2(363.7437f, 743.0506f), new Vector2(361.1437f, 742.7506f), new Vector2(361.8438f, 745.0506f), new Vector2(339.1437f, 747.0506f), new Vector2(340.4437f, 739.1506f) };
        Vector2[] vertices2D5 = new Vector2[] { new Vector2(340, 739), new Vector2(350, 688), new Vector2(369, 695), new Vector2(366, 712), new Vector2(374, 713), new Vector2(383, 714), new Vector2(387, 714), new Vector2(387, 712), new Vector2(383, 712), new Vector2(371, 708), new Vector2(373, 695), new Vector2(398, 702), new Vector2(398, 705), new Vector2(406, 707), new Vector2(407, 715), new Vector2(403, 754), new Vector2(390, 754), new Vector2(389, 756), new Vector2(365, 752), new Vector2(363, 743), new Vector2(361, 742), new Vector2(361, 745), new Vector2(339, 747), new Vector2(340, 739) };
        Vector2[] vertices2D83 = new Vector2[] { new Vector2(364, 839), new Vector2(351, 843), new Vector2(333, 784), new Vector2(333, 776), new Vector2(337, 753), new Vector2(350, 756), new Vector2(346, 773), new Vector2(346, 779), new Vector2(348, 785), new Vector2(364, 839) };
        Vector2[] vertices2D27 = new Vector2[] { new Vector2(264, 870), new Vector2(260, 865), new Vector2(257, 857), new Vector2(263, 854), new Vector2(264, 858), new Vector2(268, 856), new Vector2(271, 854), new Vector2(272, 850), new Vector2(269, 850), new Vector2(272, 837), new Vector2(261, 826), new Vector2(248, 813), new Vector2(257, 804), new Vector2(268, 814), new Vector2(270, 813), new Vector2(274, 817), new Vector2(272, 819), new Vector2(280, 827), new Vector2(284, 819), new Vector2(289, 805), new Vector2(280, 808), new Vector2(279, 803), new Vector2(304, 797), new Vector2(305, 803), new Vector2(307, 804), new Vector2(309, 813), new Vector2(307, 817), new Vector2(311, 818), new Vector2(318, 845), new Vector2(314, 848), new Vector2(290, 840), new Vector2(288, 848), new Vector2(295, 857), new Vector2(313, 862), new Vector2(323, 861), new Vector2(327, 876), new Vector2(329, 877), new Vector2(334, 890), new Vector2(328, 893), new Vector2(323, 897), new Vector2(310, 892), new Vector2(292, 886), new Vector2(292, 883), new Vector2(281, 880), new Vector2(281, 878), new Vector2(269, 874), new Vector2(264, 870) };
        Vector2[] vertices2D156 = new Vector2[] { new Vector2(264, 689), new Vector2(317, 699), new Vector2(315, 713), new Vector2(297, 709), new Vector2(295, 719), new Vector2(261, 713), new Vector2(261, 703), new Vector2(264, 689) };
        Vector2[] vertices2D162 = new Vector2[] { new Vector2(543, 711), new Vector2(542, 699), new Vector2(563, 698), new Vector2(564, 710), new Vector2(543, 711)};
        vertices2D162 = new Vector2[] { new Vector2(543, 711), new Vector2(542, 699), new Vector2(563, 698), new Vector2(564, 710)};
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
        vertices2D = vertices2D162;
        triangulator.setPoints(vertices2D);
        poly = new GameObject("Poly_162");
        poly.AddComponent(typeof(MeshRenderer));
        filter = poly.AddComponent(typeof(MeshFilter)) as MeshFilter;
        poly.GetComponent<MeshFilter>().mesh.Clear();
        poly.GetComponent<MeshFilter>().mesh = CreateMesh(10);
        poly.GetComponent<Renderer>().material = myNewMaterial;



        //-----------------------------------------------------------------------------
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

        Debug.Log("Total  Vertices is : "+m.vertices.Length);
        Debug.Log("Total  Triangles is : "+m.triangles.Length);

        // m.triangles = triangulator.checkClockWise(m.triangles, m.vertices);
        //Debug.Log("After triangles checks ------------------------------------------------------------->>>> ");
        // m.triangles = triangulator.checkClockWise(m.triangles, m.vertices);

        //        m.uv = UvCalculator.CalculateUVsUp(m.vertices, 1);
        m.uv = UvCalculator.CalculateUVs3(m.vertices, 1);

        // m.uv2 = UvCalculator.CalculateUVsRight(m.vertices, 1);
        // m.uv3 = UvCalculator.CalculateUVsForward(m.vertices, 1);

        //m.uv = UvCalculator.CalculateUVsUp(m.vertices, 1);
        //m.uv = UvCalculator.CalculateUVsRight(m.vertices, 1);
        //m.uv = UvCalculator.CalculateUVsForward(m.vertices, 1);

        //Debug.Log("Before -> " + m.uv.Length);
        Unwrapping.GenerateSecondaryUVSet(m);

        m.RecalculateNormals();
        m.RecalculateBounds();
        MeshUtility.Optimize(m);

        //Debug.Log("After -> " + m.uv.Length);

        return m;
    }

}
