using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class NextScene : Action
{
    public NextScene(string propertiesJson) : base()
    {

    }

    public override async Task Execute(World world)
    {
        //await Task.Delay(1000);
        foreach (Object obj in world.objects)
        {
            GameObject.Destroy(obj.gameObject);
        }
        world.objects.Clear();
    }
}
