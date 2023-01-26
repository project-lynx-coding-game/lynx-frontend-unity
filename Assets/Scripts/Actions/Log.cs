using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class LogProperties
{
    public string type;
    public string message;
}

public class Log : Action
{
    LogProperties properties;

    public Log(string propertiesJson) : base()
    {
        properties = JsonUtility.FromJson<LogProperties>(propertiesJson);
    }

    public override async Task Execute(World world)
    {
        world.AddLog(properties.type, properties.message);
        await ActionUtils.Wait(0.1f);
    }
}
