using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMove : MonoBehaviour
{
    [SerializeField]
    private List<NavPoint> CheckPoints;
    private NavMeshAgent NavAgent;
    [SerializeField]
    private int CheckPointNum;
    public float TimeScale;

    public bool ReturnFlg;

	// Use this for initialization
	void Start ()
    {
        NavAgent = GetComponent<NavMeshAgent>();
        NavAgent.speed = TimeScale;
        CheckPointNum = 0;
        NavAgent.SetDestination(CheckPoints[CheckPointNum].GetPosition());
    }

    private void OnGUI()
    {
        //ゲーム中の動き
        if(InputManager.InstanceSearch.IsDrag() || InputManager.InstanceSearch.IsPause())
        {
            NavAgent.speed = 0;
        }
        else if(InputManager.InstanceSearch.IsRelease()|| InputManager.InstanceSearch.IsPause()==false)
        {
            NavAgent.speed = TimeScale;
        }
    }

    // Update is called once per frame
    void Update () {
        if (NavAgent.remainingDistance < 0.4)
        {
            NavgationChange();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Shadow" && InputManager.InstanceSearch.IsPause() == false)
        {
            if (ReturnFlg)
                ReturnFlg = false;
            else
                ReturnFlg = true;

            NavgationChange();
        }
        else if (other.tag == "Player" && InputManager.InstanceSearch.IsPause() == false)
        {
            AudioManager.InstanceSearch.StopBGM();
            AudioManager.InstanceSearch.PlaySE("VegibiCruOut dog3");
            other.GetComponent<CharactorMove>().GameOver();
        }
    }

    //巡回
    private void NavgationChange()
    {
        if (NavAgent.speed > 0)
        {
            if (ReturnFlg)
            {
                CheckPointNum++;

                if (CheckPointNum >= CheckPoints.Count)
                {
                    CheckPointNum = 0;
                }
            }
            else
            {
                CheckPointNum--;

                if (CheckPointNum < 0)
                {
                    CheckPointNum = CheckPoints.Count - 1;
                }
            }

            NavAgent.SetDestination(CheckPoints[CheckPointNum].GetPosition());
        }
    }
}
