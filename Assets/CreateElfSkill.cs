using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateElfSkill : Skill
{
    
    private Elf currentElf;
    public override float costPerSecond => 15;
    public override float range => 40;

    protected override List<Character> canSelectCharacters()
    {
        return new List<Character>();
    }

    protected override List<Character> canSelectCharactersRoughly()
    {
        return new List<Character>();
    }

    public override bool init()
    {
        var go = Instantiate(Resources.Load<GameObject>("healingElf"),transform);
        currentElf = go.GetComponentInChildren<Elf>();
        base.init();
        PlayerSkillManager.Instance.rangeObject.localScale =Vector3.zero;
        return true;
    }
    
    public void mouseMove()
    {
        float moveSpeed = 10f;
        if (currentElf)
        {
            var targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            targetPosition.z = 0f; // set the z-coordinate to zero to ensure it's on the 2D plane
            // move the item towards the target position
            currentElf.transform.position = Vector3.MoveTowards(currentElf.transform.position, targetPosition, moveSpeed * Time.deltaTime);
        }
    }
    public override bool mouseDown(Character character)
    {
        base.mouseDown(character);
        return false;
    }
    protected override void Update()
    {
        base.Update();
        if (!isSelecting)
        {
            if (currentElf)
            {
                {
                    mouseMove();
                }
            }
        }
    }
}
