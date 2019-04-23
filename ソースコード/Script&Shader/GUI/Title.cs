using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Title : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void LoadScene(string Name)
    {
        AudioManager.InstanceSearch.PlaySE("PushButton decision7");
        GameSceneManager.InstanceSearch.ChangeScene(Name);
    }

    public void DataClear()
    {
        ClearFlgManager.InstanceSearch.DataClear();
        LoadScene("Title");
    }
}
