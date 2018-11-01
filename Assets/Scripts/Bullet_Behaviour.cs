using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_Behaviour : MonoBehaviour {

    public float speed;
    private Vector3 target;
    public Transform targetPosition;
	public GameObject explosion;

	// Use this for initialization
	void Start () {
		targetPosition = GameObject.Find("PLAYER").transform;

		target = new Vector3(targetPosition.position.x,targetPosition.position.y,targetPosition.position.z);

	}
	
	// Update is called once per frame
	void Update () {
		transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
		if(transform.position.x == target.x && transform.position.y == target.y && transform.position.z == target.z){
		DestroyProjectile();
		}
	}

	void DestroyProjectile ()
	{
	Instantiate(explosion, transform.position, Quaternion.identity);
	Destroy(gameObject);
	}

	void OnTriggerEnter (Collider col)
	{
		if (!col.CompareTag ("Enemy")) {
			DestroyProjectile ();
		}
	}

	void OnTriggerStay (Collider col)
	{
		if (!col.CompareTag ("Enemy")) {
			DestroyProjectile ();
		}
	}
}
