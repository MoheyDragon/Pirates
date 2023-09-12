using UnityEngine;
using System;
public class Singleton<T> : MonoBehaviour where T:MonoBehaviour
{
    public static T singleton;
    private void Awake()
    {
        if (singleton == null)
            singleton = this as T;
        else
            Destroy(this);
    }
}
