using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FinalScene : MonoBehaviour {

   public Text textEnd;
   public Score_Supervisor supervisor;

   public int result;

	// Use this for initialization
	void Start () {
		textEnd = GameObject.Find("Text").GetComponent<Text>();
		supervisor = GameObject.Find("SCORE").GetComponent<Score_Supervisor>();
		result = (supervisor.scorePlayer - supervisor.scoreRegis);
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (result >= 0) {
			textEnd.text = "You stole " + result + "MB of data."; 
		} else {
			textEnd.text = "Someone stole " + (-result) + "MB of data from you..."; 
		}

		if (Input.GetButtonDown ("Jump") || Input.GetButtonDown ("Fire1")) {
		    SceneManager.LoadScene(0);
		}
	}
}
