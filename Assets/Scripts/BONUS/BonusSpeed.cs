using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BonusSpeed : MonoBehaviour {

	public IA_Behaviour_Avoiding_Ennemies Regis;
	public Player_Movement player;
	public AudioSource audio;

	public string bonuseffect;


	// Use this for initialization
	void Start () {
	    audio = GetComponent<AudioSource>();
		Regis = GameObject.Find("REGIS").GetComponent<IA_Behaviour_Avoiding_Ennemies>();
		player = GameObject.Find("PLAYER").GetComponent<Player_Movement>();
		bonuseffect = "Virus detected, anti-viruses agents speed increase";

	}
	
	// Update is called once per frame
	void Update () {
		
	}


	void OnTriggerEnter (Collider col)
	{
		if (col.CompareTag ("Player")) {
		   audio.PlayOneShot(audio.clip);
			if (col.name.Equals ("REGIS")) {
				player.speed = 0;
				player.timerBonus = 300;
				Regis.tryToGetBonus = false;
				Regis.bonus = null;
				DecrementScore("Regis");
				player.bonusText.text = bonuseffect;

			} else if (col.name.Equals ("PLAYER")) {
				Regis.agent.speed = 0;
				Regis.timerBonus = 300;
				DecrementScore("Player");
				Regis.bonusText.text = bonuseffect;


			}
			Destroy (gameObject, 0.5f);

		}
	}


	void DecrementScore (string name)
	{

		GameObject.Find("Point"+name).GetComponent<Text>().text = (int.Parse (GameObject.Find("Point"+name).GetComponent<Text>().text)-500).ToString();
		GameObject.Find("Point"+name+"Shadow").GetComponent<Text>().text = (int.Parse (GameObject.Find("Point"+name+"Shadow").GetComponent<Text>().text)-500).ToString();

	}
}
