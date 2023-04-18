using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezeSkill : Skill
{
    
    public override float costPerSecond => 10;
    public float cost => 10;
    public override float range => 15;
    public override bool mouseDown(Character character)
    {
        base.mouseDown(character);
        if (canSelectCharacters().Contains(character) )
        {
            var patient = character as Patient;
            
            PlayerSkillManager.Instance.unconnectSkill(this);
            PlayerBreathController.Instance.decreaseBreath(cost);
            patient.freeze();
            return true;
        }

        return false;
    }
}
