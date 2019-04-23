using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[DefaultExecutionOrder(-100)]
[RequireComponent(typeof(MeshFilter[]))]

public class ShadowBlockManager : MonoBehaviour
{
    public  GameObject[]                ShadowedObject;         //影を作るオブジェクト
    public  GameObject                  ShadowPrefub;           //影のプレハブ
    public  GameObject                  LightObject;            //影を映す光

    private SkinnedMeshRenderer[]       TmpSkinMesh;            //SkinMesh格納用
    private CombineInstance[]           ShadowInstance;         //影ブロックのインスタンス

    // Use this for initialization
    void Start()
    {
        for (int i = 0; i < ShadowedObject.Length; i++)
        {
            CreateShadow(i);
        }
    }

    // Update is called once per frame
    void Update ()
    {
        
    }

    //影作成
    void CreateShadow(int MeshNum)
    {
        //MeshFilter格納用
        MeshFilter[] TempMesh = ShadowedObject[MeshNum].GetComponentsInChildren<MeshFilter>();

        ShadowInstance = new CombineInstance[TempMesh.Length];

        //メッシュの形式による場合分け
        //MeshFilter型
        if (TempMesh.Length != 0)
        {
            for (int i = 0; i < TempMesh.Length; i++)
            {
                ShadowInstance[i] = CreateInstance(ShadowInstance[i], TempMesh[i]);
            }
        }
        else   //SkinMesh型
        {
            TmpSkinMesh = ShadowedObject[MeshNum].GetComponentsInChildren<SkinnedMeshRenderer>();
            ShadowInstance = new CombineInstance[TmpSkinMesh.Length];
            
            for (int j = 0; j < TmpSkinMesh.Length; j++)
            {
                ShadowInstance[j] = CreateInstance(ShadowInstance[j], TmpSkinMesh[j]);
            }
        }

        //影ブロックの作成
        ShadowPrefub.GetComponent<MeshFilter>().mesh = new Mesh();
        ShadowPrefub.GetComponent<MeshFilter>().sharedMesh.name = "Shadow";
        ShadowPrefub.GetComponent<MeshFilter>().sharedMesh.CombineMeshes(ShadowInstance, true, false, false);
        ShadowPrefub.GetComponent<BoxCollider>().center = ShadowedObject[MeshNum].GetComponentInChildren<BoxCollider>().center;
        ShadowPrefub.GetComponent<BoxCollider>().size = ShadowedObject[MeshNum].GetComponentInChildren<BoxCollider>().size;
        ShadowPrefub.GetComponent<Shadow>().LightObject = LightObject;

        Instantiate(ShadowPrefub,ShadowedObject[MeshNum].transform);
    }
    
    //影のインスタンス作成MeshFilter用
    private CombineInstance CreateInstance(CombineInstance Instance,SkinnedMeshRenderer SkinMesh)
    {
        //((SkinnedMeshRenderer)SkinMesh).sharedMesh.RecalculateNormals();
        //((SkinnedMeshRenderer)SkinMesh).sharedMesh.RecalculateBounds();
        Instance.mesh = ((SkinnedMeshRenderer)SkinMesh).sharedMesh;
        Instance.transform = SkinMesh.transform.localToWorldMatrix;
        
        return Instance;
    }

    //SkinMesh用
    private CombineInstance CreateInstance(CombineInstance Instance, MeshFilter Mesh)
    {
        //((MeshFilter)Mesh).sharedMesh.RecalculateNormals();
        //((MeshFilter)Mesh).sharedMesh.RecalculateBounds();
        Instance.mesh = ((MeshFilter)Mesh).sharedMesh;
        Instance.transform = Mesh.transform.localToWorldMatrix;

        //Debug.Log("Vertex\n" + ((MeshFilter)Mesh).sharedMesh.vertices.Length);
        return Instance;
    }
}
