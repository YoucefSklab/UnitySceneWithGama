using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using ummisco.gama.unity.GamaAgent;

namespace ummisco.gama.unity.AgentBehaviours
{
    public class AgentBehaviour : MonoBehaviour
    {
        public bool unity_rotate;
        public bool isOnInputMove;
        public Rigidbody rb;
        public float unity_speed;

        void start()
        {
            unity_rotate = false;
            isOnInputMove = false;
            unity_speed = 10;
            rb = GetComponent<Rigidbody>();

        }

        void Update()
        {
            if (unity_rotate)
            {
                rotateObject();
            }
            if (isOnInputMove)
            {
                onInputMove();
            }

            Debug.Log("The speeeeed is :");
        }

        public void rotateObject()
        {
            transform.Rotate(new Vector3(15, 30, 45) * Time.deltaTime);
        }

        public void onInputMove()
        {

            float moveHorizontal = Input.GetAxis("Horizontal");
            float moveVertical = Input.GetAxis("Vertical");
            Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
            rb.AddForce(movement * unity_speed);
        }

    }
}

