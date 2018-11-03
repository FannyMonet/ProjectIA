using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Balise_Behaviour : MonoBehaviour {

public IA_Behaviour_Avoiding_Ennemies Regis;
public int safePoint;

public GameObject nextBalise;

	// Use this for initialization
	void Start () {
		Regis = GameObject.Find("REGIS").GetComponent<IA_Behaviour_Avoiding_Ennemies>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}


	void OnTriggerEnter (Collider col)
	{
		int i;
		if (col.CompareTag ("Enemy")) {
		    //Debug.Log("TOUCHE UN ENNEMI");
				i = safePoint;
			Regis.agent.destination = Regis.safePoints[i].position;
		} else if (col.CompareTag ("Player")) {
			//Debug.Log("TOUCHE UN Player");
			Regis.minIndex = safePoint+1;
			Destroy (this);
		}
	}

	void OnTriggerStay (Collider col)
	{
		int i = safePoint;
		;
		if (col.CompareTag ("Enemy")) {
			//Debug.Log("TOUCHE UN ENNEMI");
			Regis.agent.destination = Regis.safePoints [i].position;
		} else if (col.CompareTag ("Player")) {
			//Debug.Log("TOUCHE UN Player");
			Regis.minIndex = safePoint + 1;
			if (nextBalise != null) {
				nextBalise.SetActive (true);
			}
			Destroy (this);
		}
	}

}
