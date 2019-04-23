using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamegeObjectrRotationController : MonoBehaviour {

    public float angle = 120.0f;
    public GameObject CenterObject;
    Transform CenterPosition;
	
    // Use this for initialization
	void Start ()
    {
        CenterPosition = CenterObject.transform;
	}
	
	// Update is called once per frame
	void Update ()
    {
        transform.RotateAround(CenterPosition.position, Vector3.up, angle * Time.deltaTime);
	}
}
