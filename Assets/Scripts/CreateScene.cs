using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class CreateScene : Action
{
    public CreateScene(string propertiesJson) : base()
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
