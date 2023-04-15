
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Skill:MonoBehaviour
{
    public virtual float costPerSecond { get; }
    public abstract void hoverOver(Character character);

    public virtual bool click(Character character)
    {
        clearSelectPreview();
        return true;
    }

    void clearSelectPreview()
    {
        
        foreach (var patient in canSelectCharacters())
        {
            patient.stopCanSelect();
        }
    }
    public virtual void unconnect()
    {
        clearSelectPreview();
        Destroy(this);
    }

    protected abstract List<Character> canSelectCharacters();


    public virtual bool init()
    {
        
        foreach (var patient in canSelectCharacters())
        {
            patient.showCanSelect();
        }

        return true;
    }


}

