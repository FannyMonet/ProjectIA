using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bottle_Behaviour : MonoBehaviour {

 
	public GameObject explosion;//the explosion particule
	public Vector3 target;//the position where the explosion append
	private int destroyingTimer;//the time before explosion
	public AudioClip clip;//Bottle breaking sound
	public AudioSource source;//Audio SOurce

	// Use this for initialization
	void Start () {
		destroyingTimer = 30;
		source = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update ()
	{
		destroyingTimer--;
		if (destroyingTimer == 0) {
			DestroyProjectile ();
		}
		}
	

	void DestroyProjectile ()
	{
		GameObject explosionPrefab = Instantiate (explosion, target, Quaternion.identity);
		Destroy(explosionPrefab,0.3f);
		source.PlayOneShot (clip);
		Destroy (gameObject, 0.5f);
	}


}
