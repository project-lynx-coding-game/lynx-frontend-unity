using System;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json.Linq;

static public class Deserializer
{
    /*-- 
     * objects: [
     *      {
     *          type: "miœ",
     *          args: "{x:5, y:3, hp:100}"
     *      },
     *      {
     *           type: "floor",
     *           args: "{x:4, y:5}"
     *      }
     *  ]
     * --*/
    static public List<Object> GetObjects(string json)
    {
        JObject objectsJson = JObject.Parse(json);
        List<Object> newObjects = new List<Object>();
        foreach (JObject objectJson in objectsJson.Children<JObject>())
        {
            String argsString = objectJson["args"].ToString();
            String typeString = objectJson["type"].ToString();
            try 
            {
                Type type = Type.GetType(typeString);
                Object newObject = (Object)JsonUtility.FromJson(argsString, type);
                newObjects.Add(newObject);
                Debug.Log("[INFO] Added object of type: " + typeString);
            }
            catch (Exception e)
            {
                Debug.Log("[ERROR] Unknown type error: " + e);
            }
        }
        return newObjects;
    }  
}
