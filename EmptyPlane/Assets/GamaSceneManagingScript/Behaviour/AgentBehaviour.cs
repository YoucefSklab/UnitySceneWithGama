using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace ummisco.gama.unity.Behaviour
{
	public class AgentBehaviour : MonoBehaviour
	{
		public bool isRotate;
		public bool isOnInputMove;
		public Rigidbody rb;
		public float speed;

		void start()
		{
			isRotate = false;
			isOnInputMove = false;
			speed = 10;
			rb = GetComponent<Rigidbody> ();

		}

		void Update ()
		{
			if (isRotate) {
				rotateObject ();
			}
			if (isOnInputMove) {
				onInputMove ();
			}
		}
	
	
	
		public void rotateObject()
		{
			transform.Rotate (new Vector3 (15, 30, 45) * Time.deltaTime);
		}

		public void onInputMove(){
			
			float moveHorizontal = Input.GetAxis ("Horizontal");
			float moveVertical = Input.GetAxis ("Vertical");
			Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);
			rb.AddForce (movement * speed);
		}
	
	
	
	
	
	
	}
}

