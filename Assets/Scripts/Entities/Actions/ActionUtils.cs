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

    public static Vector3 StringToDirection(string str)
    {
        float dx, dz;

        switch (str)
        {
            case "NORTH":
                dx = 0;
                dz = 1;
                break;
            case "EAST":
                dx = 1;
                dz = 0;
                break;
            case "SOUTH":
                dx = 0;
                dz = -1;
                break;
            case "WEST":
                dx = -1;
                dz = 0;
                break;
            default:
                dx = 0;
                dz = 0;
                break;
        }

        return new Vector3(dx, 0, dz);
    }
    
}
