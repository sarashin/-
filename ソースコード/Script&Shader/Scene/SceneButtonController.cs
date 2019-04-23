using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneButtonController : MonoBehaviour
{
    private void Start()
    {
    }

    public void MoveScene_Load()
    {
        GameSceneManager.InstanceSearch.ChangeScene("Load");
    }

    public void MoveScene_License()
    {
        
        GameSceneManager.InstanceSearch.ChangeScene("License");
    }

    public void MoveScene_StageSelect()
    {
        GameSceneManager.InstanceSearch.ChangeScene("StageSelect");
    }

    public void MoveScene_Title()
    {
        GameSceneManager.InstanceSearch.ChangeScene("Title");
    }

    public void MoveScene_Stage1()
    {
        GameSceneManager.InstanceSearch.ChangeScene("TestStage1");
    }

    public void MoveScene_TutorialStage1()
    {
        GameSceneManager.InstanceSearch.ChangeScene("TutorialStage1");
    }

    public void MoveScene_TutorialStage2()
    {
        GameSceneManager.InstanceSearch.ChangeScene("TutorialStage2");
    }

    public void MoveScene_TutorialStage3()
    {
        GameSceneManager.InstanceSearch.ChangeScene("TutorialStage3");
    }

    public void MoveScene_ScendStage1()
    {
        GameSceneManager.InstanceSearch.ChangeScene("SecondStage1");
    }

    public void MoveScene_ScendStage2()
    {
        GameSceneManager.InstanceSearch.ChangeScene("SecondStage2");
    }

    public void MoveScene_ScendStage3()
    {
        GameSceneManager.InstanceSearch.ChangeScene("SecondStage3");
    }

    public void PlayPushButton()
    {
        AudioManager.InstanceSearch.PlaySE("PushButton decision7");
    }

    public void PlayPushCancelButton()
    {
        AudioManager.InstanceSearch.PlaySE("BackButton decision15");
    }
}
