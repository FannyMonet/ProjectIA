using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;


public class IA_Behaviour_Avoiding_Ennemies : MonoBehaviour {

	private NavMeshAgent agent;

    public GameObject ennemies;

    public int test;
    private float maxSpeed;

    public GameObject player;

    public int EnemyDistanceRun;

    public GameObject target;

    // Use this for initialization
    void Start () {

        agent = this.GetComponent<NavMeshAgent>();
        maxSpeed = test;
		agent.speed = maxSpeed;
        agent.acceleration =test;
        player = GameObject.Find("PLAYER");
		target = GameObject.Find("REGIS_GOAL");

    }

	// Update is called once per frame
	void Update ()
	{
		if (player.GetComponent<Player_Movement> ().remainingDistance < agent.remainingDistance) {
			Debug.Log ("Player distance :" +player.GetComponent<Player_Movement> ().remainingDistance +", Agent distance"+ agent.remainingDistance+ " Agent is LOOSING");
		} else {
			Debug.Log ("Player distance :" +player.GetComponent<Player_Movement> ().remainingDistance +", Agent distance"+ agent.remainingDistance+ " Agent is WINNING");

		}
		float distance = Vector3.Distance (transform.position, ennemies.transform.position);
		if (distance < EnemyDistanceRun) {
			Vector3 dirToEnnemy = transform.position - ennemies.transform.position;
			Vector3 newPos = transform.position + dirToEnnemy;
			agent.destination =newPos;

		} else {
			attackPlayer(); 

		}


          //  
  }



    private void attackPlayer()
    {
        agent.destination = this.target.transform.position;

    }

    void OnTriggerEnter (Collider col)
	{
	if(col.CompareTag("Enemy")){
	   this.ennemies = col.gameObject;
	}
	}



    }
