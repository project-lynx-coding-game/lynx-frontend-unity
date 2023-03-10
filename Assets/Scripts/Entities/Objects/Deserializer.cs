using System;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json.Linq;

static public class Deserializer
{
    static public List<Entity> GetEntities(string json)
    {
        JObject objectsJson = JObject.Parse(json);
        List<Entity> entities = new List<Entity>();
        foreach (JObject objectJson in objectsJson["entities"].Children<JObject>())
        {
            String typeString = objectJson["type"].ToString();
            String argsString = objectJson["attributes"].ToString();
            try 
            {
                Type type = Type.GetType(typeString);
                Entity entity = (Entity)JsonUtility.FromJson(argsString, type);
                entities.Add(entity);
                Debug.Log("[INFO] Added object of type: " + typeString);
            }
            catch (Exception e)
            {
                Debug.Log("[ERROR] Unknown type error: " + e);
            }
        }
        return entities;
    }  
}
