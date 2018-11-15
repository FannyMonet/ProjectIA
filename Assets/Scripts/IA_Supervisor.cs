using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IA_Supervisor : MonoBehaviour {

public GameObject player;
public string playerName;
public string playerPositionAtStart;
public GameObject[] agents;
public bool playerDetected;
public Material detectedMaterial;
public Material defaultMaterial;

public GameObject startingPosition;


	// Use this for initialization
	void Start () {
		player = GameObject.Find(playerName);
		startingPosition =GameObject.Find(playerPositionAtStart);
	}
	
	// Update is called once per frame
	void Update ()
	{
		//if the player is detected, every IA knows it
		if (playerDetected) {

				for (int i = 0; i < agents.Length; i++) {
					agents [i].GetComponent<MeshRenderer> ().material = detectedMaterial;
					agents [i].GetComponent<Deplacement_NavMesh> ().playerDetected = true;
					agents [i].GetComponent<TrailRenderer> ().startColor = Color.red;
					agents [i].GetComponent<LineRenderer> ().enabled = false;
				}

		}
	}
}
