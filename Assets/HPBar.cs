using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class HPBar : MonoBehaviour
{
    private int maxHP = 100;

    private int hp = 100;
    public Image front;

    public void UpdateHP(int _hp)
    {
        
        
        hp = _hp;
        front.fillAmount = (float)hp / maxHP;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
