using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class InputManager : SingletonMonoBehavior<InputManager>
{
    //入力状態
    private bool    Doragging;
    private bool    Doragged;
    private bool    Tap;
    private bool    Release;

    //ジャイロ関連
    //操作感度
    [SerializeField]
    [Range(0.1f, 3)]
    public float GyroSensitivity;
    private Vector3 GyroDefault;

    //4方向
    static float Norse;
    static float Souse;
    static float West;
    static float East;
    //[SerializeField]
    //private GameObject AnnouncedStart;
    public TextController Text;

    public bool TextFlg;
    bool PauseFlg;
    [SerializeField]
    public int SwitchCount;

    public string BGMName;

    bool OneTouch;

    private void Awake()
    {
        if(this　!=　InstanceSearch)
        {
            Destroy(this);
            return;
        }

        
    }

    // Use this for initialization
    void Start ()
    {
#if UNITY_ANDROID
        Input.gyro.enabled = true;
# endif

        PauseFlg = false;
        TextFlg = true;
        PauseFlg = false;
        AudioManager.InstanceSearch.PlayBGM(BGMName);
    }

    // Update is called once per frame
    void Update ()
    {

    }

    private void OnGUI()
    {
        if (GameSceneManager.InstanceSearch.IsLoading==false)
        {
            if (!TextFlg&&IsPause() == false)
            {
                if (SwitchCount > 0)
                {
                    InputCheck();
                }
            }
            else if(TextFlg)
            {
                if(Text == null || Text.TextControll())
                {
                    if(Text!=null)
                        Text.TextBoxClear();

                    TextFlg = false;
                }
            }
        }
    }
    //入力情報取得
    void InputCheck()
    {
#if UNITY_EDITOR

        if (Event.current.type==EventType.MouseDown)
        {
            OneTouch = true;
        }
        else
        {
            OneTouch = false;
        }

        if (Event.current.type == EventType.MouseDrag)
        {
            Doragging = true;
            Doragged = true;
        }
        else
        {
            if (Event.current.type == EventType.MouseUp)
            {
                if (!Doragged)
                {
                    Tap = true;
                }
                else
                {
                    Doragged = false;
                }

                Release = true;
                Doragging = false;
                SwitchCount--;
            }
        }

#elif UNITY_ANDROID

        if(Event.current.type==EventType.MouseDown)
        {
            OneTouch = true;
            SetNeutral();
        }
        else
        {
            OneTouch = false;
        }

        if (Event.current.type == EventType.MouseDrag)
        {
            if (Input.touchCount > 1)        //入力状態の遷移
            {
                Doragging = true;
                Doragged = true;
            }
        }
        else if (Event.current.type == EventType.MouseUp)
        {
            if (!Doragged)
            {
                Tap = true;
            }
            else
            {
                Doragged = false;
            }

            Release = true;
            Doragging = false;

            SwitchCount--;
        }
        
#endif
    }
    //タップ情報リセット
    private void FixedUpdate()
    {
        Tap = false;
        Release = false;
    }

    //取得用
    public bool IsRelease()
    {
        return Release;
    }

    public bool IsDrag()
    {
        return Doragging;
    }

    public bool IsTap()
    {
        return Tap;
    }
    
    public bool IsTouch()
    {
        return OneTouch;
    }
    
    //ジャイロの処理
    public Vector3 GyroCalc()
    {
        Vector3 GyroGravity = (Input.gyro.gravity/* - GyroDefault*/)/* / GyroSensitivity*/;

        return GyroGravity;
    }
    //ジャイロ初期位置
    public void SetNeutral()
    {
        GyroDefault = Input.gyro.gravity;
    }

    //ポーズする
    public void Pause(bool pause)
    {
        PauseFlg = pause;
    }
    //ポーズ中かどうか
    public bool IsPause()
    {
        return PauseFlg;
    }
    //チュートリアル中か
    public bool IsTextActive()
    {
        return TextFlg;
    }
}