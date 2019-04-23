using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleBGM : MonoBehaviour {

	// Use this for initialization
	void Start () {
        AudioManager.InstanceSearch.PlayBGM("Title まったりゆらゆら");
	}
}
