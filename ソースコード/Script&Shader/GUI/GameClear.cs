using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameClear : MonoBehaviour
{
    [SerializeField]
    private GameObject GameEndMenu;
    [SerializeField]
    private GameObject GameClearAnnounce;

    private void Start()
    {
        GameEndMenu.SetActive(false);
        GameClearAnnounce.SetActive(false);
    }

    public void OnTriggerEnter(Collider other)
    {
       if(other.gameObject.tag == ("Player"))
        {
            //ゲームクリアのSEとBGM記入
            AudioManager.InstanceSearch.PlaySE("GameClear decision24");
            GameClearAnnounce.SetActive(true);

            StartCoroutine(WaitEndEffect());
        }
    }

    //クリアのパーティクル再生中は待機
    IEnumerator WaitEndEffect()
    {
        ParticleManager.InstanceSearch.PlayParticle("UI_Light_08_Loop", new Vector3(0, 4.5f, 0));
        ClearFlgManager.InstanceSearch.StageClear();

        while (ParticleManager.InstanceSearch.IsPlaying("UI_Light_08_Loop") == true)
        {
            yield return null;
        }

        Debug.Log("End");
        GameEndMenu.SetActive(true);
        InputManager.InstanceSearch.Pause(true);
    }
}
