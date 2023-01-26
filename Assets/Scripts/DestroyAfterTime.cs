using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterTime : MonoBehaviour
{
    public float time = 1f;
    async void Start()
    {
        await ActionUtils.Wait(time);
        Destroy(gameObject);
    }
}
