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

    static public Object GetObject(string json, List<GameObject> prefabs)
    {
        // Parse properties into a separate JSON dict
        // { "base": "blabla", "type": "blabla", properties: {"abc": 1, "cbd": 2}} => {"abc": 1, "cbd": 2}
        string propertiesStartMark = "\"properties\" : {";
        int propertiesStart = json.IndexOf(propertiesStartMark) + propertiesStartMark.Length;
        int propertiesEnd = json.IndexOf('}', propertiesStart);
        string propertiesJson = "{ " + json.Substring(propertiesStart, propertiesEnd - propertiesStart) + " }";

        ObjectProperties properties = JsonUtility.FromJson<ObjectProperties>(json);

        if (properties.to_remove)
        {
            return null;
        }


        /*
         * Prefabs name MUST match `class_name` found in log
         */ 
        foreach(GameObject prefab in prefabs)
        {
            if (prefab.name == properties.class_name)
            {
                GameObject instance = GameObject.Instantiate(prefab);
                Object instanceObject = instance.GetComponent<Object>();
                instanceObject.Initialize(propertiesJson);
                return instanceObject;
            }
        }
        return null;
    }
}
