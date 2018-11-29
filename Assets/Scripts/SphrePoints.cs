using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SphrePoints : MonoBehaviour {

    private AudioSource audio;
    public bool took;
    public GameObject explosion;

	// Use this for initialization
	void Start () {
		audio = this.GetComponent<AudioSource>();
	}


	void OnTriggerEnter (Collider col)
	{
		if (col.CompareTag ("Player")) {
			this.GetComponent<MeshRenderer>().enabled = false;

			if (!took) {
				audio.Play ();

				if(col.name.Equals("PLAYER"))
				    col.GetComponent<Player_Movement>().score+=100;
				else 
				col.GetComponent<IA_Behaviour_Avoiding_Ennemies>().score += 100;


				took = true;
			}
			GameObject explosionPrefab = Instantiate (explosion, transform.position, Quaternion.identity, this.transform);
		    Destroy (gameObject,0.4f);
		}
	}
}
