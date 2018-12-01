using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class FinalPointReached : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider col){
	    if(col.CompareTag("Player"))
	        if(col.name.Equals("REGIS")){
	           col.GetComponent<IA_Behaviour_Avoiding_Ennemies>().score +=500;
	        }
	        else if(col.name.Equals("PLAYER")){
				col.GetComponent<Player_Movement>().score +=500;

	        }
	        SceneManager.LoadScene(2);
	}
}
