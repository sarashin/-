using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//マネージャークラスなどの基本クラス
public class SingletonMonoBehavior<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T Instance;

    public static T InstanceSearch
    {
        get
        {
            if(Instance==null)
            {
                Instance = (T)FindObjectOfType(typeof(T));
                if(Instance==null)
                {
                    Debug.Log("err");
                }
            }

            return Instance;
        }
    }
}
