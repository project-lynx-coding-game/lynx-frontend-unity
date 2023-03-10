using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class MoveProperties
{
    public int object_id;
    public string direction;
}

public class Move : Action
{
    protected MoveProperties properties { get; }

    public Move(string propertiesJson) : base()
    {
        properties = JsonUtility.FromJson<MoveProperties>(propertiesJson);
    }

    public override async Task Execute(World world)
    {
        System.Console.WriteLine(properties.direction);
        Agent agent = (Agent)world.objects.Find(x => x.GetComponent<Object>().id == properties.object_id);

        Vector3 direction = ActionUtils.StringToDirection(properties.direction);

        // Object.transform.position already
        // accounts for Objects size
        Vector3 destination = agent.transform.position + direction;

        //await Task.Delay(200);
        await agent.LookInDirection(destination - agent.transform.position);
        //await Task.Delay(200);
        await agent.MoveTo(destination);
    }
}
