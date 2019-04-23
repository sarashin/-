using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ParticleManager : SingletonMonoBehavior<ParticleManager>
{
    //再生パーティクルのリスト
    [SerializeField]
    public List<ParticleSystem> PoolerList;
    private Dictionary<string,GameObject> Particles;
    //再生するパーティクル
    [SerializeField]
    private List<GameObject> ParticlePrefubs;
    public static int MaxParticle;

    private void Awake()
    {
        //インスタンス生成済みなら削除
        if (this != InstanceSearch)
        {
            Destroy(this.gameObject);
            return;
        }

        DontDestroyOnLoad(this.gameObject);

        this.PoolerList = new List<ParticleSystem>();
        this.Particles = new Dictionary<string, GameObject>();

        //プレハブからプールの作成
        for (int i = 0; i < ParticlePrefubs.Count; i++)
        {
            this.Particles.Add(ParticlePrefubs[i].name, ParticlePrefubs[i]);
        }

        MaxParticle = 10;
    }

    //パーティクル再生
    public void PlayParticle(string Name,Vector3 Pos)
    {
        ParticleSystem SampPooler = this.PoolerList.FirstOrDefault(temp => temp.name == Name +"(Clone)"&& temp.isPlaying == false);

        if (SampPooler == null)
        {
            if (!(this.Particles.ContainsKey(Name)))
            {
                Debug.Log("パーティクルが見つかりません");
                return;
            }

            //一定数以上のパーティクルが再生されたら始めのパーティクルを削除
            if (PoolerList.Count > MaxParticle)
            {
                ParticleSystem DeleteParticle = PoolerList.FirstOrDefault(temp => temp.isPlaying==false);
                PoolerList.Remove(DeleteParticle);
                Destroy(DeleteParticle.gameObject);
                Debug.Log("Delete");
            }
            
            GameObject particle = GameObject.Instantiate(Particles[Name]);
            SampPooler = new ParticleSystem();
            SampPooler = particle.GetComponent<ParticleSystem>();
        }

        Debug.Log("Start");
        SampPooler.transform.position = Pos;
        SampPooler.Play();
    }

    //パーティクル再生
    public void PlayParticle(string Name, Vector3 Pos,int Num)
    {
        ParticleSystem SampPooler = this.PoolerList.FirstOrDefault(temp => temp.name == Name + Num.ToString()+ "(Clone)" && temp.isPlaying == false);

        if (SampPooler == null)
        {
            if (!(this.Particles.ContainsKey(Name)))
            {
                Debug.Log("パーティクルが見つかりません");
                return;
            }

            //一定数以上のパーティクルが再生されたら始めのパーティクルを削除
            if (PoolerList.Count > MaxParticle)
            {
                ParticleSystem DeleteParticle = PoolerList.FirstOrDefault(temp => temp.isPlaying == false);
                if (DeleteParticle)
                {
                    PoolerList.Remove(DeleteParticle);
                    Destroy(DeleteParticle.gameObject);
                }
                Debug.Log("Delete");
            }

            GameObject particle;
            particle = GameObject.Instantiate(Particles[Name]);
            SampPooler = new ParticleSystem();
            SampPooler = particle.GetComponent<ParticleSystem>();
            SampPooler.name = Name + Num.ToString() + "(Clone)";
            this.PoolerList.Add(SampPooler);
        }

        Debug.Log("Start");
        SampPooler.transform.position = Pos;
        SampPooler.Play();
    }

    //そのパーティクルは再生中か
    public bool IsPlaying(string Name)
    {
        ParticleSystem SampPooler = this.PoolerList.FirstOrDefault(temp => temp.name == Name + "(Clone)");

        if (SampPooler==null)
        {
            Debug.Log("err");
        }

        if(SampPooler&&SampPooler.isPlaying)
        {
            return true;
        }

        return false;
    }

    public bool IsPlaying(string Name,int Num)
    {
        ParticleSystem SampPooler = this.PoolerList.FirstOrDefault(temp => temp.name == Name + "(Clone)");

        if (SampPooler == null)
        {
            return false;
        }

        if (SampPooler.isPlaying)
        {
            return true;
        }

        return false;
    }

    //リストのクリーン
    public void Clean()
    {
        PoolerList.Clear();
        PoolerList = null;
    }

    //プールの作成
    public void CreatePooler()
    {
        PoolerList = new List<ParticleSystem>();
    }

    public int GetMaxParticle()
    {
        return MaxParticle;
    }
}
