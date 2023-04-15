using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Character : MonoBehaviour
{
    public SpriteRenderer render;
    private Color lastColor = Color.white;
    public void hoverOver()
    {
        render.color = Color.green;
    }

    public void hoverLeave()
    {
        
        render.color = lastColor;
    }
    public void showCanSelect()
    {
        
        render.color = Color.red;
        lastColor = render.color;
    }

    public void stopCanSelect()
    {
        
        render.color = Color.white;
        lastColor = render.color;
    }

    private void OnMouseEnter()
    {
        PlayerSkillManager.Instance.mouseEnter(this);
    }

    private void OnMouseExit()
    {
        hoverLeave();
    }

    // private void OnMouseDown()
    // {
    //     PlayerSkillManager.Instance.mouseClick(this);
    // }
}
