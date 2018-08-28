using UnityEngine;
using System.Collections.Generic;
using ummisco.gama.unity.utils;

[RequireComponent(typeof (MeshFilter))]
[RequireComponent(typeof (MeshRenderer))]
public class CubeGenerator : MonoBehaviour {


	GameObject objectTest;

	void Start () {
		
		objectTest = CreateCube ("testCube");

		Debug.Log ("The script Type is : "+gameObject.GetComponent ("CubeGenerator").GetType ());

	}






	private GameObject CreateCube (string name) {

		GameObject ob = new GameObject("Empty");

		ob.name = name;

		ob.AddComponent<MeshFilter>();
		ob.AddComponent<MeshRenderer>();
		ob.AddComponent<Rigidbody>();
		ob.AddComponent<BoxCollider>();

		Color objectColor = ConvertType.stringToColor ("red");
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

		Vector3 pos = new Vector3 (2, 0.5f, 4);

		ob.transform.position = pos;

		Mesh mesh = ob.GetComponent<MeshFilter> ().mesh;
		mesh.Clear ();
		mesh.vertices = vertices;
		mesh.triangles = triangles;
		//mesh.Optimize ();
		mesh.RecalculateNormals ();


		//ob.GetComponent<MeshFilter>().mesh = mesh;


		return ob;
	}
}
