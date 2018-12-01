using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BonusSize : MonoBehaviour {

	public IA_Behaviour_Avoiding_Ennemies Regis;
	public Player_Movement player;

	public AudioSource audio;

	public string bonuseffect;


	// Use this for initialization
	void Start () {
	    audio = GetComponent<AudioSource>();
		Regis = GameObject.Find("REGIS").GetComponent<IA_Behaviour_Avoiding_Ennemies>();
		player = GameObject.Find("PLAYER").GetComponent<Player_Movement>();
		bonuseffect = "Virus detected, size increase";

	}



	void OnTriggerEnter (Collider col)
	{
		if (col.CompareTag ("Player")) {
			audio.PlayOneShot(audio.clip);

			if (col.name.Equals ("REGIS")) {
				player.gameObject.transform.localScale = new Vector3 (50, 50, 50);
				player.timerBonus = 300;

				Regis.tryToGetBonus = false;
				Regis.bonus = null;
				player.bonusText.text = bonuseffect;

			} else if (col.name.Equals ("PLAYER")) {
				Regis.gameObject.transform.localScale = new Vector3 (50, 50, 50);
				Regis.timerBonus = 300;
				Regis.bonusText.text = bonuseffect;


			}
			Destroy (gameObject,0.5f);

		}
	}
}
