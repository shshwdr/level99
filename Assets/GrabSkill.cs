using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class GrabSkill : Skill
{
    
    private Patient currentPatient;
    public override float costPerSecond => 15;
    public override float range => 30;
    
    
    public override bool mouseDown(Character character)
    {
        base.mouseDown(character);
        if (canSelectCharacters().Contains(character) )
        {
            var patient = character as Patient;
            
            currentPatient = patient;
            patient.connectSkill(this);
            return true;
        }

        return false;
    }
    
    public void mouseMove()
    {
        float moveSpeed = 10f;
        if (currentPatient)
        {
            var targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            targetPosition.z = 0f; // set the z-coordinate to zero to ensure it's on the 2D plane
            // move the item towards the target position
            currentPatient.transform.position = Vector3.MoveTowards(currentPatient.transform.position, targetPosition, moveSpeed * Time.deltaTime);
        }
    }
    public void mouseUp()
    {
        if (currentPatient)
        {
            PlayerSkillManager.Instance.unconnectSkill(this);
        }
    }

    protected override void Update()
    {
        base.Update();
        if (!isSelecting)
        {
            if (currentPatient)
            {
                if (Input.GetMouseButtonUp(0))
                {
                    mouseUp();
                }
                else
                {
                    mouseMove();
                }
            }
        }
    }
}
