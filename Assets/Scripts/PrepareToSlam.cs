using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class PrepareToSlamProperties
{
    public int object_id;
}

public class PrepareToSlam : Action
{
    private PrepareToSlamProperties properties;

    public PrepareToSlam(string propertiesJson) : base()
    {
        properties = JsonUtility.FromJson<PrepareToSlamProperties>(propertiesJson);
    }

    public override async Task Execute(World world)
    {
        Agent agent = (Agent)world.objects.Find(x => x.GetComponent<Object>().id == properties.object_id);
        agent.Animator.SetBool("slam", false);
        agent.Animator.SetBool("prepare", true);
    }
}
