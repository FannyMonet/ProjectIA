using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SphrePoints : MonoBehaviour {

    public Text[] points;
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
				int score = int.Parse (points [0].text.ToString ());
				score += 100;
				for (int i = 0; i < points.Length; i++) {

					points [i].text = score.ToString ();
				}
				took = true;
			}
			GameObject explosionPrefab = Instantiate (explosion, transform.position, Quaternion.identity, this.transform);
		    Destroy (gameObject,0.3f);
		}
	}
}
