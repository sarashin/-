using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LicencePanelController : MonoBehaviour {

    public GameObject MemberPanel;
    public GameObject LicencePanel;
    public GameObject[] LicencePage;

    // Use this for initialization
    void Start () {
        MemberPanel.SetActive(true);
        LicencePanel.SetActive(false);

        for (int i=0; i<LicencePage.Length;i++)
        {
            LicencePage[i].SetActive(false);
        }
	}
	
	public void PushButton_Licence()
    {
        MemberPanel.SetActive(false);
        LicencePanel.SetActive(true);
        LicencePage[0].SetActive(true);
    }

    public void PushButton_NextPage()
    {
        LicencePage[0].SetActive(false);
        LicencePage[1].SetActive(true);
    }

    public void PushButton_Menber()
    {
        LicencePanel.SetActive(false);
        LicencePage[0].SetActive(false);
        LicencePage[1].SetActive(false);
        MemberPanel.SetActive(true);
    }
}
