using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class Player_Movement : MonoBehaviour {

    public int speed;
    public Rigidbody rgbd;

    public Transform target;
    public NavMeshAgent agent;

    public float remainingDistance;
	// Use this for initialization
	void Start () {
		rgbd = gameObject.GetComponent<Rigidbody>();
		agent = gameObject.GetComponent<NavMeshAgent>();
		agent.destination = target.position;
	}
	
	// Update is called once per frame
	void Update () {
		remainingDistance = agent.remainingDistance;
	}

	void FixedUpdate ()
	{
		if (Input.GetAxis ("Vertical") > 0.01f) {
			gameObject.transform.Translate (new Vector3 (0, 0, 0.1f)*speed);
		}
		else if (Input.GetAxis ("Vertical") < -0.01f) {
			gameObject.transform.Translate (new Vector3 (0, 0, -0.1f)*speed);
		}

		if (Input.GetAxis ("Horizontal") > 0.01f) {
			gameObject.transform.Translate (new Vector3 (0.1f, 0, 0)*speed);
		}
		else if (Input.GetAxis ("Horizontal") < -0.01f) {
			gameObject.transform.Translate (new Vector3 (-0.1f, 0,0 )*speed);
		}
	}
}
