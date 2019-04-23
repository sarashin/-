using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitGoalObject : MonoBehaviour
{

    public GameObject PanelController;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == ("Player"))
        {
            
        }
    }


}
