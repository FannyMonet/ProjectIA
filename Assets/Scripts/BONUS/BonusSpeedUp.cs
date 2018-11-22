using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusSpeedUp : MonoBehaviour {

	public IA_Behaviour_Avoiding_Ennemies Regis;
	public Player_Movement player;


	// Use this for initialization
	void Start () {
		Regis = GameObject.Find("REGIS").GetComponent<IA_Behaviour_Avoiding_Ennemies>();
		player = GameObject.Find("PLAYER").GetComponent<Player_Movement>();
	}



	void OnTriggerEnter (Collider col)
	{
		if (col.CompareTag ("Player")) {
			if (col.name.Equals ("REGIS")) {
			    player.bonusSpeedUp = true;
				player.timerBonus = 300;
				Regis.tryToGetBonus = false;
				Destroy (gameObject);
			} else if (col.name.Equals ("PLAYER")) {
			    
				Regis.timerBonus = 300;
				Regis.agent.acceleration = 1000;
				Regis.agent.speed =1000;

				Destroy (gameObject);
			}
		}
	}
}