using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateLogController : MonoBehaviour
{
    public GameObject CenterObject;
    public float angle;
    Transform CenterPos;
    RotateCharactorContoroller RotateObj;

    public bool HitShadowFlg;

	// Use this for initialization
	void Start ()
    {
        CenterPos = CenterObject.transform;
        RotateObj = CenterObject.GetComponent<RotateCharactorContoroller>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (!HitShadowFlg&&InputManager.InstanceSearch.IsDrag()==false)
        {
            transform.RotateAround(CenterPos.position, Vector3.up, angle * Time.deltaTime);
            RotateObj.Round(Mathf.Sin(-angle * Time.deltaTime));
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag=="Shadow")
        {
            HitShadowFlg = true;
        }
        else if (other.tag == "Player" && InputManager.InstanceSearch.IsPause() == false)
        {
            if (!HitShadowFlg)
            {
                AudioManager.InstanceSearch.StopBGM();
                AudioManager.InstanceSearch.PlaySE("LogHit hit01");
                other.gameObject.GetComponent<CharactorMove>().GameOver();
            }
            else
            {
                other.gameObject.GetComponent<CharactorMove>().ReturnPoint();
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Shadow")
        {
            HitShadowFlg = false;
        }
    }
}
