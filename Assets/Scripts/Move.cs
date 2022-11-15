using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class MoveProperties
{
    public int agent_id;
    public string direction;
}

public class Move : Action
{
    private MoveProperties properties;

    public Move(string propertiesJson) : base()
    {
        properties = JsonUtility.FromJson<MoveProperties>(propertiesJson);
    }

    public override async Task Execute(World world)
    {
        System.Console.WriteLine(properties.direction);
        Agent agent = world.agents.Find(x => x.id == properties.agent_id);

        float dx, dz;

        switch(properties.direction)
        {
            case "UP":
                dx = 0;
                dz = 1;
                break;
            case "RIGHT":
                dx = 1;
                dz = 0;
                break;
            case "DOWN":
                dx = 0;
                dz= -1;
                break;
            case "LEFT":
                dx = -1;
                dz = 0;
                break;
            default:
                dx = 0;
                dz = 0;
                break;
        }

        Vector3 destination = new Vector3(
            agent.transform.position.x + dx,
            agent.transform.position.y,
            agent.transform.position.z + dz
            );

        //await Task.Delay(200);
        await agent.LookInDirection(destination - agent.transform.position);
        //await Task.Delay(200);
        await agent.MoveTo(destination);
    }
}
