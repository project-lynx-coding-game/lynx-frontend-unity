using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public static class ActionUtils
{
    public static async Task Wait(float seconds)
    {
        float timeElapsed = 0f;
        while (timeElapsed < seconds)
        {
            timeElapsed += Time.deltaTime;
            await Task.Yield();
        }
    }
    
}
