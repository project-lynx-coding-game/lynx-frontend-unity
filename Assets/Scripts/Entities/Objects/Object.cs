using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ObjectProperties
{
    public string class_name;
    public int id;
    public string position;
    public bool to_remove;
}

public class Object : MonoBehaviour, Entity
{
    public int id;
    public uint size = 1;
    public virtual void Initialize(string properties)
    {
        ObjectProperties instanceProperties = JsonUtility.FromJson<ObjectProperties>(properties);
        
        id = instanceProperties.id;

        string[] positionStrings = instanceProperties.position
            .Substring(1, instanceProperties.position.Length - 2)
            .Split(',');

        transform.position = new Vector3(
            int.Parse(positionStrings[0]) + (float)(size - 1) / 2,
            0,
            int.Parse(positionStrings[1]) + (float)(size - 1) / 2
            );
    }

    public static void DestroyObject(Object objectToDestroy)
    {
        Destroy(objectToDestroy.gameObject);
    }
}
