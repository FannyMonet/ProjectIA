using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IA_Behaviour : MonoBehaviour {

    public int speed;
    public Transform target;
    public GameObject player;
	// Use this for initialization
	void Start () {
		target = player.transform;
	}
	
	// Update is called once per frame
	void Update () {
		this.transform.position = Vector3.MoveTowards(this.transform.position, target.position, speed);
	}
}
