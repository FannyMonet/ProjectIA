using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class Player_Movement : MonoBehaviour {


    public int lifePoint;
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

    public IA_Supervisor supervisor;

    public Vector3 StartingPos;

    private int timeBeforeSpawning;
	public int timeBeforeSpawningAtStart;


	// Use this for initialization
	void Start () {
	    StartingPos = this.transform.position;
		rgbd = gameObject.GetComponent<Rigidbody>();
		agent = gameObject.GetComponent<NavMeshAgent>();
		agent.destination = target.position;//Set the destination to the end of the level
		BottleTrajectory = GameObject.Find("BottleTrajectory");
		lr = BottleTrajectory.GetComponent<LineRenderer>();
		source = GetComponent<AudioSource>();
		supervisor = GameObject.Find("IA_SUPERVISOR PLAYER L1").GetComponent<IA_Supervisor>();
		timeBeforeSpawning = timeBeforeSpawningAtStart;
	}
	
	// Update is called once per frame
	void Update ()
	{
		//Get the remaining distance from the end of the level
		if(agent.isActiveAndEnabled)
		    remainingDistance = agent.remainingDistance;

		if (lifePoint <= 0) {

		RestartLevel();
		}

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

    void OnTriggerStay (Collider col)
	{

		if (col.CompareTag ("Enemy")) {
			lifePoint--;
			if (lifePoint == 0) {
			Debug.Log("DEAD");
			}
		}
	}

	void RestartLevel ()
	{
		this.GetComponent<NavMeshAgent> ().enabled = false;
		this.GetComponent<BoxCollider> ().enabled = false;


		this.GetComponent<MeshRenderer> ().enabled = false;
		this.GetComponent<TrailRenderer> ().enabled = false;//So that the player can't see the teleportation
		supervisor.playerDetected = false;
		supervisor.reset = true;


		this.transform.position = StartingPos;
		foreach (GameObject g in supervisor.agents) {
			g.GetComponent<Deplacement_NavMesh> ().index = 0;
			g.GetComponent<Deplacement_NavMesh> ().agent.SetDestination (g.GetComponent<Deplacement_NavMesh> ().ennemyPattern [g.GetComponent<Deplacement_NavMesh> ().index].position);

		}
		if (timeBeforeSpawning <= 0) {
			this.GetComponent<NavMeshAgent>().enabled = true;
			this.GetComponent<BoxCollider>().enabled = true;

			lifePoint = 3;
			this.GetComponent<TrailRenderer> ().enabled = true;
			this.GetComponent<MeshRenderer> ().enabled = true;
			timeBeforeSpawning = timeBeforeSpawningAtStart;
			for (int i = 0; i < supervisor.agents.Length; i++) {
			supervisor.agents[i].GetComponent<TrailRenderer>().enabled = true;
		}

		}
		else timeBeforeSpawning--;

	}
}
