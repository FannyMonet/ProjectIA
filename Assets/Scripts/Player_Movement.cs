using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class Player_Movement : MonoBehaviour {

    public int speed;//Speed of the player
    public Rigidbody rgbd;//RigidBody of the player

    public Transform target;//The end of the level
    public NavMeshAgent agent;//The agent of the player, allows to know how far he is from the end of the level

    public float remainingDistance;//The distance left between player and end of level

    public GameObject prefab;//The prefab of the bottle
    private GameObject BottleTrajectory;//The GameObject representing the arc renderer of the bottle

    public LineRenderer lr;//Line renderer of the bottle

	public GameObject Transformer;//The place where the bottle will appear

    public AudioSource source;//AudioSource for throwing sound
    public AudioClip clip;//Throwing sound

	// Use this for initialization
	void Start () {
		rgbd = gameObject.GetComponent<Rigidbody>();
		agent = gameObject.GetComponent<NavMeshAgent>();
		agent.destination = target.position;//Set the destination to the end of the level
		BottleTrajectory = GameObject.Find("BottleTrajectory");
		lr = BottleTrajectory.GetComponent<LineRenderer>();
		source = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update ()
	{
	    //Get the remaining distance from the end of the level
		remainingDistance = agent.remainingDistance;

		//Input to use the bottle
		if (Input.GetAxis ("RightTrigger") == -1) {
			BottleTrajectory.SetActive (true);
			//Change the angle releatively to the right joystick position
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
			//Create the bottle at the end of the arc renderer
			if (Input.GetButtonDown ("Fire1")) {
			    source.PlayOneShot(clip,0.5f);
				Vector3 impactPosition = lr.GetPosition(lr.positionCount-2);
				Debug.Log(impactPosition);
				GameObject bottle = Instantiate(prefab,Transformer.transform.position, Quaternion.identity);
				bottle.GetComponent<Bottle_Behaviour>().target = Transformer.transform.position;
			}
		} else {
			BottleTrajectory.SetActive (false);
		}

		BottleTrajectory.transform.Rotate(new Vector3(0,Input.GetAxis ("VerticalRightJoystick"), 0));
		//Transformer.transform.Translate(new Vector3(Input.GetAxis ("VerticalRightJoystick")*10, 0, Input.GetAxis ("HorizontalRightJoystick")*10));
	}
	//Movement
	void FixedUpdate ()
	{
			if (Input.GetAxis ("Vertical") > 0.01f) {
				gameObject.transform.Translate (new Vector3 (0, 0, 0.1f) * speed);
			} else if (Input.GetAxis ("Vertical") < -0.01f) {
				gameObject.transform.Translate (new Vector3 (0, 0, -0.1f) * speed);
			}

			if (Input.GetAxis ("Horizontal") > 0.01f) {
				gameObject.transform.Translate (new Vector3 (0.1f, 0, 0) * speed);
			} else if (Input.GetAxis ("Horizontal") < -0.01f) {
				gameObject.transform.Translate (new Vector3 (-0.1f, 0, 0) * speed);
			}
		}
}
