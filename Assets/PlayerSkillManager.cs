using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkillManager : Singleton<PlayerSkillManager>
{
    private Skill currentSkill;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void ClickOnSkill(SkillType type)
    {
        if (currentSkill != null)
        {
            return;
        }
        currentSkill = GetComponent<HealLinkSkill>();
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    public void mouseEnter(Character character)
    {
        if (currentSkill != null)
        {
            currentSkill.hoverOver(character);
        }
    }
    
}
