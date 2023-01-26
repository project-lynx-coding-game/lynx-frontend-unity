using System.Threading.Tasks;
using UnityEngine;

public class SlashProperties
{
    public int object_id;
}

public class Slash : Action
{
    private SlashProperties properties;

    public Slash(string propertiesJson) : base()
    {
        properties = JsonUtility.FromJson<SlashProperties>(propertiesJson);
    }

    public override async Task Execute(World world)
    {
        Agent agent = (Agent)world.objects.Find(x => x.GetComponent<Object>().id == properties.object_id);

        agent.Animator.SetBool("slash", true);
        await ActionUtils.Wait(1);
        agent.Animator.SetBool("slash", false);
    }
}