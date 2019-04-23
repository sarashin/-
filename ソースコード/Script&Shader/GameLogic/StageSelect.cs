using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StageSelect : MonoBehaviour {

    public int StageNum;
    [SerializeField]
    private int TriggerStage;
    private Image SelectButton;

    public Sprite SelectableTex;
    public Sprite NotSelectableTex;

	// Use this for initialization
	void Start ()
    {
        SelectButton = GetComponent<Image>();

        if(ClearFlgManager.InstanceSearch.IsClear(TriggerStage))
        {
            SelectButton.sprite = SelectableTex;
        }
        else
        {
            SelectButton.sprite = NotSelectableTex;
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    //対応したシーンのロード前ステージが未クリアの場合失敗SEを鳴らす
    public void LoadStage(string StageName)
    {
        if (ClearFlgManager.InstanceSearch.IsClear(TriggerStage))
        {
            ClearFlgManager.InstanceSearch.SetStageNum(StageNum);
            AudioManager.InstanceSearch.PlaySE("PushButton decision7");
            GameSceneManager.InstanceSearch.ChangeScene(StageName);
        }
        else
        {
            AudioManager.InstanceSearch.PlaySE("PushButton decision3");
        }
    }
}
