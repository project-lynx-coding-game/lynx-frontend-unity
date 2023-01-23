using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class WaitForCode : Action
{
    public WaitForCode(string properties)
    {

    }

    public override Task Execute(World world)
    {
        return Task.CompletedTask;
    }
}
