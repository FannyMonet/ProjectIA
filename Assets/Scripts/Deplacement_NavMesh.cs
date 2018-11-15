using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Deplacement_NavMesh : MonoBehaviour {


    private NavMeshAgent agent;
    private float maxSpeed;

    public GameObject player;

    private int shootCounter;
    public int shootCounterAtStart;

    public bool playerDetected;

    public Transform[] ennemyPattern;//The way point of the ennemy
    public int index;//index of the ennemyPattern tab
    public Light light;//Light that will change if a player is seen

	public float duration = 1.0F;
    public Color color0 = Color.red;//Color when IA detect player
    public Color color1 = Color.blue;//Default color

    public IA_Supervisor supervisor;//The supervisor of all IA

    public int distance;//
    public LineRenderer lineOfSight;//The cone vision of the IA

    public float VisionArea;//Angle of view

    public BoxCollider boxCol;
    public bool isTrapped;
    public int waitingTimeAtStart;
    private int waitingTime;

    // Use this for initialization
    void Start () {

        agent = this.GetComponent<NavMeshAgent>();
        maxSpeed = 100;
		agent.speed = maxSpeed;
        agent.acceleration =100;
		shootCounter = shootCounterAtStart;
		lineOfSight = this.GetComponent<LineRenderer>();
        waitingTime = waitingTimeAtStart;
    }

	// Update is called once per frame
	void Update ()
	{

	//If the player is detected, follow it and change screen color
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
			RaycastHit hitInfoCenter; 
			RaycastHit hitInfoLeft; 
			RaycastHit hitInfoRight; 


			//RAYCAST CENTER
			//if raycast hit something
			if (Physics.Raycast (transform.position, transform.TransformDirection (Vector3.forward), out hitInfoCenter, distance)) {

				Debug.DrawLine (transform.position, hitInfoCenter.point, Color.red);
				lineOfSight.SetPosition (1, hitInfoCenter.point);
				if(boxCol!=null)
					boxCol.size = new Vector3(boxCol.size.x, boxCol.size.y, Vector3.Distance(lineOfSight.GetPosition(0), lineOfSight.GetPosition(1))/25);

				//if the thing is the player
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

            if (!isTrapped)
                moveToPoint();
            else
            {
                waitingTime--;
                if (waitingTime == 0)
                {
                    moveToPoint();
                    waitingTime = waitingTimeAtStart;
                    
                }
            }
            //allways center the line renderer
			lineOfSight.SetPosition(0, transform.position);

		}
  }


  //change the destination of the IA to be the player transform
    private void attackPlayer()
    {
        agent.destination = this.player.transform.position;

    }
	//change the destination of the IA if it has reached it previous destination

	private void moveToPoint ()
	{
		if (!agent.hasPath) {
			index = (index + 1) % ennemyPattern.Length;
			agent.destination = ennemyPattern [index].position;
		}
        else if(isTrapped)
        {
            index = (index + 1) % ennemyPattern.Length;
            agent.destination = ennemyPattern[index].position;
            isTrapped = false;
        }

    }

	/*private void Shoot()
    {
       Instantiate(bullet, transform.position, Quaternion.identity);

    }
    */


	void OnTriggerEnter (Collider col)
	{
		if (col.CompareTag ("Player") && !playerDetected) {
			this.GetComponent<AudioSource> ().PlayOneShot (this.GetComponent<AudioSource> ().clip);
			this.player = col.gameObject;
			supervisor.playerDetected = true;
			
		}
        if(col.CompareTag("Bottle") && !playerDetected)
        {
            isTrapped = true;
            this.agent.destination = col.transform.position;
        }
	}


    }
