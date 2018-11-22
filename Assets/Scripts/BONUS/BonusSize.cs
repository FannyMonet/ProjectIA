using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BonusSize : MonoBehaviour {

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
				player.gameObject.transform.localScale = new Vector3 (50, 50, 50);
				player.timerBonus = 300;
				Regis.tryToGetBonus = false;
				Destroy (gameObject);
			} else if (col.name.Equals ("PLAYER")) {
				Regis.gameObject.transform.localScale = new Vector3 (50, 50, 50);
				Regis.timerBonus = 300;

				Destroy (gameObject);
			}
		}
	}
}
