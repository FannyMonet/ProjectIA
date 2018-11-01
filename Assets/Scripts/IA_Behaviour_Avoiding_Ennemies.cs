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

    // Use this for initialization
    void Start () {

        agent = this.GetComponent<NavMeshAgent>();
        maxSpeed = test;
		agent.speed = maxSpeed;
        agent.acceleration =test;


    }

	// Update is called once per frame
	void Update ()
	{
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
        agent.destination = this.player.transform.position;

    }

    void OnTriggerEnter (Collider col)
	{
	if(col.CompareTag("Enemy")){
	   this.ennemies = col.gameObject;
	}
	}



    }
