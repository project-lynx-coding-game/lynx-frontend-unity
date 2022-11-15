using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

[System.Serializable]
public class ActionProperties
{
    public string class_name;
}

public abstract class Action
{
    public abstract Task Execute(World world);
}
