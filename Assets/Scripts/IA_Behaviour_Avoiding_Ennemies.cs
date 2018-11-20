using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;


public class IA_Behaviour_Avoiding_Ennemies : MonoBehaviour {

	public NavMeshAgent agent;

    public GameObject ennemies;

    public int lifePoint = 3;

    public int test;
    private float maxSpeed;

    public GameObject player;

    public int EnemyDistanceRun;

    public GameObject target;

	public GameObject bonus;


    public int index;
    public int minIndex;

    public Transform[] safePoints;

    public bool tryToGetBonus;


		public GameObject destination;

		public Vector3 StartingPos;


	private int timeBeforeSpawning;
	public int timeBeforeSpawningAtStart;

	public IA_Supervisor supervisor;

	public GameObject firstBalise;


    // Use this for initialization
    void Start () {
    StartingPos = this.transform.position;
        agent = this.GetComponent<NavMeshAgent>();
        maxSpeed = test;
		agent.speed = maxSpeed;
        agent.acceleration =test;
        player = GameObject.Find("PLAYER");
		target = GameObject.Find("REGIS_GOAL L1");
		bonus = GameObject.Find("Bonus");

		minIndex = index;

		timeBeforeSpawning = timeBeforeSpawningAtStart;

		supervisor = GameObject.Find("IA_SUPERVISOR REGIS L1").GetComponent<IA_Supervisor>();




    }

	// Update is called once per frame
	void Update ()
	{


		if (lifePoint <= 0) {

			RestartLevel();
			return;
		}



		//know if the agent is winning or not
		if (player.GetComponent<Player_Movement> ().remainingDistance < agent.remainingDistance) {
			//Debug.Log ("Player distance :" + player.GetComponent<Player_Movement> ().remainingDistance + ", Agent distance" + agent.remainingDistance + " Agent is LOOSING");
			if (bonus != null) {
				tryToGetBonus = true;
			}

		} else {
			//Debug.Log ("Player distance :" +player.GetComponent<Player_Movement> ().remainingDistance +", Agent distance"+ agent.remainingDistance+ " Agent is WINNING");


		}
		float distance = Vector3.Distance (transform.position, ennemies.transform.position);
		//if the agent is close to ennemy, it avoid them instead of trying to reach the end of the level
		if (distance < EnemyDistanceRun) {
		//Debug.Log("EnnemyDistance RUN!!");
			Vector3 dirToEnnemy = transform.position - ennemies.transform.position;
			Vector3 newPos = transform.position + dirToEnnemy;
			agent.destination =newPos;

		} else {
			   moveToPoint();

		}


  }


  //set the movement of the agent to reach the target
    private void reachDestination ()
	{

		agent.destination = this.target.transform.position;
    }

	private void moveToPoint ()
	{
		if (tryToGetBonus) {
			if (bonus != null) {
				agent.destination = bonus.transform.position;
			}
		} else if (agent.remainingDistance < 50) {
			if (index == safePoints.Length) {
			    return;
			}
			//Debug.Log("agent hasn't path");
			index = (index + 1) % safePoints.Length;
			if (index <= minIndex) {
				index = minIndex;
			}
			agent.destination = safePoints[index].position;
		}


    }

    void OnTriggerEnter (Collider col)
	{
	if(col.CompareTag("Enemy")){
	   this.ennemies = col.gameObject;
	}
	}

	void RestartLevel ()
	{
		this.GetComponent<MeshRenderer> ().enabled = false;
		this.GetComponent<TrailRenderer> ().enabled = false;//So that the player can't see the teleportation
		supervisor.playerDetected = false;
		supervisor.reset = true;
		this.transform.position = StartingPos;
		this.GetComponent<NavMeshAgent>().enabled = false;
		foreach (GameObject g in supervisor.agents) {
			g.GetComponent<Deplacement_NavMesh> ().index = 0;
			g.GetComponent<Deplacement_NavMesh> ().agent.SetDestination (g.GetComponent<Deplacement_NavMesh> ().ennemyPattern [g.GetComponent<Deplacement_NavMesh> ().index].position);

		}
		GameObject[] balises = GameObject.FindGameObjectsWithTag ("Balise");
		foreach (GameObject b in balises) {
			foreach (Collider other in b.GetComponents<BoxCollider>()) {
			   other.enabled = false;
			}
		}
		if (timeBeforeSpawning <= 0) {
			this.GetComponent<NavMeshAgent>().enabled = true;


			this.GetComponent<TrailRenderer> ().enabled = true;
			this.GetComponent<MeshRenderer> ().enabled = true;
			timeBeforeSpawning = timeBeforeSpawningAtStart;
			firstBalise.SetActive(true);
			foreach (Collider other in firstBalise.GetComponents<BoxCollider>()) {
			   other.enabled = true;
			}
			index = minIndex;
			lifePoint = 3;

		}
		else timeBeforeSpawning--;

	}

    }
