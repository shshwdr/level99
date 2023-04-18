using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SkillType
{
    healLink,
    freeze,
    grab,
    //healRange,
};
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

        switch (type)
        {
            case SkillType.healLink:
                
                currentSkill = gameObject.AddComponent<HealLinkSkill>();
                break;
            case SkillType.freeze:
                
                currentSkill = gameObject.AddComponent<FreezeSkill>();
                break;
            case SkillType.grab:
                currentSkill = gameObject.AddComponent<GrabSkill>();
                 break;
            // case SkillType.healRange:
            //     break;
        }
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
        for (int i = 0; i < Enum.GetValues(typeof(SkillType)).Length; i++)
        {
            if (Input.GetKeyDown(KeyCode.Alpha0 + i + 1))
            {
                ClickOnSkill((SkillType)(i));
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha0 + Enum.GetValues(typeof(SkillType)).Length + 1))
        {
            UnconnectAll();
        }
    }

    public void mouseEnter(Character character)
    {
        if (currentSkill != null)
        {
            currentSkill.hoverOver(character);
        }
    }
    public void mouseDown(Character character)
    {
        if (currentSkill != null)
        {
            bool succeed = false;
            if (character != null)
            {
                
                succeed= currentSkill.mouseDown(character);
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
            
            PlayerSkillManager.Instance.rangeObject.localScale = Vector3.zero;
            
        }
    }

    
}
