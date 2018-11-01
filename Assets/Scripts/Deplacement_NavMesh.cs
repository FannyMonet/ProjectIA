using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Deplacement_NavMesh : MonoBehaviour {


    private NavMeshAgent agent;
    public int test;
    private float maxSpeed;

    public GameObject player;
    public GameObject bullet;

    private int shootCounter;
    public int shootCounterAtStart;

    public bool playerDetected;

    public Transform[] ennemyPattern;
    public int index;
    public Light light;

	public float duration = 1.0F;
    public Color color0 = Color.red;
    public Color color1 = Color.blue;

    public IA_Supervisor supervisor;

    public int distance;
    public LineRenderer lineOfSight;

    public float VisionArea;

    // Use this for initialization
    void Start () {

        agent = this.GetComponent<NavMeshAgent>();
        maxSpeed = test;
		agent.speed = maxSpeed;
        agent.acceleration =test;
		shootCounter = shootCounterAtStart;
		lineOfSight = this.GetComponent<LineRenderer>();

    }

	// Update is called once per frame
	void Update ()
	{

		if (playerDetected) {
			float t = Mathf.PingPong (Time.time, duration) / duration;
			light.color = Color.Lerp (color0, color1, t);
			attackPlayer ();  
			if (shootCounter == shootCounterAtStart) {
				//Shoot ();
				shootCounter--;
			}
			if (shootCounter < shootCounterAtStart) {
				shootCounter--;
				if (shootCounter == 0) {
					shootCounter = shootCounterAtStart;
				}
			}
		} else {
			RaycastHit hitInfoCenter; //= Physics.Raycast (transform.position, transform.right, distance);
			RaycastHit hitInfoLeft; //= Physics.Raycast (transform.position, transform.right, distance);
			RaycastHit hitInfoRight; //= Physics.Raycast (transform.position, transform.right, distance);


			//RAYCAST CENTER
			if (Physics.Raycast (transform.position, transform.TransformDirection (Vector3.forward), out hitInfoCenter, distance)) {

				Debug.DrawLine (transform.position, hitInfoCenter.point, Color.red);
				lineOfSight.SetPosition (1, hitInfoCenter.point);
				if (hitInfoCenter.collider.CompareTag ("Player")) {
					this.GetComponent<AudioSource> ().PlayOneShot (this.GetComponent<AudioSource> ().clip);
					supervisor.playerDetected = true;
				}
			}
			else {
				Debug.DrawLine (transform.position, transform.position + transform.TransformDirection(Vector3.forward) * distance, Color.green);
				lineOfSight.SetPosition(1, transform.position + transform.TransformDirection(Vector3.forward) * distance);
			}
			//RAYCAST LEFT
			if (Physics.Raycast (transform.position, transform.TransformDirection (new Vector3(VisionArea,0,1)), out hitInfoLeft, distance)) {

				Debug.DrawLine (transform.position, hitInfoLeft.point, Color.red);
				lineOfSight.SetPosition (1, hitInfoCenter.point);
				if (hitInfoLeft.collider.CompareTag ("Player")) {
					this.GetComponent<AudioSource> ().PlayOneShot (this.GetComponent<AudioSource> ().clip);
					supervisor.playerDetected = true;
				}
			}
			else {
				Debug.DrawLine (transform.position, transform.position + transform.TransformDirection(new Vector3(VisionArea,0,1)) * distance, Color.green);
				lineOfSight.SetPosition(1, transform.position + transform.TransformDirection(new Vector3(VisionArea,0,1)) * distance);

			}

			//RAYCASTRIGHT
			//RAYCAST LEFT
			if (Physics.Raycast (transform.position, transform.TransformDirection (new Vector3(-VisionArea,0,1)), out hitInfoRight, distance)) {

				Debug.DrawLine (transform.position, hitInfoRight.point, Color.red);
				lineOfSight.SetPosition (1, hitInfoCenter.point);
				if (hitInfoRight.collider.CompareTag ("Player")) {
					this.GetComponent<AudioSource> ().PlayOneShot (this.GetComponent<AudioSource> ().clip);
					supervisor.playerDetected = true;
				}
			}
			else {
				Debug.DrawLine (transform.position, transform.position + transform.TransformDirection(new Vector3(-VisionArea,0,1)) * distance, Color.green);
				lineOfSight.SetPosition(1, transform.position + transform.TransformDirection(new Vector3(-VisionArea,0,1)) * distance);

			}







			moveToPoint ();
			Debug.Log("Move to point");
			lineOfSight.SetPosition(0, transform.position);


		}
  }



    private void attackPlayer()
    {
        agent.destination = this.player.transform.position;

    }

	private void moveToPoint ()
	{
		if (!agent.hasPath) {
			index = (index + 1) % ennemyPattern.Length;
			agent.destination = ennemyPattern [index].position;
		}

    }

	private void Shoot()
    {
       Instantiate(bullet, transform.position, Quaternion.identity);

    }


	void OnTriggerEnter (Collider col)
	{
	if(col.CompareTag("Player") && !playerDetected){
			this.GetComponent<AudioSource>().PlayOneShot(this.GetComponent<AudioSource>().clip);
	   this.player = col.gameObject;
	   supervisor.playerDetected = true;
			
	}
	}


    }
