using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScenePanelController : MonoBehaviour
{
    public GameObject GameClearPanel;
    public GameObject GameOverPanel;


    // Use this for initialization
    void Start ()
    {
        GameClearPanel.SetActive(false);
        GameOverPanel.SetActive(false);
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void OnGameClearPanel()
    {
        GameClearPanel.SetActive(true);

    }

    public void OnGameOverPanel()
    {
        GameOverPanel.SetActive(true);
    }
}
