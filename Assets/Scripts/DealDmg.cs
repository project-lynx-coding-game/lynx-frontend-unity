using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class DealDmg : Action
{
    public DealDmg(string properties) { }

    public override Task Execute(World world)
    {
        return Task.CompletedTask;
    }
}
