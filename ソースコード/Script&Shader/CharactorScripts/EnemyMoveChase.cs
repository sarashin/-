using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMoveChase : MonoBehaviour
{
    public GameObject Player;
    NavMeshAgent NavAgent;

    public float TimeScale;

    bool IsHit;

	// Use this for initialization
	void Start ()
    {
        GameObject[] Players;
        Players = GameObject.FindGameObjectsWithTag("Player");

        Player = SeachPlayer(Players);

        NavAgent = GetComponent<NavMeshAgent>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        NavAgent.SetDestination(Player.transform.position);

        if(InputManager.InstanceSearch.IsDrag() || InputManager.InstanceSearch.IsPause())
        {
            NavAgent.speed = 0;
        }
        else if(InputManager.InstanceSearch.IsRelease() || InputManager.InstanceSearch.IsPause() == false)
        {
            NavAgent.speed = TimeScale;
        }
    }

    //最も近くのプレイヤーを探す
    GameObject SeachPlayer(GameObject[] Pos)
    {
        GameObject NearPos = Pos[0];
        float Dis = 100;

        for(int i=1;i<Pos.Length;i++)
        {
            float Near = Vector3.Distance(this.transform.position, Pos[i].transform.position);

            if (Near<Dis)
            {
                NearPos = Pos[i];
                Dis = Near;
            }
        }

        return NearPos;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Shadow")
        {
            NavAgent.speed = 0;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag=="Shadow")
        {
            NavAgent.speed = TimeScale;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && InputManager.InstanceSearch.IsPause() == false)
        {
            AudioManager.InstanceSearch.StopBGM();
            AudioManager.InstanceSearch.PlaySE("VegibiCruOut dog3");
            other.GetComponent<CharactorMove>().GameOver();
        }
    }
}
