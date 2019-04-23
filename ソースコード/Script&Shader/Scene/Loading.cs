using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Loading : MonoBehaviour
{

    //　非同期動作で使用するAsyncOperation
    private AsyncOperation async;
    //　シーンロード中に表示するUI画面
    [SerializeField]
    private GameObject loadUI;
    //シーンロード中に非表示にするUI
    [SerializeField]
    private GameObject UnableStageGroup;

    [SerializeField]
    private GameObject UnableBackButton;

    //　読み込み率を表示するスライダー
    [SerializeField]
    private Slider slider;

    [SerializeField]
    private GameObject SliderObject;

    public void LoadScene_TutorialStage1()
    {
        //　ロード画面UIをアクティブにする
        loadUI.SetActive(true);

        SliderObject.SetActive(true);

        UnableStageGroup.SetActive(false);

        UnableBackButton.SetActive(false);

        //　コルーチンを開始
        StartCoroutine("LoadData_TutorialStage1");
    }

    IEnumerator LoadData_TutorialStage1()
    {
        // シーンの読み込みをする
        async = SceneManager.LoadSceneAsync("TutorialStage1");

        //　読み込みが終わるまで進捗状況をスライダーの値に反映させる
        while (!async.isDone)
        {
            var progressVal = Mathf.Clamp01(async.progress / 0.9f);
            slider.value = progressVal;
            yield return null;
        }
    }

    public void LoadScene_TutorialStage2()
    {
        //　ロード画面UIをアクティブにする
        loadUI.SetActive(true);

        SliderObject.SetActive(true);

        UnableStageGroup.SetActive(false);

        UnableBackButton.SetActive(false);
        //　コルーチンを開始
        StartCoroutine("LoadData_TutorialStage2");
    }

    IEnumerator LoadData_TutorialStage2()
    {
        // シーンの読み込みをする
        async = SceneManager.LoadSceneAsync("TutorialStage2");

        //　読み込みが終わるまで進捗状況をスライダーの値に反映させる
        while (!async.isDone)
        {
            var progressVal = Mathf.Clamp01(async.progress / 0.9f);
            slider.value = progressVal;
            yield return null;
        }
    }

    public void LoadScene_TutorialStage3()
    {
        //　ロード画面UIをアクティブにする
        loadUI.SetActive(true);

        SliderObject.SetActive(true);

        UnableStageGroup.SetActive(false);

        UnableBackButton.SetActive(false);
        //　コルーチンを開始
        StartCoroutine("LoadData_TutorialStage3");
    }

    IEnumerator LoadData_TutorialStage3()
    {
        // シーンの読み込みをする
        async = SceneManager.LoadSceneAsync("TutorialStage3");

        //　読み込みが終わるまで進捗状況をスライダーの値に反映させる
        while (!async.isDone)
        {
            var progressVal = Mathf.Clamp01(async.progress / 0.9f);
            slider.value = progressVal;
            yield return null;
        }
    }
}