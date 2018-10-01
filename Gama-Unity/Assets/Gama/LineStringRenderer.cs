using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class LineStringRenderer : MonoBehaviour
{
    public LineRenderer line;
    private List<Vector3> points = new List<Vector3>();

    // Use this for initialization
    public void Start()
    {
        points.Clear();

        line = new LineRenderer();
        gameObject.AddComponent<LineRenderer>();
        line = (LineRenderer)gameObject.GetComponent(typeof(LineRenderer));
        Vector3[] verticesLineString = new Vector3[] { new Vector2(342, 586), new Vector2(345, 581), new Vector2(348, 579), new Vector2(351, 579), new Vector2(355, 579), new Vector2(357, 579), new Vector2(360, 579), new Vector2(362, 579), new Vector2(365, 579), new Vector2(370, 579), new Vector2(375, 579), new Vector2(380, 579) };
        line.SetVertexCount(verticesLineString.Length);
        line.SetPositions(verticesLineString);


        


        GameObject caret = null;
        caret = new GameObject("Lines");
        /*
        line.startWidth = 10;

        Vector3 left, right; // A position to the left of the current line

        Debug.Log("- Before - Total line position is : " + line.positionCount);
        // For all but the last point
        for (var i = 0; i < line.positionCount - 1; i++)
        {
            caret.transform.position = line.GetPosition(i);
            caret.transform.LookAt(line.GetPosition(i + 1));
            right = caret.transform.position + transform.right * line.startWidth / 2;
            left = caret.transform.position - transform.right * line.startWidth / 2;
            points.Add(left);
            points.Add(right);
        }

        Debug.Log("- After - Total line position is : " + points.Count);

        // Last point looks backwards and reverses
        caret.transform.position = line.GetPosition(line.positionCount - 1);
        caret.transform.LookAt(line.GetPosition(line.positionCount - 2));
        right = caret.transform.position + transform.right * line.startWidth / 2;
        left = caret.transform.position - transform.right * line.startWidth / 2;
        points.Add(left);
        points.Add(right);
         */
        //Destroy(caret);
       // DrawMesh();
    }

    private void DrawMesh()
    {



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
            triangles[i * position] = 2 * i;
            triangles[i * position + 3] = 2 * i;

            triangles[i * position + 1] = 2 * i + 3;
            triangles[i * position + 4] = (2 * i + 3) - 1;

            triangles[i * position + 2] = 2 * i + 1;
            triangles[i * position + 5] = (2 * i + 1) + 2;
        }
        gameObject.AddComponent(typeof(MeshRenderer));
        gameObject.AddComponent(typeof(MeshFilter));
        Mesh mesh = GetComponent<MeshFilter>().mesh;
        mesh.Clear();
        mesh.vertices = verticies;
        mesh.triangles = triangles;
        Unwrapping.GenerateSecondaryUVSet(mesh);
        mesh.RecalculateNormals();


        Debug.Log("Triangles are: " + triangles.ToString());
        foreach (var tr in triangles)
        {
            Debug.Log(tr);
        }

    }
}