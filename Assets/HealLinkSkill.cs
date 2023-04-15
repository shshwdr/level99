using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class HealLinkSkill:Skill
{
    public void UseSkill()
    {
        
    }

    public override void hoverOver(Character character)
    {
        if (character is Patient)
        {
            character.hoverOver();
        }
    }
}
