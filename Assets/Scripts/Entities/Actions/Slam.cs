using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class SlamProperties
{
    public int object_id;
}

public class Slam : Action
{
    private SlamProperties properties;

    public Slam(string propertiesJson) : base()
    {
        properties = JsonUtility.FromJson<SlamProperties>(propertiesJson);
    }

    public override async Task Execute(World world)
    {
        Agent agent = (Agent)world.objects.Find(x => x.GetComponent<Object>().id == properties.object_id);
        agent.Animator.SetBool("prepare", false);
        agent.Animator.SetBool("slam", true);
    }
}
