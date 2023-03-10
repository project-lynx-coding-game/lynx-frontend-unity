using NUnit.Framework;
using System;
using System.Collections.Generic;

public class DeserializerTest
{

    [Test]
    public void DeserializerTestSimplePasses()
    {
        /*
         * FIXME  
         * Verify "attributes", as the result of attributes has the value null
        */
        string json = @"
        {
            ""entities"": [
                {
                    ""type"": ""Move"",
                    ""attributes"": {
                        ""object_id"": 1,
                        ""direction"": ""NORTH""
                    }
                }
            ]
        }
        ";

        List<Entity> entitiesResult = Deserializer.GetEntities(json);

        Assert.AreEqual(entitiesResult.Count, 1);
        Assert.IsTrue(entitiesResult[0] is Move);
    }
}
