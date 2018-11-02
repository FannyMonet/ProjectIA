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

    public GameObject prefab;
    private GameObject BottleTrajectory;

    public LineRenderer lr;

    public GameObject Transformer;
	// Use this for initialization
	void Start () {
		rgbd = gameObject.GetComponent<Rigidbody>();
		agent = gameObject.GetComponent<NavMeshAgent>();
		agent.destination = target.position;
		BottleTrajectory = GameObject.Find("BottleTrajectory");
		lr = BottleTrajectory.GetComponent<LineRenderer>();
	}
	
	// Update is called once per frame
	void Update ()
	{
		remainingDistance = agent.remainingDistance;

		if (Input.GetAxis ("RightTrigger") == -1) {
			BottleTrajectory.SetActive (true);
			if (Input.GetAxis ("HorizontalRightJoystick") > 0.01) {
				if(BottleTrajectory.GetComponent<BottleArcRenderer>().angle>=45)
					BottleTrajectory.GetComponent<BottleArcRenderer>().angle=45;
				else
			        BottleTrajectory.GetComponent<BottleArcRenderer>().angle++;
			}
			else if (Input.GetAxis ("HorizontalRightJoystick") < -0.01) {
				if(BottleTrajectory.GetComponent<BottleArcRenderer>().angle<=-45)
					BottleTrajectory.GetComponent<BottleArcRenderer>().angle=-45;
				else
			        BottleTrajectory.GetComponent<BottleArcRenderer>().angle--;
			}
			if (Input.GetButton ("Fire1")) {
				Vector3 impactPosition = lr.GetPosition(lr.positionCount-2);
				Debug.Log(impactPosition);
				Instantiate(prefab,Transformer.transform.position, Quaternion.identity);
			}
		} else {
			BottleTrajectory.SetActive (false);
		}

		BottleTrajectory.transform.Rotate(new Vector3(0,Input.GetAxis ("VerticalRightJoystick"), 0));
		//Transformer.transform.Translate(new Vector3(Input.GetAxis ("VerticalRightJoystick")*10, 0, Input.GetAxis ("HorizontalRightJoystick")*10));
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
