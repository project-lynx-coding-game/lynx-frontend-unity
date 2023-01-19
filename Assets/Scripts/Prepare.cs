using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class PrepareProperties
{
    public int object_id;
}

public class Prepare : Action
{
    private PrepareProperties properties;

    public Prepare(string propertiesJson) : base()
    {
        properties = JsonUtility.FromJson<PrepareProperties>(propertiesJson);
    }

    public override async Task Execute(World world)
    {
        Agent agent = (Agent)world.objects.Find(x => x.GetComponent<Object>().id == properties.object_id);
        agent.Animator.SetBool("slam", false);
        agent.Animator.SetBool("prepare", true);
    }
}
