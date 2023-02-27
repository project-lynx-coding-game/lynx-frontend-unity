using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class IdleProperties
{
    public int object_id;
}

public class Idle : Action
{
    private IdleProperties properties;

    public Idle(string propertiesJson) : base()
    {
        properties = JsonUtility.FromJson<IdleProperties>(propertiesJson);
    }

    public override async Task Execute(World world)
    {
        Agent agent = (Agent)world.objects.Find(x => x.GetComponent<Object>().id == properties.object_id);
        agent.Animator.SetBool("walking", false);
    }
}
