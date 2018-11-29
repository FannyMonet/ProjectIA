using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score_Supervisor : MonoBehaviour {

	// Use this for initialization
	public int scorePlayer;
	public int scoreRegis;
	void Start () {
		scorePlayer = GameObject.Find("PLAYER").GetComponent<Player_Movement>().score;
		scoreRegis = GameObject.Find("REGIS").GetComponent<IA_Behaviour_Avoiding_Ennemies>().score;
		DontDestroyOnLoad(this.gameObject);
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (GameObject.Find ("PLAYER") != null && GameObject.Find ("REGIS") != null) {
			scorePlayer = GameObject.Find ("PLAYER").GetComponent<Player_Movement> ().score;
			scoreRegis = GameObject.Find ("REGIS").GetComponent<IA_Behaviour_Avoiding_Ennemies> ().score;
		}
	}


}
