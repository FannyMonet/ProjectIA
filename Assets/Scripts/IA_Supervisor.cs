using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IA_Supervisor : MonoBehaviour {

public GameObject player;
public string playerName;
public string playerPositionAtStart;
public GameObject[] agents;
public bool playerDetected;
public bool reset;

	public Light light;//Light that will change if a player is seen
	public Color color0 = Color.red;//Color when IA detect player
    public Color color1 = Color.blue;//Default color
	public float duration = 1.0F;


public Material detectedMaterial;
public Material defaultMaterial;

public GameObject startingPosition;


	// Use this for initialization
	void Start () {
		player = GameObject.Find(playerName);
		startingPosition =GameObject.Find(playerPositionAtStart);
		light = GameObject.Find("LIGHT").GetComponent<Light>();
	}
	
	// Update is called once per frame
	void Update ()
	{
		//if the player is detected, every IA knows it
		if (playerDetected) {
		Debug.Log("Player Detected!");
			float t = Mathf.PingPong (Time.time, duration) / duration;
			light.color = Color.Lerp (color0, color1, t);
			for (int i = 0; i < agents.Length; i++) {
				agents [i].GetComponent<MeshRenderer> ().material = detectedMaterial;
				agents [i].GetComponent<Deplacement_NavMesh> ().playerDetected = true;
				agents [i].GetComponent<TrailRenderer> ().startColor = color0;
				agents [i].GetComponent<LineRenderer> ().enabled = false;
			}

		} else if (reset) {
			light.color = color1;
			for (int i = 0; i < agents.Length; i++) {
				agents[i].GetComponent<TrailRenderer>().enabled = false;

				agents[i].transform.position = agents[i].GetComponent<Deplacement_NavMesh>().startingPosition;
			    agents[i].GetComponent<Deplacement_NavMesh>().index =0;
				agents[i].GetComponent<Deplacement_NavMesh>().moveToPoint(true);

				agents [i].GetComponent<MeshRenderer> ().material = defaultMaterial;
				agents [i].GetComponent<Deplacement_NavMesh> ().playerDetected = false;
				agents [i].GetComponent<TrailRenderer> ().startColor = color1;
				agents [i].GetComponent<LineRenderer> ().enabled = true;


			}
			reset = false;
		}
	}
}
