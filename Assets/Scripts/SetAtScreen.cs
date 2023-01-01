using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetAtScreen : MonoBehaviour
{
    public float distance = 1f;
    public float x = 0.5f;
    public float y = 0.5f;

    void Update()
    {
        Vector3 p = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width*x, Screen.height*y, distance));
        transform.position = new Vector3(p.x, p.y, p.z);
    }
}
