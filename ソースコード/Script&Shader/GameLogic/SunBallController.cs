using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunBallController : MonoBehaviour {

    public float Speed;

    private Rigidbody RD;


	// Use this for initialization
	void Start ()
    {
        RD = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        // カーソルキーの入力を取得
        var moveHorizontal = Input.GetAxis(axisName: "Horizontal");
        var moveVertical = Input.GetAxis("Vertical");

        // カーソルキーの入力に合わせて移動方向を設定
        var movement = new Vector3(moveHorizontal, 0, z: moveVertical);

        // Ridigbody に力を与えて玉を動かす
        RD.AddForce(movement * Speed);
    }
}
