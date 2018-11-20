using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class NavMeshObstacle_Behaviour : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


    void OnTriggerStay(Collider col) {
        if(col.CompareTag("Player"))
        GetComponent<NavMeshObstacle>().enabled = false;
    }
}
