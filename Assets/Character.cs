using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Character : MonoBehaviour
{
    public SpriteRenderer render;

    public void hoverOver()
    {
        render.color = Color.green;
    }

    public void hoverLeave()
    {
        
        render.color = Color.white;
    }

    private void OnMouseEnter()
    {
        PlayerSkillManager.Instance.mouseEnter(this);
    }

    private void OnMouseExit()
    {
        hoverLeave();
    }
}
