using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IA_Supervisor : MonoBehaviour {

public Player_Movement player;
public GameObject[] agents;
public bool playerDetected;
public Material detectedMaterial;


	// Use this for initialization
	void Start () {
		player = GameObject.Find("PLAYER").GetComponent<Player_Movement>();
	}
	
	// Update is called once per frame
	void Update ()
	{
	//if the player is detected, every IA knows it
		if (playerDetected) {
			for (int i = 0; i < agents.Length; i++) {
			   agents[i].GetComponent<MeshRenderer>().material = detectedMaterial;
			   agents[i].GetComponent<Deplacement_NavMesh>().playerDetected = true;
				agents[i].GetComponent<TrailRenderer>().startColor = Color.red;
				agents[i].GetComponent<LineRenderer>().enabled = false;

			}
		}
	}
}
