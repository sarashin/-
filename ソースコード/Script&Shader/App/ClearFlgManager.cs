using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
[DefaultExecutionOrder(-20)]

public class ClearFlgManager : SingletonMonoBehavior<ClearFlgManager>
{
    [SerializeField]
    private bool[] ClearFlg;
    public int StageNum;

    private void Awake()
    {
        if (this != InstanceSearch)
        {
            Destroy(this);
            return;
        }

        DontDestroyOnLoad(this.gameObject);
        ClearFlg = new bool[SceneManager.sceneCountInBuildSettings];
        StageNum = 1;

    }

    // Use this for initialization
    void Start ()
    {
        for (int i=1;i<ClearFlg.Length;i++)
        {
            ClearFlg[i] = false;
        }

        for(int i=0; i<PlayerPrefs.GetInt("ClearFlg");i++)
        {
            ClearFlg[i] = true;
        }

        ClearFlg[0] = true;
    }

    // Update is called once per frame
    void Update () {
		
	}
    //クリア時
    public void StageClear()
    {
        ClearFlg[StageNum] = true;
        PlayerPrefs.SetInt("ClearFlg", StageNum);
    }

    //クリア済みか
    public bool IsClear(int Num)
    {
        return ClearFlg[Num];
    }

    //データ削除
    public void DataClear()
    {
        for(int i=0;i<ClearFlg.Length;i++)
        {
            ClearFlg[i] = false;
        }

        PlayerPrefs.DeleteAll();
        PlayerPrefs.SetInt("ClearFlg", 0);
    }

    public void SetStageNum(int Num)
    {
        StageNum = Num;
    }

    public void NextStageNum()
    {
        StageNum++;
    }
}
