
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Skill:MonoBehaviour
{
    public abstract void hoverOver(Character character);

    public abstract void click(Character character);

    public virtual void unconnect()
    {
        Destroy(this);
    }
}

