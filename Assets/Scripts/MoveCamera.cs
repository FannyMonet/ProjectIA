﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour {

    public Camera camera;

    public Vector3 pos_1;
    public Vector3 pos_2;

    public bool gateReached;

    public bool levelUp;
    public bool itsMe_Regis;
    public bool regisPassed;
    public string[] supervisorNames;
	public string[] baliseName;

	public int minIndex;

	public int bonusNumber;

	public int transitionBetween;

	public Vector3[] targets;

	public Transform[] ChangingTarget;


	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update ()
	{
		if (gateReached && !regisPassed) {
			camera.transform.position = (pos_2);

			Vector3 tmp = pos_2;
			pos_2 = pos_1;
			pos_1 = tmp;
			gateReached = false;
			if (itsMe_Regis) {
			    regisPassed = true;
			}
		}
	}


	void OnTriggerEnter (Collider col)
	{
		if (col.CompareTag ("Player")) {
            levelUp = !levelUp;

			if (col.name.Equals ("REGIS")) {
				itsMe_Regis = true;
				IA_Behaviour_Avoiding_Ennemies regis = col.GetComponent<IA_Behaviour_Avoiding_Ennemies> ();

				regis.bonusTook = false;
				regis.supervisor.playerDetected = false;
				regis.supervisor.reset = true;
				regis.bonus = regis.bonusSpawners[bonusNumber];
				if (regis.lifePoint > 0) {
					gateReached = true;

					if (levelUp) {
						regis.level = transitionBetween;
						if(ChangingTarget[1]!=null)
						    regis.target = ChangingTarget[1];
					    regis.agent.destination = targets[1];
						regis.supervisor = GameObject.Find (supervisorNames [1]).GetComponent<IA_Supervisor> ();
						regis.firstBalise = GameObject.Find (baliseName [1]);
						regis.StartingPos = new Vector3 (this.transform.position.x, this.transform.position.y, this.transform.position.z + 120);
						regis.minIndex = this.minIndex;

					} else {
						regis.target = ChangingTarget[0];

						regis.level = transitionBetween-1;

						regis.agent.destination = targets[0];

						regis.supervisor = GameObject.Find (supervisorNames [0]).GetComponent<IA_Supervisor> ();
						//regis.firstBalise = GameObject.Find (baliseName [0]);
						regis.StartingPos = new Vector3 (this.transform.position.x, this.transform.position.y, this.transform.position.z - 120);

					}
				}
				regis.supervisor.playerDetected = false;

				regis.supervisor.reset = true;

			}else {
				Player_Movement player = col.GetComponent<Player_Movement> ();
				player.supervisor.playerDetected = false;

				player.supervisor.reset = true;




				if (player.lifePoint >= 0) {
					gateReached = true;


					if (levelUp) {
					    player.level = transitionBetween;
					    player.target = ChangingTarget[1];
						player.agent.destination = targets[1];

						player.supervisor = GameObject.Find (supervisorNames [1]).GetComponent<IA_Supervisor> ();
						player.StartingPos = new Vector3 (this.transform.position.x, this.transform.position.y, this.transform.position.z + 120);

					} else {

						player.target = ChangingTarget[0];

						player.level = transitionBetween-1;

						player.agent.destination = targets[0];

						player.supervisor = GameObject.Find (supervisorNames [0]).GetComponent<IA_Supervisor> ();
						player.StartingPos = new Vector3 (this.transform.position.x, this.transform.position.y, this.transform.position.z - 120);

					}
				}
				player.supervisor.playerDetected = false;

				player.supervisor.reset = true;

		    }
		}
	}

	void OnTriggerExit (Collider col)
	{
		if (col.CompareTag ("Player")) {
		    
			if (col.transform.position.z > this.transform.position.z && !levelUp) {
				OnTriggerEnter (col);
			}
			else if (col.transform.position.z < this.transform.position.z && levelUp) {
				OnTriggerEnter (col);
			}
		}
	}
}
