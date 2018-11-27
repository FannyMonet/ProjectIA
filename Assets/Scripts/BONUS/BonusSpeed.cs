using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BonusSpeed : MonoBehaviour {

	public IA_Behaviour_Avoiding_Ennemies Regis;
	public Player_Movement player;


	// Use this for initialization
	void Start () {
		Regis = GameObject.Find("REGIS").GetComponent<IA_Behaviour_Avoiding_Ennemies>();
		player = GameObject.Find("PLAYER").GetComponent<Player_Movement>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}


	void OnTriggerEnter (Collider col)
	{
		if (col.CompareTag ("Player")) {
			if (col.name.Equals ("REGIS")) {
				player.speed = 0;
				player.timerBonus = 300;
				Regis.tryToGetBonus = false;
				Regis.bonus = null;
				DecrementScore("Regis");
				Destroy (gameObject);
			} else if (col.name.Equals ("PLAYER")) {
				Regis.agent.speed = 0;
				Regis.timerBonus = 300;
				DecrementScore("Player");

				Destroy (gameObject);
			}
		}
	}


	void DecrementScore (string name)
	{

		GameObject.Find("Point"+name).GetComponent<Text>().text = (int.Parse (GameObject.Find("Point"+name).GetComponent<Text>().text)-500).ToString();
		GameObject.Find("Point"+name+"Shadow").GetComponent<Text>().text = (int.Parse (GameObject.Find("Point"+name+"Shadow").GetComponent<Text>().text)-500).ToString();

	}
}
