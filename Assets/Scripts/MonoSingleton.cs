using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonoSinleton<T> : MonoBehaviour where T : MonoSinleton<T>
{
    public static T Instance;
    public virtual void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError(this + "Is not a single, maybe creat other instace");
        }
        Instance = this as T;
    }

    protected virtual void OnDestroy()
    {
        Destroy();
    }
    public void Destroy()
    {
        Instance = null;
    }

}
