using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

static public class Deserializer
{
    static public Action GetAction(string json)
    {
        // Parse properties into a separate JSON dict
        // { "base": "blabla", "type": "blabla", properties: {"abc": 1, "cbd": 2}} => {"abc": 1, "cbd": 2}
        string propertiesStartMark = "\"properties\" : {";
        int propertiesStart = json.IndexOf(propertiesStartMark) + propertiesStartMark.Length;
        int propertiesEnd = json.IndexOf('}', propertiesStart);
        string propertiesJson = "{ " + json.Substring(propertiesStart, propertiesEnd - propertiesStart) + " }";

        ActionProperties properties = JsonUtility.FromJson<ActionProperties>(json);

        Type type = Type.GetType(properties.class_name.ToString());
        Action action = (Action)Activator.CreateInstance(type, propertiesJson);
        return action;
    }
}
