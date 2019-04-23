using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemySpawn : MonoBehaviour
{
    public GameObject EnemyPrefub;
    [SerializeField]
    Transform[] SpawnPos;
    public float Speed;
    int HitCount;
    public int MaxSpawn;
    EnemyMoveWarp WarpEnemy;

	// Use this for initialization
	void Start () {
        EnemyPrefub.GetComponent<NavMeshAgent>().speed = Speed;
        WarpEnemy = EnemyPrefub.GetComponent<EnemyMoveWarp>();
        HitCount = 0;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    //敵出現
    public void SpawnEnemy()
    {
        for (int i = 0; i < SpawnPos.Length; i++)
        {
            if (WarpEnemy)
            {
                WarpEnemy.FirstCheckPoint = SpawnPos[i].GetComponent<NavPoint>();
                WarpEnemy.AgentSpeed = Speed;
            }
            EnemyPrefub.transform.position = SpawnPos[i].position;
            Instantiate(EnemyPrefub);
            HitCount++;
        }
    }
    //スポーンのトリガー
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("hit");

        if (HitCount<MaxSpawn&&other.tag == "Player")
        {
            SpawnEnemy();
            AudioManager.InstanceSearch.PlaySE("");
            ParticleManager.InstanceSearch.PlayParticle("", transform.position);
        }
    }
}
