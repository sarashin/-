using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseButton : MonoBehaviour
{
    public GameObject PausePanel;

	// Use this for initialization
	void Start () {
        PausePanel.SetActive(false);
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void OpenPauseGUI()
    {
        if (PausePanel.activeSelf)
        {
            PausePanel.SetActive(false);
            InputManager.InstanceSearch.Pause(false);
        }
        else
        {
            PausePanel.SetActive(true);
            InputManager.InstanceSearch.Pause(true);
        }
    }
}
