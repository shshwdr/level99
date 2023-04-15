using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkillManager : Singleton<PlayerSkillManager>
{
    private List<Skill> allActiveSkills = new List<Skill>(); 
    private Skill currentSkill;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void UnconnectAll()
    {
        foreach (var skill in allActiveSkills)
        {
            skill.unconnect();
        }
        allActiveSkills.Clear();
    }
    public void ClickOnSkill(SkillType type)
    {
        if (currentSkill != null)
        {
            return;
        }
        currentSkill = gameObject.AddComponent<HealLinkSkill>();
        allActiveSkills.Add(currentSkill);
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
    public void mouseClick(Character character)
    {
        if (currentSkill != null)
        {
            currentSkill.click(character);
            currentSkill = null;
        }
    }
    
}
