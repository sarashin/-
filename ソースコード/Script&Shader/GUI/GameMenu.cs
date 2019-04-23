using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject Menu;
    [SerializeField]
    private GameObject StartText;
    private TextController Text;
    private bool TextFlg; 

    // Use this for initialization
    void Start ()
    {
        if (StartText != null)
            StartText.SetActive(true);
        if(Menu!=null)
            Menu.SetActive(false);
        TextFlg = false;
	}
	
	// Update is called once per frame
	void Update ()
    {
		if(Input.GetMouseButtonDown(0))
        {
            if (StartText != null)
                StartText.SetActive(false);
        }
	}

    //タイトルへ
    public void ToTitle()
    {
        GameSceneManager.InstanceSearch.ChangeScene("Title");
    }
    //ステージセレクトへ
    public void ToStageSelect()
    {
        GameSceneManager.InstanceSearch.ChangeScene("StageSelect");

    }
    //リトライ
    public void Retry()
    {
        GameSceneManager.InstanceSearch.ReLoadScene();
    }
    //ステージ移動
    public void NextStage(string Next)
    {
        AudioManager.InstanceSearch.PlaySE("decision22");
        GameSceneManager.InstanceSearch.ChangeScene(Next);
        ClearFlgManager.InstanceSearch.NextStageNum();
    }
    //メニュー表示
    public void OpenMenu()
    {
        if (Menu != null)
            Menu.SetActive(true);
    }
    //ポーズ
    public void PauseGame()
    {
        if (InputManager.InstanceSearch.IsPause() == false)
            InputManager.InstanceSearch.Pause(true);
        else
            InputManager.InstanceSearch.Pause(false);
    }
}
