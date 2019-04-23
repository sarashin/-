using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[DefaultExecutionOrder(-10)]

public class LightMove : MonoBehaviour
{
    //ジャイロ感度
    [SerializeField]
    [Range(0, 10)]
    public float GyroSensitivity;

    private Vector3 Default;
    public Light LightData;

    // Use this for initialization
    void Start()
    {
        SetDefault(new Vector3(0, 0, 0));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    //ジャイロの位置計算
    public void CalcGyroVec(Vector3 Gravity)
    {
        Vector3 GravityVec = (new Vector3(Gravity.x, 0, Gravity.y));
       
        float Accele = Vector3.Angle(Vector3.right, GravityVec);

        //Angleはマイナスを取らないため調整
        if(Vector3.Cross(Vector3.right, GravityVec).y>0)
        {
            Accele = 180 - Accele;
        }
        else
        {
            Accele -= 180;
        }
        //ラジアン角への変換
        float RadAccele = Mathf.PI / (180 / Accele);
        float Rad = -3;

        Vector3 GyroVec = new Vector3(Mathf.Cos(RadAccele) * Rad, transform.position.y, Mathf.Sin(RadAccele) * Rad);
        
        transform.position = GyroVec;
    }
    //ジャイロによる角度計算
    public void CalcGyroRot()
    {
        Vector3 Vec = transform.position * -1;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vec), 0.5f);
    }

    //値渡し用
    public Quaternion GetRotation()
    {
        return transform.rotation;
    }

    public Vector3 GetPosition()
    {
        return transform.position;
    }

    //デバッグ用ライト移動
    public void MovetoMouse(Vector2 MousePosition)
    {
        Vector3 Vec = Camera.main.ScreenToWorldPoint(MousePosition);
        Vec.y = transform.position.y;
        transform.position = Vec;
    }

    //オプションでの変更用
    public void SetSensitivity(float Sensitivity)
    {
        GyroSensitivity = Sensitivity;
    }
    //ニュートラルポジション設定用
    public void SetDefault(Vector3 Gravity)
    {
        Default = new Vector3(Gravity.x, 0, Gravity.y);
    }
    //ライトの色を変える
    public void ChangeLightColor(Color color)
    {
        LightData.color = color;
    }
}
