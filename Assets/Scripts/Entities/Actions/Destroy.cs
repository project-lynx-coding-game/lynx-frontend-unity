using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class DestroyProperties
{
    public int object_id;
}

public class Destroy : Action
{

    private DestroyProperties properties;

    public Destroy(string propertiesJson) : base()
    {
        properties = JsonUtility.FromJson<DestroyProperties>(propertiesJson);
    }

    public override async Task Execute(World world)
    {
        System.Console.WriteLine(properties.object_id);
        Object objectToDestroy = world.objects.Find(x => x.GetComponent<Object>().id == properties.object_id);
        world.objects.Remove(objectToDestroy);
        GameObject.Destroy(objectToDestroy.gameObject);
    }
}

