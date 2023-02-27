using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class DealDmgProperties
{
    public int object_id;
    public int dmg;
}

public class DealDmg : Action
{
    private DealDmgProperties properties;

    public DealDmg(string propertiesJson)
    {
        properties = JsonUtility.FromJson<DealDmgProperties>(propertiesJson);
    }

    public override async Task Execute(World world)
    {
        Object objectTakingDmg = world.objects.Find(x => x.GetComponent<Object>().id == properties.object_id);

        Vector2 screenPosition = Camera.main.WorldToScreenPoint(objectTakingDmg.transform.position);

        Vector2 finalPosition = new Vector2(screenPosition.x / world.canvas.scaleFactor, screenPosition.y / world.canvas.scaleFactor);

        world.SpawnDmgLog(finalPosition, properties.dmg);
    }
}
