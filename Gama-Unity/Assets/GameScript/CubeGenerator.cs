using UnityEngine;
using System.Collections.Generic;
using ummisco.gama.unity.utils;
using System;
using System.Collections;

[RequireComponent(typeof (MeshFilter))]
[RequireComponent(typeof (MeshRenderer))]
public class CubeGenerator : MonoBehaviour {


	private Rigidbody rb;
	public float inverseMoveTime;
	public float moveTime = 10000.1f;

	public MapBuilder mapBuilder ; //= new MapBuilder();

	GameObject objectTest;


	Boolean isMove = false;

	void Start () {
		
		//objectTest = CreateCube ("testCube");

		//rb = GetComponent<Rigidbody> ();

		//inverseMoveTime = 1f / moveTime;

		//Debug.Log ("The script Type is : "+gameObject.GetComponent ("CubeGenerator").GetType ());

		//mapBuilder = new MapBuilder();


	}






	private GameObject CreateCube (string name) {

		GameObject ob = new GameObject(name);

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


		ob.GetComponent<MeshFilter>().mesh = mesh;


		return ob;
	}



	void FixedUpdate ()
	{
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");

		Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);
	//	rb.AddForce (movement * 20);
	//	rb.MovePosition (movement);
	//	transform.position = new Vector3 (moveHorizontal, 0.0f, moveVertical+1);

		if (isMove) {
			//transform.position = new Vector3 (moveHorizontal+3, 0.0f, moveVertical+4);
			moveToPosition (moveHorizontal+20,  0.0f, moveVertical+18, 10);
			isMove = false;
		}

	}

	public void UpdatePosition (float moveHorizontal, float moveVertical)
	{
		Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);
		//rb.AddForce (movement * speed);
		//rb.MovePosition (movement);
	}


	public void OnMoveEvent()
	{
		Debug.Log("OnMove called.");
	}





	public void moveToPosition (float xDir, float yDir, float zDir, int speed)
	{

		//Store start position to move from, based on objects current transform position.
		Vector3 start = gameObject.transform.position;

		// Calculate end position based on the direction parameters passed in when calling Move.
		Vector3 end = start + new Vector3 (xDir, yDir, zDir);

		Vector3 movement = new Vector3 (xDir, yDir, zDir);

		//rb.AddForce (movement * speed);

		StartCoroutine (SmoothMovement (end, speed));
	}

	//Co-routine for moving units from one space to next, takes a parameter end to specify where to move to.
	protected IEnumerator SmoothMovement (Vector3 end, int speed)
	{
		//Calculate the remaining distance to move based on the square magnitude of the difference between current position and end parameter. 
		//Square magnitude is used instead of magnitude because it's computationally cheaper.
		float sqrRemainingDistance = (transform.position - end).sqrMagnitude;

	

		inverseMoveTime = 100f;

		//While that distance is greater than a very small amount (Epsilon, almost zero):
		//while (sqrRemainingDistance > float.Epsilon) {
		while (sqrRemainingDistance > 0.1f) {
			//Find a new position proportionally closer to the end, based on the moveTime
			//Vector3 newPostion = Vector3.MoveTowards (rb.position, end, inverseMoveTime * Time.deltaTime);
			Vector3 newPostion = Vector3.MoveTowards (rb.position, end, speed * Time.deltaTime);


			//Call MovePosition on attached Rigidbody2D and move it to the calculated position.
			rb.MovePosition (newPostion);

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

}
