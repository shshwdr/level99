using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezeSkill : Skill
{
    
    public override float costPerSecond => 10;
    public float cost => 10;
    public override float range => 15;
    public void UseSkill(Patient patient)
    {
        //currentPatient = patient;
        
        PlayerSkillManager.Instance.unconnectSkill(this);
        PlayerBreathController.Instance.decreaseBreath(cost);
    }

    public override bool click(Character character)
    {
        base.click(character);
        if (canSelectCharacters().Contains(character) )
        {
            var patient = character as Patient;
            
            UseSkill(patient);
            patient.freeze();
            return true;
        }

        return false;
    }
}
