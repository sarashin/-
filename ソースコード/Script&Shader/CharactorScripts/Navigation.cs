using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
[DefaultExecutionOrder(-19)]

//ナビゲーションの動きを管理するクラス
public class Navigation : MonoBehaviour
{
    //目的地
    private Vector3     DistinationPoint;
    //次移動位置
    public int         Next;
    public int         Now;
    //現在地
    public NavPoint Distination;
    //はじめ位置
    public GameObject   FirstCheckPoint;
    //ナビゲーションUI
    public LineRenderer NavigationLine;
    Vector3[] Line;

    // Use this for initialization
    void Start ()
    {
        Line = new Vector3[3];
        Next = 0;
        Now = -1;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (NavigationLine != null)
        {
            Line[0] = transform.position;
            NavigationLine.SetPositions(Line);
        }
    }

    public void Initialize()
    {
        if (FirstCheckPoint)
            Distination = FirstCheckPoint.GetComponent<NavPoint>();
        SetDistination(Distination.GetPosition());
    }
    //次の位置へ移動
    public void MoveNextPosition()
    {
        if (Distination.IsGoal==false)
        {
            if (Now != Next)
            {

                Distination = Distination.GetNextDirect(Next);

                Next = 0;
                Now = -1;

                SetLinePos(Distination.GetDirection(Next).GetPosition(), Distination.GetDirection(Next).GetDirection(0).GetPosition());
                SetDistination(Distination.GetDirection(Next).GetPosition());
                
            }
            else
            {
                Next++;
                if (Next > Distination.GetNavPosLength())
                {
                    Next = 0;
                }

                SetLinePos(Distination.GetDirection(Next).GetPosition(), Distination.GetDirection(Next).GetDirection(0).GetPosition());
                SetDistination(Distination.GetDirection(Next).GetPosition());
                Debug.Log(Next);
            }
        }


    }

    //陰に当たったら前の位置へ戻る
    public void MoveBackPosition()
    {
        if (Now != Next)
        {
            Now = Next;

            if (Next > Distination.GetNavPosLength())
            {
                Next = 0;
            }

            SetLinePos(Distination.GetPosition(),Distination.GetDirection(Next+1).GetPosition());
            SetDistination(Distination.GetPosition());
        }
        else //戻ってくるときにまた影に当たったら
        {
            SetDistination(Distination.GetDirection(Next).GetPosition());            
            Now++;
            SetLinePos(Distination.GetDirection(Next).GetPosition(), Distination.GetDirection(Next).GetDirection(0).GetPosition());
        }

        Debug.Log("Hit!");
    }
    //目的地設定
    public void SetDistination(Vector3 Pos)
    {
        DistinationPoint = Pos;
        ParticleManager.InstanceSearch.PlayParticle("3D_Savepoint_01_Loop", Pos);
    }
    //現在地取得
    public Vector3 GetDistination()
    {
        return DistinationPoint;
    }
    //ナビゲーションライン描画
    private void SetLinePos(Vector3 Pos,Vector3 NextPos)
    {
        if (NavigationLine != null)
        {
            Line[0] = transform.position;
            Line[1] = Pos;
            Line[2] = NextPos;

            NavigationLine.positionCount = Line.Length;
            NavigationLine.SetPositions(Line);
        }
    }
    //ワープ地点かチェックポイントのタグ取得
    public string GetTag()
    {
        if (Next != Now)
        {
            return Distination.GetDirection(Next).tag;
        }
        else
        {
            return Distination.tag;
        }
    }
}
