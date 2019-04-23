using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
[RequireComponent(typeof(AudioSource))]

public class AudioManager : SingletonMonoBehavior<AudioManager>
{
    public List<AudioClip> BGMList;
    public List<AudioClip> SEList;

    private AudioSource         BGMSource;
    private List<AudioSource>   SESource;

    private Dictionary<string, AudioClip> BGMDict;
    private Dictionary<string, AudioClip> SEDict;

    private int MaxSE;

	// Use this for initialization
	void Start ()
    {
        MaxSE = 10;
        BGMSource.loop = true;
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    private void Awake()
    {
        //インスタンス作成済みなら破棄
        if(this != InstanceSearch)
        {
            Destroy(this);
            return;
        }

        DontDestroyOnLoad(this.gameObject);

        //AudioListenerなければ追加
        if (FindObjectsOfType(typeof(AudioListener)).All(o => !((AudioListener)o).enabled))
        {
            this.gameObject.AddComponent<AudioListener>();
        }

        this.BGMSource = GetComponent<AudioSource>();
        this.SESource = new List<AudioSource>();
        this.BGMDict = new Dictionary<string, AudioClip>();
        this.SEDict = new Dictionary<string, AudioClip>();

        //BGM,SEをリストから追加
        for (int i = 0; i < BGMList.Count; i++)
        {
            this.BGMDict.Add(BGMList[i].name,BGMList[i]);
        }

        for (int i = 0; i < SEList.Count; i++)
        {
            this.SEDict.Add(SEList[i].name, SEList[i]);
        }
    }

    //SE再生
    public void PlaySE(string SEName)
    {
        //存在確認
        if(!(this.SEDict.ContainsKey(SEName)))
        {
            Debug.Log("SEが見つかりません");
            return;
        }

        //再生済み？
        AudioSource Source = SESource.FirstOrDefault(se => !se.isPlaying);
        if (Source == null)
        {
            //最大同時再生数以上なら再生しない
            if(this.SESource.Count　>　MaxSE)
            {
                return;
            }
            //再生してなければ追加
            Source = this.gameObject.AddComponent<AudioSource>();
            this.SESource.Add(Source);
        }

        Source.clip = SEDict[SEName];
        Source.Play();
    }

    //SE再生中かどうか
    public bool IsSEPlaying(string Name)
    {
        AudioSource IsPlay = SESource.FirstOrDefault(temp => temp.name == Name + "(Clone)" && temp.isPlaying == false);

        if(IsPlay!=null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool IsSEPlaying()
    {
        AudioSource IsPlay = SESource.FirstOrDefault(temp => temp.isPlaying == true);

        if (IsPlay != null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    //SE止める
    public void StopSE()
    {
        this.SESource.ForEach(se => se.Stop());
    }

    //BGM再生
    public void PlayBGM(string BGMName)
    {
        //存在確認
        if (!(this.BGMDict.ContainsKey(BGMName)))
        {
            Debug.Log("BGMが見つかりません");
            return;
        }
        //現在再生中かどうか
        if(BGMDict[BGMName] == this.BGMSource)
        {
            Debug.Log("stop");
            return;
        }

        this.BGMSource.Stop();
        this.BGMSource.clip = BGMDict[BGMName];
        this.BGMSource.Play();
    }

    //BGMストップ
    public void StopBGM()
    {
        this.BGMSource.Stop();
        this.BGMSource.clip = null;
    }
}
