using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMoveWarp : MonoBehaviour
{
    private Navigation Nav;
    private NavMeshAgent NavAgent;
    public NavPoint FirstCheckPoint;
    public float AgentSpeed;

    bool WarpFlg;
    int WarpNum;
    SkinnedMeshRenderer CharMesh;

    // Use this for initialization
    void Start () {
        Nav = this.GetComponent<Navigation>();
        NavAgent = this.GetComponent<NavMeshAgent>();
        Nav.Distination = FirstCheckPoint;
        Nav.Initialize();
        NavAgent.SetDestination(Nav.GetDistination());
        SetNavTime(0);
        CharMesh = GetComponentInChildren<SkinnedMeshRenderer>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (WarpFlg == true)
        {
            if (ParticleManager.InstanceSearch.IsPlaying("3D_MagicCircle_03" + WarpNum.ToString()) == false && InputManager.InstanceSearch.IsPause() == false)
            {
                Nav.MoveNextPosition();
                NavAgent.Warp(Nav.GetDistination());
                CharMesh.enabled = true;
                NavAgent.speed = AgentSpeed;
                WarpNum = 0;
                WarpFlg = false;
            }
        }
        else if (WarpFlg == false)
        {
            //ゲーム中の動き
            if (NavAgent.remainingDistance < 0.4)
            {
                if (Nav.GetTag() == "WarpPoint")
                    WarpPoint();
                else
                    NextPoint();
            }


            //ゲーム中の動き

            if (InputManager.InstanceSearch.IsDrag() || InputManager.InstanceSearch.IsPause())
            {
                NavAgent.speed = 0;
            }
            else if (InputManager.InstanceSearch.IsRelease() || InputManager.InstanceSearch.IsPause() == false)
            {
                NavAgent.speed = AgentSpeed;
            }
        }
    }

    private void OnGUI()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        //陰に当たった
        if (other.tag == "Shadow" && InputManager.InstanceSearch.IsPause() == false)
        {
            ReturnPoint();
        }
        else if(other.tag =="Player" && InputManager.InstanceSearch.IsPause() == false)
        {
            AudioManager.InstanceSearch.StopBGM();
            AudioManager.InstanceSearch.PlaySE("VegibiCruOut dog3");
            other.GetComponent<CharactorMove>().GameOver();
        }
    }

    public void NextPoint()
    {
        Nav.MoveNextPosition();
        NavAgent.SetDestination(Nav.GetDistination());
    }

    //前のチェックポイントへ戻る
    public void ReturnPoint()
    {
        if (NavAgent.speed > 0)
        {
            Nav.MoveBackPosition();
            NavAgent.SetDestination(Nav.GetDistination());
        }
    }

    //ワープ
    public void WarpPoint()
    {
        while (ParticleManager.InstanceSearch.IsPlaying("3D_MagicCircle_03" + WarpNum.ToString()))
        {
            WarpNum++;
        }

        ParticleManager.InstanceSearch.PlayParticle("3D_MagicCircle_03", Nav.GetDistination(), WarpNum);

        CharMesh.enabled = false;
        NavAgent.speed = 0;
        WarpFlg = true;
    }

    //ナビゲーションのスピード
    public void SetNavTime(float Scale)
    {
        NavAgent.speed = Scale;
    }
}
