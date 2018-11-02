using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BottleArcRenderer : MonoBehaviour {

    private LineRenderer lr;
    public float velocity;
    public float angle;
    public int resolution = 10;

    private float gravity;
    private float radianAngle;
	// Use this for initialization
	void Awake () {
		lr = GetComponent<LineRenderer>();
		gravity = Mathf.Abs(Physics.gravity.y);
	}

	void Start ()
	{
	RenderArc();
	}

	void Update(){
		RenderArc();
	}


	void RenderArc () {
		lr.SetVertexCount(resolution + 1);
		lr.SetPositions(CalculateArcArray());
	}

	//create array of vector3 positions for arc
	Vector3[] CalculateArcArray ()
	{
		Vector3[] arcArray = new Vector3[resolution + 1];

		radianAngle = Mathf.Deg2Rad * angle;
		float maxDistance = (velocity * velocity * Mathf.Sin (2 * radianAngle)) / gravity;

		for (int i = 0; i <= resolution; i++) {

			float t = (float)i / (float)resolution;
			arcArray [i] = CalculateArcPoint (t, maxDistance);
		}

		return arcArray;
	}
	//calculate heigth and vertex of each vertex
	Vector3 CalculateArcPoint (float t, float maxDistance)
	{
		float x = t * maxDistance;
		float z = t * maxDistance;
		float y = x * Mathf.Tan (radianAngle) - ((gravity * x * x) / (2 * velocity * velocity * Mathf.Cos (radianAngle) * Mathf.Cos (radianAngle)));
		return new Vector3(x,y,0);
	}
}
