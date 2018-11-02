using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class transformBottle : MonoBehaviour {



    public LineRenderer lr;
	// Use this for initialization
	void Start () {
		lr = GameObject.Find("BottleTrajectory").GetComponent<LineRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
		this.transform.localPosition = new Vector3(lr.GetPosition(lr.positionCount-1).x,0, 0);
	}
}
