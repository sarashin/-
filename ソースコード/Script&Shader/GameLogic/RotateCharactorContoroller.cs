using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCharactorContoroller : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //キャラの回転
    public void Round(float RotateSpeed)
    {
        if (InputManager.InstanceSearch.IsDrag() == false)
        {
            transform.Rotate(new Vector3(0, -RotateSpeed, 0), Space.World);
        }
    }
}
