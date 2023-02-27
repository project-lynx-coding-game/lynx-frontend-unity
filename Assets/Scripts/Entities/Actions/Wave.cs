using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class Wave : Action
{
    class WaveProperties
    {
        public int agent_id;
    }

    private WaveProperties properties;

    public Wave(string propertiesJson) : base()
    {
        properties = JsonUtility.FromJson<WaveProperties>(propertiesJson);
    }

    public override async Task Execute(World world)
    {
        Debug.Log("Waving?");
        Agent agent = (Agent)world.objects.Find(x => x.GetComponent<Object>().id == properties.agent_id);

        agent.SetAnimatorBool("waving", true);

        // Move the timer into some helper
        float timer = 0;
        while (timer < 3.0f) {
            timer += Time.deltaTime;
            await Task.Yield();
        }

        agent.SetAnimatorBool("waving", false);

        timer = 0;
        while (timer < 1.0f)
        {
            timer += Time.deltaTime;
            await Task.Yield();
        }
        Debug.Log("Waving Done?");
    }
}
