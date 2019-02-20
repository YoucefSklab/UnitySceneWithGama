using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentWalkingOnRoad : MonoBehaviour
{

    // Use this for initialization

    public GameObject walker; // = new GameObject("WalgerAgent");

    void Start()
    {
		/* 
        walker = GameObject.CreatePrimitive(PrimitiveType.Cube);

		Debug.Log(" Cube created ");
               
        walker.AddComponent<AgentBehaviour>();
        Renderer rend = walker.GetComponent<Renderer>();

        rend.material.color = new Color(100, 0, 100);

        walker.transform.position = new Vector3(150, 5, 1);

        walker.transform.localScale = new Vector3(5, 5, 5);

        walker.transform.localEulerAngles = new Vector3(45, 45, 45);


        // Display all gameObjects list

        GameObject[] allObjects = UnityEngine.Object.FindObjectsOfType<GameObject>();

        foreach (GameObject go in allObjects)
        {
            if (go.transform.name.Equals("317352709"))
            {

                Debug.Log(go.name + " is an active object nad its parent name is --->  " + go.transform.parent.gameObject.name);
                go.AddComponent<AgentBehaviour>();

                go.transform.position = new Vector3(1, 5, 1);

                Renderer rendB = go.GetComponent<Renderer>();

                rendB.material.color = new Color(100, 0, 100);

                Debug.Log(go.name + " changes are done ");
               
                //	go.transform.parent = walker;

            }
        }



	*/


    }

    // Update is called once per frame
    void Update()
    {



    }




}
