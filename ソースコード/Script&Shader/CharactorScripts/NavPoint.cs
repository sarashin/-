using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[DefaultExecutionOrder(-20)]

//ナビゲーションのチェックポイントを管理するクラス
public class NavPoint : MonoBehaviour
{
    //次移動位置
    [SerializeField]
    public GameObject[] NavPoints;
    //次移動位置の座標を保持
    private NavPoint[] NavPos;
    //現在地
    private Vector3 Position;

    public bool IsGoal;

	// Use this for initialization
	void Start ()
    {
        NavPos = new NavPoint[NavPoints.Length];

        for (int i = 0; i < NavPos.Length; i++)
        {
            NavPos[i] = NavPoints[i].GetComponent<NavPoint>();
        }

        Position = transform.position;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    //次の目標点設定
    public NavPoint GetNextDirect(int Next)
    {
        if (Next < NavPos.Length)
        {
            return NavPos[Next];
        }
        else
        {
            return this;
        }
    }

    //目標点の座標を取得
    public NavPoint GetDirection(int Num)
    {
        if(Num>=NavPos.Length)
        {
            Num = 0;
        }

        return NavPos[Num];
    }

    //次の目標点の総数取得
    public int GetNavPosLength()
    {
        if (NavPos != null)
        {
            return NavPoints.Length;
        }

        return 0;
    }

    //この目標点の座標を取得
    public Vector3 GetPosition()
    {
        return Position;
    }
}
