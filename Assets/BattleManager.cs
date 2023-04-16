using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BattleManager : Singleton<BattleManager>
{

    public Text recoveredAmountText;
    public Text diedAmountText;

     bool isBattleEnd = false;
     private int recoveredCount = 0;
     private int diedCount = 0;
     public int maxDiedCount = 3;
    public bool IsBattleEnd => isBattleEnd;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void addRecoveredCount()
    {
        recoveredCount++;
        recoveredAmountText.text = "Revived: " + recoveredCount;
        
    }

    public void addDiedCount()
    {
        diedCount++;
        diedAmountText.text = "Died: " + diedCount;
        if (diedCount >= maxDiedCount)
        {
            FloatingTextManager.Instance.addText("Game Over - Too many death", Vector3.zero, Color.white);
            Invoke("restart",1);
        }
    }

    public void outOfBreath()
    {
        FloatingTextManager.Instance.addText("Game Over - Out of breath", Vector3.zero, Color.white);
        Invoke("restart",1);
    }
    
    void restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            restart();
        }
    }
}
