using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;


public class Patient : Character
{
    private float _hp = 90;

    public float hpDecreasePerSecond = 5;

    private HPBar _hpBar;

    public List<Skill> connectedSkills = new List<Skill>();

    public bool isDead;
    public bool isRevived;
    public bool isActive => !isDead && !isRevived && !isFrozen;

    public bool isFrozen = false;

    public GameObject freezeEffect;

    public void freeze()
    {
        if (!isActive)
        {
            return;
        }

        isFrozen = true;
        freezeEffect.SetActive(true);
    }
    
    public void unfreeze()
    {
        if (!isFrozen)
        {
            return;
        }

        isFrozen = false;
        freezeEffect.SetActive(false);
    }
    private void Awake()
    {
        _hpBar = GetComponentInChildren<HPBar>();
        _hp = Random.Range(50, 90);
    }

    public bool hasType(Type skillType)
    {
        bool hasB = connectedSkills.Where(item => item.GetType() == skillType).Any();
        return hasB;
    }
    public void connectSkill(Skill skill)
    {
        connectedSkills.Add(skill);
    }
    public void unconnectSkill(Skill skill)
    {
        connectedSkills.Remove(skill);
    }
    

    // Update is called once per frame
    void Update()
    {
        if (!isActive)
        {
            return;
        }
        _hp -= hpDecreasePerSecond * Time.deltaTime;
        updateHP();
    }


    public void Heal(float hp)
    {
        if (isActive)
        {
            _hp += hp;
            updateHP();

        }
    }

    void updateHP()
    {
        
        _hp = math.clamp(_hp, 0, 100);
        if (_hp >= 100)
        {
            Revive();
        }else if (_hp <= 0)
        {
            DieForReal();
        }
        
        _hpBar.UpdateHP((int)_hp);
    }

    public void Revive()
    {
        if (isActive)
        {
            isRevived = true;
            clearSkillsOnIt();
            FloatingTextManager.Instance.addText("Take a deep breath!", transform.position, Color.white);
            
            Destroy(gameObject);
            BattleManager.Instance.addRecoveredCount();
        }
    }
    public void DieForReal()
    {
        
        if (isActive)
        {
            isDead = true;
            clearSkillsOnIt();
            FloatingTextManager.Instance.addText("Gone for good..", transform.position, Color.white);
            Destroy(gameObject);
            BattleManager.Instance.addDiedCount();
        }
    }

    void clearSkillsOnIt()
    {
        PatientManager.Instance.removePatient(this);
        while(connectedSkills.Count>0)
        {
            var skill = connectedSkills[0];
            PlayerSkillManager.Instance.unconnectSkill(skill);
        }
    }
}
