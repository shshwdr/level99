using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkillManager : Singleton<PlayerSkillManager>
{
    private List<Skill> allActiveSkills = new List<Skill>();
    public List<Skill> GetAllActiveSkills => allActiveSkills;
    private Skill currentSkill;

    public Transform rangeObject;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void unconnectSkill(Skill skill)
    {
        skill.unconnect();
        allActiveSkills.Remove(skill);
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
        if (currentSkill.init())
        {
            allActiveSkills.Add(currentSkill);
        }
        else
        {
            Destroy(currentSkill);
        }
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
            bool succeed = false;
            if (character != null)
            {
                
                succeed= currentSkill.click(character);
            }
            
            
            if (succeed)
            {
            }
            else
            {
                currentSkill.unconnect();
                allActiveSkills.Remove(currentSkill);
            }
            
            currentSkill = null;
            
        }
    }
    
}
