using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[DefaultExecutionOrder(-9)]

public class Shadow : MonoBehaviour
{
    //映されるライトの情報
    public GameObject LightObject;
    private LightController LightMoveData;
    private Quaternion LightRot;

    //映すオブジェクトの高さ
    public float ObjectHeight;
    //基準の位置
    public Vector3 Neutral;

    //影倍率
    public float Magnification;
    
	// Use this for initialization
	void Start ()
    {
        LightMoveData = LightObject.GetComponent<LightController>();
        Neutral = transform.position;
        ShadowMove();
    }
	
	// Update is called once per frame
	void Update ()
    {
        if(!gameObject.activeSelf&&InputManager.InstanceSearch.TextFlg==false)
        {
            gameObject.SetActive(true);
        }

        ShadowMove();

    }

    //影の移動
    void ShadowMove()
    {
        Vector3 ShadowPos,ShadowVec;
        ShadowPos = LightMoveData.GetPosition();
        ShadowPos = Vector3.Normalize(ShadowPos);
        ShadowVec = ShadowPos;
        ShadowVec.y = 0;
        ShadowPos *= -1;
        ShadowPos.y = ObjectHeight;    //y座標の設定

        //影自体の角度
        float ShadowAngle;

        if (Vector3.Cross(Vector3.right, ShadowVec).y > 0)
        {
            ShadowAngle = 90 - Vector3.Angle(Vector3.right, ShadowVec);
        }
        else
        {
            ShadowAngle = Vector3.Angle(Vector3.right, ShadowVec) - 90;
        }

        transform.rotation =  Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(ShadowVec * -1), 1.0f)*Quaternion.AngleAxis(ShadowAngle, Vector3.forward); 

        //float angle;
       // angle = Mathf.PI / (180 / ShadowAngle);

        transform.position = Neutral + (Magnification * ShadowPos);
    }

    //高さのセット
    public void SetHeight(float Height)
    {
        ObjectHeight = Height;
    }
}
