using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameSceneManager : SingletonMonoBehavior<GameSceneManager>
{
    //シーンデータ
    private string ThisScene;
    public bool IsLoading;

    //　非同期動作で使用するAsyncOperation
    private AsyncOperation async;
    ////　シーンロード中に表示するUI画面
    [SerializeField]
    private GameObject loadUI;

    ////　読み込み率を表示するスライダー
    [SerializeField]
    private Slider slider;

    [SerializeField]
    private GameObject SliderObject;

    public void Awake()
    {
        if (this != InstanceSearch)
        {
            Destroy(this.gameObject);
            return;
        }
        DontDestroyOnLoad(this.gameObject);
    }

        // Use this for initialization
    void Start () {
        ThisScene = SceneManager.GetActiveScene().name;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    //シーン切り替え
    public void ChangeScene(string SceneName)
    {
        //　ロード画面UIをアクティブにする
        loadUI.SetActive(true);

        SliderObject.SetActive(true);

        //　コルーチンを開始
        StartCoroutine(LoadData_Scene(SceneName));

        //現シーンの格納
        ThisScene = SceneName;

        
    }

    //リトライ
    public void ReLoadScene()
    {
        //　ロード画面UIをアクティブにする
        loadUI.SetActive(true);

        SliderObject.SetActive(true);

        //　コルーチンを開始
        StartCoroutine(LoadData_Scene(ThisScene));
    }

    //フェードインアウトのコルーチン
    IEnumerator LoadData_Scene(string SceneName)
    {
        IsLoading = true;
        ParticleManager.InstanceSearch.Clean();
        AudioManager.InstanceSearch.StopBGM();
        async = SceneManager.LoadSceneAsync(SceneName);

        //　読み込みが終わるまで進捗状況をスライダーの値に反映させる
        while (!async.isDone || AudioManager.InstanceSearch.IsSEPlaying())
        {
            var progressVal = Mathf.Clamp01(async.progress / 0.9f);
            slider.value = progressVal;
            ParticleManager.InstanceSearch.CreatePooler();
            yield return null;
        }

        IsLoading = false;

        loadUI.SetActive(false);
        SliderObject.SetActive(false);
    }
}
