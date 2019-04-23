using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightController : MonoBehaviour
{
    //ライトのデータを保持
    private Light LightData;
    public GameObject Sun;

    float Accele;
    public float Round;
    [SerializeField]
    [Range(1,10)]
    public float Afford;

    [SerializeField]
    [Range(1,10)]
    public float Speed;
    float DeltaTime;
    float ThisTime;

    //ライトの縦横比率
    float VerticalRad;
    float HorizonRad;

    public Color ShadowColor;

    // Use this for initialization
    void Start()
    {
        LightData = GetComponent<Light>();

#if UNITY_EDITOR
        MovetoMouse(Input.mousePosition);
#elif UNITY_ANDROID
        CalcGyroVec(InputManager.InstanceSearch.GyroCalc());
#endif
        if(Sun)
            Sun.SetActive(false);

        VerticalRad = 3;
        HorizonRad = 5;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnGUI()
    {
        if(InputManager.InstanceSearch.IsTouch())
        {
            if(Input.touchCount>2)
                AudioManager.InstanceSearch.PlaySE("SE_Switch_01_byOtogets_mp3");
        }

        if(InputManager.InstanceSearch.IsDrag())
        {
#if UNITY_EDITOR
            MovetoMouse(Input.mousePosition);
            ThisTime = Time.time-DeltaTime;
#elif UNITY_ANDROID
            CalcGyroVec(InputManager.InstanceSearch.GyroCalc());
#endif
            if (Sun)
                Sun.SetActive(true);
            CalcGyroRot();
            ChangeLightColor(ShadowColor);
        }
        else
        {
            DeltaTime = Time.time - ThisTime;
        }

        if(InputManager.InstanceSearch.IsRelease())
        {
            //指が画面から離れるとき
            AudioManager.InstanceSearch.PlaySE("");
            ChangeLightColor(Color.white);
            if (Sun)
                Sun.SetActive(false);
        }
    }

    //ジャイロで位置計算
    public void CalcGyroVec(Vector3 Gravity)
    {
        Vector3 GravityVec = new Vector3(-1*Gravity.x, 0, -1*Gravity.y);
        Vector3 NowVec = new Vector3(transform.position.x, 0, transform.position.z);
        float GravityAngle = Vector3.Angle(Vector3.right,GravityVec);

        //Angleはマイナスを取らないため調整
        if (Vector3.Cross(NowVec, GravityVec).y>0)
        {
            Accele = 180 - Vector3.Angle(NowVec, GravityVec);
        }
        else
        {
            Accele = Vector3.Angle(NowVec, GravityVec) - 180;
        }

        if (Mathf.Abs(Accele) > Afford)
        {
            float RotateSpeed;

            if (Accele > 0)
            {   
                RotateSpeed = Speed;
            }
            else
            {
                RotateSpeed = -Speed;
            }

            //ラジアン角への変換、移動。
            ThisTime = Time.time-DeltaTime;
            transform.position = new Vector3(Mathf.Cos((ThisTime) * RotateSpeed) * HorizonRad, transform.position.y, Mathf.Sin((ThisTime) * RotateSpeed) * VerticalRad);
        }
        else
        {
            DeltaTime = Time.time-ThisTime;
        }
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

    //ライトの色を変える
    public void ChangeLightColor(Color color)
    {
        LightData.color = color;
    }
}

