using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class bonusBehaviour : MonoBehaviour {

	public IA_Behaviour_Avoiding_Ennemies Regis;
	public GameObject player;


	// Use this for initialization
	void Start () {
		Regis = GameObject.Find("REGIS").GetComponent<IA_Behaviour_Avoiding_Ennemies>();
		player = GameObject.Find("PLAYER");
	}
	
	// Update is called once per frame
	void Update () {
		
	}


	void OnTriggerEnter (Collider col)
	{
		if (col.CompareTag ("Player")) {
		   player.transform.localScale = new Vector3(50,50,50);
		   Regis.tryToGetBonus = false;
		   Destroy(gameObject);
		}
	}
}
