using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_Behaviour : MonoBehaviour {

	public GameObject explosion;//the explosion particule
    public GameObject target;
    public int speed;
    public AudioSource audio;
    public AudioClip clip;

    public string cameraName;

    public GameObject mainCamera;
	// Use this for initialization
	void Start () {
	    //target = GameObject.Find("PLAYER");
	    //transform.rotation = Quaternion.LookRotation(target.transform.position);
	    audio = GetComponent<AudioSource>();
	    audio.PlayOneShot(clip);
	    mainCamera = GameObject.Find(cameraName);
	    mainCamera.GetComponent<Animator>().SetTrigger("shoot");
	   	}
	
	// Update is called once per frame
	void Update () {
		
		this.transform.position +=transform.forward*speed*2*Time.deltaTime;
	}


	void OnTriggerEnter (Collider col)
	{
		if (!col.CompareTag ("Enemy")) {
			if (col.CompareTag ("Player")) {
				if (col.GetComponent<Player_Movement> () != null)
					col.GetComponent<Player_Movement> ().lifePoint -= 1;
				else {
					col.GetComponent<IA_Behaviour_Avoiding_Ennemies> ().lifePoint -= 1;
				}
			}
			DestroyProjectile ();
		}
	}

	void DestroyProjectile ()
	{
		GameObject explosionPrefab = Instantiate (explosion, this.transform.position, Quaternion.identity, this.transform);
		Destroy (gameObject, 0.2f);
	}
}
