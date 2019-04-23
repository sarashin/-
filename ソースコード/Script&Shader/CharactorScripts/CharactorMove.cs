using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;
using UnityEngine.Animations;
[RequireComponent(typeof(Navigation))]
[RequireComponent(typeof(NavMeshAgent))]
[DefaultExecutionOrder(-18)]

//自キャラクターのクラス
public class CharactorMove : MonoBehaviour
{
    private Navigation Nav;
    private NavMeshAgent NavAgent;
    private Animator Anim;
    [SerializeField]
    public float AgentSpeed;

    private enum CharctorState
    {
        Start,
        Normal,
        Warp,
        GameOver
    };
    CharctorState State;

    [SerializeField]
    private GameObject Menu;

    AnimatorStateInfo info;

    SkinnedMeshRenderer CharMesh;
    BoxCollider CharCollider;

    int WarpNum;

    private void Start()
    {
        Nav = GetComponent<Navigation>();
        NavAgent = GetComponent<NavMeshAgent>();
        Anim = GetComponent<Animator>();
        CharCollider = GetComponent<BoxCollider>();
        Nav.Initialize();
        NavAgent.SetDestination(Nav.GetDistination());
        SetNavTime(0);
        Menu.SetActive(false);
        CharMesh = GetComponentInChildren<SkinnedMeshRenderer>();
        State = CharctorState.Start;
    }

    private void Update()
    {
        if (State==CharctorState.Start)
        {
            //ゲームスタート時チュートリアルがあれば開始
            StartCoroutine(WaitStart());
        }
        else if(State==CharctorState.Normal)
        {
            //ゲーム中の動き
            if (NavAgent.remainingDistance < NavAgent.radius)
            {
                if (Nav.GetTag() == "WarpPoint")
                {
                    WarpPoint();
                    State = CharctorState.Warp;
                }
                else
                {
                    CharMesh.enabled = true;
                    NavAgent.speed = AgentSpeed;
                    NextPoint();
                }
            }

            if (InputManager.InstanceSearch.IsDrag() || InputManager.InstanceSearch.IsPause())
            {
                SetNavTime(0);
            }
            else if(InputManager.InstanceSearch.IsRelease()||InputManager.InstanceSearch.IsPause()==false)
            {
                SetNavTime(AgentSpeed);
            }
        }
        else if(State == CharctorState.Warp&&InputManager.InstanceSearch.IsDrag()==false)
        {
            if (ParticleManager.InstanceSearch.IsPlaying("3D_MagicCircle_03" + WarpNum.ToString())==false)
            {
                Nav.MoveNextPosition();
                NavAgent.Warp(Nav.GetDistination());
                CharMesh.enabled = true;
                CharCollider.enabled = true;
                NavAgent.speed = AgentSpeed;
                State = CharctorState.Normal;
                WarpNum = 0;
                ParticleManager.InstanceSearch.PlayParticle("3D_MagicCircle_03", Nav.GetDistination());
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (State == CharctorState.Normal)
        {
            //陰に当たった
            if (other.tag == "Shadow")
            {
                ReturnPoint();
            }
        }
    }
    //次のチェックポイントへ進む
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
        while(ParticleManager.InstanceSearch.IsPlaying("3D_MagicCircle_03" + WarpNum.ToString()))
        {
            WarpNum++;
        }

        ParticleManager.InstanceSearch.PlayParticle("3D_MagicCircle_03", Nav.GetDistination() , WarpNum);
        CharMesh.enabled = false;
        CharCollider.enabled = false;
        NavAgent.speed = 0;
    }
    //ナビゲーションのスピード
    public void SetNavTime(float Scale)
    {
        NavAgent.speed = Scale;
    }
    //アニメーションの切り替え
    public void ChangeAnim(int num)
    {
        Anim.SetInteger("animation", num);
    }

    public void GameOver()
    {
        //Dieのアニメーション再生
        
        SetNavTime(0);
        State = CharctorState.GameOver;
        
        StartCoroutine(WaitEndEffect(transform.position));
        Debug.Log("Die!");
    }
    //ゲームスタートまで待機
    IEnumerator WaitStart()
    {
        while(InputManager.InstanceSearch.IsTouch()==false)
        {
            yield return null;
        }

        SetNavTime(AgentSpeed);
        State=CharctorState.Normal;
    }
    //ゲームオーバー時
    IEnumerator WaitEndEffect(Vector3 Pos)
    {
        yield return null;

        while (info.fullPathHash != Animator.StringToHash("Base Layer.Die") || info.normalizedTime < 1.0f)
        {
            ChangeAnim(6);
            info = Anim.GetCurrentAnimatorStateInfo(0);
            yield return null;
            Debug.Log(info.normalizedTime);
        }

        AudioManager.InstanceSearch.PlaySE("GameOver tin1");
        Menu.SetActive(true);
        InputManager.InstanceSearch.Pause(true);
    }
}
