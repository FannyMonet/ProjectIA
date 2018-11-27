using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BonusCrazySentinelle : MonoBehaviour
{

    public IA_Behaviour_Avoiding_Ennemies Regis;
    public Player_Movement player;


    // Use this for initialization
    void Start()
    {
        Regis = GameObject.Find("REGIS").GetComponent<IA_Behaviour_Avoiding_Ennemies>();
        player = GameObject.Find("PLAYER").GetComponent<Player_Movement>();

    }



    void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Player"))
        {
            if (col.name.Equals("REGIS"))
            {
                foreach (GameObject agent in player.supervisor.agents)
                {
                    agent.GetComponent<NavMeshAgent>().speed = 300;
                    agent.GetComponent<NavMeshAgent>().acceleration = 300;
                }
                player.timerBonus = 300;
                Regis.tryToGetBonus = false;
                Regis.bonus = null;
                Destroy(gameObject);
            }
            else if (col.name.Equals("PLAYER"))
            {
                Regis.timerBonus = 300;
                foreach (GameObject agent in Regis.supervisor.agents)
                {
                    agent.GetComponent<NavMeshAgent>().speed = 300;
                    agent.GetComponent<NavMeshAgent>().acceleration = 300;
                }
                Destroy(gameObject);
            }
        }
    }
}
