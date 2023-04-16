using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBreathController : Singleton<PlayerBreathController>
{
    private float maxHP = 100;

    private float _hp = 100;

    public Image breathHPBar;

    public float hpIncreasePerSecond = 20;
    void updateBreathBar()
    {
        breathHPBar.fillAmount = _hp / maxHP;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void decreaseBreath(float value)
    {
        _hp -= value;
        updateHP();
    }

    void updateHP()
    {
        
        _hp = math.clamp(_hp, 0, 100);
        if (_hp <= 0)
        {
            BattleManager.Instance.outOfBreath();
        }
        
        updateBreathBar();
    }
    // Update is called once per frame
    void Update()
    {
        if (PlayerSkillManager.Instance.GetAllActiveSkills.Count > 0)
        {
            foreach (var skill in PlayerSkillManager.Instance.GetAllActiveSkills)
            {
                _hp -= Time.deltaTime * skill.costPerSecond;
            }
        }
        else
        {
            _hp += Time.deltaTime * hpIncreasePerSecond;
        }

        updateHP();
    }
}
