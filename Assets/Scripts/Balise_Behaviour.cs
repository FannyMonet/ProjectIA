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
		int i = safePoint;
		if (col.CompareTag ("Enemy")) {
			//Debug.Log("TOUCHE UN ENNEMI");
			if (Regis.agent.isActiveAndEnabled)//&& !Regis.tryToGetBonus)
				Regis.agent.destination = Regis.safePoints [i].position;
		} else if (col.CompareTag ("Player")) {
			//Debug.Log("TOUCHE UN Player");
			Regis.minIndex = safePoint + 1;
			//enabled = false;
			//Destroy (this);
			foreach (BoxCollider colider in this.GetComponents<BoxCollider>())
				colider.enabled = false;
			if (nextBalise != null) {

				foreach (BoxCollider colider in nextBalise.GetComponents<BoxCollider>())
					colider.enabled = true;
			}


			//this.gameObject.SetActive(false);
		}
	}

	void OnTriggerStay (Collider col)
	{
		int i = safePoint;
		if (col.CompareTag ("Enemy")) {
			//Debug.Log("TOUCHE UN ENNEMI");
			if(Regis.agent.isActiveAndEnabled)// && !Regis.tryToGetBonus)
			    Regis.agent.destination = Regis.safePoints [i].position;
		} else if (col.CompareTag ("Player")) {
			//Debug.Log("TOUCHE UN Player");
			Regis.minIndex = safePoint + 1;
			foreach(BoxCollider colider in this.GetComponents<BoxCollider>())
			    colider.enabled = false;
			if (nextBalise != null) {

				foreach (BoxCollider colider in nextBalise.GetComponents<BoxCollider>())
					colider.enabled = true;
			}
			//enabled = false;
			//Destroy (this);
			//this.enabled = false;

		}
	}

}
