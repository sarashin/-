using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectStageGUIController : MonoBehaviour {
//親GUIオブジェクト
    public GameObject[] StageGUI;

    // Use this for initialization
    void Start ()
    {
        StageGUI[0].SetActive(true);
        for(int i=1; i<StageGUI.Length;i++)
            StageGUI[i].SetActive(false);

        AudioManager.InstanceSearch.PlayBGM("StageSelectBGM のんびり行こうよ");
    }

    //ボタンの入力後GUIを切り替えるスクリプト。番号でGUIを指定 
    public void ChangeStageGUI(int Num)
    {
        AudioManager.InstanceSearch.PlaySE("");

        for(int i=0;i<StageGUI.Length;i++)
        {
            StageGUI[i].SetActive(false);
        }

        StageGUI[Num].SetActive(true);
    }
}
