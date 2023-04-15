using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum SkillType
{
    healLink
};
public class SkillButton : MonoBehaviour
{
    public SkillType type;
    
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(() =>
        {
            PlayerSkillManager.Instance.ClickOnSkill(type);
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
