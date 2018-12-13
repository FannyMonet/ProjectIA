using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusSpeedUp : MonoBehaviour {

	public IA_Behaviour_Avoiding_Ennemies Regis;
	public Player_Movement player;
	public AudioSource audio;


	public string bonuseffect;

	// Use this for initialization
	void Start () {
	    audio = GetComponent<AudioSource>();
		Regis = GameObject.Find("REGIS").GetComponent<IA_Behaviour_Avoiding_Ennemies>();
		player = GameObject.Find("PLAYER").GetComponent<Player_Movement>();
		bonuseffect = "Virus detected, movement are uncontrollable !";


	}



	void OnTriggerEnter (Collider col)
	{
		if (col.CompareTag ("Player")) {
			audio.PlayOneShot(audio.clip);

			if (col.name.Equals ("REGIS")) {
			    player.bonusSpeedUp = true;
				player.timerBonus = 300;
				Regis.tryToGetBonus = false;
				Regis.bonus = null;
				player.bonusText.text = bonuseffect;

			} else if (col.name.Equals ("PLAYER")) {
			    
				Regis.timerBonus = 300;
				Regis.agent.acceleration = 1000;
				Regis.agent.speed =1000;
				Regis.bonusText.text = bonuseffect;

			}
			Destroy (gameObject,0.5f);

		}
	}
}