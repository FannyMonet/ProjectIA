using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusInstantiateur : MonoBehaviour {


    public bool isPlayer;
    public GameObject[] bonuses;
    public Transform[] spawners;

	// Use this for initialization
	void Start ()
	{
		for (int i = 0; i < spawners.Length; i++) {
			int rand = Random.Range (0, bonuses.Length);

			GameObject bonus = Instantiate (bonuses [rand], spawners [i].position, Quaternion.identity);
			if (!isPlayer) {
			    bonus.tag = "Bonus";
			}
		}
	}

}
