using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSystem : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}

public void GameEnd()
{
#if UNITY_EDITOR
		UnityEditor.EditorApplication.isPlaying = false;
#elif UNITY_WEBPLAYER
		Application.OpenURL("http://www.yahoo.co.jp/");
#else
    Application.Quit();
#endif

}
	
	// Update is called once per frame
	void Update () {
		
	}
}
