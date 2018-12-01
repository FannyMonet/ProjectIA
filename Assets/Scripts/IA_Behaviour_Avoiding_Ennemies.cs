using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
//using UnityEngine.PostProcessing;



public class IA_Behaviour_Avoiding_Ennemies : MonoBehaviour {

	public NavMeshAgent agent;

	public Text[] texts;

	public Text bonusText;

    public GameObject ennemies;

    public int lifePoint = 3;

    public int test;
    private float maxSpeed;

    public GameObject player;

    public int EnemyDistanceRun;

    public Transform target;

	public GameObject bonus;


    public int index;
    public int minIndex;

    public Transform[] safePoints;

    public bool tryToGetBonus;

    public bool isWinning;


		public GameObject objectif;

		public Vector3 StartingPos;


	private int timeBeforeSpawning;
	public int timeBeforeSpawningAtStart;

	public IA_Supervisor supervisor;

	public GameObject firstBalise;

	public int timerBonus;

	public int indexBonusMax;
	public GameObject[] bonusSpawners;

	public int acceleration;

    public int speed;

    public int angleSpeed;

	public int level;

	public int score;

	public bool bonusTook;//to know if the IA took the bonus of the level



    // Use this for initialization
    void Start () {
    StartingPos = this.transform.position;
        agent = this.GetComponent<NavMeshAgent>();
        maxSpeed = test;
		agent.speed = speed;
        agent.acceleration =acceleration;
        agent.angularSpeed = angleSpeed;
        player = GameObject.Find("PLAYER");
		target = GameObject.Find("REGIS_GOAL_1").transform;
		bonus = bonusSpawners[0];

		minIndex = index;

		timeBeforeSpawning = timeBeforeSpawningAtStart;

		supervisor = GameObject.Find("IA_SUPERVISOR REGIS L1").GetComponent<IA_Supervisor>();

		bonusText = GameObject.Find("BonusTextR").GetComponent<Text>();



    }

	// Update is called once per frame
	void Update ()
	{
          	
		foreach (Text text in texts) {
			text.text = score.ToString ();
		}
		if (lifePoint <= 0) {
			CancelBonus ();
			RestartLevel ();
			return;
		}

		//Bonus Timer
		if (timerBonus >= 0) {
			if (timerBonus == 0) {
				CancelBonus ();
		
			}
			timerBonus--;
		} else {
		bonusText.text = "";
		}
		

		float distancePlayer = Vector3.Distance(player.transform.position, player.GetComponent<Player_Movement> ().target.position) + player.GetComponent<Player_Movement> ().level *-1000;
		float distanceRegis = Vector3.Distance( this.transform.position, this.target.transform.position) + level*-1000;
		//know if the agent is winning or not
		if (distanceRegis >distancePlayer)
		{ //&& minIndex< indexBonusMax) {
			Debug.Log ("Player distance :" + distancePlayer + ", Agent distance" + distanceRegis + " Agent is LOSING");
			isWinning = false;
			if (bonus != null && !bonusTook && (minIndex == 1|| minIndex ==4||minIndex == 6)) {
				tryToGetBonus = true;
			}
			
		} else {
		    isWinning = true;
			Debug.Log ("Player distance :" + distancePlayer +", Agent distance"+ distanceRegis+ " Agent is WINNING");
			tryToGetBonus = false;


		}
		float distance = Vector3.Distance (transform.position, ennemies.transform.position);
		//if the agent is close to ennemy, it avoid them instead of trying to reach the end of the level
		//if (distance < EnemyDistanceRun) {
		//Debug.Log("EnnemyDistance RUN!!");
		//	Vector3 dirToEnnemy = transform.position - ennemies.transform.position;
		//	Vector3 newPos = transform.position + dirToEnnemy;
		//	agent.destination =newPos;

	//	} else {
			   moveToPoint();

	//	}


  }


  //set the movement of the agent to reach the target
    private void reachDestination ()
	{

		agent.destination = this.target.transform.position;
    }

	private void moveToPoint ()
	{
		if (tryToGetBonus) {
			if (bonus != null) {
				agent.destination = bonus.transform.position;
			}
		} else if (agent.remainingDistance < 50) {
			if (index == safePoints.Length) {
			    return;
			}
			//Debug.Log("agent hasn't path");
			index = (index + 1) % safePoints.Length;
			if (index <= minIndex) {
				index = minIndex;
			}
			agent.destination = safePoints[index].position;
		} 
		else agent.destination = safePoints[index].position;


    }

    void OnTriggerEnter (Collider col)
	{
		if (col.CompareTag ("Enemy")) {
			this.ennemies = col.gameObject;
		} else if (col.CompareTag ("Bonus")) {
			bonusTook = true;
		}
	}

	void RestartLevel ()
	{
		this.GetComponent<MeshRenderer> ().enabled = false;
		this.GetComponent<TrailRenderer> ().enabled = false;//So that the player can't see the teleportation
		supervisor.playerDetected = false;
		supervisor.reset = true;
		tryToGetBonus = false;
		this.transform.position = StartingPos;
		this.GetComponent<NavMeshAgent> ().enabled = false;
		foreach (GameObject g in supervisor.agents) {
			g.GetComponent<Deplacement_NavMesh> ().index = 0;
			g.GetComponent<Deplacement_NavMesh> ().agent.SetDestination (g.GetComponent<Deplacement_NavMesh> ().ennemyPattern [g.GetComponent<Deplacement_NavMesh> ().index].position);

		}
		GameObject[] balises = GameObject.FindGameObjectsWithTag ("Balise");
		foreach (GameObject b in balises) {
			foreach (Collider other in b.GetComponents<BoxCollider>()) {
				other.enabled = false;
			}
		}
		if (timeBeforeSpawning <= 0) {
			this.GetComponent<NavMeshAgent> ().enabled = true;


			this.GetComponent<TrailRenderer> ().enabled = true;
			this.GetComponent<MeshRenderer> ().enabled = true;
			timeBeforeSpawning = timeBeforeSpawningAtStart;
			firstBalise.SetActive (true);
			foreach (Collider other in firstBalise.GetComponents<BoxCollider>()) {
				other.enabled = true;
			}
			if (!bonusTook) {
			    bonus = bonusSpawners[level];
			}
			minIndex = firstBalise.GetComponent<Balise_Behaviour>().safePoint;
			index = minIndex;
			lifePoint = 3;
			CancelBonus();

		}
		else timeBeforeSpawning--;

	}

	void CancelBonus ()
	{
			
			this.transform.localScale = new Vector3 (25, 25, 25);
			this.agent.speed = test;
		    this.agent.acceleration = test;
		    foreach(GameObject agent in this.supervisor.agents)
                {
                    agent.GetComponent<Deplacement_NavMesh>().distance = 200;
                    agent.GetComponent<Deplacement_NavMesh>().VisionArea = 0.26f;
                    agent.GetComponent<LineRenderer>().endWidth = 100;
                    agent.GetComponent<NavMeshAgent>().speed = 150;
			        agent.GetComponent<NavMeshAgent>().acceleration = 150;

                }
	}

    }
