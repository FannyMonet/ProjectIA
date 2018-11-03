using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;


public class IA_Behaviour_Avoiding_Ennemies : MonoBehaviour {

	public NavMeshAgent agent;

    public GameObject ennemies;

    public int test;
    private float maxSpeed;

    public GameObject player;

    public int EnemyDistanceRun;

    public GameObject target;

    public int index;
    public int minIndex;

    public Transform[] safePoints;



		public GameObject destination;



    // Use this for initialization
    void Start () {

        agent = this.GetComponent<NavMeshAgent>();
        maxSpeed = test;
		agent.speed = maxSpeed;
        agent.acceleration =test;
        player = GameObject.Find("PLAYER");
		target = GameObject.Find("REGIS_GOAL");
		minIndex = index;

    }

	// Update is called once per frame
	void Update ()
	{
          	


		if (index <= minIndex) {
		index = minIndex;
		}


	//know if the agent is winning or not
		if (player.GetComponent<Player_Movement> ().remainingDistance < agent.remainingDistance) {
			Debug.Log ("Player distance :" +player.GetComponent<Player_Movement> ().remainingDistance +", Agent distance"+ agent.remainingDistance+ " Agent is LOOSING");
		} else {
			Debug.Log ("Player distance :" +player.GetComponent<Player_Movement> ().remainingDistance +", Agent distance"+ agent.remainingDistance+ " Agent is WINNING");

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
		if (!agent.hasPath) {
			//Debug.Log("agent hasn't path");
			index = (index + 1) % safePoints.Length;
			agent.destination = safePoints [index].position;
		} 


    }

    void OnTriggerEnter (Collider col)
	{
	if(col.CompareTag("Enemy")){
	   this.ennemies = col.gameObject;
	}
	}



    }
